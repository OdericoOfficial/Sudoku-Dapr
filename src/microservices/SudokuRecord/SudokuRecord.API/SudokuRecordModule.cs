using ModuleDistributor;
using ModuleDistributor.HealthCheck.Dapr;
using ModuleDistributor.Serilog;
using ModuleDistributor.Swagger;
using SudokuRecord.Application;
using SudokuRecord.EntityFrameworkCore;

namespace SudokuRecord.API
{
    [DependsOn(typeof(SerilogModule),
        typeof(SwaggerModule),
        typeof(DaprHealthCheckModule),
        typeof(SudokuRecordApplicationModule))]
    public class SudokuRecordModule : CustomModule
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
