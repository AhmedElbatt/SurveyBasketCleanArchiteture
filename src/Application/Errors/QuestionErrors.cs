using Microsoft.AspNetCore.Http;

namespace Application.Errors;
public static class QuestionErrors
{
    public static readonly Error DuplicatedPollsNotAllowed = new Error("DUPLICATED_QUESTIONS_NOT_ALLOWED", "There is an existing question with the same name", StatusCodes.Status409Conflict);
}
