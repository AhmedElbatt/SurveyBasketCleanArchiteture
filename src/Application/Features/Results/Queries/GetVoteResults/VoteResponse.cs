
namespace Application.Features.Results.Queries.GetVoteResults;

public record VoteResponse(string VoterName, DateTime VoteDate, IEnumerable<VoteAnswerResponse> Answers);

