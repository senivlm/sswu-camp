namespace Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu(@"../../../Menu.txt");
            
            Console.WriteLine("Choose currency (eur/usd)");
            string? _curr = string.Empty;
            Currency currency = 0;    

            Console.Write(">> ");
            _curr = Console.ReadLine();
            
            switch(_curr?.ToUpper())
            {
            case "EUR":
                currency = Currency.EUR;
                break;
            case "USD":
                currency = Currency.USD;
                break;
            default:
                throw new ArgumentException(_curr);
            }

            try
            {
                menu.Init(Currency.EUR);
            }
            catch(TaskCanceledException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Runtime error. Try to change your input files and run application again.");
                Console.ResetColor();
            }
        }
    }
}