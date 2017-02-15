using System;
using Feed.MessageBus;
using RabbitMQ.Client;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ConsoleApp3
{
    public class FeedParserService : BusService
    {
        public FeedParserService(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public override void Initialize()
        {
            this.Bus.Publishes<ParsedFeedLineMessage>();
            this.Bus.Handles<DownloadedFeedMessage>(this.Handle, "tsv.v1");
        }
        private void Handle(IModel model, DownloadedFeedMessage message)
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