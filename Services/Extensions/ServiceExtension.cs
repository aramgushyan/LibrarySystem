using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.Scan(scan =>
            {
                scan.FromAssemblyOf<IReaderService>()
                    .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            }
        );
        return services;
    }
}
