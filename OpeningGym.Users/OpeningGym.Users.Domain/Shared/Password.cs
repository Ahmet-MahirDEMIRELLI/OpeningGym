namespace OpeningGym.Users.Domain.Shared;
public sealed record Password
{
    public string Value { get; init; }
    public Password(string value, bool hash)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Şifre alanı boş olamaz");
        }

        if (value.Length < 6)
        {
            throw new ArgumentException("Şifre alanı altı karakterden küçük olamaz");
        }

        Value = hash ? PasswordHasher.Hash(value) : value;
    }
    public bool Verify(string plainPassword)
    {
        return PasswordHasher.Verify(plainPassword, Value);
    }
    public override string ToString() => Value;
}
