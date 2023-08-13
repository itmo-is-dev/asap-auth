using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using Itmo.Dev.Asap.Auth.Identity;
using Itmo.Dev.Asap.Auth.Identity.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Users.Queries.FindUsers;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Users;

internal class FindUsersHandler : IRequestHandler<Query, Response>
{
    private readonly AsapIdentityContext _context;

    public FindUsersHandler(AsapIdentityContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        List<AsapIdentityUser> users = await _context.Users
            .Where(x => request.UserIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        IEnumerable<IdentityUserDto> dto = users.Select(x => x.ToDto());

        return new Response(dto);
    }
}