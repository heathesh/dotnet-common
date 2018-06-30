using System;
using System.Threading.Tasks;
using dotnet_common.Model;

namespace dotnet_common.Interface
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Based on the cache key specified will either return the cached object, or run the function
        /// and then cache the result and return it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemToCache">The item to cache.</param>
        /// <param name="minutesToCache">The minutes to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T ReturnFromCache<T>(Func<T> itemToCache, int minutesToCache, string cacheKey,
            params CacheKeyParameter[] parameters);

        /// <summary>
        /// Based on the cache key specified will either return the cached object, or run the function
        /// and then cache the result and return it asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemToCache">The item to cache.</param>
        /// <param name="minutesToCache">The minutes to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<T> ReturnFromCacheAsync<T>(Func<Task<T>> itemToCache, int minutesToCache, string cacheKey,
            params CacheKeyParameter[] parameters);

        /// <summary>
        /// Checks whether or not there is an object in the cache with the specified cache key
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        bool Exists(string cacheKey, params CacheKeyParameter[] parameters);

        /// <summary>
        /// If there is an object in the cache with the specified cache key, the object is removed
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        void Remove(string cacheKey, params CacheKeyParameter[] parameters);
    }
}
