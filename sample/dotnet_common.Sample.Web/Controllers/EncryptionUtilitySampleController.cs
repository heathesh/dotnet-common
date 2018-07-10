using dotnet_common.Model;
using dotnet_common.Sample.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_common.Sample.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/EncryptionUtilitySample")]
    public class EncryptionUtilitySampleController : Controller
    {
        /// <summary>
        /// The encryption utility
        /// </summary>
        private readonly dotnet_common.Interface.IEncryptionUtility _encryptionUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionUtilitySampleController"/> class.
        /// </summary>
        /// <param name="encryptionUtility">The encryption utility.</param>
        public EncryptionUtilitySampleController(dotnet_common.Interface.IEncryptionUtility encryptionUtility)
        {
            _encryptionUtility = encryptionUtility;
        }

        /// <summary>
        /// HTTP Get example showing simple encryption and decryption
        /// </summary>
        /// <returns></returns>
        [HttpGet("{passPhrase}/{text}")]
        public dynamic Get(string passPhrase, string text)
        {
            var encryptedString = _encryptionUtility.EncryptString(passPhrase, text);
            var decryptedString = _encryptionUtility.DecryptString(passPhrase, encryptedString);

            dynamic result = new
            {
                EncryptedString = encryptedString,
                DecryptedString = decryptedString
            };

            return result;
        }

        /// <summary>
        /// HTTP Get example showing creating a hashed password and salt
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpGet("{password}")]
        public HashedPassword Get(string password)
        {
            return _encryptionUtility.HashPassword(password);
        }

        /// <summary>
        /// HTTP Post example showing verifying a password using it's hashed password and salt
        /// </summary>
        /// <param name="hashedPasswordModel">The hashed password model.</param>
        /// <returns></returns>
        [HttpPost]
        public bool Post([FromBody] HashedPasswordModel hashedPasswordModel)
        {
            return _encryptionUtility.VerifyHashedPassword(hashedPasswordModel.Password,
                hashedPasswordModel.HashedPassword, hashedPasswordModel.Salt);
        }
    }
}