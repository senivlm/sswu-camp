namespace _6._1
{
    class Program
    { 
        public static void Main(string[] args)
        {
            try
            {
                Accounting accounting = new Accounting(@"C:\Users\vital\source\repos\sswu-camp\Task6\6.1\6.1\Accounts.txt");
                TableHandler tableHandler = new TableHandler();
                tableHandler.Headers = new List<string> { "#", "Owner\'s surname ", accounting.quartalMonths[0], accounting.quartalMonths[1], accounting.quartalMonths[2], "Debt($)" };
                foreach (var flat in accounting.data)
                {
                    List<string> dates = new List<string>();
                    foreach (var date in flat.captureDates)
                    {
                        dates.Add($"{date.Day}.{date.Month}.{date.Year}");
                    }

                    tableHandler.AddRow(new List<string>{ flat.number.ToString(), flat.ownerSurname,
                    dates[0], dates[1], dates[2], "" }); ;
                    tableHandler.AddRow(new List<string> { "", "", flat.prevKilowatt.ToString(), "", flat.nextKilowatt.ToString(), flat.debt.ToString() });
                }

                tableHandler.PrintTable(@"C:\Users\vital\source\repos\sswu-camp\Task6\6.1\6.1\Result.txt");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}