
using Application.Contracts.Services.Authentication;

namespace Application.Features.Auth.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService = authService;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _authService.Login(request.Email, request.Password);
        return response;
    }
}

