namespace OpeningGym.Users.Domain.Users;
public interface IUserRepository
{
    Task CreateAsync(string userName, string email, string password, CancellationToken cancellationToken = default);
    Task ChangeUserNameAsync(Guid id, string newUserName, CancellationToken cancellationToken = default);
    Task ChangePasswordAsync(Guid id, string newPassword, CancellationToken cancellationToken = default);
    Task UpdateRatingAsync(Guid id, string ratingType, int newRating, int newK, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}
