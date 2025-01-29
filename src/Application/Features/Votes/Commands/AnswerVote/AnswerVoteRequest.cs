namespace Application.Features.Votes.Commands.AnswerVote;

public record AnswerVoteRequest(IEnumerable<AnswerVoteItem> Answers);
