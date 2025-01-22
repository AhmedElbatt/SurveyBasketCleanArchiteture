using Application.Contracts.Repositories.Persistance;
using Application.Features.Questions.Shared;
using Mapster;

namespace Persistance.Repositories;
public class QuestionRepository(ApplicationDbContext context) : IQuestionRepository
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<QuestionResponse>> GetQuestionList(int pollId, CancellationToken cancellationToken = default)
    {
        return _context.Questions
            .Where(x => x.PollId == pollId)
            .Include(x => x.Answers)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
