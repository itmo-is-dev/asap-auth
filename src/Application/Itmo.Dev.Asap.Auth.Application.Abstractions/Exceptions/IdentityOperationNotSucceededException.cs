namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

public class IdentityOperationNotSucceededException : ApplicationException
{
    public IdentityOperationNotSucceededException(string? message)
        : base(message) { }
}