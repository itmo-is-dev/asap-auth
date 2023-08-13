namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

public class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException(string? message) : base(message) { }
}