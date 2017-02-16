using ConsoleApp3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedDownloader
{
    public abstract class AspNetService : IService
    {
        protected IWebHostBuilder Builder;
        protected IWebHost Host;
        protected IConfiguration Configuration;
        public AspNetService(IConfiguration config)
        {
            this.Configuration = config;
            this.Builder = new WebHostBuilder()
                .UseConfiguration(config);
        }
        public virtual void Dispose()
        {
            this.Host.Dispose();
            this.Host = null;
            this.Builder = null;
        }

        public void Initialize()
        {
            this.Host = this.BuildHost(this.Builder);  
            this.Host.Run();
        }

        protected abstract IWebHost BuildHost(IWebHostBuilder builder);
    }
}
