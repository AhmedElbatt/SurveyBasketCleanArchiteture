namespace Application.Features.Questions.Commands.CreateQuestion;
public record CreateQuestionRequest(string Content, List<string> Answers);

