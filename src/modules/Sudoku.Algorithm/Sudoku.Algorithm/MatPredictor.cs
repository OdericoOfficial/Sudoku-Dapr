using Sudoku.Algorithm.Abstracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sudoku.Algorithm
{
    internal class MatPredictor : IMatPredictor
    {
        /// <summary>
        /// 异步预测数独的下一步
        /// </summary>
        /// <param name="mat">数独</param>
        /// <param name="condition">现条件</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<int> PredictAsync(byte[] mat, byte[] condition, byte[] input, int i, int j, int failedCount = 1000)
            => Task.Run(() => Predict(mat, condition, input, i, j, failedCount));

        /// <summary>
        /// 预测数独的下一步，使用栈分配、值类型、指针的方式减轻gc压力并加速数值读取
        /// </summary>
        /// <param name="mat">数独</param>
        /// <param name="condition">现条件</param>
        /// <returns>k</returns>
        public unsafe int Predict(byte[] mat, byte[] condition, byte[] input, int i, int j, int failedCount = 1000)
        {
            Random random = new Random();
            byte* tempMat = stackalloc byte[81];
            byte* tempCondition = stackalloc byte[324];
            byte* exist = stackalloc byte[81];
            byte* rest = stackalloc byte[9];

            Marshal.Copy(mat, 0, (nint)tempMat, 81);
            Marshal.Copy(condition, 0, (nint)tempCondition, 324);

            // 判断当前是否存在条件冲突
            for (int iter = 0; iter < 324; iter++)
            {
                if (tempCondition[i] == 1 && input[i] == 1)
                    return -1;
                if (tempCondition[i] == 0 && input[i] == 1)
                    tempCondition[i] = 1;
            }

            // 生成终盘
            int cnt = 0;
            for (int iter = 0; iter < 81; iter++)
            {
                if (tempMat[iter] != 0)
                    cnt++;
            }

            int failedCnt = 0;
            bool isFailed = false;

            while (cnt < 81)
            {
                int existLength = ResolveExist(tempCondition, exist);
                var blankItem = exist[random.Next(0, existLength)];
                var ix = blankItem / 10;
                var jx = blankItem % 10;

                int restLength = ResolveRest(tempCondition, rest, ix, jx);
                if (restLength == 0)
                {
                    isFailed = true;
                    if (isFailed)
                        failedCnt++;

                    if (failedCnt >= failedCount)
                        return -1;

                    RandomThrow(tempCondition, tempMat, random, ix, jx);
                    cnt--;
                    continue;
                }

                isFailed = false;

                var item = rest[random.Next(0, restLength)];

                var ex = ix * 9 + jx;
                var row = ix * 9 + 80 + item;
                var col = jx * 9 + 161 + item;
                var part = (ix / 3 * 3 + jx / 3) * 9 + 242 + item;

                tempCondition[ex] = 1;
                tempCondition[row] = 1;
                tempCondition[col] = 1;
                tempCondition[part] = 1;
                tempMat[ex] = item;
                cnt++;
            }

            return tempMat[i * 9 + j];
        }

        /// <summary>
        /// 收集空白的格子，按 ij 的方式存到exist中
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="exist"></param>
        /// <returns>空白格子的数量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe int ResolveExist(byte* condition, byte* exist)
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
        /// 收集某个空白格子可以填入的所有结果
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="rest"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>结果的可能数量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe int ResolveRest(byte* condition, byte* rest, int i, int j)
        {
            int len = 0;
            var row = i * 9 + 80;
            var col = j * 9 + 161;
            var part = (i / 3 * 3 + j / 3) * 9 + 242;

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
        public unsafe void RandomThrow(byte* condition, byte* mat, Random random, int i, int j)
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
