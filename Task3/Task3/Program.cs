namespace Task3
{
    class Program
    {
        public static void Main(string[] args)
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
                var operation = Console.ReadLine();
                try
                {
                    if (operation?.ToUpper() == Commands.PRINT_VECTOR.ToString())
                    {
                        Console.WriteLine(vector);
                    }
                    if (operation?.ToUpper() == Commands.RANDOM_INIT.ToString())
                    {
                        Console.Write("Enter first number >> ");
                        int a = Convert.ToInt32(Console.ReadLine()); 
                        Console.Write("Enter second number >> ");
                        int b = Convert.ToInt32(Console.ReadLine());
                        vector.InitRand(a, b);
                        Console.WriteLine(vector);
                    }
                    if (operation?.ToUpper() == Commands.SHUFFLE_INIT.ToString())
                    {
                        vector.InitShuffle();
                        Console.WriteLine(vector);
                    }
                    if (operation?.ToUpper() == Commands.CALC_FREQ.ToString())
                    {
                        Pair<int, int>[] pairs = vector.CalculateFrequence();
                        foreach (var pair in pairs)
                        {
                            Console.WriteLine(pair);
                        }
                    }
                    if (operation?.ToUpper() == Commands.PALINDROME_CHECK.ToString())
                    {
                        string isPalindrome = vector.CheckPalindrome() ? " is Palindrome" : " is not Palindrome";
                        Console.WriteLine("Vector" + isPalindrome);
                    }
                    if (operation?.ToUpper() == Commands.VECTOR_REVERSE.ToString())
                    {
                        vector.ReverseVector();
                        Console.WriteLine(vector);
                    }
                    if (operation?.ToUpper() == Commands.LONGEST_COMMON_SUBSEQUENCE.ToString())
                    {
                        Console.WriteLine(vector);
                        Console.WriteLine(vector.FindLongestСommonSubsequence());
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        enum Commands
        {
            PRINT_VECTOR,
            RANDOM_INIT,
            SHUFFLE_INIT,
            CALC_FREQ,
            PALINDROME_CHECK,
            VECTOR_REVERSE,
            LONGEST_COMMON_SUBSEQUENCE,
            QUICK_SORT
        }
    }
}