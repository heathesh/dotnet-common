using dotnet_common.Model;
using System;
using dotnet_common.Interface;
using dotnet_common.Test.Interface;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace dotnet_common.Test
{
    /// <summary>
    /// Cache manager tests
    /// </summary>
    public class CacheManagerTest
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager = new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()));
        
        /// <summary>
        /// Tests that ReturnFromCache with no parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);
            
            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "ReturnFromCache_Executes_Positive");

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache = _cacheManager.ReturnFromCache(Function, 10, "ReturnFromCache_Executes_Positive");

            Assert.Equal(cachedItem, returnedFromCache);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that ReturnFromCache with string parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10,
                "ReturnFromCache_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                new CacheKeyParameter("param2", "value2"));

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache = _cacheManager.ReturnFromCache(Function, 10,
                "ReturnFromCache_WithStringParameters_Executes_Positive", new CacheKeyParameter("param1", "value1"),
                new CacheKeyParameter("param2", "value2"));

            Assert.Equal(cachedItem, returnedFromCache);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that ReturnFromCache with string parameters executes positive.
        /// </summary>
        [Fact]
        public void ReturnFromCache_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10,
                "ReturnFromCache_WithIntParameters_Executes_Positive", new CacheKeyParameter("param1", 100),
                new CacheKeyParameter("param2", 999));

            Assert.Equal(itemToCache, cachedItem);

            var returnedFromCache =
                _cacheManager.ReturnFromCache(Function, 10, "ReturnFromCache_WithIntParameters_Executes_Positive");

            Assert.Equal(cachedItem, returnedFromCache);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that ReturnFromCache for null object executes positive
        /// </summary>
        [Fact]
        public void ReturnFromCache_NullObject_Executes_Positive()
        {
            object objectToCache = null;
            object Function() => objectToCache;

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "ReturnFromCache_NullObject_Executes_Positive");

            Assert.Null(cachedItem);
        }

        /// <summary>
        /// Tests that Exists with no parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Exists_NoParameters_Executes_Positive");

            Assert.NotNull(cachedItem);

            var result = _cacheManager.Exists("Exists_NoParameters_Executes_Positive");

            Assert.True(result);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Exists with string parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Exists_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.NotNull(cachedItem);

            var result = _cacheManager.Exists("Exists_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.True(result);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Exists with int parameters executes positive
        /// </summary>
        [Fact]
        public void Exists_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Exists_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.NotNull(cachedItem);

            var result = _cacheManager.Exists("Exists_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.True(result);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Exists for an invalid cache key returns false
        /// </summary>
        [Fact]
        public void Exists_InvalidCacheKey_Returns_False()
        {
            var exists = _cacheManager.Exists("Exists_InvalidCacheKey_Returns_False");

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Exists for an invalid cache key with parameters returns false
        /// </summary>
        [Fact]
        public void Exists_InvalidCacheKeyWithParameters_Returns_False()
        {
            var exists = _cacheManager.Exists("Exists_InvalidCacheKeyWithParameters_Returns_False",
                new CacheKeyParameter("param1", "value1"));

            Assert.False(exists);
        }

        /// <summary>
        /// Tests that Remove with no parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_NoParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Remove_NoParameters_Executes_Positive");

            Assert.NotNull(cachedItem);

            var exists = _cacheManager.Exists("Remove_NoParameters_Executes_Positive");

            Assert.True(exists);

            _cacheManager.Remove("Remove_NoParameters_Executes_Positive");

            exists = _cacheManager.Exists("Remove_NoParameters_Executes_Positive");

            Assert.False(exists);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Remove with string parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_WithStringParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.NotNull(cachedItem);

            var exists = _cacheManager.Exists("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.True(exists);

            _cacheManager.Remove("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            exists = _cacheManager.Exists("Remove_WithStringParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"), new CacheKeyParameter("param2", "value2"));

            Assert.False(exists);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Remove with int parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_WithIntParameters_Executes_Positive()
        {
            var itemToCache = "Testing Caching";

            var cacheManagerTest = new Mock<ICacheManagerTest>();
            cacheManagerTest.Setup(_ => _.ReturnString(itemToCache)).Returns(itemToCache);

            string Function() => cacheManagerTest.Object.ReturnString(itemToCache);

            var cachedItem = _cacheManager.ReturnFromCache(Function, 10, "Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.NotNull(cachedItem);

            var exists = _cacheManager.Exists("Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.True(exists);

            _cacheManager.Remove("Remove_WithIntParameters_Executes_Positive", new CacheKeyParameter("param1", 100),
                new CacheKeyParameter("param2", 999));

            exists = _cacheManager.Exists("Remove_WithIntParameters_Executes_Positive",
                new CacheKeyParameter("param1", 100), new CacheKeyParameter("param2", 999));

            Assert.False(exists);
            cacheManagerTest.Verify(_ => _.ReturnString(itemToCache), Times.Once);
        }

        /// <summary>
        /// Tests that Remove for an invalid cache key executes positive
        /// </summary>
        [Fact]
        public void Remove_InvalidCacheKey_Executes_Positive()
        {
            _cacheManager.Remove("Remove_InvalidCacheKey_Executes_Positive");

            Assert.True(true);
        }

        /// <summary>
        /// Tests that Remove for an invalid cache key with parameters executes positive
        /// </summary>
        [Fact]
        public void Remove_InvalidCacheKeyWithParameters_Executes_Positive()
        {
            _cacheManager.Remove("Remove_InvalidCacheKeyWithParameters_Executes_Positive",
                new CacheKeyParameter("param1", "value1"));

            Assert.True(true);
        }
    }
}
