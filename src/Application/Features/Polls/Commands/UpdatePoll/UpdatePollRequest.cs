namespace Application.Features.Polls.Commands.UpdatePoll;
public record UpdatePollRequest(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt);
