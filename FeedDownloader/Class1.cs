using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FeedDownloader
{
    public class Class1 : AspNetBusService
    {
        public Class1(IConfigurationRoot configuration) : base(configuration)
        {
        }

        protected override IWebHost BuildHost(IWebHostBuilder builder)
        {
            return builder.UseKestrel()
                .UseStartup<Startup>()
                .Build();
        }
    }
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }

}
