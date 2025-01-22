using Application.Contracts.Repositories.Persistance;
using Application.Features.Questions.Shared;

namespace Persistance.Repositories;
public class QuestionRepository(ApplicationDbContext dbContext) : Repository<Question>(dbContext), IQuestionRepository
{
    public async Task<List<QuestionResponse>> GetListAsync(int pollId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Questions
            .Where(x => x.PollId == pollId)
            .Include(x => x.Answers)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<QuestionResponse?> GetAsync(int pollId, int questionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Questions
            .Where(x => x.PollId == pollId && x.Id== questionId)
            .Include(x => x.Answers)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);
    }
}
