namespace Storage_
{
    class Program
    {
        public static void Main(string[] args)
        {
            Storage storage = new Storage();

            foreach(Commands command in (Commands[]) Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine(command);
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter command >> ");
                    var operation = Console.ReadLine();
                    if (operation?.ToUpper() == Commands.ADD_PRODUCT_FROM_FILE.ToString())
                    {
                        string path = string.Empty;
                        do
                        {
                            Console.Write("Enter path >> ");
                            path = Console.ReadLine();
                            if (!File.Exists(path))
                            {
                                Console.WriteLine("Wrong path, file doesn\'t exist");
                            }   
                        }
                        while (!File.Exists(path));

                        storage.AddProductsFromFile(path);
                    }
                    else if (operation?.ToUpper() == Commands.ADD_PRODUCT.ToString())
                    {
                        storage.AddProductDialog();
                    }
                    else if (operation?.ToUpper() == Commands.CHANGE_PRICE.ToString())
                    {
                        Console.Write("Enter percents >> ");
                        double percents = Convert.ToDouble(Console.ReadLine());
                        storage.ChangePrice(percents);
                    }
                    else if (operation?.ToUpper() == Commands.GET_MEATS.ToString())
                    {
                        List<Meat> meats = storage.GetMeats();
                        for (int i = 0; i < meats.Count; ++i)
                        {
                            Console.WriteLine("Product #{0}", i+1);
                            meats[i].PrintInformation();
                        }
                    }
                    else if (operation?.ToUpper() == Commands.PRINT_PRODUCTS.ToString())
                    {
                        storage.PrintProducts();
                    }
                    else if (operation?.ToUpper() == Commands.AT.ToString())
                    {
                        Console.Write("Enter the index of product (starts from 1) >> ");
                        int i = Convert.ToInt32(Console.ReadLine());
                        var temp = storage.At(i);
                        temp.PrintInformation();
                    }
                    else if (operation?.ToUpper() == Commands.GET_ERRORS_FOR_DATE.ToString())
                    {
                        Console.WriteLine("Enter date (dd/MM/year HH:mm:ss)");
                        string sDate = Console.ReadLine();
                        DateTime date = DateTime.ParseExact(sDate, ErrorLogs.sDateFormats, new System.Globalization.CultureInfo("FR-fr"), System.Globalization.DateTimeStyles.None);
                        List<string> errors = storage.GetErrorForDate(date);
                        errors.ForEach(item => Console.WriteLine(item));
                    }
                    else if (operation?.ToUpper() == Commands.EXIT.ToString())
                    {
                        return;
                    }
                    else
                    {
                        throw new InvalidOperationException("Error: Invalid operation");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private enum Commands
        {
            ADD_PRODUCT_FROM_FILE,
            ADD_PRODUCT,
            CHANGE_PRICE,
            GET_MEATS,
            PRINT_PRODUCTS,
            AT,
            GET_ERRORS_FOR_DATE,
            EXIT
        }
    }
}
