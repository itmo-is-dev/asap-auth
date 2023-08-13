using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Platform.Testing;
using Moq;
using Xunit.Abstractions;

namespace Itmo.Dev.Asap.Auth.Tests;

public class AuthTestBase : TestBase
{
    protected AuthTestBase(ITestOutputHelper? output = null) : base(output)
    {
        AuthorizationServiceMock = new Mock<IAuthorizationService>();
    }

    protected Mock<IAuthorizationService> AuthorizationServiceMock { get; }
}