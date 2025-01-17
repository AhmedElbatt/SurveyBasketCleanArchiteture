using Application.Abstractions;
using Application.Features.Polls.Commands.CreatePoll;
using Application.Features.Polls.Commands.DeletePoll;
using Application.Features.Polls.Commands.TogglePublish;
using Application.Features.Polls.Commands.UpdatePoll;
using Application.Features.Polls.Requests.GetPoll;
using Application.Features.Polls.Requests.GetPollsList;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PollsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet("")]
    public async Task<IActionResult> GetList(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPollsListQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPollQuery(id), cancellationToken);
        return Ok(result.Payload);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Payload.Id }, result) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePollRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdatePollCommand(id, request.Title, request.Summary, request.StartsAt, request.EndsAt), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/toggle-publish")]
    public async Task<IActionResult> TogglePublish(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TogglePublishCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePollCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
