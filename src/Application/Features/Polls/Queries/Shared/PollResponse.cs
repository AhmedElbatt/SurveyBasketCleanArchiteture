﻿public record PollResponse(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt, bool IsPublished);

