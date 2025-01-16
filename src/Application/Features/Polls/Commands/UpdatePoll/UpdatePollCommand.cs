
using Application.Contracts.Persistance;
using Domain.Entities;

namespace Application.Features.Polls.Commands.UpdatePoll;
public record UpdatePollCommand(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest;

public class UpdatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<UpdatePollCommand>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task Handle(UpdatePollCommand request, CancellationToken cancellationToken)
    {
        await _pollRepository.UpdateAsync(request.Adapt<Poll>(), cancellationToken);
    }
}

