using NumberPlace.Application.Abstracts;
using NumberPlace.Application;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Algorithm.Test
{
    public class TestMatAlgorithm 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConfigLog()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();
        }

        [Fact]
        public void TestMat()
        {
            IMatFactory factory = new MatFactory();
            var mat = factory.GenerateMat(17);
        }

        [Fact]
        public void TestMatWithLog()
        {
            ConfigLog();
            IMatFactory factory = new MatFactory();
            var mat = factory.GenerateMat(17);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    builder.Append($"{mat.Matrix[i * 9 + j]} ");
                }
                builder.AppendLine();
            }

            Log.Information(builder.ToString());
        }

        [Fact]
        public async Task TestNineMatWithLogAsync()
        {
            ConfigLog();
            IMatFactory factory = new MatFactory();
            var tasks = new Task<Mat>[9];
            for (int i = 0; i < 9; i++)
                tasks[i] = factory.GenerateMatAsync(17);

            foreach (var task in await Task.WhenAll(tasks))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        builder.Append($"{task.Matrix[i * 9 + j]} ");
                    }
                    builder.AppendLine();
                }

                Log.Information(builder.ToString());
            }
        }

        [Fact]
        public async Task TestNineMatAsync()
        {
            IMatFactory factory = new MatFactory();
            var tasks = new Task<Mat>[9];
            for (int i = 0; i < 9; i++)
                tasks[i] = factory.GenerateMatAsync(17);

            await Task.WhenAll(tasks);
        }
    }
}
