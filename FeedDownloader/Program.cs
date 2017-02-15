using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using Feed.MessageBus;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static HttpClient httpClient;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddCommandLine(args);

            var config = builder.Build();

            var typeInfo = typeof(Program).GetTypeInfo();
            var serviceType = typeInfo.Assembly.GetTypes().Single(t=>t.Name == config["service"]);
            var constructor = serviceType.GetConstructor(new[] { typeof(IConfigurationRoot) });
            using (var service = constructor.Invoke(new[] { config }) as IService)
            {
                service.Initialize();
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}