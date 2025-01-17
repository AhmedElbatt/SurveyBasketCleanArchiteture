using Application.Features.Auth.Commands.Login;

namespace Application.Contracts.Services.Authentication;
public interface IAuthService
{
    Task<LoginResponse> Login(string email, string password);
}
