using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ModuleDistributor.Serilog
{
    public class SerilogModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
            => context.Host.UseSerilog();
    }
}