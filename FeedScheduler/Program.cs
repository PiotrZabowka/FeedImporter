using ConsoleApp3;
using Feed.MessageBus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace FeedScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bus = new Bus("localhost", "/", "guest", "guest"))
            {
                bus.Publishes<FeedUrlMessage>();
                for (int i = 0; i < 1000; i++)
                {
                    var newMessage = new FeedUrlMessage
                    {
                        Type = "tsv",
                        Version = 1,
                        LocationId = Guid.NewGuid(),
                        Url = "http://developer.miinto.com/sample/tsv-sample.tsv"
                    };

                    bus.Publish(newMessage);
                }
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }
        }
    }
}
