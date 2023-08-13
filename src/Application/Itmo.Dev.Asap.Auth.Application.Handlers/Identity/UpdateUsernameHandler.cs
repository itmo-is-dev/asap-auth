using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.UpdateUsername;
using ApplicationException = Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions.ApplicationException;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class UpdateUsernameHandler : IRequestHandler<Command, Response>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public UpdateUsernameHandler(ICurrentUser currentUser, IAuthorizationService authorizationService)
    {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        IdentityUserDto user = await _authorizationService.GetUserByIdAsync(_currentUser.Id, cancellationToken);

        if (user.Username.Equals(request.Username, StringComparison.Ordinal))
            throw new ApplicationException("the old username is the same as the new one");

        await _authorizationService.UpdateUserNameAsync(user.Id, request.Username, cancellationToken);

        string token = await _authorizationService.GetUserTokenAsync(request.Username, cancellationToken);

        return new Response(token);
    }
}