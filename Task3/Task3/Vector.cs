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
        {//лишній допоміжний масив. Цикл треба крутити тільки до половини
            int[] temp = new int[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                temp[i] = array[array.Length - 1 - i];
            }
            

            for (int i = 0; i < array.Length; ++i)
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
        швидкого сортування практично немає
        //----------------------------------------------------------------------------
        public void QuickSort(SortPivots pivot, int begin, int end)
        {
            if (begin < end)
            {
                int p = pivot == SortPivots.BEGIN ? begin :
                    pivot == SortPivots.END ? end :
                    end - begin / 2;

                //
            }
        }
    
        public enum SortPivots
        {
            BEGIN,
            MIDDLE,
            END
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

/*        private int Partition(ref int[] Arr, int begin, int end)
        {
            int pivot = Arr[end];
            int i = begin - 1;

            for (int j = begin; j <= end; ++j)
            {
                if (Arr[j] <= pivot)
                {
                    ++i;
                    Swap(ref Arr, i, j);
                }
            }

            Swap(ref Arr, i + 1, end);
            return i + 1;
        } 
        */
        private void Swap(ref int[] Arr, int indexLeft, int indexRight)
        {
            if (Arr.Length <= indexRight || Arr.Length <= indexLeft)
                throw new IndexOutOfRangeException("Indexes must be on the array length interval");

            var temp = Arr[indexLeft];
            Arr[indexLeft] = Arr[indexRight];
            Arr[indexRight] = temp;
        }

        private int[] array;
    }
}
