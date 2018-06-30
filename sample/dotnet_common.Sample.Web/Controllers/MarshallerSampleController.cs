using System;
using dotnet_common.Sample.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_common.Sample.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/MarshallerSample")]
    public class MarshallerSampleController : Controller
    {
        /// <summary>
        /// The marshaller
        /// </summary>
        private readonly dotnet_common.Interface.IMarshaller _marshaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarshallerSampleController"/> class.
        /// </summary>
        /// <param name="marshaller">The marshaller.</param>
        public MarshallerSampleController(dotnet_common.Interface.IMarshaller marshaller)
        {
            _marshaller = marshaller;
        }

        /// <summary>
        /// HTTP Get example
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            var marshallerSample = new MarshallerSample
            {
                Age = 25,
                DateOfBirth = new DateTime(1990, 10, 10),
                Name = "John",
                Surname = "Doe"
            };

            return _marshaller.SerializeObject(marshallerSample);
        }
    }
}