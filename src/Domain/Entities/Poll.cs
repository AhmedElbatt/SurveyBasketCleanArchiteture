using Domain.Common;

namespace Domain.Entities;

public sealed class Poll : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateOnly StartsAt { get; set; }
    public DateOnly EndsAt { get; set; }

    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
    public static Poll Create(string title, string summary, DateOnly startsAt, DateOnly endsAt)
    {
        return new Poll
        {
            Title = title,
            Summary = summary,
            StartsAt = startsAt,
            EndsAt = endsAt
        };
    }

    public Poll Update(string title, string summary, DateOnly startsAt, DateOnly endsAt)
    {
        Title = title;
        Summary = summary;
        StartsAt = startsAt;
        EndsAt = endsAt;
        return this;
    }

    public Poll Delete()
    {
        IsDeleted = true;
        return this;
    }

    public Poll TogglePublishStatus()
    {
        IsPublished = !IsPublished;
        return this;
    }

}
