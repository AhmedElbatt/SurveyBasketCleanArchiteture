namespace Application.Features.Votes.Commands.AnswerVote;
public class AnswerVoteItemValidator : AbstractValidator<AnswerVoteItem>
{
    public AnswerVoteItemValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0);

        RuleFor(x => x.AnswerId)
            .GreaterThan(0);
    }
}
