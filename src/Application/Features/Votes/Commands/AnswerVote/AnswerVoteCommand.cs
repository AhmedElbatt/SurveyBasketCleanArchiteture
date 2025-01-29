using Application.Errors;
using System.Linq;

namespace Application.Features.Votes.Commands.AnswerVote;
public record AnswerVoteCommand(int PollId, string UserId, IEnumerable<AnswerVoteItem> Answers) : IRequest<Result>;

public class AnswerVoteCommandHandler(IRepository<Vote> voteRepository, IRepository<Poll> pollRepository, IRepository<Question> questionRepository) : IRequestHandler<AnswerVoteCommand, Result>
{
    private readonly IRepository<Vote> _voteRepository = voteRepository;
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result> Handle(AnswerVoteCommand request, CancellationToken cancellationToken)
    {
        var voteExists = await _voteRepository.AnyAsync(x => x.PollId == request.PollId && x.UserId == request.UserId);
        if (voteExists)
            return Result.Failure(VoteErrors.DuplicatedVote);


        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId
                                                             && !x.IsDeleted
                                                             && x.IsPublished
                                                             && x.StartsAt <= DateOnly.FromDateTime(DateTime.Now)
                                                             && x.EndsAt >= DateOnly.FromDateTime(DateTime.Now));
        if (!pollExists)
            return Result.Failure(PollErrors.PollNotFound);

        //var poll = await _pollRepository.GetAsync(x => x.Id == request.PollId, true, cancellationToken, x => x.Questions);

        var availableQuestions = await _questionRepository.GetListAsync(x => x.PollId == request.PollId && x.IsActive, cancellationToken: cancellationToken, includeProperties: x => x.Answers);

        var validQuestionFound = request.Answers.Select(x => x.QuestionId).SequenceEqual(availableQuestions.Select(x => x.Id));
        if (!validQuestionFound)
            return Result.Failure(VoteErrors.InvalidQuestions);

        foreach (var question in availableQuestions)
        {
            var incomingAnswer = request.Answers.SingleOrDefault(x => x.QuestionId == question.Id)!;
            var validAnswerFound = question.Answers.Where(x => x.IsActive).Select(x => x.Id).Contains(incomingAnswer.AnswerId);
            if (!validAnswerFound)
                return Result.Failure(VoteErrors.InvalidAnswers);
        }

        var createdVote = _voteRepository.AddAsync(Vote.Create(request.PollId, request.UserId, request.Answers.Adapt<List<VoteAnswer>>()), cancellationToken);
        return Result.Success();
    }
}

