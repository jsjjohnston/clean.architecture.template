namespace Domain.Shared;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? first, Error? second) => (first is null && second is null) || 
                                                                   (first is not null && second is not null && first.Code == second.Code);

    public static bool operator !=(Error? first, Error? second) => !(first == second);

    public bool Equals(Error? other) => other is not null && other.Code == Code;

    public override bool Equals(object? obj) => obj is not null &&obj is Error error &&error.Code == Code;

    public override int GetHashCode() => Code.GetHashCode();
}
