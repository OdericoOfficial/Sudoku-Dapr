using ModuleDistributor;
using ModuleDistributor.DependencyInjection;
using SudokuRecord.EntityFrameworkCore;

namespace SudokuRecord.Application
{
    [DependsOn(typeof(InjectServiceModule<SudokuRecordApplicationModule>),
        typeof(SudokuRecordEntityFrameworkCoreModule))]
    public class SudokuRecordApplicationModule : CustomModule
    {
    }
}