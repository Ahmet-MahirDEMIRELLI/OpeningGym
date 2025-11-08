using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.Admins;
public sealed class Admin : Entity
{

#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private Admin() { }
#pragma warning restore CS8618
    private Admin(Guid id, FullName fullName, Email email, Password password) : base(id)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public FullName FullName { get; private set; }
    public Email Email { get; init; }
    public Password Password { get; private set; }

    public static Admin CreateAdmin(string fullName, string email, string password)
    {
        return new Admin(
            id: Guid.NewGuid(),
            fullName: new(fullName),
            email: new(email),
            password: new(password, true)
        );
    }
    public void ChangePassword(string password) => Password = new(password, true);
}
