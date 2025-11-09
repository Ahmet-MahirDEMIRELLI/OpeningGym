namespace OpeningGym.Users.Domain.Admins;
public interface IAdminRepository
{
    Task<Admin> CreateAsync(string fullName, string phoneNumber, string password, CancellationToken cancellationToken = default);
    Task ChangePasswordAsync(Guid id, string newPassword, CancellationToken cancellationToken = default);
}
