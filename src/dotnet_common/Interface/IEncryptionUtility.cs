namespace dotnet_common.Interface
{
    /// <summary>
    /// Encryption utility interface
    /// </summary>
    public interface IEncryptionUtility
    {
        /// <summary>
        /// Encrypts the specified text using the password specified.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string Encrypt(string password, string text);

        /// <summary>
        /// Decrypts the specified text using the password specified.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string Decrypt(string password, string text);
    }
}
