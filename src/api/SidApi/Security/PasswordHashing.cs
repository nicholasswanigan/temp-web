using Isopoh.Cryptography.Argon2;
using System.Security.Cryptography;
using System.Text;

namespace SidApi.Security;

public static class PasswordHashing
{
    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 4, // iterations
            MemoryCost = 65536, // 64 MB
            Lanes = 2,
            Threads = 2,
            Password = Encoding.UTF8.GetBytes(password),
            Salt = salt,
            HashLength = 32
        };

        return Argon2.Hash(config);
    }

    public static bool Verify(string password, string encodedHash)
    {
        return Argon2.Verify(encodedHash, password);
    }
}