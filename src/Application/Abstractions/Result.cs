namespace Application.Abstractions;
public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            throw new InvalidOperationException("Something went wrong in the result");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; } = default!;  

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<TPayload> Success<TPayload>(TPayload payload) => new(payload, true, Error.None);
    public static Result<TPayload> Failure<TPayload>(Error error) => new(default!, false, error);
}

public class Result<TPayload> : Result
{
    private readonly TPayload? _payload;

    public Result(TPayload? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _payload = value;
    }

    public TPayload Payload => IsSuccess
        ? _payload!
        : throw new InvalidOperationException("Failure results cannot have value");

    public static implicit operator Result<TPayload>(TPayload payload) => Success<TPayload>(payload);
    public static implicit operator Result<TPayload>(Error error) => Failure<TPayload>(error);
}

