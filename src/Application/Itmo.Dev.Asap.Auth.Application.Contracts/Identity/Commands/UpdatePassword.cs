using MediatR;

namespace Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;

public class UpdatePassword
{
    public record Command(string CurrentPassword, string NewPassword) : IRequest<Response>;

    public record Response(string Token);
}