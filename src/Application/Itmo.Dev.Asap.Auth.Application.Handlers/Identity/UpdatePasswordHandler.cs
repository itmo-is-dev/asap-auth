using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.UpdatePassword;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class UpdatePasswordHandler : IRequestHandler<Command, Response>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public UpdatePasswordHandler(ICurrentUser currentUser, IAuthorizationService authorizationService)
    {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        IdentityUserDto existingUser = await _authorizationService.UpdateUserPasswordAsync(
            _currentUser.Id,
            request.CurrentPassword,
            request.NewPassword,
            cancellationToken);

        string token = await _authorizationService.GetUserTokenAsync(existingUser.Username, cancellationToken);

        return new Response(token);
    }
}