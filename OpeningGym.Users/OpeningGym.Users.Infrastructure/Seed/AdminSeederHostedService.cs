using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpeningGym.Users.Domain.Admins;
using OpeningGym.Users.Infrastructure.Context;

namespace OpeningGym.Users.Infrastructure.Seed;
public sealed class AdminSeederHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AdminSeederHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync(cancellationToken);

        if (!await context.Admins.AnyAsync(cancellationToken))
        {
            var admin = Admin.CreateAdmin(
                fullName: "Ahmet Mahir Demirelli",
                email: "project.test.amd@gmail.com",
                password: "123Admin456"
            );

            context.Admins.Add(admin);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
