using Microsoft.Extensions.DependencyInjection;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Admins;
using OpeningGym.Users.Domain.PendingUsers;
using OpeningGym.Users.Domain.Users;
using OpeningGym.Users.Infrastructure.Context;
using OpeningGym.Users.Infrastructure.Repositories;
using OpeningGym.Users.Infrastructure.Seed;

namespace OpeningGym.Users.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork>(opt => opt.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPendingUserRepository, PendingUserRepository>();

        services.AddHostedService<AdminSeederHostedService>();

        return services;
    }
}
