using Application.Errors;
using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetQuestionList;
public record GetQuestionListQuery(int PollId) : IRequest<Result<List<QuestionResponse>>>;

public class GetQuestionListQueryHandler(IRepository<Poll> pollRepository, IQuestionRepository questionRepository) : IRequestHandler<GetQuestionListQuery, Result<List<QuestionResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<Result<List<QuestionResponse>>> Handle(GetQuestionListQuery request, CancellationToken cancellationToken)
    {
        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId);
        if (!pollExists)
            return PollErrors.PollNotFound;

        return await _questionRepository.GetListAsync(request.PollId, cancellationToken);
    }
}

