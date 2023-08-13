using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.CreateAdmin;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class CreateAdminHandler : IRequestHandler<Command>
{
    private readonly IAuthorizationService _authorizationService;

    public CreateAdminHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        await _authorizationService.CreateUserAsync(
            Guid.NewGuid(),
            request.Username,
            request.Password,
            AsapIdentityRoleNames.AdminRoleName,
            cancellationToken);
    }
}