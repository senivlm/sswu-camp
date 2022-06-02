using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Vector
    {
        public Vector(int n)
        {
            array = new int[n];
        }
        //---------------------------------------------------------------------------
        public int this[int index]
        {
            get 
            { 
                if (index < 0 || index >= array.Length)
                    throw new ArgumentOutOfRangeException("index");
                else
                    return array[index];
            }
            set
            { 
                array[index] = value; 
            }
        }
        //----------------------------------------------------------------------------
        public void InitRand(int a, int b)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = random.Next(a, b);
            }
        }
        //----------------------------------------------------------------------------
        public void InitShuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                int value;
                do
                {
                    value = rand.Next(1, array.Length+1);
                    
                }
                while (Array.Exists(array, element => element == value));

                array[i] = value;
            }
        }
        //----------------------------------------------------------------------------
        public Pair<int, int>[] CalculateFrequence()
        {
            Pair<int, int>[] pairs = new Pair<int, int>[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                pairs[i] = new Pair<int, int>(0, 0);
            }

            int countDifference = 0;

            for (int i = 0; i < array.Length; ++i)
            {  
                bool isElement = false;
                for (int j = 0; j < countDifference; ++j)
                {
                    if (array[i] == pairs[j].Key)
                    {
                        pairs[j].Value++;
                        isElement = true;
                        break;
                    }
                }

                if (!isElement)
                {
                    pairs[countDifference].Value++;
                    pairs[countDifference].Key = array[i];
                    ++countDifference;
                }
            }
            
            pairs = pairs.Where(element => element.Key != 0 && element.Value != 0).ToArray();

            return pairs;
        }
        //----------------------------------------------------------------------------
        public bool CheckPalindrome()
        {
            int[] temp = new int[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                temp[i] = array[array.Length - 1 - i];
            }
            

            for (int i = 0; i < array.Length / 2; ++i)
            {
                if (temp[i] != array[i])
                    return false;
            }
            

            return true; 
        }
        //----------------------------------------------------------------------------
        public void ReverseVector()
        {
            int[] temp = new int[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                temp[i] = array[array.Length - 1 - i];
            }

            array = temp;
        }
        //----------------------------------------------------------------------------
        public Pair<int, int> FindLongestСommonSubsequence()
        {
            int max = 0;
            int numberMax = 0;
            int counter = 0;
            int current = array[0];

            foreach (var item in array)
            {
                if (current == item)
                {
                    counter++;
                    if (counter > max)
                    {
                        max = counter;
                        numberMax = current;
                    }
                }
                else
                {
                    current = item;
                    counter = 1;
                }
                
            }

            return new Pair<int, int>(max, numberMax);
        }
        //----------------------------------------------------------------------------
        public void QuickSort(SortPivots pivot, int begin, int end)
        {
            if (begin < end)
            {
                int q = Partition(pivot, begin, end);
                QuickSort(pivot, begin, q - 1);
                QuickSort(pivot, q + 1, end);
            }
        }
    
        public enum SortPivots
        {
            BEGIN,
            MIDDLE,
            END
        }
        //----------------------------------------------------------------------------
        private int Partition(SortPivots pivot, int begin, int end)
        {
            int p;
            if (pivot == SortPivots.BEGIN) 
                p = begin;
            else if (pivot == SortPivots.END) 
                p = end;
            else 
                p = end + begin / 2;
            
            int i = pivot == SortPivots.BEGIN ? begin + 1 : begin - 1;
            int j = pivot == SortPivots.BEGIN ? begin + 1 : begin;
            end = pivot == SortPivots.BEGIN ? end - 1 : end;
            for (; j <= end; ++j)
            {
                
                if (array[j] < array[p])
                {
                    ++i;
                    Swap(i, j);                    
                }
                
            }

            if (pivot == SortPivots.BEGIN)
            {
                Swap(i - 1, begin);
                return i;
            }

            Swap(i + 1, end);
            return i + 1;
        } 
        
        private void Swap(int indexLeft, int indexRight)
        {
            if (array.Length <= indexRight || array.Length <= indexLeft)
                throw new IndexOutOfRangeException("Indexes must be on the array length interval");

            var temp = array[indexLeft];
            array[indexLeft] = array[indexRight];
            array[indexRight] = temp;
        }
        //----------------------------------------------------------------------------
        public override string ToString()
        {
            string line = "";
            for (int i = 0; i < array.Length; ++i)
            {
                if (i == 0)
                    line = "{ ";
                line += array[i];
                if (i != array.Length - 1)
                    line += ", ";
                else
                    line += " }";
            }

            return line;
        }

        private int[] array;
    }
}
