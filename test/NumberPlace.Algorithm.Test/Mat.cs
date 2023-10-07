using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.Application.Abstracts
{
    /// <summary>
    /// 数独类
    /// </summary>
    public class Mat
    {
        public readonly byte[] Matrix;
        public readonly byte[] Condition;
        public readonly IDictionary<int, byte> Addition = new Dictionary<int, byte>();

        public Mat(byte[] matrix, byte[] condition)
        {
            Matrix = matrix;
            Condition = condition;
        }

        /// <summary>
        /// 获取当前数独的某格
        /// </summary>
        /// <param name="i">行</param>
        /// <param name="j">列</param>
        /// <returns>数独某格的数字</returns>
        public byte this[int i, int j]
        {
            get
            {
                var exist = i * 9 + j;
                if (Condition[exist] == 0)
                    return Addition[exist];

                return Matrix[exist];
            }
        }
    }
}
