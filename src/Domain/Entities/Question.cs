using Domain.Common;

namespace Domain.Entities;
public sealed class Question : AuditableEntity
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int PollId { get; set; }

    public Poll Poll { get; set; } = default!;

    public ICollection<Answer> Answers { get; set; } = [];

    public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];

    public static Question Create(int pollId, string content, ICollection<Answer> answers)
    {
        return new Question
        {
            PollId = pollId,
            Content = content,
            Answers = answers
        };
    }

    public Question ToggleActiveStatus()
    {
        IsActive = !IsActive;
        return this;
    }

    public Question Update(string content, ICollection<Answer> answers)
    {
        Content = content;
        Answers = answers;
        return this;
    }
}
