public record CreatePollResponse(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt, bool IsPublished);
