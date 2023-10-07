namespace SudokuPredictor.Shared
{
#nullable disable
    public class MatPredict
    {
        public string Id { get; set; }
        public byte[] Mat { get; set; }
        public byte[] Input { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}