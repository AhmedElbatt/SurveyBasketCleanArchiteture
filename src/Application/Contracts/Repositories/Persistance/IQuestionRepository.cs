using Application.Features.Questions.Shared;

namespace Application.Contracts.Repositories.Persistance;
public interface IQuestionRepository 
{
    Task<List<QuestionResponse>> GetQuestionList(int pollId, CancellationToken cancellationToken = default);
}
