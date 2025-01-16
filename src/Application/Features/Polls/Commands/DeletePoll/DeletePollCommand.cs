using Domain.Entities;

namespace Application.Features.Polls.Commands.DeletePoll;
public record DeletePollCommand(int Id) : IRequest;

public class DeletePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<DeletePollCommand>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task Handle(DeletePollCommand request, CancellationToken cancellationToken)
    {
        var pollToDelete = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);

        if (pollToDelete == null)
            throw new NullReferenceException($"Poll with Id: {request.Id} not found.");

        await _pollRepository.DeleteAsync(pollToDelete, cancellationToken);
    }
}
