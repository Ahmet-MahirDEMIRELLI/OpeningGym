using MediatR;

namespace OpeningGym.Users.Domain.Users.Events;
public sealed class UserDeletedDomainEvent : INotification
{
    public string UserName { get; }
    public string Email { get; }
    public UserDeletedDomainEvent(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
}

