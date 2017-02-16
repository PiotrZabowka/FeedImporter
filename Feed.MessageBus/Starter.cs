using ConsoleApp3;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Feed.Microservice
{
    public class Starter : IDisposable
    {
        private IService service;
        public Starter(Assembly assembly, string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddCommandLine(args);

            var config = builder.Build();

            var serviceType = assembly.GetTypes().Single(t => t.Name == config["service"]);
            var constructor = serviceType.GetConstructor(new[] { typeof(IConfiguration) });
            service = constructor.Invoke(new[] { config }) as IService;
            service.Initialize();
        }

        public void Dispose()
        {
            service.Dispose();
        }
    }
}
