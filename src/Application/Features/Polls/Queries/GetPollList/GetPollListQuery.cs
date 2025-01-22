namespace Application.Features.Polls.Requests.GetPollsList;
public record GetPollListQuery : IRequest<Result<List<PollResponse>>>;

public class GetPollsListQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollListQuery, Result<List<PollResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<List<PollResponse>>> Handle(GetPollListQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetListAsync(x => !x.IsDeleted, cancellationToken);
        return polls.Adapt<List<PollResponse>>();
    }
}
