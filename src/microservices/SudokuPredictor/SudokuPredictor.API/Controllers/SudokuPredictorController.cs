using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ModuleDistributor.Logging;
using Sudoku.Algorithm.Abstracts;
using SudokuFactory.Shared;
using SudokuPredictor.Shared;
using System;

namespace SudokuPredictor.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SudokuPredictorController : ControllerBase
    {
        private readonly IMatPredictor _predictor;
        private readonly DaprClient _dapr;

        public SudokuPredictorController(IMatPredictor predictor, DaprClient dapr)
        {
            _predictor = predictor;
            _dapr = dapr;
        }

        [ExLogging]
        [HttpPost]
        public async Task<IActionResult> PredictSudokuAsync(MatPredict submit, [FromServices] ILogger<SudokuPredictorController> logger)
        {
            var state = await _dapr.GetStateAsync<MatState>("statestore", submit.Id);
            if (state is null)
                return BadRequest("Cannot find mat id.");

            var ret = await _predictor.PredictAsync(submit.Mat, state.Condition, submit.Input, submit.X, submit.Y);
            
            if (ret == -1)
                return BadRequest("Mat have no solution.");

            return Ok(ret);
        }
    }
}