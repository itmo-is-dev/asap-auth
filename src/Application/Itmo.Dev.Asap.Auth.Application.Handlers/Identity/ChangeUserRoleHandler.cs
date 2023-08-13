using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.ChangeUserRole;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class ChangeUserRoleHandler : IRequestHandler<Command>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public ChangeUserRoleHandler(ICurrentUser currentUser, IAuthorizationService authorizationService)
    {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        IdentityUserDto user = await _authorizationService.GetUserByNameAsync(request.Username, cancellationToken);

        string userRoleName = await _authorizationService.GetUserRoleAsync(user.Id, cancellationToken);

        if (_currentUser.CanChangeUserRole(userRoleName, request.Role) is false)
            throw new AccessDeniedException($"Unable to change role of {user.Username}");

        await _authorizationService.UpdateUserRoleAsync(user.Id, request.Role, cancellationToken);
    }
}