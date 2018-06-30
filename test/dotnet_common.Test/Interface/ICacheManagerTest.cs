using System.Threading.Tasks;

namespace dotnet_common.Test.Interface
{
    /// <summary>
    /// Cache manager test interface
    /// </summary>
    public interface ICacheManagerTest
    {
        /// <summary>
        /// Returns the string.
        /// </summary>
        /// <param name="stringToReturn">The string to return.</param>
        /// <returns></returns>
        string ReturnString(string stringToReturn);

        /// <summary>
        /// Returns the string asynchronously.
        /// </summary>
        /// <param name="stringToReturn">The string to return.</param>
        /// <returns></returns>
        Task<string> ReturnStringAsync(string stringToReturn);
    }
}
