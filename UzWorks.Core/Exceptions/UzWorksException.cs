namespace UzWorks.Core.Exceptions;

public class UzWorksException : Exception
{
    public UzWorksException()
    {
    }

    public UzWorksException(string? message) : base(message)
    {
    }

    public UzWorksException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
