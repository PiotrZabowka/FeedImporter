using Feed.MessageBus;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp3
{
    public abstract class  BusService : IService
    {
        protected Bus Bus;
        public BusService(IConfiguration configuration)
        {
            this.Bus = new Bus(configuration["bus:hostname"], configuration["bus:virtualHost"], configuration["bus:user"], configuration["bus:password"]);
        }
        public abstract void Initialize();

        public void Dispose()
        {
            this.Bus.Dispose();
        }
    }
}