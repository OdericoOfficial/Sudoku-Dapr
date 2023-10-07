using ModuleDistributor;
using ModuleDistributor.Serilog;

namespace SudokuFactory.API
{
    public class Program
    {
        [Serilog]
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            await builder.ConfigureServiceAsync<SudokuFactoryModule>();
            var app = builder.Build();
            await app.OnApplicationInitializationAsync();
            await app.RunAsync();
        }
    }
}