namespace OpeningGym.Users.Domain.Shared;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}