using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ModuleDistributor.Swagger
{
    public class SwaggerModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
        {
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddSwaggerGen();
        }

        public override void OnApplicationInitialization(ApplicationContext context)
        {
            if (context.Environment.IsDevelopment())
            {
                context.App.UseSwagger();
                context.App.UseSwaggerUI();
            }
        }
    }
}