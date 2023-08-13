using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;

internal static class UpdateUsername
{
    public record Command(string Username) : IRequest<Response>;

    public record Response(string Token);
}