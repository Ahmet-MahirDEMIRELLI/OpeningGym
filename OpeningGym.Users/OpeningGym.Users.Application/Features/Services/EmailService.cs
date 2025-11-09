using Microsoft.Extensions.Options;
using OpeningGym.Users.Domain.Shared;
using System.Net;
using System.Net.Mail;

namespace OpeningGym.Users.Application.Features.Services;
internal sealed class EmailService : IEmailService
{
    private readonly string _email;
    private readonly string _password;
    private readonly string _smtpHost = "smtp.gmail.com";
    private readonly int _smtpPort = 587;

    public EmailService(IOptions<EmailSettings> options)
    {
        _email = options.Value.Email;
        _password = options.Value.Password;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_email, _password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage(_email, to, subject, body)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(mailMessage);
    }
}

public class EmailSettings
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
