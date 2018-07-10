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

        /// <summary>
        /// Tests that HashPassword returns a valid and populated hashed password entity
        /// </summary>
        [Fact]
        public void HashPassword_Returns_HashedPassword_Entity()
        {
            var password = Guid.NewGuid().ToString();
            var result = _encryptionUtility.HashPassword(password);

            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.Password));
            Assert.False(string.IsNullOrWhiteSpace(result.Salt));
        }

        /// <summary>
        /// Tests that HashPassword returns a valid and populated hashed password entity when an empty password is used
        /// </summary>
        [Fact]
        public void HashPassword_Returns_HashedPassword_Entity_Empty_Password()
        {
            var result = _encryptionUtility.HashPassword(string.Empty);

            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.Password));
            Assert.False(string.IsNullOrWhiteSpace(result.Salt));
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly matches a hashed password entity
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Matches_HashedPassword_Entity()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var result = _encryptionUtility.VerifyHashedPassword(password, hashedPassword);

            Assert.True(result);
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly matches a passed in hashed password and salt
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Matches_String_HashedPassword_Salt()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var result =
                _encryptionUtility.VerifyHashedPassword(password, hashedPassword.Password, hashedPassword.Salt);

            Assert.True(result);
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly matches a hashed password entity when an empty password is used
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Matches_HashedPassword_Entity_Empty_Password()
        {
            var password = string.Empty;
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var result = _encryptionUtility.VerifyHashedPassword(password, hashedPassword);

            Assert.True(result);
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly matches a passed in hashed password and salt when an empty password is used
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Matches_String_HashedPassword_Salt_Empty_Password()
        {
            var password = string.Empty;
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var result =
                _encryptionUtility.VerifyHashedPassword(password, hashedPassword.Password, hashedPassword.Salt);

            Assert.True(result);
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly does not match a hashed password entity
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Does_Not_Match_HashedPassword_Entity()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var incorrectPassword = password.Substring(0, password.Length - 2);
            var result = _encryptionUtility.VerifyHashedPassword(incorrectPassword, hashedPassword);

            Assert.False(result);
        }

        /// <summary>
        /// Tests that VerifyHashedPassword correctly does not match a passed in hashed password and salt
        /// </summary>
        [Fact]
        public void VerifyHashedPassword_Does_Not_Match_String_HashedPassword_Salt()
        {
            var password = Guid.NewGuid().ToString();
            var hashedPassword = _encryptionUtility.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Password));
            Assert.False(string.IsNullOrWhiteSpace(hashedPassword.Salt));

            var incorrectPassword = password.Substring(0, password.Length - 2);
            var result =
                _encryptionUtility.VerifyHashedPassword(incorrectPassword, hashedPassword.Password, hashedPassword.Salt);

            Assert.False(result);
        }
    }
}
