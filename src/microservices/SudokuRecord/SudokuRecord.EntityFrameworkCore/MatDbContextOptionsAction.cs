using Dapr.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModuleDistributor.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRecord.EntityFrameworkCore
{
    internal class MatDbContextOptionsAction : OptionsActionWrapper
    {
        public override Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; } = AddDbContextOptions;

        private static void AddDbContextOptions(IServiceProvider provider, DbContextOptionsBuilder optionsBuilder)
        {
            using (var scope = provider.CreateScope())
            {
                var dapr = scope.ServiceProvider.GetRequiredService<DaprClient>();
                var connectionString = dapr.GetSecretAsync("secretstore", "ConnectionStrings:SqlServer").Result.First().Value;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
