using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.PendingUsers.Events.PendingUserVerifiedDomainEvent;
internal class PendingUserVerifiedDomainEventHandler : INotificationHandler<PendingUserVerifiedDomainEvent>
{
    private readonly IEmailService _emailService;

    public PendingUserVerifiedDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(PendingUserVerifiedDomainEvent notification, CancellationToken cancellationToken)
    {
        var to = notification.Email.Value;
        var subject = "Hesabınız doğrulandı!";

        var body = $@"
            <h3>Merhaba {notification.UserName.Value},</h3>
            <p>Hesabınız başarıyla doğrulandı ve artık sisteme giriş yapabilirsiniz.</p>
            <p>Hesabınızı kullanmaya başlamak için aşağıdaki linkten giriş yapabilirsiniz:</p>
            <p><a href='http://localhost:4200/login'>Giriş Yap</a></p>
        ";

        await _emailService.SendEmailAsync(to, subject, body);
    }
}
