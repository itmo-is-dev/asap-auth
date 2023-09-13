using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;

internal static class GetRoleNames
{
    public record Query : IRequest<Response>;

    public record Response(IEnumerable<string> Roles);
}