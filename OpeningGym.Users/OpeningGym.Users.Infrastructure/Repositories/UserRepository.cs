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

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.UserName == userName, cancellationToken);
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

    public async Task ChangeUserNameAsync(Guid id, string newUserName, CancellationToken cancellationToken = default)
    {
        if (await ExistsByUserNameAsync(newUserName, cancellationToken))
        {
            throw new InvalidOperationException("Bu kullanıcı adı alınmış durumda");
        }

        var user = await GetById(id, cancellationToken) ?? throw new KeyNotFoundException("Id bilgisi hatalı");
        user.ChangeUserName(newUserName);
        _context.Users.Update(user);
    }

    public async Task ChangePasswordAsync(Guid id, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await GetById(id, cancellationToken) ?? throw new KeyNotFoundException("Id bilgisi hatalı");
        user.ChangePassword(newPassword);
        _context.Users.Update(user);
    }
    public async Task UpdateRatingAsync(Guid id, string ratingType, int newRating, int newK, CancellationToken cancellationToken = default)
    {
        var user = await GetById(id, cancellationToken) ?? throw new KeyNotFoundException("Id bilgisi hatalı");
        switch (ratingType)
        {
            case "bullet":
                user.UpdateBulletRating(newRating, newK);
                break;
            case "blitz":
                user.UpdateBlitzRating(newRating, newK);
                break;
            case "rapid":
                user.UpdateRapidRating(newRating, newK);
                break;
            case "classical":
                user.UpdateClassicalRating(newRating, newK);
                break;
            default:
                throw new InvalidOperationException("Geçersiz rating tipi");
        }

        _context.Users.Update(user);
    }

    public async Task<User?> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetById(id, cancellationToken) ?? throw new KeyNotFoundException("Id bilgisi hatalı");
        _context.Users.Remove(user);
        return user;
    }
}
