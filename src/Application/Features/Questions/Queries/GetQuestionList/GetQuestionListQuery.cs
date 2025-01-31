using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetQuestionList;
public record GetQuestionListQuery(int PollId) : IRequest<Result<List<QuestionResponse>>>;

public class GetQuestionListQueryHandler(IRepository<Poll> pollRepository, IRepository<Question> questionRepository) : IRequestHandler<GetQuestionListQuery, Result<List<QuestionResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result<List<QuestionResponse>>> Handle(GetQuestionListQuery request, CancellationToken cancellationToken)
    {
        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId);
        if (!pollExists)
            return PollErrors.PollNotFound;

        var questions = await _questionRepository.GetListAsync<QuestionResponse>(x => x.PollId == request.PollId, cancellationToken);

        return questions.ToList();
    }
}

