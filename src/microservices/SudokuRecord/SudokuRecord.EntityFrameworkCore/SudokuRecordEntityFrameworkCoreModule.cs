using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModuleDistributor;
using ModuleDistributor.EntityFrameworkCore;
using ModuleDistributor.Repository;

namespace SudokuRecord.EntityFrameworkCore
{
    [DependsOn(typeof(EntityFrameworkCoreModule<MatDbContext, MatDbContextOptionsAction>),
        typeof(RepositoryModule))]
    public class SudokuRecordEntityFrameworkCoreModule : CustomModule
    {
        public override async ValueTask OnApplicationInitializationAsync(ApplicationContext context)
        {
            using (var scope = context.App.ApplicationServices.CreateScope())
                await scope.ServiceProvider.GetRequiredService<MatDbContext>().Database.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }
}