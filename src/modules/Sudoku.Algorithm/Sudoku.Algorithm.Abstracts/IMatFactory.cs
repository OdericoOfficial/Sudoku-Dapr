using System.Runtime.CompilerServices;

namespace Sudoku.Algorithm.Abstracts
{
    public interface IMatFactory
    {
        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount">数独上初始的数字个数</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<(byte[] Mat, byte[] StandardFinal, byte[] Condition)> GenerateMatAsync(int initCount);
        /// <summary>
        /// 初始化数独，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="initCount">数独上初始的数字个数</param>
        (byte[] Mat, byte[] StandardFinal, byte[] Condition) GenerateMat(int initCount);
    }
}
