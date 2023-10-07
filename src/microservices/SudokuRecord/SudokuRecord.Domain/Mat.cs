using ModuleDistributor.Repository.Abstractions;

namespace SudokuRecord.Domain
{
#nullable disable
    public sealed class Mat : Entity<Guid>
    {
        public string Matrix { get; set; }
        public string StandardFinal { get; set; }
        public string SubmitFinal { get; set; } 
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}