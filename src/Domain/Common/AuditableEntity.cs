using Domain.Entities;

namespace Domain.Common;
public class AuditableEntity
{
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? LastUpdatedById { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public string? DeletedById { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ApplicationUser CreatedBy { get; set; } = default!;
    public ApplicationUser? LastUpdatedBy { get; set; }
    public ApplicationUser? DeletedBy { get; set; }
}
