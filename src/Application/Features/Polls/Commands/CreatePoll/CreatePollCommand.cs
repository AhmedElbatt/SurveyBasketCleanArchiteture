namespace Application.Features.Polls.Commands.CreatePoll;
public record CreatePollCommand(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest<CreatePollResponse>;

public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, CreatePollResponse>
{
    public Task<CreatePollResponse> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


