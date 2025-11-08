using OpeningGym.Users.Domain.Abstractions;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.PendingUsers;
public sealed class PendingUser : Entity
{

#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private PendingUser() { }
#pragma warning restore CS8618
    private PendingUser(Guid id, UserName userName, Email email, Password password, string verificationCode, DateTime createdTime) : base(id)
    {
        UserName = userName;
        Email = email;
        Password = password;
        VerificationCode = verificationCode;
        CreatedTime = createdTime;
    }

    public UserName UserName { get; private set; }
    public Email Email { get; init; }
    public Password Password { get; private set; }
    public string VerificationCode { get; init; }
    public DateTime CreatedTime { get; init; }

    public static PendingUser CreatePendingUser(string userName, string email, string password, string verificationCode)
    {
        return new PendingUser(
            id: Guid.NewGuid(),
            userName: new(userName),
            email: new(email),
            password: new(password, true),
            verificationCode: verificationCode,
            createdTime: DateTime.UtcNow
        );
    }
}
