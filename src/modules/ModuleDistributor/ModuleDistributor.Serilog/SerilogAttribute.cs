using Rougamo;
using Rougamo.Context;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ModuleDistributor.Serilog
{
    public class SerilogAttribute : ExMoAttribute
    {
        protected override void ExOnEntry(MethodContext context)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

            Log.Information("Starting Host.");
        }

        protected override void ExOnException(MethodContext context)
        {
            if (context.Exception is null || context.Exception is HostAbortedException)
                return;

            Log.Error(context.Exception, "Host terminated unexpectedly!");
            context.HandledException(this, 1);
        }

        protected override void ExOnExit(MethodContext context)
            => Log.CloseAndFlush();
    }
}
