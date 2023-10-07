namespace SudokuSubmitter.Shared
{
#nullable disable
    public sealed class MatSubmit
    {
        public string Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public byte[] Mat { get; set; }
        public byte[] Input { get; set; }
        public byte[] Condition { get; set; }
        public byte[] Matrix { get; set; }
        public byte[] StandardFinal { get; set; }
    }
}