using System;

namespace ConsoleApp3
{
    public class FeedUrlMessage : IMessage
    {
        public Guid LocationId { get; set; }
        public string Url { get; set; }

        public string Type { get; set; }

        public int Version { get; set; }
    }
    public static class MessageExtensions
    {
        public static string GetRoutingKey(this IMessage message)
        {
            return $"{message.Type}.v{message.Version}";
        }
    }
}