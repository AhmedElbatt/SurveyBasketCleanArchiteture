using Application.Contracts.Repositories.Persistance;

namespace Application.Features.Polls.Commands.CreatePoll;
public record CreatePollCommand(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest<CreatePollResponse>;

public class CreatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<CreatePollCommand, CreatePollResponse>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<CreatePollResponse> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var titleExists = await _pollRepository.AnyAsync(x => x.Title == request.Title, cancellationToken);
        if (titleExists)
            throw new Exception("Duplicate poll titles not possible");

        var response = await _pollRepository.AddAsync(request.Adapt<Poll>(), cancellationToken);
        return response.Adapt<CreatePollResponse>();
    }
}


