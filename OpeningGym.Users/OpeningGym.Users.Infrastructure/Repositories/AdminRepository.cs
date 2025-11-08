using Microsoft.EntityFrameworkCore;
using OpeningGym.Users.Domain.Admins;
using OpeningGym.Users.Domain.Shared;
using OpeningGym.Users.Infrastructure.Context;

namespace OpeningGym.Users.Infrastructure.Repositories;
internal sealed class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;
    public AdminRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Admin> CreateAsync(string fullName, string email, string password, CancellationToken cancellationToken = default)
    {
        var existingAdmin = await GetByEmailAsync(email, cancellationToken);
        if (existingAdmin is not null)
        {
            throw new InvalidOperationException("Bu e-posta adresiyle kayıtlı yönetici bulunmakta");
        }

        Admin admin = Admin.CreateAdmin(fullName, email, password);
        await _context.Admins.AddAsync(admin, cancellationToken);
        return admin;
    }

    public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Admins.Where(a => a.Email == new Email(email)).FirstOrDefaultAsync(cancellationToken);
    }
}
