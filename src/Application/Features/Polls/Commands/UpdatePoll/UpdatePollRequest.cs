namespace Application.Features.Polls.Commands.UpdatePoll;
public record UpdatePollRequest(
    string Title,
    string Summary,
    DateOnly StartsAt,
    DateOnly EndsAt
);
