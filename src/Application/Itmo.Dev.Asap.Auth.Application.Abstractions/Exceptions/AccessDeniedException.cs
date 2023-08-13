namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

public class AccessDeniedException : ApplicationException
{
    public AccessDeniedException() : base("Access denied") { }

    public AccessDeniedException(string message) : base(message) { }
}