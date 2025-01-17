using Application.Features.Auth.Commands.Login;

namespace Application.Contracts.Services.Authentication;
public interface IAuthService
{
    Task<Result<LoginResponse>> Login(string email, string password);
}
