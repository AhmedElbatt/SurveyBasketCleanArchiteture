using Application.Errors;

namespace Application.Features.Polls.Commands.DeletePoll;
public record DeletePollCommand(int Id) : IRequest<Result>;

public class DeletePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<DeletePollCommand, Result>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result> Handle(DeletePollCommand request, CancellationToken cancellationToken)
    {
        var pollToDelete = await _pollRepository.GetByIdAsync(request.Id, cancellationToken);

        if (pollToDelete == null)
           return Result.Failure(PollErrors.PollNotFound);

        await _pollRepository.DeleteAsync(pollToDelete, cancellationToken);
        return Result.Success();

    }
}
