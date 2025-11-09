using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.PendingUsers.Events.PendingUserCreatedDomainEvent;
internal class PendingUserCreatedDomainEventHandler : INotificationHandler<PendingUserCreatedDomainEvent>
{
    private readonly IEmailService _emailService;

    public PendingUserCreatedDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(PendingUserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var to = notification.Email.Value;
        var subject = "Hoşgeldiniz!";
        var verificationLink = $"http://localhost:4200/activate?userName={notification.UserName.Value}&verificationCode={notification.VerificationCode}";
        var body = $@"
            <h3>Merhaba {notification.UserName.Value},</h3>
            <p>Hesabınız başarıyla oluşturuldu. Hesabınızı aktifleştirmek için lütfen aşağıdaki linke tıklayın:</p>
            <p><a href='{verificationLink}'>Hesabımı Aktifleştir</a></p>
            <p>Link 24 saat boyunca geçerlidir.</p>
            <p>Link çalışmazsa tarayıcınıza aşağıdaki adresi kopyalayabilirsiniz:</p>
            <p>{verificationLink}</p>
        ";

        await _emailService.SendEmailAsync(to, subject, body);
    }
}

