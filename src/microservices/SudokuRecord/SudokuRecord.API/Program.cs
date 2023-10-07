using ModuleDistributor;
using ModuleDistributor.Serilog;

namespace SudokuRecord.API
{
    public class Program
    {
        [Serilog]
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            await builder.ConfigureServiceAsync<SudokuRecordModule>();
            var app = builder.Build();
            await app.OnApplicationInitializationAsync();
            await app.RunAsync();
        }
    }
}