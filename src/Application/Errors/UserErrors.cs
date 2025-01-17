using Microsoft.AspNetCore.Http;

namespace Application.Errors;
public static class UserErrors
{
    public static readonly Error InvalidCredientials = new Error("INVALID_CREDENTIALS", "Invalid email/password", StatusCodes.Status401Unauthorized);
}
