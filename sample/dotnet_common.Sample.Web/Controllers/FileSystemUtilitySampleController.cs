using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_common.Sample.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/FileSystemUtilitySample")]
    public class FileSystemUtilitySampleController : Controller
    {
        /// <summary>
        /// The file system utility
        /// </summary>
        private readonly dotnet_common.Interface.IFileSystemUtility _fileSystemUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemUtilitySampleController"/> class.
        /// </summary>
        /// <param name="fileSystemUtility">The file system utility.</param>
        public FileSystemUtilitySampleController(dotnet_common.Interface.IFileSystemUtility fileSystemUtility)
        {
            _fileSystemUtility = fileSystemUtility;
        }

        /// <summary>
        /// HTTP Get example
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _fileSystemUtility.GetFilesInDirectory(".");
        }
    }
}