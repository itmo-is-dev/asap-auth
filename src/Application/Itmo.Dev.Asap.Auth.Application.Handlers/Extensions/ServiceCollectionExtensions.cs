using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Dev.Asap.Auth.Application.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationHandlers(this IServiceCollection collection)
    {
        collection.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>());
        return collection;
    }
}