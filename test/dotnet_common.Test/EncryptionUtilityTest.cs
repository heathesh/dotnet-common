using System;
using dotnet_common.EncryptionUtility;
using dotnet_common.Interface;
using Xunit;

namespace dotnet_common.Test
{
    /// <summary>
    /// Encryption utility tests
    /// </summary>
    public class EncryptionUtilityTest
    {
        /// <summary>
        /// The encryption utility
        /// </summary>
        private readonly IEncryptionUtility _encryptionUtility = new AesEncryptionUtility();

        /// <summary>
        /// Tests that EncrpytString encrypts a normal string
        /// </summary>
        [Fact]
        public void EncryptString_Encrypts_Normal_String()
        {
            const string text = "The quick brown fox";
            var passPhrase = Guid.NewGuid().ToString();

            var result = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        /// <summary>
        /// Tests that DecryptString decrypts a normal string
        /// </summary>
        [Fact]
        public void DecryptString_Encrypts_Normal_String()
        {
            const string text = "The quick brown fox";
            var passPhrase = Guid.NewGuid().ToString();

            var encryptedString = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(encryptedString));

            var decryptedString = _encryptionUtility.DecryptString(passPhrase, encryptedString);
            Assert.False(string.IsNullOrWhiteSpace(decryptedString));
            Assert.Equal(text, decryptedString);
        }

        /// <summary>
        /// Tests that EncrpytString encrypts an alphanumeric string
        /// </summary>
        [Fact]
        public void EncryptString_Encrypts_Alphanumeric_String()
        {
            const string text = "The quick brown fox had 2343.23432 reasons to jump the FENCE";
            var passPhrase = Guid.NewGuid().ToString();

            var result = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        /// <summary>
        /// Tests that DecryptString decrypts an alphanumeric string
        /// </summary>
        [Fact]
        public void DecryptString_Encrypts_Alphanumeric_String()
        {
            const string text = "The quick brown fox had 2343.23432 reasons to jump the FENCE";
            var passPhrase = Guid.NewGuid().ToString();

            var encryptedString = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(encryptedString));

            var decryptedString = _encryptionUtility.DecryptString(passPhrase, encryptedString);
            Assert.False(string.IsNullOrWhiteSpace(decryptedString));
            Assert.Equal(text, decryptedString);
        }

        /// <summary>
        /// Tests that EncrpytString encrypts a string with symbols
        /// </summary>
        [Fact]
        public void EncryptString_Encrypts_Symbols_String()
        {
            const string text = "!@#$ |.<The quick brown fox had 2343.23432 reasons to jump the FENCE `}|";
            var passPhrase = Guid.NewGuid().ToString();

            var result = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        /// <summary>
        /// Tests that DecryptString decrypts a string with symbols
        /// </summary>
        [Fact]
        public void DecryptString_Encrypts_Symbols_String()
        {
            const string text = "!@#$ |.<The quick brown fox had 2343.23432 reasons to jump the FENCE `}|";
            var passPhrase = Guid.NewGuid().ToString();

            var encryptedString = _encryptionUtility.EncryptString(passPhrase, text);
            Assert.False(string.IsNullOrWhiteSpace(encryptedString));

            var decryptedString = _encryptionUtility.DecryptString(passPhrase, encryptedString);
            Assert.False(string.IsNullOrWhiteSpace(decryptedString));
            Assert.Equal(text, decryptedString);
        }
    }
}
