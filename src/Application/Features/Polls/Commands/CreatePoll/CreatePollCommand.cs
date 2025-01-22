using Application.Errors;

namespace Application.Features.Polls.Commands.CreatePoll;
public record CreatePollCommand(string Title, string Summary, DateOnly StartsAt, DateOnly EndsAt) : IRequest<Result<CreatePollResponse>>;

public class CreatePollCommandHandler(IRepository<Poll> pollRepository) : IRequestHandler<CreatePollCommand, Result<CreatePollResponse>>
{
    private readonly IRepository<Poll> _pollRepository = pollRepository;

    public async Task<Result<CreatePollResponse>> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var titleExists = await _pollRepository.AnyAsync(x => x.Title == request.Title, cancellationToken);
        if (titleExists)
            return PollErrors.DuplicatedPollsNotAllowed;

        var pollToCreate = Poll.Create(request.Title, request.Summary, request.StartsAt, request.EndsAt);
        var response = await _pollRepository.AddAsync(pollToCreate, cancellationToken);
        return response.Adapt<CreatePollResponse>();
    }
}


