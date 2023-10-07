using Microsoft.AspNetCore.Mvc;
using SudokuRecord.Application.Abstracts;
using SudokuRecord.Domain.Shared;
using System.Runtime.CompilerServices;

namespace SudokuRecord.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SudokuRecordController : ControllerBase
    {
        private readonly ISudokuRecordService _service;

        public SudokuRecordController(ISudokuRecordService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GetCompletedSudokuAsync()
            => Ok(await _service.GetCompletedSudokuAsync());
    }
}