namespace Application.Features.Polls.Commands.CreatePoll;
public record CreatePollRequest(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt);
