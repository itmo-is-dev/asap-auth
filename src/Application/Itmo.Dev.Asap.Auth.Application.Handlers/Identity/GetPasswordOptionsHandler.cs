using Itmo.Dev.Asap.Auth.Application.Dto.Identity;
using Itmo.Dev.Asap.Auth.Application.Mapping;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries.GetPasswordOptions;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Identity;

internal class GetPasswordOptionsHandler : IRequestHandler<Query, Response>
{
    private readonly IOptionsSnapshot<IdentityOptions> _identityOptions;

    public GetPasswordOptionsHandler(IOptionsSnapshot<IdentityOptions> identityOptions)
    {
        _identityOptions = identityOptions;
    }

    public Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        IdentityOptions value = _identityOptions.Value;
        PasswordOptionsDto dto = value.ToDto();

        var response = new Response(dto);

        return Task.FromResult(response);
    }
}