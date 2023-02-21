namespace Domain.Shared;

public interface IValidationResult
{
    public static readonly Error VailidationError = new(
        "Validation.Error",
        "A validation problem occured.");

    Error[] Errors { get; }
}
