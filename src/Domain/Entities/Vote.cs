using Domain.Common;
namespace Domain.Entities;
public sealed class Vote : AuditableEntity
{
    public int Id { get; set; }

    public int PollId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public Poll Poll { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
