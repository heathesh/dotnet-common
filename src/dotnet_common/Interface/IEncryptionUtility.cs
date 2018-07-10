using dotnet_common.Model;

namespace dotnet_common.Interface
{
    /// <summary>
    /// Encryption utility interface
    /// </summary>
    public interface IEncryptionUtility
    {
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string EncryptString(string passPhrase, string text);

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string DecryptString(string passPhrase, string text);

        /// <summary>
        /// Hashes the password and returns an object with the hashed password and salt.
        /// Both the hashed password and salt must be stored in order to verify it at a later stage.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        HashedPassword HashPassword(string password);

        /// <summary>
        /// Verifies the hashed password using the password supplied, and the previously stored hashed password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns></returns>
        bool VerifyHashedPassword(string password, HashedPassword hashedPassword);

        /// <summary>
        /// Verifies the hashed password using the password supplied, and the previously stored hashed password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        bool VerifyHashedPassword(string password, string hashedPassword, string salt);
    }
}
