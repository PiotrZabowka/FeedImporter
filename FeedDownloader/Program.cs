using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using Feed.MessageBus;

namespace ConsoleApp3
{
    class Program
    {
        static HttpClient httpClient;
        static void Main(string[] args)
        {
            httpClient = new HttpClient();
            using (var bus = new Bus("localhost", "/", "guest", "guest"))
            {
                bus.Publishes<DownloadedFeedMessage>();
                bus.Handles<FeedUrlMessage>(Bus_Received, "tsv.v1");
                
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void Bus_Received(IModel model, FeedUrlMessage message)
        {
            Console.WriteLine($"downloading {message.Url}");
            var raw = httpClient.GetByteArrayAsync(message.Url).Result;
            var newMessage = new DownloadedFeedMessage
            {
                LocationId = message.LocationId,
                RawData = raw,
                Type = "tsv",
                Version = 1
            };
            model.Publish(newMessage);
        }
    }
}