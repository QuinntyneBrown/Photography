using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Photography.Api.Core
{
    public interface IPasswordHasher
    {
        string HashPassword(byte[] salt, string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(Byte[] salt, string password)
            => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
