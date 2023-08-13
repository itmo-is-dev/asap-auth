using Itmo.Dev.Asap.Auth.Application.Abstractions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Itmo.Dev.Asap.Auth.Identity.Extensions;

internal static class IdentityResultExtensions
{
    public static void EnsureSucceeded(this IdentityResult result)
    {
        if (result.Succeeded)
            return;

        string message = string.Join(' ', result.Errors.Select(x => x.Description));
        throw new IdentityOperationNotSucceededException(message);
    }
}