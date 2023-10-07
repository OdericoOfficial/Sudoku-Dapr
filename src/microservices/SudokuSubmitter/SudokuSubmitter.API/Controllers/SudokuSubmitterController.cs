using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using ModuleDistributor.Logging;
using ModuleDistributor.Repository.Abstractions;
using SudokuFactory.Shared;
using SudokuRecord.Domain;
using SudokuSubmitter.Shared;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SudokuSubmitter.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SudokuSubmitterController : ControllerBase
    {
        private readonly DaprClient _client;
        private readonly IRepository<Mat, Guid> _repository;

        public SudokuSubmitterController(DaprClient client, IRepository<Mat, Guid> repository)
        {
            _client = client;
            _repository = repository;
        }

        [ExLogging]
        [HttpPost]
        public async Task<IActionResult> SubmitSudokuAsync(MatSubmit submit, [FromServices] ILogger<SudokuSubmitterController> logger)
        {
            for (int i = 0; i < 324; i++)
            {
                if ((submit.Condition[i] ^ submit.Input[i]) == 0)
                    return Ok(new MatRet { Result = false });
            }

            await _repository.AddAsync(new Mat
            {
                Id = Guid.NewGuid(),
                Matrix = JsonSerializer.Serialize(submit.Matrix),
                StandardFinal = JsonSerializer.Serialize(submit.StandardFinal),
                SubmitFinal = JsonSerializer.Serialize(submit.Mat),
                StartTime = submit.StartTime,
                EndTime = submit.EndTime
            });
            await _repository.EnsureChangesAsync();

            return Ok(new MatRet { Result = true });
        }
    }
}