using Microsoft.Extensions.DependencyInjection;
using OpeningGym.Users.Domain.Abstractions;
using System.Reflection;

namespace OpeningGym.Users.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfr =>
        {
            cfr.RegisterServicesFromAssemblies(
                Assembly.GetExecutingAssembly(),
                typeof(Entity).Assembly);
        });

        return services;
    }
}
