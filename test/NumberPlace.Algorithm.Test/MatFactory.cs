using NumberPlace.Application.Abstracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NumberPlace.Application
{
    internal class MatFactory : IMatFactory
    {
        public event MatGeneratedFailedEventHandler? OnMatGeneratedFailed;

        /// <summary>
        /// 异步初始化数独，带CancellationToken
        /// </summary>
        /// <param name="initCount">数独上初始的数字个数</param>
        /// <param name="token"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Mat> GenerateMatAsync(int initCount, CancellationToken token)
            => Task.Run(() => GenerateMat(initCount), token);

        /// <summary>
        /// 异步初始化数独
        /// </summary>
        /// <param name="initCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<Mat> GenerateMatAsync(int initCount)
            => Task.Run(() => GenerateMat(initCount));

        /// <summary>
        /// 初始化数独，使用值类型、指针的方式减轻gc压力，使用在栈上分配空间规避跨线程问题并加速数值读取
        /// </summary>
        /// <param name="initCount"></param>
        public unsafe Mat GenerateMat(int initCount)
        {
            try
            {
                Random random = new Random();
                byte* condition = stackalloc byte[324];
                byte* mat = stackalloc byte[81];
                byte* exist = stackalloc byte[81];
                byte* rest = stackalloc byte[9];

                // 生成终盘
                int cnt = 0;
                while (cnt < 81)
                {
                    int existLength = ResolveExist(condition, exist);
                    var blankItem = exist[random.Next(0, existLength)];
                    var i = blankItem / 10;
                    var j = blankItem % 10;

                    int restLength = ResolveRest(condition, rest, i, j);
                    if (restLength == 0)
                    {
                        RandomThrow(condition, mat, random, i, j);
                        cnt--;
                        continue;
                    }

                    var item = rest[random.Next(0, restLength)];

                    var ex = i * 9 + j;
                    var row = i * 9 + 80 + item;
                    var col = j * 9 + 161 + item;
                    var part = (i / 3 * 3 + j / 3) * 9 + 242 + item;

                    condition[ex] = 1;
                    condition[row] = 1;
                    condition[col] = 1;
                    condition[part] = 1;
                    mat[ex] = item;
                    cnt++;
                }

                // 挖空
                cnt = 0;
                while (cnt++ < initCount)
                {
                    var notExistLength = ResolveNotExist(condition, exist);
                    var existItem = exist[random.Next(0, notExistLength)];
                    var i = existItem / 10;
                    var j = existItem % 10;

                    var ex = i * 9 + j;
                    var item = mat[ex];
                    var row = i * 9 + 80 + item;
                    var col = j * 9 + 161 + item;
                    var part = (i / 3 * 3 + j / 3) * 9 + 242 + item;

                    condition[ex] = 0;
                    condition[row] = 0;
                    condition[col] = 0;
                    condition[part] = 0;
                    mat[ex] = 0;
                }

                byte[] outMat = new byte[81];
                byte[] outCondition = new byte[324];

                Marshal.Copy(source: (nint)mat, destination: outMat, 0, 81);
                Marshal.Copy(source: (nint)condition, destination: outCondition, 0, 81);

                return new Mat(outMat, outCondition);
            }
            catch (Exception ex)
            {
                OnMatGeneratedFailed?.Invoke(ex);
                throw;
            }
        }

        /// <summary>
        /// 收集空白的格子，按 ij 的方式存到exist中
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="exist"></param>
        /// <returns>空白格子的数量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe int ResolveExist(byte* condition, byte* exist)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (condition[i * 9 + j] == 0)
                        exist[len++] = (byte)(i * 10 + j);
                }
            }
            return len;
        }

        /// <summary>
        /// 收集已确定的格子，按 ij 的方式存到exist中
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="exist"></param>
        /// <returns>空白格子的数量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe int ResolveNotExist(byte* condition, byte* exist)
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (condition[i * 9 + j] == 1)
                        exist[len++] = (byte)(i * 10 + j);
                }
            }
            return len;
        }

        /// <summary>
        /// 收集某个空白格子可以填入的所有结果
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="rest"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>结果的可能数量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe int ResolveRest(byte* condition, byte* rest, int i, int j)
        {
            int len = 0;
            var row = i * 9 + 80;
            var col = j * 9 + 161;
            var part = (i / 3 * 3 +  j / 3) * 9 + 242;

            for (byte iter = 1; iter <= 9; iter++)
            {
                if (condition[row + iter] == 0
                    && condition[col + iter] == 0
                        && condition[part + iter] == 0)
                    rest[len++] = iter;
            }

            return len;
        }
    
        /// <summary>
        /// 当错误生成不可解的某格时随机丢弃约束该格的一个已确认的格子
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="mat"></param>
        /// <param name="random"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private unsafe void RandomThrow(byte* condition, byte* mat, Random random, int i, int j)
        {
            int len = 0;
            byte* buffer = stackalloc byte[18];

            for (int iter = 0; iter < 9; iter++)
            {
                if (mat[i * 9 + iter] != 0)
                    buffer[len++] = (byte)(i * 10 + iter);
                if (mat[iter * 9 + j] != 0)
                    buffer[len++] = (byte)(iter * 10 + j);
            }

            byte tuple = buffer[random.Next(0, len)];
            i = tuple / 10;
            j = tuple % 10;

            var ex = i * 9 + j;
            var item = mat[ex];
            var row = i * 9 + 80 + item;
            var col = j * 9 + 161 + item;
            var part = (i / 3 * 3 + j / 3) * 9 + 242 + item;

            condition[ex] = 0;
            condition[row] = 0;
            condition[col] = 0;
            condition[part] = 0;
            mat[ex] = 0;
        }
    }
}
