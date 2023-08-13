using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.CreateUserAccount;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class CreateUserAccountHandler : IRequestHandler<Command>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public CreateUserAccountHandler(
        ICurrentUser currentUser,
        IAuthorizationService authorizationService)
    {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
        if (_currentUser.CanCreateUserWithRole(request.Role) is false)
            throw new AccessDeniedException($"User {_currentUser.Id} can't create user with role {request.Role}");

        await _authorizationService.CreateUserAsync(
            request.UserId,
            request.Username,
            request.Password,
            request.Role,
            cancellationToken);
    }
}