namespace Application.Features.Votes.Commands.AnswerVote;
public class AnswerVoteRequestValidator : AbstractValidator<AnswerVoteRequest>
{
    public AnswerVoteRequestValidator()
    {
        RuleFor(x => x.Answers)
            .NotEmpty();

        RuleForEach(x=>x.Answers)
            .SetInheritanceValidator(v=>v.Add(new AnswerVoteItemValidator()));
    }
}
