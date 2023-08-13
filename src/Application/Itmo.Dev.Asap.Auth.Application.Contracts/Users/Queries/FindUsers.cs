using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Users.Queries;

internal static class FindUsers
{
    public record Query(IEnumerable<Guid> UserIds) : IRequest<Response>;

    public record Response(IEnumerable<IdentityUserDto> Users);
}