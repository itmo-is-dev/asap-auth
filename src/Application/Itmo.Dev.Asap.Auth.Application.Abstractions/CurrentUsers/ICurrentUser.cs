namespace Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;

public interface ICurrentUser
{
    Guid Id { get; }

    bool CanCreateUserWithRole(string roleName);

    bool CanChangeUserRole(string currentRoleName, string newRoleName);
}