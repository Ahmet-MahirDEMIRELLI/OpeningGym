using Microsoft.Extensions.DependencyInjection;
using OpeningGym.Users.Application.Features.Services;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Shared;
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

        services.AddTransient<VerificationCodeService>();
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
