using Application.Errors;

namespace Application.Features.Polls.Commands.TogglePublish;
public record TogglePublishCommand(int Id) : IRequest<Result>;

public class TogglePublishCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<TogglePublishCommand, Result>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result> Handle(TogglePublishCommand request, CancellationToken cancellationToken)
    {
        var pollToUpdate = await _pollRepository.GetByIdAsync(request.Id);
        if (pollToUpdate == null)
            return Result.Failure(PollErrors.PollNotFound);

        pollToUpdate.IsPublished = !pollToUpdate.IsPublished;
        await _pollRepository.UpdateAsync(pollToUpdate, cancellationToken);

        return Result.Success();
    }
}