using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FeedDownloader
{
    public class SampleApi : AspNetBusService
    {
        public SampleApi(IConfigurationRoot configuration) : base(configuration)
        {
        }

        protected override IWebHost BuildHost(IWebHostBuilder builder)
        {
            return builder
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
        }
    }
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }

}
