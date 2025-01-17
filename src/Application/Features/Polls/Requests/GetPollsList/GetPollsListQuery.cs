namespace Application.Features.Polls.Requests.GetPollsList;
public record GetPollsListQuery : IRequest<Result<List<GetPollResponse>>>;

public class GetPollsListQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollsListQuery, Result<List<GetPollResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<List<GetPollResponse>>> Handle(GetPollsListQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetListAsync(cancellationToken);
        return polls.Adapt<List<GetPollResponse>>();
    }
}
