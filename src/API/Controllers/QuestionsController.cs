using Application.Abstractions;
using Application.Features.Questions.Commands.CreateQuestion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace API.Controllers;
[Route("api/polls/{pollId}/[controller]")]
[ApiController]
public class QuestionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromRoute] int pollId, [FromBody] CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateQuestionCommand(pollId, request.Content, request.Answers));
        return result.IsSuccess ? CreatedAtAction(nameof(GetQuestion), new { pollId, result.Payload.Id }, result.Payload) : result.ToProblem();
    }
}
