using System.Collections.Generic;

namespace dotnet_common.Interface
{
    /// <summary>
    /// File system utility interface
    /// </summary>
    public interface IFileSystemUtility
    {
        /// <summary>
        /// Reads the file bytes and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        byte[] ReadFileBytes(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Reads the file text and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        string ReadFileText(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Gets the files in directory and can be set to throw an exception if the directory is not found.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        IEnumerable<string> GetFilesInDirectory(string directoryName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Reads the file lines as an enumerable list of string and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        IEnumerable<string> ReadFileLines(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Deletes the file and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        void DeleteFile(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Writes the file to the specified location and can be set to delete the file if it already 
        /// exists, otherwise it will throw an exception.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="deleteIfExists">if set to <c>true</c> [delete if exists].</param>
        void WriteFile(string folderName, string fileName, string fileContents, bool deleteIfExists);
    }
}
