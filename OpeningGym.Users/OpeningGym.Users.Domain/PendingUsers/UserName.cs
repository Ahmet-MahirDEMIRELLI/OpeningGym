namespace OpeningGym.Users.Domain.PendingUsers;
public sealed record UserName
{
    public string Value { get; init; }
    public UserName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Kullanıcı adı alanı boş olamaz");
        }

        if (value.Length < 3)
        {
            throw new ArgumentException("Kullanıcı adı alanı üç karakterden küçük olamaz");
        }

        Value = value;
    }
}
