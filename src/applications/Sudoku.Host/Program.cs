using ModuleDistributor;
using ModuleDistributor.Serilog;

namespace Sudoku.Host
{
    public class Program
    {
        [Serilog]
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            await builder.ConfigureServiceAsync<SudokuHostModule>();
            var app = builder.Build();
            await app.OnApplicationInitializationAsync();
            await app.RunAsync();
        }
    }
}