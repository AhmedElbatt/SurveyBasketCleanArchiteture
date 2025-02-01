using Application.Contracts.Repositories.Persistance;
using Application.Features.Results.Queries.GetVoteResults;

namespace Persistance.Repositories;
public class ResultRepository(ApplicationDbContext context) : IResultRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<VoteResultRespose?> GetVoteResults(int pollId)
    {
        return await _context.Polls
                     .Where(x => x.Id == pollId)
                     .Select(x => 
                                        new VoteResultRespose(x.Title, 
                                                              x.Votes.Select(v => 
                                                              new VoteResponse($"{v.User.FirstName} {v.User.LastName}", v.SubmittedAt, v.VoteAnswers.Select(n => 
                                                                                        new VoteAnswerResponse(n.Question.Content, n.Answer.Content)))))).SingleOrDefaultAsync();
    }
}
