using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ModuleDistributor.Logging;
using Sudoku.Algorithm.Abstracts;
using SudokuFactory.Shared;
using SudokuRecord.Domain.Shared;
using System.Text.Json;

namespace SudokuFactory.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SudokuFactoryController : ControllerBase
    {
        private readonly IMatFactory _factory;
        private readonly DaprClient _dapr;

        public SudokuFactoryController(IMatFactory factory, DaprClient dapr)
        {
            _factory = factory;
            _dapr = dapr;
        }

        [ExLogging]
        [HttpPost]
        public async Task<IActionResult> GetSudokuAsync(MatInit matInit, [FromServices]ILogger<SudokuFactoryController> logger)
        {
            var tuple = await _factory.GenerateMatAsync(matInit.InitCount);
            Guid id = Guid.NewGuid();
            await _dapr.SaveStateAsync("statestore", id.ToString(), new MatState
            {
                Mat = tuple.Mat,
                Condition = tuple.Condition,
                StandardFinal = tuple.StandardFinal
            });

            return Ok(new MatResult
            {
                Id = id.ToString(),
                Mat = tuple.Mat
            });
        }

        [ExLogging]
        [HttpPost]
        public async Task<IActionResult> GetStandardFinalAsync(MatGuid guid, [FromServices] ILogger<SudokuFactoryController> logger)
        {
            var state = await _dapr.GetStateAsync<MatState>("statestore", guid.Guid);
            if (state is null)
                throw new ArgumentNullException("Cannot find mat id.");
            return Ok(new MatResult
            {
                Mat = state.StandardFinal,
                Id = guid.Guid
            });
        }

        [ExLogging]
        [HttpPost]
        public async Task<IActionResult> GetConditionAsync(MatGuid guid, [FromServices] ILogger<SudokuFactoryController> logger)
        {
            var state = await _dapr.GetStateAsync<MatState>("statestore", guid.Guid);
            if (state is null)
                throw new ArgumentNullException("Cannot find mat id.");
            return Ok(new MatResult
            {
                Mat = state.Condition,
                Id = guid.Guid
            });
        }
    }
}