using Microsoft.EntityFrameworkCore;
using OpeningGym.Users.Domain.Users;
using OpeningGym.Users.Infrastructure.Context;

namespace OpeningGym.Users.Infrastructure.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(string userName, string email, string hashedPassword, CancellationToken cancellationToken = default)
    {
        var existingUser = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Bu e-posta adresiyle kayıtlı kullanıcı bulunmakta");
        }

        User user = User.CreateUser(userName, email, hashedPassword);
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.UserName == userName, cancellationToken);
    }
}
