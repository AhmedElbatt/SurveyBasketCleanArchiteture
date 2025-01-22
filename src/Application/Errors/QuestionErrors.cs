using Microsoft.AspNetCore.Http;

namespace Application.Errors;
public static class QuestionErrors
{
    public static readonly Error QuestionNotFound = new Error("QUESTION_NOT_FOUND", "Question not found", StatusCodes.Status404NotFound);
    public static readonly Error DuplicatedQuestionContent = new Error("DUPLICATED_QUESTION_CONTENT", "There is an existing question with the same content", StatusCodes.Status409Conflict);
}
