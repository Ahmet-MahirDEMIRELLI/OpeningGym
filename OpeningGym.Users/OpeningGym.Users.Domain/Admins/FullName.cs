namespace OpeningGym.Users.Domain.Admins;
public sealed record FullName
{
    public string Value { get; init; }
    public FullName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Ad-Soyad alanı boş olamaz");
        }

        if (value.Length < 3)
        {
            throw new ArgumentException("Ad-Soyad alanı üç karakterden küçük olamaz");
        }

        Value = value;
    }
}
