using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;

namespace Itmo.Dev.Asap.Auth.Application.CurrentUsers;

internal class AnonymousUser : ICurrentUser
{
    public Guid Id => throw new UnauthorizedException("Tried to access anonymous user Id");

    public bool CanCreateUserWithRole(string roleName)
        => false;

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => false;
}