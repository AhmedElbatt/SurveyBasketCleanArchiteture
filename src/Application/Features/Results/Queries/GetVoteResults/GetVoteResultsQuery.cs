namespace Application.Features.Results.Queries.GetVoteResults;
public record GetVoteResultsQuery(int PollId) : IRequest<Result<VoteResultRespose>>;

public class GetVoteResultsQueryHandler(IResultRepository resultRepository) : IRequestHandler<GetVoteResultsQuery, Result<VoteResultRespose>>
{
    private readonly IResultRepository _resultRepository = resultRepository;

    public async Task<Result<VoteResultRespose>> Handle(GetVoteResultsQuery request, CancellationToken cancellationToken)
    {
        var result = await _resultRepository.GetVoteResults(request.PollId);
        if(result == null)
            return ResultErrors.ResultNotFound;

        return result;
    }
}

