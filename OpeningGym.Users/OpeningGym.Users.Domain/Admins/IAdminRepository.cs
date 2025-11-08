namespace OpeningGym.Users.Domain.Admins;
public interface IAdminRepository
{
    Task<Admin> CreateAsync(string fullName, string phoneNumber, string password, CancellationToken cancellationToken = default);
}
