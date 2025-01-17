namespace Application.Features.Auth.Commands.Login;
public record LoginResponse(string UserId, string FirstName, string LastName, string Token, int ExpirationInMinutes);
