using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;

namespace Itmo.Dev.Asap.Auth.Application.CurrentUsers;

internal class ModeratorUser : ICurrentUser
{
    private static readonly HashSet<string?> AvailableRolesToChange = new()
    {
        AsapIdentityRoleNames.MentorRoleName,
    };

    public ModeratorUser(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName)
    {
        return AvailableRolesToChange.Contains(roleName);
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
    {
        return AvailableRolesToChange.Contains(currentRoleName) && AvailableRolesToChange.Contains(newRoleName);
    }
}