using Application.Features.Questions.Shared;

namespace Application.Features.Votes.Queries.GetAvilableQuestions;
public record GetVoteQuestionsQuery(int PollId, string UserId) : IRequest<Result<List<QuestionResponse>>>;

public class GetVoteQuestionsQueryHandler(IRepository<Poll> pollRepository, IRepository<Question> questionRepository, IRepository<Vote> voteRepository) : IRequestHandler<GetVoteQuestionsQuery, Result<List<QuestionResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IRepository<Question> _questionRepository = questionRepository;
    private readonly IRepository<Vote> _voteRepository = voteRepository;

    public async Task<Result<List<QuestionResponse>>> Handle(GetVoteQuestionsQuery request, CancellationToken cancellationToken)
    {
        var voteExists = await _voteRepository.AnyAsync(x => x.PollId == request.PollId && x.UserId == request.UserId);
        if (voteExists)
            return VoteErrors.DuplicatedVote;


        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId
                                                             && !x.IsDeleted
                                                             && x.IsPublished
                                                             && x.StartsAt <= DateOnly.FromDateTime(DateTime.Now)
                                                             && x.EndsAt >= DateOnly.FromDateTime(DateTime.Now));
        if (!pollExists)
            return PollErrors.PollNotFound;



        var questions = await _questionRepository.GetListAsync(x => x.PollId == request.PollId && x.IsActive,
                                                                                disableTracking: true,
                                                                                cancellationToken: cancellationToken,
                                                                                includeProperties: x => x.Answers);

        return questions.Select(x => new QuestionResponse(x.Id,
                                                    x.Content,
                                                    x.IsActive,
                                                    x.Answers.Where(a => a.IsActive).Select(a => new AnswerResponse(a.Id, a.Content, a.IsActive)).ToList())).ToList();
    }
}
