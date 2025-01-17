using Application.Contracts.Services.Authentication;
using Application.Features.Auth.Commands.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Authentication;
public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<LoginResponse> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception($"User with Email {email} not found.");

        var passwordisCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (passwordisCorrect)
            throw new Exception("UserName or password is wrong");

        var tokenResult = _jwtProvider.GenerateJwtToken(user);

        return new LoginResponse(user.Id, user.FirstName, user.LastName, tokenResult.token, tokenResult.expirationInMinutes);
    }
}
