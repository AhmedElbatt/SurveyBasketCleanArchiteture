using Application.Abstractions;
using Application.Features.Questions.Commands.CreateQuestion;
using Application.Features.Questions.Commands.ToggleQuestionStatus;
using Application.Features.Questions.Commands.UpdateQuestion;
using Application.Features.Questions.Queries.GetAvilableQuestions;
using Application.Features.Questions.Queries.GetQuestion;
using Application.Features.Questions.Queries.GetQuestionList;
using Application.Features.Questions.Shared;

namespace API.Controllers;
[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetQuestions([FromRoute] int pollId)
    {
        var result = await _mediator.Send(new GetQuestionListQuery(pollId));
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }

    [HttpGet("get-available-questions")]
    public async Task<IActionResult> GetAvailableQuestions([FromRoute] int pollId)
    {
        var result = await _mediator.Send(new GetAvilableQuestionsQuery(pollId));
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestion([FromRoute] int pollId, int id)
    {
        var result = await _mediator.Send(new GetQuestionQuery(pollId, id));
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromRoute] int pollId, [FromBody] QuestionRequest request)
    {
        var result = await _mediator.Send(new CreateQuestionCommand(pollId, request.Content, request.Answers));
        return result.IsSuccess ? CreatedAtAction(nameof(GetQuestion), new { pollId, result.Payload.Id }, result.Payload) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int pollId, int id, [FromBody] QuestionRequest request)
    {
        var result = await _mediator.Send(new UpdateQuestionCommand(pollId, id, request.Content, request.Answers));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpPut("{id}/toggle-active-status")]
    public async Task<IActionResult> ToggleActiveStatus([FromRoute] int pollId, int id)
    {
        var result = await _mediator.Send(new ToggleActiveStatusCommand(pollId, id));
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
