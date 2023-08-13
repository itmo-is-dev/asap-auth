namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string? message) : base(message) { }

    public ApplicationException(string? message, Exception? innerException) : base(message, innerException) { }
}