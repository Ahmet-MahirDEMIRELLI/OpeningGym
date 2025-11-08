using System.Text.RegularExpressions;

namespace OpeningGym.Users.Domain.Shared;
public sealed record Email
{
    public string Value { get; init; }
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("E-posta alanı boş olamaz");
        }

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(value, emailRegex))
        {
            throw new ArgumentException("Geçersiz e-posta formatı");
        }

        Value = value;
    }
}
