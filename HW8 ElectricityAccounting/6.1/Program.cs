namespace _6._1
{
    class Program
    {
        private static Accounting accounting;
        private static TableHandler tableHandler;
        private static DateTime _date;
        private static string inputFile = @"C:\Users\vital\source\repos\sswu-camp\HW8 ElectricityAccounting\6.1\Accounts1.txt";
        private static string outputFile = @"C:\Users\vital\source\repos\sswu-camp\HW8 ElectricityAccounting\6.1\Result.txt";

        public static void Main(string[] args)
        {
            try
            {
                accounting = new Accounting(inputFile);
                tableHandler = new TableHandler(outputFile);
                tableHandler.Headers = new List<string> { "#", "Owner\'s surname ", accounting.quarterMonths[0], accounting.quarterMonths[1], accounting.quarterMonths[2], "Debt($)", "Days after" };
                _date = DateTime.Now;

                foreach (Operations op in (Operations[])Enum.GetValues(typeof(Operations)))
                {
                    Console.WriteLine($"- {op}");
                }

                Console.Write(">> ");
                var operation = Console.ReadLine();
                if (operation?.ToUpper() == Operations.PRINT_FULL_ACCOUNTING.ToString())
                {
                    foreach (var account in accounting.data)
                    {
                        AddAccountRow(account);
                    }
                }
                else if (operation?.ToUpper() == Operations.PRINT_ACCOUNT_BY_FLAT.ToString())
                {
                    Console.Write("Enter flat number >> ");
                    int number = Convert.ToInt32(Console.ReadLine());
                    FindAccountByFlat(number);
                }
                else if (operation?.ToUpper() == Operations.PRINT_MAX_DEBT_ACCOUNT.ToString())
                {
                    AddAccountRow(accounting.GetMaxDebtAccount());
                }
                else if (operation?.ToUpper() == Operations.ACCOUNTS_ADDITION.ToString())
                {
                    Console.WriteLine("Enter path of accounts file to add");
                    Console.Write(">> ");
                    string _path = Console.ReadLine();
                    Accounting other = new Accounting(_path);
                    accounting = accounting + other;

                    foreach (var account in accounting.data)
                    {
                        AddAccountRow(account);
                    }
                }
                else if (operation?.ToUpper() == Operations.ACCOUNTS_SUBSTRACT.ToString())
                {
                    Console.WriteLine("Enter path of accounts file to substract");
                    Console.Write(">> ");
                    string _path = Console.ReadLine();
                    Accounting other = new Accounting(_path);
                    accounting = accounting - other;

                    foreach (var account in accounting.data)
                    {
                        AddAccountRow(account);
                    }
                }
                else
                {
                    throw new InvalidOperationException($"{operation} is wrong.");
                }
                tableHandler.PrintTable();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }    
        }

        private static void AddAccountRow(Accounting.FlatAccount account)
        {
            List<string> dates = new List<string>();
            foreach (var date in account.captureDates)
            {
                dates.Add($"{date.Day}.{date.Month}.{date.Year}");
            }

            tableHandler.AddRow(new List<string>{ account.number.ToString(), account.ownerSurname,
                dates[0], dates[1], dates[2], "", ($"{(int)(_date - account.captureDates[2]).TotalDays}") }); ;
            tableHandler.AddRow(new List<string> { "", "", account.prevKilowatt.ToString(), "", account.nextKilowatt.ToString(), account.debt.ToString(), "" });
        }

        private static void FindAccountByFlat(int number)
        {
            foreach (var account in accounting.data)
            {
                if (account.number == number)
                {
                    AddAccountRow(account);
                    return;
                }
            }
            throw new ArgumentException($"There is not flat with number {number}");
        }
    }
}