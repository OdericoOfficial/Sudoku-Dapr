using ModuleDistributor;
using ModuleDistributor.HealthCheck.Dapr;
using ModuleDistributor.Serilog;
using ModuleDistributor.Swagger;
using Sudoku.Algorithm;

namespace SudokuFactory.API
{
    [DependsOn(typeof(SerilogModule),
        typeof(SwaggerModule),
        typeof(DaprHealthCheckModule),
        typeof(SudokuAlgorithmFactoryModule))]
    public class SudokuFactoryModule : CustomModule
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
