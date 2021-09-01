using Photography.Api.Core;
using System;
using System.Security.Cryptography;

namespace Photography.Api.Models
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }

        public User(string username, string password, IPasswordHasher passwordHasher)
        {
            Salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }

            Username = username;
            Password = passwordHasher.HashPassword(Salt, password);
        }

        private User()
        {

        }
    }
}
