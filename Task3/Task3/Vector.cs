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

        public void InitRand(int a, int b)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = random.Next(a, b);
            }
        }

        public void InitShuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                int value;
                do
                {
                    value = rand.Next(1, array.Length - i);
                }
                while (array.Contains(value));

                array[i] = value;
            }
        }

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
            
            return pairs;
        }

        public bool CheckPalindrome()
        {
            //int[] temp = new int[array.Length];

            /*for (int i = 0; i < array.Length; ++i)
            {
                temp[i] = array[array.Length - 1 - i];
            }

           for (int i = 0; i < array.Length; ++i)
           {
                if (temp[i] != array[i])
                    return false;
           }
            
           return true;  */

            int[] temp = array;

            Array.Reverse(temp);

            return array.Equals(temp);
        }

        public void ReverseArray()
        {
            int[] temp = new int[array.Length];

            for (int i = 0; i < array.Length; ++i)
            {
                temp[i] = array[array.Length - 1 - i];
            }

            array = temp;
        }



        public override string ToString()
        {
            string line = "";
            foreach(var item in array)
            {
                line += item + " ";
            }

            return line;
        }

        private int[] array;
    }
}
