﻿using Domain.Common;

namespace Domain.Entities;
public sealed class VoteAnswer : AuditableEntity
{
    public int Id { get; set; }

    public int VoteId { get; set; }

    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public Vote Vote { get; set; } = default!;
    public  Question Question { get; set; } = default!;
    public Answer Answer { get; set; } = default!;
}
