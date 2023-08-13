#pragma warning disable CA1506

using Itmo.Dev.Asap.Auth.Application.Abstractions.CurrentUsers;
using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Application.Contracts.Identity.Commands;
using Itmo.Dev.Asap.Auth.Application.Extensions;
using Itmo.Dev.Asap.Auth.Application.Handlers.Extensions;
using Itmo.Dev.Asap.Auth.Identity.Extensions;
using Itmo.Dev.Asap.Auth.Models;
using Itmo.Dev.Asap.Auth.Presentation.Grpc.Extensions;
using MediatR;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddApplicationHandlers()
    .AddIdentityConfiguration(builder.Configuration)
    .AddGrpcPresentation();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
{
    await InitAsync(scope.ServiceProvider, app.Configuration);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseRouting();

app.UseGrpcPresentation();

await app.RunAsync();
return;

static async Task InitAsync(IServiceProvider provider, IConfiguration configuration)
{
    var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.Role, AsapIdentityRoleNames.AdminRoleName),
        new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString()),
    }));

    ICurrentUserManager currentUserManager = provider.GetRequiredService<ICurrentUserManager>();
    currentUserManager.Authenticate(principal);

    IAuthorizationService authorizationService = provider.GetRequiredService<IAuthorizationService>();

    await authorizationService.CreateRoleIfNotExistsAsync(AsapIdentityRoleNames.AdminRoleName);
    await authorizationService.CreateRoleIfNotExistsAsync(AsapIdentityRoleNames.MentorRoleName);
    await authorizationService.CreateRoleIfNotExistsAsync(AsapIdentityRoleNames.ModeratorRoleName);

    IMediator mediatr = provider.GetRequiredService<IMediator>();
    ILogger<Program> logger = provider.GetRequiredService<ILogger<Program>>();
    IConfigurationSection adminsSection = configuration.GetSection("Startup:DefaultAdmins");

    AdminModel[] admins = adminsSection.Get<AdminModel[]>() ?? Array.Empty<AdminModel>();

    foreach (AdminModel admin in admins)
    {
        try
        {
            var registerCommand = new CreateAdmin.Command(admin.Username, admin.Password);
            await mediatr.Send(registerCommand);
        }
        catch (Exception e)
        {
            logger.LogInformation(e, "Failed to register admin {AdminUsername}", admin.Username);
        }
    }
}