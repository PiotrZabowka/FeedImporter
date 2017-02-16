using Feed.MessageBus;
using Microsoft.Extensions.Configuration;

namespace FeedDownloader
{
    public abstract class AspNetBusService : AspNetService
    {
        protected Bus Bus;
        public AspNetBusService(IConfiguration configuration) : base(configuration)
        {
            this.Bus = new Bus(configuration["bus:hostname"], configuration["bus:virtualHost"], configuration["bus:user"], configuration["bus:password"]);

        }
        public override void Dispose()
        {
            this.Bus.Dispose();
            base.Dispose();
        }
    }
}
