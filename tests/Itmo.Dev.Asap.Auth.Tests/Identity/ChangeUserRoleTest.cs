using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;
using Itmo.Dev.Asap.Auth.Application.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using Itmo.Dev.Asap.Auth.Application.Handlers.Identity;
using Moq;
using Xunit;

namespace Itmo.Dev.Asap.Auth.Tests.Identity;

public class ChangeUserRoleTest : AuthTestBase
{
    [Theory]
    [InlineData(AsapIdentityRoleNames.MentorRoleName, AsapIdentityRoleNames.AdminRoleName)]
    [InlineData(AsapIdentityRoleNames.MentorRoleName, AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName, AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName, AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName, AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName, AsapIdentityRoleNames.AdminRoleName)]
    public async Task AdminChangeAnyRole_NoThrow(string currentRole, string newRole)
    {
        var user = new IdentityUserDto(
            Id: Guid.Empty,
            Username: string.Empty);

        var currentUser = new AdminUser(Guid.Empty);

        AuthorizationServiceMock
            .Setup(x => x.GetUserByNameAsync(user.Username, default))
            .ReturnsAsync(user);

        AuthorizationServiceMock
            .Setup(x => x.GetUserRoleAsync(user.Id, default))
            .ReturnsAsync(currentRole);

        var command = new ChangeUserRole.Command(user.Username, newRole);
        var handler = new ChangeUserRoleHandler(currentUser, AuthorizationServiceMock.Object);

        await handler.Handle(command, default);
    }

    [Theory]
    [InlineData(AsapIdentityRoleNames.MentorRoleName, AsapIdentityRoleNames.AdminRoleName)]
    [InlineData(AsapIdentityRoleNames.MentorRoleName, AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName, AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName, AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName, AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName, AsapIdentityRoleNames.AdminRoleName)]
    public async Task MentorChangeAnyRole_ThrowException(string currentRole, string newRole)
    {
        var user = new IdentityUserDto(
            Id: Guid.Empty,
            Username: string.Empty);

        var currentUser = new MentorUser(Guid.Empty);

        AuthorizationServiceMock
            .Setup(x => x.GetUserByNameAsync(user.Username, default))
            .ReturnsAsync(user);

        AuthorizationServiceMock
            .Setup(x => x.GetUserRoleAsync(user.Id, default))
            .ReturnsAsync(currentRole);

        var command = new ChangeUserRole.Command(user.Username, newRole);
        var handler = new ChangeUserRoleHandler(currentUser, AuthorizationServiceMock.Object);

        await Assert.ThrowsAsync<AccessDeniedException>(() =>
            handler.Handle(command, default));
    }
}