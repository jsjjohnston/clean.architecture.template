namespace Domain.Shared;

public static class ResultExtentions
{
    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error error) => result.IsFailure ? result : 
                                           predicate(result.Value) ? result :
                                           Result.Failure<T>(error);

    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc) => result.IsSuccess ? Result.Success(mappingFunc(result.Value)) :
                                                           Result.Failure<TOut>(result.Error);
}
