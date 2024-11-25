using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class HmacTokenHelper
    {
        private static readonly TimeSpan TokenExpirationTime = TimeSpan.FromMinutes(5);
        private static byte[] _key;

        public HmacTokenHelper(IConfiguration configuration)
        {
            // Read the secret key from appsettings.json
            var secretKey = configuration["HmacSettings:SecretKey"];
            _key = Encoding.UTF8.GetBytes(secretKey); // Convert the secret key to bytes
        }

        // Method to generate an HMAC token
        public string GenerateResetToken(string userId)
        {
            var expiration = DateTime.Now.Add(TokenExpirationTime);
            var tokenData = $"{userId}:{expiration:o}"; // Use ISO 8601 format for expiration
            using (var hmac = new HMACSHA256(_key))
            {
                var tokenBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenData));
                var token = Convert.ToBase64String(tokenBytes);
                return $"{token}:{expiration:o}"; // Include expiration in the token
            }
        }

        // Method to validate the token
        public (bool IsValid, bool IsExpired) ValidateResetToken(string token, string userId)
        {
            var parts = token.Split(new[] { ':' }, 2); // Split into two parts only
            if (parts.Length != 2)
                return (false, false); // Invalid token format

            var tokenHash = parts[0]; // The HMAC hash part
            var expirationString = parts[1]; // The expiration part

            // Check if expiration is valid
            if (!DateTime.TryParse(expirationString, out var expiration))
                return (false, false); // Invalid expiration format

            bool isExpired = DateTime.Now > expiration;

            // Recreate the HMAC to compare
            var tokenData = $"{userId}:{expirationString}"; // Use the user ID and expiration for HMAC
            using (var hmac = new HMACSHA256(_key))
            {
                var expectedTokenBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenData));
                var expectedToken = Convert.ToBase64String(expectedTokenBytes);
                bool isValid = expectedToken == tokenHash; // Compare hashes

                return (isValid, isExpired);
            }
        }


    }
}
