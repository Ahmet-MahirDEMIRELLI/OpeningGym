using Microsoft.EntityFrameworkCore;
using OpeningGym.Users.Domain.PendingUsers;
using OpeningGym.Users.Domain.Shared;
using OpeningGym.Users.Domain.Users;
using OpeningGym.Users.Infrastructure.Context;

namespace OpeningGym.Users.Infrastructure.Repositories;
internal sealed class PendingUserRepository : IPendingUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUserRepository _userRepository;
    public PendingUserRepository(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<PendingUser> CreateAsync(string userName, string email, string password, string verificationCode, CancellationToken cancellationToken = default)
    {
        if (await ExistsByEmailAsync(new Email(email), cancellationToken))
        {
            throw new InvalidOperationException("Bu e-posta adresiyle kayıtlı kullanıcı bulunmakta");
        }

        if (await ExistsByUserNameAsync(new UserName(userName), cancellationToken))
        {
            throw new InvalidOperationException("Bu kullanıcı adıyla kayıtlı kullanıcı bulunmakta");
        }

        if (await _userRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            throw new InvalidOperationException("Bu e-posta adresiyle kayıtlı kullanıcı bulunmakta");
        }

        if (await _userRepository.ExistsByUserNameAsync(userName, cancellationToken))
        {
            throw new InvalidOperationException("Bu kullanıcı adıyla kayıtlı kullanıcı bulunmakta");
        }

        PendingUser pendingUser = PendingUser.CreatePendingUser(userName, email, password, verificationCode);
        await _context.PendingUsers.AddAsync(pendingUser, cancellationToken);
        return pendingUser;
    }

    public async Task<PendingUser?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _context.PendingUsers.Where(pu => pu.Email == email).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _context.PendingUsers.AnyAsync(pu => pu.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByUserNameAsync(UserName userName, CancellationToken cancellationToken = default)
    {
        return await _context.PendingUsers.AnyAsync(pu => pu.UserName == userName, cancellationToken);
    }

    public async Task<PendingUser> VerifyAsync(string email, string verificationCode, CancellationToken cancellationToken = default)
    {
        var existingPendingUser = await GetByEmailAsync(new Email(email), cancellationToken);
        if (existingPendingUser is null)
        {
            throw new InvalidOperationException("Bu e-posta adresiyle kayıtlı hesap bulunamadı");
        }

        if ((DateTime.UtcNow - existingPendingUser.CreatedTime) >= TimeSpan.FromHours(24))
        {
            _context.PendingUsers.Remove(existingPendingUser);
            await _context.SaveChangesAsync(cancellationToken);
            throw new InvalidOperationException("Hesap doğrulama süresi dolmuş. Lütfen tekrar kayıt olunuz");
        }

        await _userRepository.CreateAsync(existingPendingUser.UserName.Value, existingPendingUser.Email.Value, existingPendingUser.Password.Value, cancellationToken);
        _context.PendingUsers.Remove(existingPendingUser);
        return existingPendingUser;
    }
}
