using Itmo.Dev.Asap.Auth.Application.Abstractions.Services.Results;
using Itmo.Dev.Asap.Auth.Application.Dto.Users;
using System.Security.Claims;

namespace Itmo.Dev.Asap.Auth.Application.Abstractions.Services;

public interface IAuthorizationService
{
    Task<string> GetUserTokenAsync(string username, CancellationToken cancellationToken);

    ClaimsPrincipal? DecodePrincipal(string token);

    Task CreateRoleIfNotExistsAsync(string roleName, CancellationToken cancellationToken = default);

    Task<CreateUserResult> CreateUserAsync(
        Guid userId,
        string username,
        string password,
        string roleName,
        CancellationToken cancellationToken = default);

    Task<IdentityUserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<IdentityUserDto>> GetUsersByIdsAsync(
        IEnumerable<Guid> userIds,
        CancellationToken cancellationToken = default);

    Task<IdentityUserDto> GetUserByNameAsync(string username, CancellationToken cancellationToken = default);

    Task<IdentityUserDto?> FindUserByNameAsync(string username, CancellationToken cancellationToken);

    Task UpdateUserNameAsync(Guid userId, string newUsername, CancellationToken cancellationToken = default);

    Task<IdentityUserDto> UpdateUserPasswordAsync(
        Guid userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default);

    Task UpdateUserRoleAsync(Guid userId, string newRoleName, CancellationToken cancellationToken = default);

    Task<string> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> CheckUserPasswordAsync(Guid userId, string password, CancellationToken cancellationToken = default);
}