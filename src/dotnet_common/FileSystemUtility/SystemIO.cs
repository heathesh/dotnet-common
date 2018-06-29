using System.Collections.Generic;
using System.IO;
using dotnet_common.Interface;

namespace dotnet_common.FileSystemUtility
{
    /// <summary>
    /// System.IO implementation of the file system utility
    /// </summary>
    /// <seealso cref="dotnet_common.Interface.IFileSystemUtility" />
    public class SystemIO : IFileSystemUtility
    {
        /// <summary>
        /// Reads the file bytes and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public byte[] ReadFileBytes(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                return File.ReadAllBytes(fileName);

            if (throwExceptionIfNotFound)
                throw new FileNotFoundException(fileName);

            return null;
        }

        /// <summary>
        /// Reads the file text and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public string ReadFileText(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                return File.ReadAllText(fileName);

            if (throwExceptionIfNotFound)
                throw new FileNotFoundException(fileName);

            return null;
        }

        /// <summary>
        /// Gets the files in directory and can be set to throw an exception if the directory is not found.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public IEnumerable<string> GetFilesInDirectory(string directoryName, bool throwExceptionIfNotFound = false)
        {
            if (Directory.Exists(directoryName))
                return Directory.GetFiles(directoryName);

            if (throwExceptionIfNotFound)
                throw new DirectoryNotFoundException(directoryName);

            return null;
        }

        /// <summary>
        /// Reads the file lines as an enumerable list of string and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IEnumerable<string> ReadFileLines(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                return File.ReadAllLines(fileName);

            if (throwExceptionIfNotFound)
                throw new FileNotFoundException(fileName);

            return null;
        }

        /// <summary>
        /// Deletes the file and can be set to throw an exception if the file is not found.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <exception cref="FileNotFoundException"></exception>
        public void DeleteFile(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            else
            {
                if (throwExceptionIfNotFound)
                    throw new FileNotFoundException(fileName);
            }
        }

        /// <summary>
        /// Writes the file to the specified location and can be set to delete the file if it already
        /// exists, otherwise it will throw an exception.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="deleteIfExists">if set to <c>true</c> [delete if exists].</param>
        public void WriteFile(string folderName, string fileName, string fileContents, bool deleteIfExists)
        {
            var combinedFileName = Path.Combine(folderName, fileName);

            if (deleteIfExists)
                DeleteFile(combinedFileName);

            File.WriteAllText(combinedFileName, fileContents);
        }
    }
}
