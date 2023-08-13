using System.Security.Claims;

namespace Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;

public interface ICurrentUserManager
{
    void Authenticate(ClaimsPrincipal principal);
}