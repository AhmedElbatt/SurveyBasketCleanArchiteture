namespace Application.Features.Polls.Requests.GetPoll;
public record GetPollQuery(int Id) : IRequest<Result<GetPollResponse>>;

public class GetPollQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollQuery, Result<GetPollResponse>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<GetPollResponse>> Handle(GetPollQuery request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
        return poll.Adapt<GetPollResponse>();
    }
}

