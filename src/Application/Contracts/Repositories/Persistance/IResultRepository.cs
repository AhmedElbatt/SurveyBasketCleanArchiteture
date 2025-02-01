using Application.Features.Results.Queries.GetDailyVotes;
using Application.Features.Results.Queries.GetVoteResults;

namespace Application.Contracts.Repositories.Persistance;
public interface IResultRepository
{
    Task<VoteResultRespose?> GetVoteResults(int pollId, CancellationToken cancellationToken=default);

    Task<IEnumerable<DailyVotesResponse>> GetDailyVotes(int pollId, CancellationToken cancellationToken=default);
}
