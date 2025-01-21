namespace Application.Features.Polls.Requests.GetPollsList;
public record GetPollListQuery : IRequest<Result<List<GetPollResponse>>>;

public class GetPollsListQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollListQuery, Result<List<GetPollResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<List<GetPollResponse>>> Handle(GetPollListQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetListAsync(x => !x.IsDeleted, cancellationToken);
        return polls.Adapt<List<GetPollResponse>>();
    }
}
