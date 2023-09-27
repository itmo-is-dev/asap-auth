using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;
using Itmo.Dev.Asap.Auth.Application.Contracts.Users.Queries;
using Itmo.Dev.Asap.Auth.Application.Dto.Identity;
using Riok.Mapperly.Abstractions;

namespace Itmo.Dev.Asap.Auth.Presentation.Grpc.Mapping;

[Mapper]
internal static partial class IdentityServiceMapper
{
    public static partial Login.Query MapTo(this LoginRequest request);

    public static partial ValidateToken.Query MapTo(this ValidateTokenRequest request);

    public static partial ChangeUserRole.Command MapTo(this ChangeUserRoleRequest request);

    public static partial CreateUserAccount.Command MapTo(this CreateUserAccountRequest request);

    public static partial UpdateUsername.Command MapTo(this UpdateUsernameRequest request);

    public static partial UpdatePassword.Command MapTo(this UpdatePasswordRequest request);

    public static partial GetPasswordOptions.Query MapTo(this GetPasswordOptionsRequest request);

    public static partial FindUsers.Query MapTo(this FindUsersRequest request);

    public static partial LoginResponse MapFrom(this Login.Response.Success response);

    [MapProperty(nameof(ValidateToken.Response.IsValid), nameof(ValidateTokenResponse.IsTokenValid))]
    public static partial ValidateTokenResponse MapFrom(this ValidateToken.Response response);

    public static partial UpdateUsernameResponse MapFrom(this UpdateUsername.Response response);

    public static partial UpdatePasswordResponse MapFrom(this UpdatePassword.Response response);

    public static partial GetPasswordOptionsResponse MapFrom(this PasswordOptionsDto dto);

    public static partial FindUsersResponse MapFrom(this FindUsers.Response response);
}