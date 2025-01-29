using Microsoft.AspNetCore.Http;

namespace Application.Errors;
public static class VoteErrors
{
    public static readonly Error DuplicatedVote = new Error("VOTE_ALREADY_EXISTS", "Vote already exist for this user", StatusCodes.Status409Conflict);
    public static readonly Error InvalidQuestions = new Error("VOTE_HAS_INVALID_QUESTIONS", "There is invalid questions found", StatusCodes.Status409Conflict);
    public static readonly Error InvalidAnswers = new Error("VOTE_HAS_INVALID_ANSWERS", "There is invalid answers found", StatusCodes.Status409Conflict);
}
