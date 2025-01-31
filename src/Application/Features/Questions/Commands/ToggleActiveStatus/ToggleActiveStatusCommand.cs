namespace Application.Features.Questions.Commands.ToggleQuestionStatus;
public record ToggleActiveStatusCommand(int PollId, int QuestionId) : IRequest<Result>;

public class ToggleActiveStatusCommandHandler(IRepository<Question> QuestionRepository) : IRequestHandler<ToggleActiveStatusCommand, Result>
{
    private readonly IRepository<Question> _questionRepository = QuestionRepository;

    public async Task<Result> Handle(ToggleActiveStatusCommand request, CancellationToken cancellationToken)
    {
        var questionToUpdate = await _questionRepository.GetAsync(x => x.PollId == request.PollId && x.Id == request.QuestionId, cancellationToken: cancellationToken);
        if (questionToUpdate is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        await _questionRepository.UpdateAsync(questionToUpdate.ToggleActiveStatus(), cancellationToken);
        return Result.Success();
    }
}
