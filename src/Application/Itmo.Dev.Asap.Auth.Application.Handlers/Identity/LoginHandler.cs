using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries.Login;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class LoginHandler : IRequestHandler<Query, Response>
{
    private readonly IAuthorizationService _authorizationService;

    public LoginHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        IdentityUserDto? user = await _authorizationService.FindUserByNameAsync(request.Username, cancellationToken);

        if (user is null)
            return new Response.InvalidUsername();

        bool passwordCorrect = await _authorizationService.CheckUserPasswordAsync(
            user.Id,
            request.Password,
            cancellationToken);

        if (passwordCorrect is false)
            return new Response.InvalidUsername();

        string token = await _authorizationService.GetUserTokenAsync(request.Username, cancellationToken);

        return new Response.Success(token);
    }
}