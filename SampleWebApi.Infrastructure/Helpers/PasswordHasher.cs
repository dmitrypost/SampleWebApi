using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace SampleWebApi.Infrastructure.Helpers
{
    public interface IHashPasswords
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordHashing : IHashPasswords
    {
        private const int SaltSize = 16; 
        private const int Iterations = 10000; 
        private const int KeySize = 256; 

        public string HashPassword(string password)
        {
            var salt = GenerateSalt();

            var generator = new Pkcs5S2ParametersGenerator();
            generator.Init(Encoding.UTF8.GetBytes(password), salt, Iterations);

            // Generate the derived key (hashed password)
            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeySize);

            // Combine salt and hashed password (both base64 encoded) for storage
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(key.GetKey())}";
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            var random = new SecureRandom();
            random.NextBytes(salt);
            return salt;
        }

        public bool VerifyPassword(string password, string hash)
        {
            // Split the stored hash into salt and hash parts
            var parts = hash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);

            // Generate a hash from the input password using the stored salt
            var generator = new Pkcs5S2ParametersGenerator();
            generator.Init(Encoding.UTF8.GetBytes(password), salt, Iterations);
            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeySize);

            // Compare the generated hash with the stored hash
            return AreEqual(storedPasswordHash, key.GetKey());
        }

        private static bool AreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            // Compare each byte in the arrays
            return !a.Where((t, i) => t != b[i]).Any();
        }
    }
}
