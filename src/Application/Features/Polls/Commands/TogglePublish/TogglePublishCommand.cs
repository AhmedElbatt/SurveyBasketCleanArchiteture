namespace Application.Features.Polls.Commands.TogglePublish;
public record TogglePublishCommand(int Id) : IRequest;

public record TogglePublishCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<TogglePublishCommand>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task Handle(TogglePublishCommand request, CancellationToken cancellationToken)
    {
        var pollToUpdate = await _pollRepository.GetByIdAsync(request.Id);
        if (pollToUpdate == null)
            throw new NullReferenceException($"Poll with Id: {request.Id} not found.");

        pollToUpdate.IsPublished = !pollToUpdate.IsPublished;
        await _pollRepository.UpdateAsync(pollToUpdate, cancellationToken);
    }
}