using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services.Results;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;
using Itmo.Dev.Asap.Auth.Application.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using Itmo.Dev.Asap.Auth.Application.Handlers.Identity;
using Moq;
using Xunit;

namespace Itmo.Dev.Asap.Auth.Tests.Identity;

public class CreateUserAccountTest : AuthTestBase
{
    [Theory]
    [InlineData(AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName)]
    public async Task AdminCreateAnyRole_NoThrow(string roleName)
    {
        string username = string.Empty;
        string password = string.Empty;

        var admin = new AdminUser(Guid.Empty);

        var command = new CreateUserAccount.Command(Faker.Random.Guid(), username, password, roleName);

        AuthorizationServiceMock
            .Setup(x => x.CreateUserAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new CreateUserResult.Success(
                new IdentityUserDto(Faker.Random.Guid(), Faker.Internet.UserName())));

        var handler = new CreateUserAccountHandler(
            admin,
            AuthorizationServiceMock.Object);

        await handler.Handle(command, default);
    }

    [Theory]
    [InlineData(AsapIdentityRoleNames.MentorRoleName)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName)]
    public async Task MentorCreateAnyRole_ThrowException(string roleName)
    {
        string username = string.Empty;
        string password = string.Empty;

        var admin = new MentorUser(Guid.Empty);

        var command = new CreateUserAccount.Command(Faker.Random.Guid(), username, password, roleName);

        var handler = new CreateUserAccountHandler(
            admin,
            AuthorizationServiceMock.Object);

        await Assert.ThrowsAsync<AccessDeniedException>(() =>
            handler.Handle(command, default));
    }

    [Theory]
    [InlineData(AsapIdentityRoleNames.MentorRoleName, false)]
    [InlineData(AsapIdentityRoleNames.ModeratorRoleName, true)]
    [InlineData(AsapIdentityRoleNames.AdminRoleName, true)]
    public async Task ModeratorCanCreateOnlyMentor(string roleName, bool throwExpected)
    {
        string username = string.Empty;
        string password = string.Empty;

        var admin = new ModeratorUser(Guid.Empty);

        var command = new CreateUserAccount.Command(Faker.Random.Guid(), username, password, roleName);

        if (throwExpected is false)
        {
            AuthorizationServiceMock
                .Setup(x => x.CreateUserAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new CreateUserResult.Success(
                    new IdentityUserDto(Faker.Random.Guid(), Faker.Internet.UserName())));
        }

        var handler = new CreateUserAccountHandler(
            admin,
            AuthorizationServiceMock.Object);

        if (throwExpected)
        {
            await Assert.ThrowsAsync<AccessDeniedException>(() =>
                handler.Handle(command, default));
        }
        else
        {
            await handler.Handle(command, default);
        }
    }
}