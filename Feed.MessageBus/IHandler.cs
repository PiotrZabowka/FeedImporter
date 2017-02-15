using ConsoleApp3;
using RabbitMQ.Client;

namespace Feed.MessageBus
{
    public interface IHandler
    {
        void Initialize(IModel model);
        string RouteKey { get; }
    }
    public interface IHandler<TMessage>:IHandler where TMessage : IMessage
    {
        void Handle(IModel model, TMessage message);
    }
}
