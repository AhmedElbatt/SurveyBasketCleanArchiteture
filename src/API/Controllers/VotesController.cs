using Application.Abstractions;
using Application.Features.Votes.Commands.AnswerVote;
using Application.Features.Votes.Queries.GetAvilableQuestions;

namespace API.Controllers;
[Route("api/polls/{pollId}/vote")]
[ApiController]
[Authorize]
public class VotesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get-vote-questions")]
    public async Task<IActionResult> GetVoteQuestions(int pollId)
    {
        var result = await _mediator.Send(new GetVoteQuestionsQuery(pollId, User.GetUserId()!));
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> AnswerVote(int pollId, [FromBody] AnswerVoteRequest request)
    {
        var result = await _mediator.Send(new AnswerVoteCommand(pollId, User.GetUserId()!, request.Answers));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
