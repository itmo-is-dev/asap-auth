using Itmo.Dev.Asap.Auth.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itmo.Dev.Asap.Auth.Identity;

public sealed class AsapIdentityContext : IdentityDbContext<AsapIdentityUser, AsapIdentityRole, Guid>
{
    public AsapIdentityContext(DbContextOptions<AsapIdentityContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}