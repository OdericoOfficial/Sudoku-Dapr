namespace SudokuRecord.Domain.Shared
{
#nullable disable
    public sealed class MatRecord
    {
        public byte[] Mat { get; set; }
        public byte[] Final { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}