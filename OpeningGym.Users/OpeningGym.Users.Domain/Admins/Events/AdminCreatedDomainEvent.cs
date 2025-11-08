using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.Admins.Events;
public sealed class AdminCreatedDomainEvent : INotification
{
    public FullName FullName { get; }
    public Email Email { get; }
    public AdminCreatedDomainEvent(FullName fullName, Email email)
    {
        FullName = fullName;
        Email = email;
    }
}
