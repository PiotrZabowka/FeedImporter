using ConsoleApp3;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Feed.MessageBus
{
    public static class BusExtensions
    {
        public static void Publish<T>(this IModel model, T message) where T: IMessage
        {
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            model.BasicPublish(message.GetType().FullName, message.GetRoutingKey(), basicProperties: null, body: bytes);
        }
    }
}
