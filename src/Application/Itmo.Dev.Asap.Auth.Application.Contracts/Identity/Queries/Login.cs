using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;

internal static class Login
{
    public record Query(string Username, string Password) : IRequest<Response>;

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(string Token) : Response;

        public sealed record InvalidUsername : Response;

        public sealed record InvalidPassword : Response;
    }
}