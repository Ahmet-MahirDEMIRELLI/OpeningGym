using System.Security.Cryptography;

namespace OpeningGym.Users.Application.Features.Auth.Services;
internal sealed class VerificationCodeService
{
    public VerificationCodeService() { }

    public string GenerateEmailVerificationCode(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var codeChars = new char[length];
        using var rng = RandomNumberGenerator.Create();
        byte[] data = new byte[length];

        rng.GetBytes(data);

        for (int i = 0; i < length; i++)
        {
            codeChars[i] = chars[data[i] % chars.Length];
        }

        return new string(codeChars);
    }
}
