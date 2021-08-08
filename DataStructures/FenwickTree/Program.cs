using System;
using System.Collections.Generic;
using System.Linq;
namespace FenwickTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] {2, 1, -1, 3, 5, 4, -2};
            FenwickTree ft = new FenwickTree(test.ToList());
            Console.WriteLine(ft.Sum(3, 6));
        }
    }

    public class FenwickTree
    {
        int[] bit;
        int n;

        public FenwickTree(List<int> array)
        {
            n = array.Count;
            bit = new int[n];
            for (int i = 0; i < n; i++)
            {
                this.Add(i, array[i]);
            }
        }

        public int Sum(int rightIndex)
        {
            int sum = 0;
            for (int i = rightIndex; i >= 0; i = (i & (i + 1)) - 1)
            {
                sum += bit[i];
            }
            return sum;
        }

        public int Sum(int leftIndex, int rightIndex)
        {
            return this.Sum(rightIndex) - this.Sum(leftIndex - 1);
        }

        public void Add(int index, int delta)
        {
            for (int i = index; i < n; i = i | (i + 1))
            {
                bit[i] += delta;
            }
        }
    }
}
