using Microsoft.Extensions.DependencyInjection;
using ModuleDistributor;
using Sudoku.Algorithm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithm
{
    public class SudokuAlgorithmFactoryModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
            => context.Services.AddSingleton<IMatFactory, MatFactory>();
    }
}
