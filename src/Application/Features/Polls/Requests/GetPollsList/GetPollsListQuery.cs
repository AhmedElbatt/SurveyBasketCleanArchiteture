using Application.Contracts.Repositories.Persistance;

namespace Application.Features.Polls.Requests.GetPollsList;
public record GetPollsListQuery : IRequest<List<GetPollResponse>>;

public class GetPollsListQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetPollsListQuery, List<GetPollResponse>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<List<GetPollResponse>> Handle(GetPollsListQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetListAsync(cancellationToken);
        return polls.Adapt<List<GetPollResponse>>();
    }
}
