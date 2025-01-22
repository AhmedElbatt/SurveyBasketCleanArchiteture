using Application.Features.Questions.Shared;

namespace Application.Contracts.Repositories.Persistance;
public interface IQuestionRepository : IRepository<Question>
{
    Task<List<QuestionResponse>> GetListAsync(int pollId, CancellationToken cancellationToken = default);

    Task<QuestionResponse?> GetAsync(int pollId,int questionId, CancellationToken cancellationToken = default);
}
