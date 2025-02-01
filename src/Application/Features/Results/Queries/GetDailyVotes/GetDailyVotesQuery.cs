namespace Application.Features.Results.Queries.GetDailyVotes;

public record DailyVotesResponse(DateOnly Date, int CountOfVotes);

public record GetDailyVotesQuery(int PollId) : IRequest<Result<IEnumerable<DailyVotesResponse>>>;

public class GetDailyVotesQueryHandler(IRepository<Poll> pollRepository, IResultRepository resultRepository) : IRequestHandler<GetDailyVotesQuery, Result<IEnumerable<DailyVotesResponse>>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;
    private readonly IResultRepository _resultRepository = resultRepository;

    public async Task<Result<IEnumerable<DailyVotesResponse>>> Handle(GetDailyVotesQuery request, CancellationToken cancellationToken)
    {
        var pollExists = await _pollRepository.AnyAsync(x => x.Id == request.PollId, cancellationToken);
        if (!pollExists)
            return PollErrors.PollNotFound;

        return Result.Success(await _resultRepository.GetDailyVotes(request.PollId, cancellationToken));
    }
}

