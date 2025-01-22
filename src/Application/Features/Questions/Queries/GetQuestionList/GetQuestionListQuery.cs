using Application.Features.Questions.Shared;

namespace Application.Features.Questions.Queries.GetQuestionList;
public record GetQuestionListQuery(int PollId):IRequest<Result<List<QuestionResponse>>>;

public class GetQuestionListQueryHandler(IQuestionRepository questionRepository) : IRequestHandler<GetQuestionListQuery, Result<List<QuestionResponse>>>
{
    private readonly IQuestionRepository _questionRepository = questionRepository;

    public Task<Result<List<QuestionResponse>>> Handle(GetQuestionListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

