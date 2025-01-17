using Microsoft.AspNetCore.Http;

namespace Application.Errors;
public static class PollErrors
{
    public static readonly Error PollNotFound = new Error("POLL_NOT_FOUND", "Poll not found", StatusCodes.Status404NotFound);
    public static readonly Error DuplicatedPollsNotAllowed = new Error("DUPLICATED_POLLS_NOT_ALLOWED", "There is an existing poll with the same name", StatusCodes.Status409Conflict);
}
