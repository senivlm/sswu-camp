namespace Task3
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose operation type: ");
            Console.WriteLine("- ARRAY_SORT\n- FILE_SORT");
            try
            {
                var operation = string.Empty;
                do
                {
                    Console.Write(">> ");
                    operation = Console.ReadLine();
                    try
                    {
                        if (!Enum.IsDefined(typeof(Operations), operation.ToUpper()))
                        {
                            throw new InvalidOperationException(operation + " is invalid.");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!Enum.IsDefined(typeof(Operations), operation.ToUpper()));

                if (operation?.ToUpper() == Operations.ARRAY_SORT.ToString())
                {
                    ArraySort();
                }
                else if (operation?.ToUpper() == Operations.FILE_SORT.ToString())
                {
                    FileHandler inputFile = new FileHandler(@"C:\Users\vital\source\repos\sswu-camp\Task3\Task3\InputArray.txt");
                    FileHandler outputFile = new FileHandler(@"C:\Users\vital\source\repos\sswu-camp\Task3\Task3\OutputArray.txt");
                    Vector.FileMergeSort(ref inputFile, ref outputFile);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ArraySort()
        {
            Console.WriteLine("Enter the length of vector");
            int size;
            do
            {
                Console.Write(">> ");
                size = Convert.ToInt32(Console.ReadLine());
                try
                {
                    if (size <= 0)
                        throw new ArgumentOutOfRangeException(nameof(size) + " must be > 0");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (size <= 0);

            Vector vector = new Vector(size);

            Console.WriteLine();
            foreach (Commands command in (Commands[])Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine("- " + command);
            }

            Console.WriteLine();

            while (true)
            {
                Console.Write(">> ");
                var command = Console.ReadLine();

                if (command?.ToUpper() == Commands.PRINT_VECTOR.ToString())
                {
                    Console.WriteLine(vector);
                }
                if (command?.ToUpper() == Commands.RANDOM_INIT.ToString())
                {
                    Console.Write("Enter first number >> ");
                    int a = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter second number >> ");
                    int b = Convert.ToInt32(Console.ReadLine());
                    vector.InitRand(a, b);
                    Console.WriteLine(vector);
                }
                if (command?.ToUpper() == Commands.SHUFFLE_INIT.ToString())
                {
                    vector.InitShuffle();
                    Console.WriteLine(vector);
                }
                if (command?.ToUpper() == Commands.CALC_FREQ.ToString())
                {
                    Pair<int, int>[] pairs = vector.CalculateFrequence();
                    foreach (var pair in pairs)
                    {
                        Console.WriteLine(pair);
                    }
                }
                if (command?.ToUpper() == Commands.PALINDROME_CHECK.ToString())
                {
                    string isPalindrome = vector.CheckPalindrome() ? " is Palindrome" : " is not Palindrome";
                    Console.WriteLine("Vector" + isPalindrome);
                }
                if (command?.ToUpper() == Commands.VECTOR_REVERSE.ToString())
                {
                    vector.ReverseVector();
                    Console.WriteLine(vector);
                }
                if (command?.ToUpper() == Commands.LONGEST_COMMON_SUBSEQUENCE.ToString())
                {
                    Console.WriteLine(vector);
                    Console.WriteLine(vector.FindLongestСommonSubsequence());
                }
                if (command?.ToUpper() == Commands.HEAP_SORT.ToString())
                {
                    vector.HeapSort();
                    Console.WriteLine(vector);
                }
                if (command?.ToUpper() == Commands.QUICK_SORT.ToString())
                {

                    Console.WriteLine();
                    foreach (Vector.SortPivots p in (Vector.SortPivots[])Enum.GetValues(typeof(Vector.SortPivots)))
                    {
                        Console.WriteLine("- " + p);
                    }

                    Console.WriteLine();

                    Console.WriteLine("Choose sort pivot");
                    Console.Write(">> ");
                    Vector.SortPivots pivot = (Vector.SortPivots)Enum.Parse(typeof(Vector.SortPivots), (Console.ReadLine())?.ToUpper() ?? string.Empty, true);
                    vector.QuickSort(pivot, 0, size - 1);
                    Console.WriteLine(vector);

                }
                if (command?.ToUpper() == Commands.MERGE_SORT.ToString())
                {
                    vector.MergeSort(0, size);
                    Console.WriteLine(vector);
                }
            }
        }
    }
}