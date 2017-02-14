using System;
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
        static void Main(string[] args)
        {
            using (var bus = new Bus("localhost", "/", "guest", "guest"))
            {
                bus.Publishes<ParsedFeedLineMessage>();
                bus.Handles<DownloadedFeedMessage>(Bus_Received, "tsv.v1");

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }

        private static void Bus_Received(IModel model, DownloadedFeedMessage message)
        {
            Console.WriteLine($"parsing {message.LocationId}");

            //parse message raw data
            var text = Encoding.UTF8.GetString(message.RawData);
            string[] lines = text.Split('\n');

            var headers = lines[0].Split('\t');

            for (int i = 1; i < lines.Length; i++)
            {
                var lineValues = lines[i].Split('\t');
                var line = new ParsedFeedLineMessage()
                {
                    LocationId = message.LocationId,
                    Type = "dict",
                    Version = 1
                };
                for (int j = 0; j < Math.Min(lineValues.Length, headers.Length); j++)
                {
                    line.Values[headers[j]] = lineValues[j];
                }
                model.Publish(line);
            }
        }
    }
}