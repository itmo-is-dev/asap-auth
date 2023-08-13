using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;

internal static class CreateUserAccount
{
    public record Command(Guid UserId, string Username, string Password, string Role) : IRequest;
}
