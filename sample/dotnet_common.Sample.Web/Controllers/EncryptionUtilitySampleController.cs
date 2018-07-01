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
        /// HTTP Get example
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
    }
}