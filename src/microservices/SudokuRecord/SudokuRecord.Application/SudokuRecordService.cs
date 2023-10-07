using Dapr.Client;
using Microsoft.Extensions.Logging;
using ModuleDistributor.DependencyInjection;
using ModuleDistributor.Logging;
using ModuleDistributor.Repository.Abstractions;
using SudokuFactory.Shared;
using SudokuRecord.Application.Abstracts;
using SudokuRecord.Domain;
using SudokuRecord.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SudokuRecord.Application
{
    [Transient<ISudokuRecordService>]
    internal class SudokuRecordService : ISudokuRecordService, ILoggerProxy<SudokuRecordService>
    {
        private readonly DaprClient _client;
        private readonly IRepository<Mat, Guid> _repository;

        public ILogger<SudokuRecordService> Logger { get; }

        ILogger ILoggerProxy.Logger
            => Logger;

        public SudokuRecordService(DaprClient client, IRepository<Mat, Guid> repository, ILogger<SudokuRecordService> logger)
        {
            _client = client;
            _repository = repository;
            Logger = logger;
        }

        [ExLogging]
        public async Task<MatRecord[]> GetCompletedSudokuAsync()
            => (await _repository.SelectAsync(item => new MatRecord
            {
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Mat = Deserialize(item.Matrix),
                Final = Deserialize(item.SubmitFinal)
            })).ToArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[]? Deserialize(string str)
            => JsonSerializer.Deserialize<byte[]>(str);
    }
}
