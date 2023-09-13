using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;

internal static class CreateUserAccount
{
    public record Command(Guid UserId, string Username, string Password, string Role) : IRequest<Response>;

    public abstract record Response
    {
        public sealed record Success(IdentityUserDto User) : Response;

        public sealed record AlreadyExists : Response;

        public sealed record Failure(string Description) : Response;
    }
}