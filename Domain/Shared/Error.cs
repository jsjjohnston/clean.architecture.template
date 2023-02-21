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

    public static bool operator ==(Error? first, Error? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Code == second.Code;
    }

    public static bool operator !=(Error? first, Error? second)
    {
        return !(first == second);
    }

    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return other.Code == Code;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Error error)
        {
            return false;
        }

        return error.Code == Code;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}
