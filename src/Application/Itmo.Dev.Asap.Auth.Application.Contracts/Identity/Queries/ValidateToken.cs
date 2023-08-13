using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;

internal static class ValidateToken
{
    public record Query(string Token) : IRequest<Response>;

    public record Response(bool IsValid);
}