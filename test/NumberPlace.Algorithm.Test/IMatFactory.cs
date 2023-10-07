using System.Runtime.CompilerServices;

namespace NumberPlace.Application.Abstracts
{
    public interface IMatFactory : INotifyMatGeneratedFailed
    {
        /// <summary>
        /// 异步初始化数独，带CancellationToken
        /// </summary>
        /// <param name="initCount">数独上初始的数字个数</param>
        /// <param name="token"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<Mat> GenerateMatAsync(int initCount, CancellationToken token);
        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Task<Mat> GenerateMatAsync(int initCount);
        /// <summary>
        /// 初始化数独，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="initCount"></param>
        Mat GenerateMat(int initCount);
    }
}
