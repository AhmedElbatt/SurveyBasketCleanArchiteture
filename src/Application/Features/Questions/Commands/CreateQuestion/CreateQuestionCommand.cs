
using Application.Errors;
using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Commands.CreateQuestion;
public record CreateQuestionCommand(int PollId, string Content, List<string> Answers) : IRequest<Result<QuestionResponse>>;

public class CreateQuestionCommandHandler(IRepository<Poll> pollRepository, IRepository<Question> questionRepository) : IRequestHandler<CreateQuestionCommand, Result<QuestionResponse>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result<QuestionResponse>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId, cancellationToken);
        if (!pollExists)
            return PollErrors.PollNotFound;

        var questionExists = await _questionRepository.AnyAsync(x => x.PollId == request.PollId && x.Content == request.Content, cancellationToken);
        if (questionExists)
            return QuestionErrors.DuplicatedQuestionContent;

        var questionToCreate = Question.Create(request.PollId, request.Content, request.Answers.Select(answer => new Answer { Content = answer }).ToList());
        var question = await _questionRepository.AddAsync(questionToCreate, cancellationToken);

        return question.Adapt<QuestionResponse>();
    }
}
