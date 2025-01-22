using Application.Errors;

namespace Application.Features.Polls.Commands.UpdatePoll;
public record UpdatePollCommand(int Id, string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest<Result>;

public class UpdatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<UpdatePollCommand, Result>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result> Handle(UpdatePollCommand request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);

        if (poll == null)
            return Result.Failure(PollErrors.PollNotFound);

        var titleExists = await _pollRepository.AnyAsync(x => x.Title == request.Title && x.Id != request.Id, cancellationToken);
        if (titleExists)
            return Result.Failure(PollErrors.DuplicatedPollsNotAllowed);

        var pollToUpdate = poll.Update(request.Title, request.Summary, request.StartsAt, request.EndsAt);
        await _pollRepository.UpdateAsync(pollToUpdate, cancellationToken);
        return Result.Success();
    }
}

