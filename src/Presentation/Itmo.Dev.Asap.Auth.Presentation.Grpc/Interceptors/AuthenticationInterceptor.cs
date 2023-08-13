using Grpc.Core;
using Grpc.Core.Interceptors;
using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using System.Security.Claims;

namespace Itmo.Dev.Asap.Auth.Presentation.Grpc.Interceptors;

public class AuthenticationInterceptor : Interceptor
{
    private readonly ICurrentUserManager _currentUserManager;
    private readonly IAuthorizationService _authorizationService;

    public AuthenticationInterceptor(ICurrentUserManager currentUserManager, IAuthorizationService authorizationService)
    {
        _currentUserManager = currentUserManager;
        _authorizationService = authorizationService;
    }

    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        Metadata.Entry? authorizationHeader = context.RequestHeaders.FirstOrDefault(
            x => x.Key.Equals("authorization", StringComparison.OrdinalIgnoreCase));

        if (authorizationHeader is null || string.IsNullOrEmpty(authorizationHeader.Value))
            return continuation.Invoke(request, context);

        ClaimsPrincipal? principal = _authorizationService.DecodePrincipal(authorizationHeader.Value);

        if (principal is not null)
        {
            _currentUserManager.Authenticate(principal);
        }

        return continuation.Invoke(request, context);
    }
}