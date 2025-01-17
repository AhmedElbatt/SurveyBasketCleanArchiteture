
using Application.Contracts.Services.Authentication;

namespace Application.Features.Auth.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.Email, request.Password);
    }
}

