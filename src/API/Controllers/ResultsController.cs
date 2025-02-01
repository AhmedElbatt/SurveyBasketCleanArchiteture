using Application.Abstractions;
using Application.Features.Results.Queries.GetVoteResults;

namespace API.Controllers;
[Route("api/polls/{pollId}/[controller]")]
[ApiController]
public class ResultsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetPollVoteResults([FromRoute] int pollId)
    {
        var result = await _mediator.Send(new GetVoteResultsQuery(pollId));
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }
}
