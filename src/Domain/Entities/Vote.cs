using Domain.Common;
namespace Domain.Entities;
public sealed class Vote : AuditableEntity
{
    public int Id { get; set; }

    public int PollId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; }

    public Poll Poll { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];

    public static Vote Create(int pollId, string userId, List<VoteAnswer> answers)
    {
        return new Vote
        {
            PollId = pollId,
            UserId = userId,
            SubmittedAt = DateTime.UtcNow,
            VoteAnswers = answers
        };
    }
}
