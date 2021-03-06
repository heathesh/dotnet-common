﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_common.Sample.Web
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //cache manager class added as a singleton to keep one instance alive in your application
            services.AddSingleton<dotnet_common.Interface.ICacheManager, dotnet_common.CacheManager.MemoryCache>();

            //file system utility
            services.AddScoped<dotnet_common.Interface.IFileSystemUtility, dotnet_common.FileSystemUtility.SystemIO>();

            //marshaller that uses the data contract serializer
            services.AddScoped<dotnet_common.Interface.IMarshaller, dotnet_common.Marshaller.DataContractSerializer>();

            //encyption utility that uses AES
            services
                .AddScoped<dotnet_common.Interface.IEncryptionUtility,
                    dotnet_common.EncryptionUtility.AesEncryptionUtility>();

            services.AddMvc();
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
