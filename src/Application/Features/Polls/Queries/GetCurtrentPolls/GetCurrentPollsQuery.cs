
namespace Application.Features.Polls.Queries.GetCurtrentPolls;
public record GetCurrentPollsQuery() : IRequest<Result<List<PollResponse>>>;

public class GetCurrentPollsQueryHandler(IRepository<Poll> pollRepository) : IRequestHandler<GetCurrentPollsQuery, Result<List<PollResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<List<PollResponse>>> Handle(GetCurrentPollsQuery request, CancellationToken cancellationToken)
    {
        var polls = await _pollRepository.GetListAsync<PollResponse>(x => x.StartsAt <= DateOnly.FromDateTime(DateTime.Now) && x.EndsAt >= DateOnly.FromDateTime(DateTime.Now) && x.IsPublished && !x.IsDeleted,
                                                                     cancellationToken);
        return polls.ToList();
    }
}

