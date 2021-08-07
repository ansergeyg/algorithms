using System;

namespace SparseTable
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] test = new long[] {3, 1, 4, -2, 5, 4, -1, 6, 1};
            // STMin sp = new STMin(test);
            // Console.WriteLine(sp.GetMin(0, 3));
            STSum sp = new STSum(test);
            Console.WriteLine(sp.GetSum(0, 3));
        }
    }

    public abstract class SparseTableBase
    {
        protected long[,] dp;
        protected int[] log2;
        protected long[] array;
        protected int len;
        protected int maxLog2;
        public SparseTableBase(long[] array)
        {
            len = array.Length;
            this.array = array; 
            //TODO: math.log method can be imprecise. Use binary operations.
            maxLog2 = (int)Math.Log(len, 2.0);
            dp = new long[len + 1, maxLog2 + 1];
            log2 = new int[len + 1];
            log2[1] = 0;
            //Calculate logarithms base 2 up to len
            for (int i = 2; i <= len; i++)
            {
                log2[i] = log2[i / 2] + 1;
            }
            this.Build();
        }

        public abstract void Build();
    }
    public class STMin: SparseTableBase
    {
        public STMin(long[] array) : base(array)
        {
        }

        public override void Build()
        {
            //For log 0 the values are the same as input array.
            for (int i = 0; i < len; i++)
            {
                dp[i, 0] = array[i];
            }
            //Now we calculate values depending on the function (min/max or sum)
            for (int j = 1; j <= maxLog2; j++)
            {
                for (int i = 0; i + (1 << j) <= len; i++)
                {
                    /*
                    for range min/max query:
                    we split a given range into two:
                    2^j is 2^(j - 1) and 2^(j - 1).
                    Because min/max function is idempotent we can use overlaping intervals to calculate 
                    */
                    dp[i, j] = Math.Min(dp[i, j - 1], dp[i + (1 << (j - 1)), j - 1]);
                }
            }

        }

        public long GetMin(int leftIndex, int rightIndex)
        {
            // to calculate the length of the interval [l, r] we use 
            // simple formula l - r + 1
            int pow2 = log2[rightIndex - leftIndex + 1];
            return Math.Min(dp[leftIndex, pow2], dp[rightIndex - (1 << pow2) + 1, pow2]);
        }
    }


    public class STSum: SparseTableBase
    {
        public STSum(long[] array) : base(array)
        {
        }

        public override void Build()
        {
            for (int i = 0; i < len; i++)
            {
                dp[i, 0] = array[i];
            }
            for (int j = 1; j <= maxLog2; j++)
            {
                for (int i = 0; i + (1 << j) <= len; i++)
                {
                    dp[i, j] = dp[i, j - 1] + dp[i + (1 << (j - 1)), j - 1];
                }
            }

        }

        public long GetSum(int leftIndex, int rightIndex)
        {
            long sum = 0;
            for (int j = maxLog2; j >= 0; j--)
            {
                if ((1 << j) <= rightIndex - leftIndex + 1)
                {
                    sum += dp[leftIndex, j];
                    leftIndex += 1 << j;
                }
            }
            return sum;
        }
    }
}
