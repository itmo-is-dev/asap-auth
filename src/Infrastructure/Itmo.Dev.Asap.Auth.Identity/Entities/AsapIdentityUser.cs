using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using Microsoft.AspNetCore.Identity;

namespace Itmo.Dev.Asap.Auth.Identity.Entities;

public class AsapIdentityUser : IdentityUser<Guid>
{
    public IdentityUserDto ToDto()
    {
        return new IdentityUserDto(Id, UserName ?? string.Empty);
    }
}