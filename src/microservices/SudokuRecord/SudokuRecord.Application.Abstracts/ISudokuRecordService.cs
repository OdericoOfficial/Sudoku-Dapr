using SudokuFactory.Shared;
using SudokuRecord.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRecord.Application.Abstracts
{
    public interface ISudokuRecordService
    {
        Task<MatRecord[]> GetCompletedSudokuAsync();
    }
}
