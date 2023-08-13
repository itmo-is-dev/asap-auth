namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string? message) : base(message) { }
}