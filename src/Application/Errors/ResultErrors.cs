namespace Application.Errors;
public static class ResultErrors
{
    public static readonly Error ResultNotFound = new Error("VOTE_RESULT_NOT_FOUND", "The voting result not found", StatusCodes.Status404NotFound);
}
