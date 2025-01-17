using Application.Contracts.Repositories.Persistance;

namespace Application.Features.Polls.Requests.GetPoll;
public record GetPollQuery(int Id) : IRequest<GetPollResponse>;

public class GetPollQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollQuery, GetPollResponse>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<GetPollResponse> Handle(GetPollQuery request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
        return poll.Adapt<GetPollResponse>();
    }
}

