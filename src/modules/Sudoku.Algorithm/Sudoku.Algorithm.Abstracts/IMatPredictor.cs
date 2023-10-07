using System;
using System.Runtime.CompilerServices;

namespace Sudoku.Algorithm.Abstracts
{
    public interface IMatPredictor
    {
        /// <summary>
        /// 异步预测数独的下一步
        /// </summary>
        /// <param name="mat">数独</param>
        /// <param name="condition">现条件</param>
        /// <returns>k</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<int> PredictAsync(byte[] mat, byte[] condition, byte[] input, int i, int j, int failedCount = 1000);
        /// <summary>
        /// 预测数独的下一步，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="mat">数独</param>
        /// <param name="condition">现条件</param>
        /// <returns>k</returns>
        int Predict(byte[] mat, byte[] condition, byte[] input, int i, int j, int failedCount = 1000);
    }
}
