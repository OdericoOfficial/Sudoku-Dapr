using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleDistributor.Repository.Abstractions;

namespace ModuleDistributor.Repository
{
    public class RepositoryModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
            => context.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
                .AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>))
                .AddScoped(typeof(IBasicRepository<,>), typeof(BasicRepository<,>));
    }
}