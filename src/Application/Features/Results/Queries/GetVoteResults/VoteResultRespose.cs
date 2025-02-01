
namespace Application.Features.Results.Queries.GetVoteResults;

public record VoteResultRespose(string Title, IEnumerable<VoteResponse> Votes);

