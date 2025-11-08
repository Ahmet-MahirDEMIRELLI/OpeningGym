using Microsoft.EntityFrameworkCore;
using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Admins;
using OpeningGym.Users.Domain.PendingUsers;
using OpeningGym.Users.Domain.Users;

namespace OpeningGym.Users.Infrastructure.Context;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        if (isDocker)
        {
            optionsBuilder.UseSqlServer("Server=db;Database=OpeningGymUsersDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;");
        }
        else
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OpeningGymUsersDb;Integrated Security=True;TrustServerCertificate=True;");
        }
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PendingUser> PendingUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(builder =>
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FullName)
                .HasConversion(fullName => fullName.Value, value => new(value))
                .IsRequired();

            builder.Property(a => a.Email)
                .HasConversion(email => email.Value, value => new(value))
                .IsRequired();

            builder.Property(a => a.Password)
                .HasConversion(password => password.Value, value => new(value, false))
                .IsRequired();
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasConversion(password => password.Value, value => new(value, false))
                .IsRequired();

            builder.OwnsOne(u => u.Rating, rating =>
            {
                rating.Property(r => r.Bullet).HasColumnName("Rating_Bullet");
                rating.Property(r => r.BulletK).HasColumnName("Rating_Bullet_K");
                rating.Property(r => r.Blitz).HasColumnName("Rating_Blitz");
                rating.Property(r => r.BlitzK).HasColumnName("Rating_Blitz_K");
                rating.Property(r => r.Rapid).HasColumnName("Rating_Rapid");
                rating.Property(r => r.RapidK).HasColumnName("Rating_Rapid_K");
                rating.Property(r => r.Classical).HasColumnName("Rating_Classical");
                rating.Property(r => r.ClassicalK).HasColumnName("Rating_Classical_K");
            });
        });

        modelBuilder.Entity<PendingUser>(builder =>
        {
            builder.HasKey(pu => pu.Id);

            builder.Property(pu => pu.UserName)
                .HasConversion(userName => userName.Value, value => new(value))
                .IsRequired();

            builder.Property(pu => pu.Email)
                .HasConversion(email => email.Value, value => new(value))
                .IsRequired();

            builder.Property(pu => pu.Password)
                .HasConversion(password => password.Value, value => new(value, false))
                .IsRequired();

            builder.Property(pu => pu.VerificationCode)
                .IsRequired();

            builder.Property(pu => pu.CreatedTime)
                .IsRequired();
        });
    }
}
