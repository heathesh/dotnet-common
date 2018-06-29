using System;
using System.Text;
using dotnet_common.Interface;
using dotnet_common.Model;
using Microsoft.Extensions.Caching.Memory;

namespace dotnet_common.CacheManager
{
    /// <summary>
    /// Memory cache implementation of the cache manager
    /// </summary>
    /// <seealso cref="dotnet_common.Interface.ICacheManager" />
    public class MemoryCache : ICacheManager
    {
        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCache"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public MemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

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
        public T ReturnFromCache<T>(Func<T> itemToCache, int minutesToCache, string cacheKey,
            params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            if (!Exists(generatedCacheKey))
            {
                var item = itemToCache();

                _memoryCache.Set(generatedCacheKey, item, TimeSpan.FromMinutes(minutesToCache));
            }

            return _memoryCache.Get<T>(generatedCacheKey);
        }

        /// <summary>
        /// Checks whether or not there is an object in the cache with the specified cache key
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public bool Exists(string cacheKey, params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            _memoryCache.TryGetValue(generatedCacheKey, out object cachedItem);

            return cachedItem != null;
        }

        /// <summary>
        /// If there is an object in the cache with the specified cache key, the object is removed
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        public void Remove(string cacheKey, params CacheKeyParameter[] parameters)
        {
            var generatedCacheKey = GenerateCacheKey(cacheKey, parameters);

            if (Exists(generatedCacheKey))
                _memoryCache.Remove(generatedCacheKey);
        }

        /// <summary>
        /// Generates the cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private string GenerateCacheKey(string cacheKey, params CacheKeyParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return cacheKey;

            var generatedKey = new StringBuilder(cacheKey);

            foreach (var parameter in parameters)
                generatedKey.Append($"__{parameter.Name}_{parameter.Value}");

            return generatedKey.ToString();
        }
    }
}
