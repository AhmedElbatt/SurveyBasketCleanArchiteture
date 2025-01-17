namespace Application.Contracts.Services.Authentication;
public interface IJwtProvider
{
    (string token, int expirationInMinutes) GenerateJwtToken(ApplicationUser user);
}
