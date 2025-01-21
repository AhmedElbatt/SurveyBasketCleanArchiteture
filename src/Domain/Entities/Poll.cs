using Domain.Common;
using Mapster;

namespace Domain.Entities;

public record PollToCreate(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt);
public record PollToUpdate(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt);
public sealed class Poll : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateOnly StartsAt { get; set; }
    public DateOnly EndsAt { get; set; }

    public ICollection<Question> Questions { get; set; } = [];

    public static Poll Create(PollToCreate pollToCreate)
    {
        return pollToCreate.Adapt<Poll>();
    }

    public Poll Update(PollToUpdate pollToUpdate)
    {
        return pollToUpdate.Adapt(this);
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
