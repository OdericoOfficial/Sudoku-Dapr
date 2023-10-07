using Microsoft.Extensions.DependencyInjection;
using ModuleDistributor;
using Sudoku.Algorithm.Abstracts;
namespace Sudoku.Algorithm
{
    public class SudokuAlgorithmPredictorModule : CustomModule
    {
        public override void ConfigureServices(ServiceContext context)
            => context.Services.AddSingleton<IMatPredictor, MatPredictor>();
    }
}
