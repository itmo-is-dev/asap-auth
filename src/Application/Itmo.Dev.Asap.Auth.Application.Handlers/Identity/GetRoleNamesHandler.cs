using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using MediatR;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries.GetRoleNames;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class GetRoleNamesHandler : IRequestHandler<Query, Response>
{
    private static readonly Task<Response> CachedTask = Task.FromResult(new Response(new[]
    {
        AsapIdentityRoleNames.AdminRoleName,
        AsapIdentityRoleNames.MentorRoleName,
        AsapIdentityRoleNames.ModeratorRoleName,
    }));

    public Task<Response> Handle(Query request, CancellationToken cancellationToken)
        => CachedTask;
}