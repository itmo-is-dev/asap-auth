using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;

namespace Itmo.Dev.Asap.Auth.Application.CurrentUsers;

internal class MentorUser : ICurrentUser
{
    public MentorUser(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public bool CanCreateUserWithRole(string roleName)
        => false;

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
        => false;
}