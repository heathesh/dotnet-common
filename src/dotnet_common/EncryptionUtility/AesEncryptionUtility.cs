using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using dotnet_common.Interface;
using dotnet_common.Model;

namespace dotnet_common.EncryptionUtility
{
    /// <summary>
    /// Aes implementation of the encryption utility
    /// </summary>
    /// <seealso cref="dotnet_common.Interface.IEncryptionUtility" />
    public class AesEncryptionUtility : IEncryptionUtility
    {
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string EncryptString(string passPhrase, string text)
        {
            var textBytes = Encoding.Unicode.GetBytes(text);
            string encryptedString = null;

            using (var encryptor = Aes.Create())
            {
                if (encryptor != null)
                {
                    var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase,
                        new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

                    encryptor.Key = rfc2898DeriveBytes.GetBytes(32);
                    encryptor.IV = rfc2898DeriveBytes.GetBytes(16);

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(textBytes, 0, textBytes.Length);
                            cryptoStream.Close();
                        }

                        encryptedString = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            return encryptedString;
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string DecryptString(string passPhrase, string text)
        {
            var cipherText = text.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipherText);
            string decryptedString = null;

            using (var encryptor = Aes.Create())
            {
                if (encryptor != null)
                {
                    var pdb = new Rfc2898DeriveBytes(passPhrase,
                        new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream =
                            new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                            cryptoStream.Close();
                        }

                        decryptedString = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
            }

            return decryptedString;
        }

        /// <summary>
        /// Hashes the password and returns an object with the hashed password and salt.
        /// Both the hashed password and salt must be stored in order to verify it at a later stage.
        /// Reference: https://www.ktlsolutions.com/how-tos/effective-password-hashing/
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public HashedPassword HashPassword(string password)
        {
            var hashedPassword = new HashedPassword();

            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            hashedPassword.Salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            hashedPassword.Password = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return hashedPassword;
        }

        /// <summary>
        /// Verifies the hashed password using the password supplied, and the previously stored hashed password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns></returns>
        public bool VerifyHashedPassword(string password, HashedPassword hashedPassword)
        {
            var saltBytes = Convert.FromBase64String(hashedPassword.Salt);
            var rfc = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            return Convert.ToBase64String(rfc.GetBytes(256)) == hashedPassword.Password;
        }

        /// <summary>
        /// Verifies the hashed password using the password supplied, and the previously stored hashed password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public bool VerifyHashedPassword(string password, string hashedPassword, string salt)
        {
            return VerifyHashedPassword(password, new HashedPassword {Salt = salt, Password = hashedPassword});
        }
    }
}
