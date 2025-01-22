namespace Application.Features.Questions.Shared;

public record AnswerResponse(int Id, string Content);
public record QuestionResponse(int Id, string Content, List<AnswerResponse> Answers);

