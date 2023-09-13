using Itmo.Dev.Asap.Auth.Application.Dto.Users;

namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Services.Results;

public abstract record CreateUserResult
{
    private CreateUserResult() { }

    public sealed record Success(IdentityUserDto User) : CreateUserResult;

    public sealed record AlreadyExists : CreateUserResult;

    public sealed record Failure(string Description) : CreateUserResult;
}