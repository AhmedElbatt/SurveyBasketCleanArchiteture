﻿namespace Persistance.EntitiesConfigurations;
public class VoteAnswerConfiguration : IEntityTypeConfiguration<VoteAnswer>
{
    public void Configure(EntityTypeBuilder<VoteAnswer> builder)
    {
        builder.HasIndex(x => new { x.QuestionId, x.VoteId }).IsUnique();
    }
}
