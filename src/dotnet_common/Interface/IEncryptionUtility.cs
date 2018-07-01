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
    }
}
