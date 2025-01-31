using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetQuestion;
public record GetQuestionQuery(int PollId, int QuestionId) : IRequest<Result<QuestionResponse>>;

public class GetQuestionQueryHandler(IRepository<Question> questionRepository) : IRequestHandler<GetQuestionQuery, Result<QuestionResponse>>
{
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result<QuestionResponse>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var response = await _questionRepository.GetAsync<QuestionResponse>(x=>x.PollId==request.PollId && x.Id == request.QuestionId,true, cancellationToken, x=>x.Answers);
        return response == null ? QuestionErrors.QuestionNotFound : response;
    }
}

