using Application.Features.Polls.Commands.CreatePoll;
using Application.Features.Polls.Commands.UpdatePoll;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PollsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok("");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request.Adapt<CreatePollCommand>(), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePollRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request.Adapt<UpdatePollCommand>(), cancellationToken);
        return NoContent();
    }
}
