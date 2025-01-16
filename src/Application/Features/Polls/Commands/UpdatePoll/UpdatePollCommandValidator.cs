using Application.Features.Polls.Commands.CreatePoll;

namespace Application.Features.Polls.Commands.UpdatePoll;
public class UpdatePollCommandValidator:AbstractValidator<UpdatePollCommand>
{
    public UpdatePollCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(x => x.Summary)
            .NotEmpty()
            .Length(3, 1000);

        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x)
            .Must(HasValidDates)
            .WithName(nameof(CreatePollCommand.EndsAt))
            .WithMessage("{PropertyName} must be greater than or equal start date");
    }

    private bool HasValidDates(UpdatePollCommand command)
    {
        return command.EndsAt >= command.StartsAt;
    }
}
