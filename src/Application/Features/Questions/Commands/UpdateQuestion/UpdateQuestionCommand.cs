using Application.Errors;

namespace Application.Features.Questions.Commands.UpdateQuestion;
public record UpdateQuestionCommand(int PollId, int QuestionId, string Content, List<string> Answers) : IRequest<Result>;

public class UpdateQuestionCommandHandler(IRepository<Question> questionRepository) : IRequestHandler<UpdateQuestionCommand, Result>
{
    private readonly IRepository<Question> _questionRepository = questionRepository;

    public async Task<Result> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var questionContentDuplicated = await _questionRepository.AnyAsync(x => x.PollId == request.PollId && x.Id != request.QuestionId && x.Content == request.Content, cancellationToken);
        if (questionContentDuplicated)
            return Result.Failure(QuestionErrors.DuplicatedQuestionContent);

        var questionToUpdate = await _questionRepository.GetAsync(x => x.PollId == request.PollId && x.Id == request.QuestionId,false, cancellationToken: cancellationToken, includeProperties: x => x.Answers);
        if (questionToUpdate is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        var currentAnswers = questionToUpdate.Answers.Select(x => x.Content).ToList();

        // get the new answers 
        var newAnswers = request.Answers.Except(currentAnswers).ToList();

        // update current answers 
        questionToUpdate.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = request.Answers.Contains(answer.Content);
        });

        // Add new answers 
        newAnswers.ForEach(answer =>
        {
            questionToUpdate.Answers.Add(new Answer { Content = answer });
        });

        questionToUpdate.Update(request.Content, questionToUpdate.Answers);
        await _questionRepository.UpdateAsync(questionToUpdate, cancellationToken);
        return Result.Success();
    }
}
