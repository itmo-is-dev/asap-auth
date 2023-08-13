using Microsoft.AspNetCore.Identity;

namespace Itmo.Dev.Asap.Auth.Identity.Entities;

public class AsapIdentityRole : IdentityRole<Guid>
{
    public AsapIdentityRole(string roleName) : base(roleName) { }

    protected AsapIdentityRole() { }
}