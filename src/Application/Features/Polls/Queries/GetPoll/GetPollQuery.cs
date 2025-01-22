namespace Application.Features.Polls.Requests.GetPoll;
public record GetPollQuery(int Id) : IRequest<Result<PollResponse>>;

public class GetPollQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollQuery, Result<PollResponse>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<PollResponse>> Handle(GetPollQuery request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);
        return poll.Adapt<PollResponse>();
    }
}

