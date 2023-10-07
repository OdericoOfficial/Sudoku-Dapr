using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuFactory.Shared
{
#nullable disable
    public sealed class MatState
    {
        public byte[] Mat { get; set; }
        public byte[] Condition { get; set; }
        public byte[] StandardFinal { get; set; }
    }
}
