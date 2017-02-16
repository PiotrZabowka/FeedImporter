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
using Feed.Microservice;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var typeInfo = typeof(Program).GetTypeInfo();
            using (new Starter(typeInfo.Assembly, args))
            {
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}