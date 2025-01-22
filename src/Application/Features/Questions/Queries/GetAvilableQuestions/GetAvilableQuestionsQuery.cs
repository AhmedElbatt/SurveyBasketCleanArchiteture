using Application.Errors;
using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetAvilableQuestions;
public record GetAvilableQuestionsQuery(int PollId) : IRequest<Result<List<QuestionResponse>>>;

public class GetAvilableQuestionsQueryHandler(IRepository<Poll> pollRepository, IRepository<Question> questionRepository) : IRequestHandler<GetAvilableQuestionsQuery, Result<List<QuestionResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result<List<QuestionResponse>>> Handle(GetAvilableQuestionsQuery request, CancellationToken cancellationToken)
    {
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
