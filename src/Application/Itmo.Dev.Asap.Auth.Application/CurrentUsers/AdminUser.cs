using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;

namespace Itmo.Dev.Asap.Auth.Application.CurrentUsers;

internal class AdminUser : ICurrentUser
{
    public AdminUser(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName)
        => true;

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => true;
}