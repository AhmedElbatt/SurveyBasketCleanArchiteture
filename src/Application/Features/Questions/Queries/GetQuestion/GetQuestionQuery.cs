using Application.Errors;
using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetQuestion;
public record GetQuestionQuery(int PollId, int QuestionId) : IRequest<Result<QuestionResponse>>;

public class GetQuestionQueryHandler(IQuestionRepository questionRepository) : IRequestHandler<GetQuestionQuery, Result<QuestionResponse>>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public async Task<Result<QuestionResponse>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var response = await _questionRepository.GetAsync(request.PollId, request.QuestionId, cancellationToken);
        return response == null ? QuestionErrors.QuestionNotFound : response;
    }
}

