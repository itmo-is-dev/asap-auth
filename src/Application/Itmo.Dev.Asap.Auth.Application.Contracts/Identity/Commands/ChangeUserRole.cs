using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;

internal static class ChangeUserRole
{
    public record Command(string Username, string Role) : IRequest;
}