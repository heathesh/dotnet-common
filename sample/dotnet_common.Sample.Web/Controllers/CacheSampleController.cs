using System;
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
        /// HTTP Get example using a local function
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //create a string array to return
            var itemToCache = new[] { "localfunction1", "localfunction2" };

            //create a local function to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            string[] Function() => itemToCache;

            //return the value from the cache if it's not in the cache already
            return _cacheManager.ReturnFromCache(Function, 10, "CacheSampleController_Get");
        }

        /// <summary>
        /// Asynchronous HTTP Get example using a local function
        /// </summary>
        /// <returns></returns>
        [HttpGet("asyncLocalFunction")]
        public async Task<IEnumerable<string>> GetAsync()
        {
            //create a string array to return
            var itemToCache = new[] { "asyncLocalFunction1", "asyncLocalFunction2" };

            //create a local function to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            async Task<string[]> Function()
            {
                return await Task.FromResult(itemToCache);
            }

            //dummy parameter to show using CacheKeyParameter
            //you pass in all the parameters that make this particular item being cached unique
            var asyncLocalFunction = "asyncLocalFunction";

            //return the value from the cache if it's not in the cache already
            return await _cacheManager.ReturnFromCacheAsync(Function, 10, "CacheSampleController_Get",
                new dotnet_common.Model.CacheKeyParameter(nameof(asyncLocalFunction), asyncLocalFunction));
        }

        /// <summary>
        /// HTTP Get example using a Func
        /// </summary>
        /// <returns></returns>
        [HttpGet("func")]
        public IEnumerable<string> GetFunc()
        {
            //create a Func<string[]> to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            Func<string[]> function = () => { return new[] { "func1", "func2" }; };

            //dummy parameter to show using CacheKeyParameter
            //you pass in all the parameters that make this particular item being cached unique
            var func = "func";

            //return the value from the cache if it's not in the cache already
            return _cacheManager.ReturnFromCache(function, 10, "CacheSampleController_Get",
                new dotnet_common.Model.CacheKeyParameter(nameof(func), func));
        }

        /// <summary>
        /// Asynchronous HTTP Get example using a Func
        /// </summary>
        /// <returns></returns>
        [HttpGet("funcasync")]
        public async Task<IEnumerable<string>> GetFuncAsync()
        {
            //create an async Func<Task<string[]>> to return the string array
            //we use a function so that the cache method only needs to execute the function i.e. do the work
            //if the object is not already in the cached memory
            Func<Task<string[]>> function = async () =>
            {
                var itemToCache = new[] { "funcasync1", "funcasync2" };
                return await Task.FromResult(itemToCache);
            };

            //multiple dummy parameters to show using multiple cache key parameters of different data types
            var funcasync = "funcasync";
            var id = 10;

            //return the value from the cache if it's not in the cache already
            return await _cacheManager.ReturnFromCacheAsync(function, 10, "CacheSampleController_Get",
                new dotnet_common.Model.CacheKeyParameter(nameof(funcasync), funcasync),
                new dotnet_common.Model.CacheKeyParameter(nameof(id), id));
        }
    }
}