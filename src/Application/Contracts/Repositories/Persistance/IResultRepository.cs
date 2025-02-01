using Application.Features.Results.Queries.GetVoteResults;

namespace Application.Contracts.Repositories.Persistance;
public interface IResultRepository
{
    Task<VoteResultRespose?> GetVoteResults(int pollId);
}
