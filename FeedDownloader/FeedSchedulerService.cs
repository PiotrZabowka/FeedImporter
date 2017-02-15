using System;
using Feed.MessageBus;
using RabbitMQ.Client;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp3
{
    public class FeedSchedulerService : BusService
    {
        public FeedSchedulerService(IConfigurationRoot configuration) : base(configuration)
        {
        }

        public override void Initialize()
        {
            this.Bus.Publishes<DownloadedFeedMessage>();
            for (int i = 0; i < 10000; i++)
            {
                var newMessage = new FeedUrlMessage
                {
                    Type = "tsv",
                    Version = 1,
                    LocationId = Guid.NewGuid(),
                    Url = "http://developer.miinto.com/sample/tsv-sample.tsv"
                };

                this.Bus.Publish(newMessage);
            }
        }
    }
}