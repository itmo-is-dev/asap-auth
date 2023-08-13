using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using System.Security.Claims;

namespace Itmo.Dev.Asap.Auth.Application.CurrentUsers;

public class CurrentUserProxy : ICurrentUser, ICurrentUserManager
{
    private ICurrentUser _user = new AnonymousUser();

    public Guid Id => _user.Id;

    public bool CanCreateUserWithRole(string roleName)
    {
        return _user.CanCreateUserWithRole(roleName);
    }

    public bool CanChangeUserRole(string currentRoleName, string newRoleName)
    {
        return _user.CanChangeUserRole(currentRoleName, newRoleName);
    }

    public void Authenticate(ClaimsPrincipal principal)
    {
        var roles = principal.Claims
            .Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        string nameIdentifier = principal.Claims
            .Single(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))
            .Value;

        if (!Guid.TryParse(nameIdentifier, out Guid id))
        {
            throw new UnauthorizedException("Failed to parse user NameIdentifier to Guid");
        }

        if (roles.Contains(AsapIdentityRoleNames.AdminRoleName)
            || roles.Contains(AsapIdentityRoleNames.ServiceRoleName))
        {
            _user = new AdminUser(id);
        }
        else if (roles.Contains(AsapIdentityRoleNames.ModeratorRoleName))
        {
            _user = new ModeratorUser(id);
        }
        else if (roles.Contains(AsapIdentityRoleNames.MentorRoleName))
        {
            _user = new MentorUser(id);
        }
        else
        {
            _user = new AnonymousUser();
        }
    }
}