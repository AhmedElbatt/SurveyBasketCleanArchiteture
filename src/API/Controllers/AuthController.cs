using Application.Abstractions;
using Application.Features.Auth.Commands.Login;

namespace API.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        //throw new Exception("Test Exceptions");
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Payload) : result.ToProblem();
    }
}
