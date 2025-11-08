using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.PendingUsers.Events;
public sealed class PendingUserCreatedDomainEvent : INotification
{
    public UserName UserName { get; }
    public Email Email { get; }
    public string VerificationCode { get; }
    public PendingUserCreatedDomainEvent(UserName userName, Email email, string verificationCode)
    {
        UserName = userName;
        Email = email;
        VerificationCode = verificationCode;
    }
}
