using Application.Features.Polls.Commands.CreatePoll;
using Application.Features.Polls.Commands.DeletePoll;
using Application.Features.Polls.Commands.UpdatePoll;
using Application.Features.Polls.Requests.GetPoll;
using Application.Features.Polls.Requests.GetPollsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PollsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet("")]
    public async Task<IActionResult> GetList(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPollsListQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPollQuery(id), cancellationToken);
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePollCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePollCommand(id), cancellationToken);
        return NoContent();
    }
}
