namespace Application.Features.Questions.Shared;

public record AnswerResponse(int Id, string Content, bool IsActive);
public record QuestionResponse(int Id, string Content, bool IsActive, List<AnswerResponse> Answers);

