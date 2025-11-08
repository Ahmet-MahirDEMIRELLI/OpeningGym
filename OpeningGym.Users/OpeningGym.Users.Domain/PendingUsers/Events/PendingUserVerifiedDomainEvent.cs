using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.PendingUsers.Events;
public sealed class PendingUserVerifiedDomainEvent : INotification
{
    public UserName UserName { get; }
    public Email Email { get; }
    public PendingUserVerifiedDomainEvent(UserName userName, Email email)
    {
        UserName = userName;
        Email = email;
    }
}
