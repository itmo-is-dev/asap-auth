using Itmo.Dev.Asap.Auth.Application.Dto.Identity;
using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;

internal static class GetPasswordOptions
{
    public record Query : IRequest<Response>;

    public record Response(PasswordOptionsDto PasswordOptions);
}