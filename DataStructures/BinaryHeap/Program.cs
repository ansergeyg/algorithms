using System;
using System.Linq;
using System.Collections.Generic;

namespace BinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryHeap theHeap = new BinaryHeap(20);
            // Random rnd = new Random();
            // for (int i = 0; i < 20; i++)
            // {
            //     theHeap.AddItem(rnd.Next(1, 100));
            // }
            long[] test = new long[20] {20, 10, 3, 15, 14, 21, 32, 23, 17, 71, 29, 44, 35, 74, 33, 8, 11, 18, 43, 46};
            for (int i = 0; i < test.Length; i++)
            {
                theHeap.AddItem(test[i]);
            }
            //theHeap.FastBuild(test);
            Console.WriteLine(theHeap.getMax());
        }
    }

    public class BinaryHeap
    {

        public int Size { get; set; }
        private long[] list = new long[this.Size];

        public BinaryHeap(int size)
        {
            this.list = new List<long>(size);
            Size = size;
        }
        
        //Fast O(n) heap build 
        public void FastBuild(IEnumerable<long> sourceList)
        {
            this.list = sourceList.ToList<long>();
            for (int i = this.Size / 2; i >= 0; i--)
            {
                this.Heapify(i);
            }
        }
 
        public long getMax()
        {
            long maxValue = this.list[0];
            this.list[0] = this.list[this.Size - 1];
            this.list.RemoveAt(this.Size - 1);
            this.Heapify(0);

            return maxValue;
        }

        //Creates heap for O(n log n)
        public void AddItem(long item)
        {
            list.Add(item);
            int newId = this.Size - 1;
            int parentId = (newId - 1) / 2; 
            while (newId > 0 && list[parentId] < list[newId])
            {
                this.Swap(list, newId, parentId);
                newId = parentId;
                parentId = (newId - 1) / 2;
            }
        }

        public void Heapify(int startId)
        {
            int id = startId;
            while (true)
            {
                int leftId = 2 * id + 1;
                int rightId = 2 * id + 2;
                int maxId = id;

                if (leftId < this.Size && list[leftId] > list[maxId])
                {
                    maxId = leftId;
                }
                if (rightId < this.Size && list[rightId] > list[maxId])
                {
                    maxId = rightId;
                }
                if (maxId == id)
                {
                    break;
                }
                this.Swap(list, id, maxId);
                id = maxId;
            }
        }

        private void Swap(List<long> list, int id1, int id2)
        {
            long tempValue = list[id1];
            list[id1] = list[id2];
            list[id2] = tempValue;
        }
    }

}