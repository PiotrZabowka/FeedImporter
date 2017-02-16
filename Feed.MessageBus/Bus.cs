using ConsoleApp3;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Feed.MessageBus
{
    public class Bus : IDisposable
    {
        private IConnection conn;
        private IModel model;

        public Bus(string hostname, string virtualHost, string user, string pass)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = user;
            factory.Password = pass;
            factory.VirtualHost = virtualHost;
            factory.HostName = hostname;

            this.conn = factory.CreateConnection();
            this.model = conn.CreateModel();
        }

        public void Publishes<T>() where T : IMessage
        {
            this.model.ExchangeDeclare(typeof(T).FullName, ExchangeType.Topic);
        }

        public void Publish<T>(T message) where T : IMessage
        {
            this.model.Publish<T>(message);
        }

        public void Handles<T>(Action<IModel, T> handler, string queueName, string routingKey) where T : IMessage
        {
            var exchange = typeof(T).FullName;

            this.model.QueueDeclare(queueName, true, false, false, null);
            this.model.ExchangeDeclare(exchange, ExchangeType.Direct);
            this.model.QueueBind(queueName, exchange, routingKey);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (m, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var feedUrlMessage = JsonConvert.DeserializeObject<T>(message);
                handler(this.model, feedUrlMessage);
            };
            model.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            model.Dispose();
            conn.Dispose();
        }
    }
}
