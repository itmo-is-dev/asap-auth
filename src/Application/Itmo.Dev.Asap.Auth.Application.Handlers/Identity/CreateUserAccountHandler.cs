using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services.Results;
using MediatR;
using System.Diagnostics;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands.CreateUserAccount;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class CreateUserAccountHandler : IRequestHandler<Command, Response>
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

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        if (_currentUser.CanCreateUserWithRole(request.Role) is false)
            throw new AccessDeniedException($"User {_currentUser.Id} can't create user with role {request.Role}");

        CreateUserResult result = await _authorizationService.CreateUserAsync(
            request.UserId,
            request.Username,
            request.Password,
            request.Role,
            cancellationToken);

        return result switch
        {
            CreateUserResult.Success s => new Response.Success(s.User),
            CreateUserResult.AlreadyExists => new Response.AlreadyExists(),
            _ => throw new UnreachableException("CreateUserResult DU has unhandled case"),
        };
    }
}