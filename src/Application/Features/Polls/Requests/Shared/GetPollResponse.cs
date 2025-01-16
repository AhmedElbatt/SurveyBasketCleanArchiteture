public record GetPollResponse(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt, bool IsPublished);

