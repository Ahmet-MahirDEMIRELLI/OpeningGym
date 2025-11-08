namespace OpeningGym.Users.Domain.PendingUsers;
public interface IPendingUserRepository
{
    Task<PendingUser> CreateAsync(string userName, string email, string password, string verificationCode, CancellationToken cancellationToken = default);
    Task<PendingUser> VerifyAsync(string email, string verificationCode, CancellationToken cancellationToken = default);
}
