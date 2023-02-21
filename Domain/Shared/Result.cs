namespace Domain.Shared;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result Failure(Error error) => new(true, error);
    public static Result<TValue> Failure<TValue>(TValue value, Error error) => new(value, true, error);
    protected static Result<TValue> Create<TValue>(TValue value) => new(value, false, Error.None);
    protected static Result<TValue> Create<TValue>(TValue? value, Error error)
            where TValue : class
            => value ?? Failure<TValue>(error);

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default!, false, error);

    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Success();
    }
}




