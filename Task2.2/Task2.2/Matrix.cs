using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2._2
{
    public class Matrix
    {   
        public Matrix (int rows, int cols)
        {
            Reset(rows, cols);
        }



        public void PrintMatrix()
        {
            Console.WriteLine(NumRows + " " + NumCols);
            for (int i = 0; i < NumRows; ++i)
            {
                for (int j = 0; j < NumCols; j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        public void GenerateVerticalSnake()
        {
            int value = 1; 

            for (int i = 0; i < NumCols; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < NumRows; ++j)
                    {
                        data[j, i] = value++;
                    }
                }
                
                else
                {
                    for (int j = NumRows - 1 ;j >= 0; --j)
                    {
                        data[j, i] = value++;
                    }
                }
            }

            PrintMatrix();
        }

        public void GenerateSpiralSnake()
        {
            int top = 0, bottom = NumRows - 1, left = 0, right = NumCols - 1;

            char direction = 'r';

            for (int value = 1; value <= NumCols * NumRows;)
            {
                if (direction == 'r')
                {
                    for (int j = left; j <= right; ++j)
                    {
                        data[top,j] = value++;
                    }

                    top++;
                    direction = 'd';
                }

                else if (direction == 'd')
                {
                    for (int j = top; j <= bottom; ++j)
                    {
                        data[j, right] = value++;
                    }

                    right--;
                    direction = 'l';
                }
                
                else if (direction == 'l')
                {
                    for (int j = right; j >= left; --j)
                    {
                        data[bottom, j] = value++;
                    }

                    bottom--;
                    direction = 't';
                }

                else if (direction == 't')
                {
                    for (int j = bottom; j >= top; --j)
                    {
                        data[j,left] = value++;
                    }

                    left++;
                    direction = 'r';
                }
            }

            PrintMatrix();
        }

        public void GenerateDiagonalSnake()
        {
            if (NumCols != NumRows)
                throw new Exception("Error: number of cols must equal to number of rows");

            bool isUp = true;
            int i = 0, j = 0;

            for (int value = 1; value <= NumCols * NumCols;)
            {
                if (isUp)
                {
                    for (; i >= 0 && j < NumCols; ++j, --i)
                    {
                        data[i, j] = value++;
                    }

                    if (i < 0 && j <= NumCols - 1)
                    {
                        i = 0;
                    }
                    else if (j == NumCols)
                    {
                        i += 2;
                        --j;
                    }
                }

                else
                {
                    for (; j >= 0 && i < NumCols; ++i, --j)
                    {
                        data[i, j] = value++;
                    }

                    if (j < 0 && i <= NumCols - 1)
                    {
                        j = 0;
                    }
                    else if (i == NumCols)
                    {
                        j += 2;
                        --i;
                    }
                }

                isUp = !isUp;
            }

            PrintMatrix();
        }

        public void Reset(int newRows, int newCols)
        {
            if (newRows <= 0 || newCols <= 0)
            {
                throw new ArgumentOutOfRangeException("Error: " + ((newRows < 0) ? nameof(newRows) : nameof(newCols)) + " must be > 0");
            }
            NumRows = newRows;
            NumCols = newCols;
            data = new int[newRows, newCols];
        }

        ~Matrix() { }

        public int NumCols { get; private set; }
        public int NumRows { get; private set; }

        private int[,] data;
    
    }
}
