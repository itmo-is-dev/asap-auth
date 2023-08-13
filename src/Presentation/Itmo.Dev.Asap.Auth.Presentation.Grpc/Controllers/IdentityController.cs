using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Queries;
using Itmo.Dev.Asap.Auth.Application.Contracts.Users.Queries;
using Itmo.Dev.Asap.Auth.Presentation.Grpc.Mapping;
using MediatR;

namespace Itmo.Dev.Asap.Auth.Presentation.Grpc.Controllers;

public class IdentityController : IdentityService.IdentityServiceBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        Login.Query query = request.MapTo();
        Login.Response response = await _mediator.Send(query, context.CancellationToken);

        return response.MapFrom();
    }

    public override async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, ServerCallContext context)
    {
        ValidateToken.Query query = request.MapTo();
        ValidateToken.Response response = await _mediator.Send(query, context.CancellationToken);

        return response.MapFrom();
    }

    public override async Task<Empty> ChangeUserRole(ChangeUserRoleRequest request, ServerCallContext context)
    {
        ChangeUserRole.Command command = request.MapTo();
        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> CreateUserAccount(CreateUserAccountRequest request, ServerCallContext context)
    {
        CreateUserAccount.Command command = request.MapTo();
        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }

    public override async Task<UpdateUsernameResponse> UpdateUsername(
        UpdateUsernameRequest request,
        ServerCallContext context)
    {
        UpdateUsername.Command command = request.MapTo();
        UpdateUsername.Response response = await _mediator.Send(command, context.CancellationToken);

        return response.MapFrom();
    }

    public override async Task<UpdatePasswordResponse> UpdatePassword(
        UpdatePasswordRequest request,
        ServerCallContext context)
    {
        UpdatePassword.Command command = request.MapTo();
        UpdatePassword.Response response = await _mediator.Send(command, context.CancellationToken);

        return response.MapFrom();
    }

    public override async Task<GetPasswordOptionsResponse> GetPasswordOptions(
        GetPasswordOptionsRequest request,
        ServerCallContext context)
    {
        GetPasswordOptions.Query query = request.MapTo();
        GetPasswordOptions.Response response = await _mediator.Send(query, context.CancellationToken);

        return response.PasswordOptions.MapFrom();
    }

    public override async Task<FindUsersResponse> FindUsers(FindUsersRequest request, ServerCallContext context)
    {
        FindUsers.Query query = request.MapTo();
        FindUsers.Response response = await _mediator.Send(query, context.CancellationToken);

        return response.MapFrom();
    }
}