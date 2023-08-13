using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using MediatR;
using System.Security.Claims;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries.ValidateToken;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class ValidateTokenHandler : IRequestHandler<Query, Response>
{
    private readonly IAuthorizationService _authorizationService;

    public ValidateTokenHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = _authorizationService.DecodePrincipal(request.Token);
        var response = new Response(principal is not null);

        return Task.FromResult(response);
    }
}