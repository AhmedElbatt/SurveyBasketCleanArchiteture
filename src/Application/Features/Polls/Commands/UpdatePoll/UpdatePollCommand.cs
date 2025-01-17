using Application.Contracts.Repositories.Persistance;

namespace Application.Features.Polls.Commands.UpdatePoll;
public record UpdatePollCommand(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest;

public class UpdatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<UpdatePollCommand>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task Handle(UpdatePollCommand request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);

        if (poll == null)
            throw new NullReferenceException($"Poll with Id: {request.Id} not found.");

        var titleExists = await _pollRepository.AnyAsync(x => x.Title == request.Title && x.Id != request.Id, cancellationToken);
        if (titleExists)
            throw new Exception("Duplicate poll titles not possible");

        await _pollRepository.UpdateAsync(request.Adapt(poll), cancellationToken);
    }
}

