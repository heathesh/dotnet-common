using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_common.Sample.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/CacheSample")]
    public class CacheSampleController : Controller
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly dotnet_common.Interface.ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheSampleController"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        public CacheSampleController(dotnet_common.Interface.ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// HTTP Get example
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //create a string array to return
            var itemToCache = new[] { "value1", "value2" };

            //create a local function to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            string[] Function() => itemToCache;

            //return the value from the cache if it's not in the cache already
            return _cacheManager.ReturnFromCache(Function, 10, "CacheSampleController_Get");
        }

        /// <summary>
        /// Asynchronous HTTP Get example
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(int id)
        {
            //create a string array to return
            var itemToCache = new[] { "value3", "value4" };

            //create a local function to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            async Task<string[]> Function()
            {
                return await Task.FromResult(itemToCache);
            }

            //return the value from the cache if it's not in the cache already
            return await _cacheManager.ReturnFromCacheAsync(Function, 10, "CacheSampleController_Get",
                new dotnet_common.Model.CacheKeyParameter(nameof(id), id));
        }
    }
}