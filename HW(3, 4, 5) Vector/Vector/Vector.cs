using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class Vector
    {
        public Vector(int n)
        {
            array = new int[n];
        }

        public Vector(List<int> list)
        {
            array = list.ToArray();
        }

        public Vector(int[] array)
        {
            this.array = array;
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
            int[] temp = array;
            Array.Reverse(temp);
          
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
        
        public void HeapSort()
        {
            for (int i = array.Length / 2 - 1; i >= 0; --i)
            {
                Heapify(array.Length, i);
            }

            for (int i = array.Length - 1; i >= 0; --i)
            {
                Swap(0, i);
                Heapify(i, 0);
            }
        }

        private void Heapify(int length, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < length && array[left] > array[largest])
            {
                largest = left;
            }
            if (right < length && array[right] > array[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                Swap(i, largest);
                Heapify(length, largest);
            }
        }
        //----------------------------------------------------------------------------

        public void MergeSort(int begin, int end)
        {
            if (end - begin <= 1) return;
            
            int middle = (end + begin) / 2;
            MergeSort(begin, middle);
            MergeSort(middle, end);
            Merge(begin, middle, end);
        }

        private void Merge(int left, int mid, int right)
        {
            int i = left;
            int j = mid;
            int k = 0;
            int[] temp = new int[right - left];
            while (i < mid && j < right)
            {
                if (array[i] < array[j])
                {
                    temp[k] = array[i++];
                }
                else
                {
                    temp[k] = array[j++];
                }
                ++k;
            }

            if (i == mid)
            {
                for (int m = j; m < right; ++m)
                {
                    temp[k++] = array[m];
                }
            }
            else
            {
                while (i < mid)
                {
                    temp[k++] = array[i++];
                }
            }
            
            for (int n = 0; n < temp.Length; ++n)
            {
                array[n + left] = temp[n];
            }
        }

        public static void FileMergeSort(ref FileHandler inputFile, ref FileHandler outputFile)
        {
            int[] input = inputFile.GetValuesFromFile().ToArray();
            List<int> firstPart = new List<int>();
            List<int> secondPart = new List<int>();

            for (int i = 0; i < input.Length / 2; ++i)
            {
                firstPart.Add(input[i]);
                secondPart.Add(input[input.Length - 1 - i]);
            }

            Vector firstVector = new Vector(firstPart);
            Vector secondVector = new Vector(secondPart);

            firstVector.MergeSort(0, firstVector.array.Length);
            secondVector.MergeSort(0, secondVector.array.Length);

            Vector mergeVector = firstVector + secondVector;
            mergeVector.MergeSort(0, mergeVector.array.Length);

            outputFile.WriteFile(ref mergeVector.array);
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
            MEDIAN,
            END
        }
        //----------------------------------------------------------------------------

        private int Partition(SortPivots pivot, int begin, int end)
        {
            int p, i, j;

            switch (pivot)
            {
                case SortPivots.BEGIN:
                    p = begin;
                    i = begin + 1;
                    j = begin + 1;
                    break;
                case SortPivots.END:
                    p = end;
                    i = begin - 1;
                    j = begin;
                    break;
                default:
                    return MiddlePartition(begin, end);
            }

            for (; j <= end; ++j)
            {
                if (array[j] < array[p])
                {
                    if (pivot == SortPivots.BEGIN)
                    {
                        if (j != i)
                        {
                            Swap(i, j);
                        }
                        ++i;
                    }
                    else
                    {
                        Swap(j, ++i);
                    }
                }
            }

            if (pivot == SortPivots.BEGIN)
            {
                Swap(i - 1, begin);
                return i - 1;
            }

            Swap(i + 1, end);
            return i + 1;
        }

        private int MiddlePartition(int begin, int end)
        {
            int p = (end + begin) / 2;

            while (begin != end)
            {
                if (array[begin] < array[p])
                    ++begin;
                else
                    Swap(begin, end--);
            }



            return end;
        }
        //----------------------------------------------------------------------------
        
        private void Swap(int indexLeft, int indexRight)
        {
            if (array.Length <= indexRight || array.Length <= indexLeft)
                throw new IndexOutOfRangeException("Indexes must be on the array length interval");

            var temp = array[indexLeft];
            array[indexLeft] = array[indexRight];
            array[indexRight] = temp;
        }
        //----------------------------------------------------------------------------
        
        public static Vector operator+(Vector lhs, Vector rhs)
        {
         //   return new Vector(lhs.array.Take(lhs.array.Length).Concat(rhs.array.Take(rhs.array.Length)));
            return new Vector(lhs.array.Concat(rhs.array).ToList());
        }

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
