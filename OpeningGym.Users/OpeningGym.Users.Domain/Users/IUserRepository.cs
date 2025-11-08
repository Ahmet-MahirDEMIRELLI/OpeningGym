namespace OpeningGym.Users.Domain.Users;
public interface IUserRepository
{
    Task CreateAsync(string userName, string email, string password, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
