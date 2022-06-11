namespace Matrix
{
    static class Program
    {
        static void Main(string[] Args)
        {
            Console.WriteLine("Enter number of rows and columns");
                       
            int rows, cols;
            do
            {
                Console.Write("Rows >> ");
                rows = Convert.ToInt32(Console.ReadLine());
                
                try
                {
                    if (rows <= 0)
                        throw new ArgumentOutOfRangeException(nameof(rows) + " must be > 0");
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            }
            while (rows <= 0);

            do
            {
                Console.Write("Columns >> ");
                cols = Convert.ToInt32(Console.ReadLine());
                
                try
                {
                    if (cols <= 0)
                        throw new ArgumentOutOfRangeException(nameof(cols) + " must be > 0");
                }
                catch(Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            
            }
            while (cols <= 0);

            Matrix matrix = new Matrix(rows, cols);

            Console.WriteLine("Choose matrix generate method\n");
            foreach (Patterns pattern in (Patterns[])Enum.GetValues(typeof(Patterns)))
            {
                Console.WriteLine("- " + pattern);
            }

            while (true)
            {
                try
                {
                    Console.Write("\n>>");
                    var operation = Console.ReadLine();

                    if (operation?.ToUpper() == Patterns.VERTICAL_SNAKE_PATTERN.ToString())
                    {
                        matrix.GenerateVerticalSnake();
                        Console.Write(matrix);
                    }
                    else if (operation?.ToUpper() == Patterns.SPIRAL_SNAKE_PATTERN.ToString())
                    {
                        matrix.GenerateSpiralSnake();
                        Console.Write(matrix);
                    }
                    else if (operation?.ToUpper() == Patterns.DIAGONAL_SNAKE_PATTERN.ToString())
                    {
                        Console.WriteLine("Choose direction (left/right)");
                        Console.Write(">> ");
                        var direction = Console.ReadLine();
                        if (direction?.ToUpper() == "LEFT")
                        {
                            matrix.GenerateDiagonalSnake(Matrix.Direction.LEFT);
                        }
                        else if (direction?.ToUpper() == "RIGHT")
                        {
                            matrix.GenerateDiagonalSnake(Matrix.Direction.RIGHT);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid direction");
                        }
                        Console.Write(matrix);
                    }
                    else if (operation?.ToUpper() == Patterns.RESET.ToString())
                    {
                        Console.WriteLine("Enter new dimensions");
                        Console.Write("Rows >> ");
                        int _rows = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Columns >> ");
                        int _cols = Convert.ToInt32(Console.ReadLine());
                        matrix.Reset(_rows, _cols);
                    }
                    else if (operation?.ToUpper() == Patterns.EXIT.ToString())
                    {
                        return;
                    }
                    else
                    {
                        throw new InvalidOperationException("Error: invalid operation");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
        private enum Patterns
        {
            VERTICAL_SNAKE_PATTERN,
            SPIRAL_SNAKE_PATTERN,
            DIAGONAL_SNAKE_PATTERN,
            RESET,
            EXIT
        }
    }
}