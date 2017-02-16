using System;
using Feed.MessageBus;
using RabbitMQ.Client;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp3
{
    public class FeedDownloaderService : BusService
    {
        HttpClient httpClient;
        public FeedDownloaderService(IConfigurationRoot configuration) : base(configuration)
        {
            this.httpClient = new HttpClient();
        }

        public override void Initialize()
        {
            this.Bus.Publishes<DownloadedFeedMessage>();
            this.Bus.Handles<FeedUrlMessage>(this.Handle,"FeedDownloader", "*.v1");
        }
        private void Handle(IModel model, FeedUrlMessage message)
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