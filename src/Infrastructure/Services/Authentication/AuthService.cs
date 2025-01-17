using Application.Contracts.Services.Authentication;
using Application.Errors;
using Application.Features.Auth.Commands.Login;

namespace Infrastructure.Services.Authentication;
public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result<LoginResponse>> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return UserErrors.InvalidCredientials;

        var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordIsCorrect)
            return UserErrors.InvalidCredientials;

        var tokenResult = _jwtProvider.GenerateJwtToken(user);
        return new LoginResponse(user.Id, user.FirstName, user.LastName, tokenResult.token, tokenResult.expirationInMinutes);
    }
}
