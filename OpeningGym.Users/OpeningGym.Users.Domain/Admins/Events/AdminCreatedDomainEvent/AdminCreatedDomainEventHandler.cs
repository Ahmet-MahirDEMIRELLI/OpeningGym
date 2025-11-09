using MediatR;
using OpeningGym.Users.Domain.Shared;

namespace OpeningGym.Users.Domain.Admins.Events.AdminCreatedDomainEvent;
internal class AdminCreatedDomainEventHandler : INotificationHandler<AdminCreatedDomainEvent>
{
    private readonly IEmailService _emailService;

    public AdminCreatedDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(AdminCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var to = notification.Email.Value;
        var subject = "Hoşgeldiniz!";
        var body = $@"
                <h3>Merhaba {notification.FullName.Value},</h3>
                <p>Admin hesabınız başarıyla oluşturuldu.</p>
                <p>Artık sisteme yönetici olarak giriş yapabilirsiniz.</p>
            ";

        await _emailService.SendEmailAsync(to, subject, body);
    }
}
