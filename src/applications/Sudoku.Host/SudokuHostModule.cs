using ModuleDistributor;
using ModuleDistributor.HealthCheck.Dapr;
using ModuleDistributor.Serilog;

namespace Sudoku.Host
{
    [DependsOn(typeof(DaprHealthCheckModule),
        typeof(SerilogModule))]
    internal class SudokuHostModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
        {
            context.Services.AddMasaBlazor();
            context.Services.AddRazorPages();
            context.Services.AddServerSideBlazor();
        }

        public override void OnApplicationInitialization(ApplicationContext context)
        {
            if (!context.Environment.IsDevelopment())
                context.App.UseExceptionHandler("/Error");
            context.App.UseStaticFiles();
            context.App.UseRouting();
            context.EndPoint.MapBlazorHub();
            context.EndPoint.MapFallbackToPage("/_Host");
        }
    }
}
