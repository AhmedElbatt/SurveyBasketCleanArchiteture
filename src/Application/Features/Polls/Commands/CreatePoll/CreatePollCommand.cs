using Application.Contracts.Persistance;
using Domain.Entities;

namespace Application.Features.Polls.Commands.CreatePoll;
public record CreatePollCommand(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest<CreatePollResponse>;

public class CreatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<CreatePollCommand, CreatePollResponse>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<CreatePollResponse> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var response = await _pollRepository.AddAsync(request.Adapt<Poll>(), cancellationToken);
        return response.Adapt<CreatePollResponse>();
    }
}


