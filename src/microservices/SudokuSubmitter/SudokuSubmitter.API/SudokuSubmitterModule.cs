using ModuleDistributor;
using ModuleDistributor.HealthCheck.Dapr;
using ModuleDistributor.Serilog;
using ModuleDistributor.Swagger;
using SudokuRecord.EntityFrameworkCore;

namespace SudokuSubmitter.API
{
    [DependsOn(typeof(SerilogModule),
        typeof(SwaggerModule),
        typeof(DaprHealthCheckModule),
        typeof(SudokuRecordEntityFrameworkCoreModule))]
    public class SudokuSubmitterModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
        {
            context.Services.AddControllers();
        }

        public override void OnApplicationInitialization(ApplicationContext context)
        {
            if (context.Environment.IsDevelopment())
            {
                context.App.UseSwagger();
                context.App.UseSwaggerUI();
            }

            context.App.UseAuthorization();
            context.EndPoint.MapControllers();
        }
    }
}
