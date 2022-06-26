using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_ 
{
    public class Storage
    {
        private static ErrorLogs eLogs = new ErrorLogs(@"C:\Users\vital\source\repos\sswu-camp\HW7 Storage\Storage\error_log.txt");

        public Storage()
        {
            data = new List<Product>();
        }

        public void AddProductsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            int lineCount = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] productInfo = sr.ReadLine().Split(" ");
                    ++lineCount;
                    try
                    {
                        switch (productInfo[0].ToUpper())
                        {
                            case "MEAT":
                                {
                                    string productName = productInfo[1].Replace(productInfo[1][0], char.ToUpper(productInfo[1][0]));
                                    Meat.MeatType type = (Meat.MeatType)Enum.Parse(typeof(Meat.MeatType), productInfo[2].ToUpper());
                                    Meat.MeatSort sort = (Meat.MeatSort)Enum.Parse(typeof(Meat.MeatSort), productInfo[3].ToUpper());
                                    double weight = Convert.ToDouble(productInfo[4]);
                                    double price = Convert.ToDouble(productInfo[5]);

                                    data.Add(new Meat(productName, price, weight, sort, type));
                                    break;
                                }
                            case "DIARY_PRODUCT":
                                {
                                    string productName = productInfo[1].Replace(productInfo[1][0], char.ToUpper(productInfo[1][0]));
                                    double weight = Convert.ToDouble(productInfo[2]);
                                    double price = Convert.ToDouble(productInfo[3]);
                                    int daysToExpire = Convert.ToInt32(productInfo[3]);

                                    data.Add(new DiaryProducts(productName, price, weight, daysToExpire));
                                    break;
                                }
                            default:
                                throw new ArgumentException($"Item Type {productInfo[0]} is not correct item type");
                        }
                        /*
                         add try catch here with adding to error_log and add class ErrorLogs object
                         */
                    }
                    catch (Exception ex)
                    {
                        eLogs[DateTime.Now] = ($" (line {lineCount}) " + ex.Message);
                    }
                }
            }
        }

        public List<string> GetErrorForDate(DateTime date)
        {
            return eLogs.GetLogsForDate(date);     
        }

        public void AddProductDialog()
        {
            Console.WriteLine("MEAT \nDIARY_PRODUCTS");
            Console.Write("Select product \n>> ");
            var typeOfProduct = Console.ReadLine();
            
            if (typeOfProduct?.ToUpper() != "MEAT") 
                if(typeOfProduct?.ToUpper() != "DIARY_PRODUCTS")
            {
                throw new ArgumentException("Error: requested argument is not found");
            }

            Console.Write("Enter the name of product \n>> ");
            string? name = Console.ReadLine();
            name = name?.Replace(name[0], char.ToUpper(name[0]));

            Console.Write("Enter the weight of product (kg) \n>> ");
            double weight = Convert.ToDouble(Console.ReadLine());
            if (weight <= 0)
            {
                throw new ArgumentException("Error: invalid weight argument");
            }

            Console.Write("Enter the price of product \n>> ");
            double price = Convert.ToDouble(Console.ReadLine());
            if (price <= 0)
            {
                throw new ArgumentException("Error: invalid price argument");
            }
            
            if (typeOfProduct?.ToUpper() == "MEAT")
            {                
                Console.WriteLine("Select the type of meat");
                Console.WriteLine("MUTTON, VEAL, PORK, CHICKEN");
                Console.Write(">> ");
                var _type = Console.ReadLine();
                Meat.MeatType type = (Meat.MeatType) Enum.Parse(typeof(Meat.MeatType), _type?.ToUpper() ?? string.Empty, true);

                Console.WriteLine("Select the sort of meat");
                Console.WriteLine("FIRST, SECOND");
                Console.Write(">> ");
                var _sort = Console.ReadLine();
                Meat.MeatSort sort = (Meat.MeatSort) Enum.Parse(typeof(Meat.MeatSort), _sort?.ToUpper() ?? string.Empty, true);

                data.Add(new Meat(name, price, weight, sort, type));
            }

            else if (typeOfProduct?.ToUpper() == "DIARY_PRODUCTS")
            {
                Console.WriteLine("Enter days to expiration >> ");
                int days = Console.Read();

                data.Add(new DiaryProducts(name, price, weight, days));
            }

            else
            {
                throw new ArrayTypeMismatchException("Error: list type mismatch");  
            }
        }

        public List<Meat> GetMeats()
        {
            List<Meat> temp = new List<Meat>();

            foreach(var item in data)
            {
                if (item is Meat)
                {
                    temp.Add((Meat)item);
                }
            }

            return temp;
        }

        public static void PrintProducts(List<Product> storage)
        {
            uint indexOfProduct = 1;

            foreach (var item in storage)
            {
                Console.WriteLine("Product #{0}", indexOfProduct);
                Console.WriteLine(item);

                ++indexOfProduct;
            }
        }

        public bool Contains(Product product)
        {
            foreach (var item in data)
            {
                if (product.GetHashCode() == item.GetHashCode())
                {
                    return true;
                }
            }

            return false;
        }

        public static void GetCommonProducts(Storage lhs, Storage rhs, out List<Product> productsList)
        {
            List<Product> temp = new List<Product>();
            foreach (var item in rhs.data)
            {
                if (lhs.Contains(item))
                {
                    temp.Add(item);
                }
            }

            productsList = temp;
        }

        public static void GetUniqueProducts(Storage lhs, Storage rhs, out List<Product> productsList)
        {
            List<Product> temp = new List<Product>();
            foreach(var item in rhs.data)
            {
                if (!lhs.Contains(item))
                {
                    temp.Add(item);
                }
            }

            productsList = temp;
        }

        public static void GetCombinedList(Storage lhs, Storage rhs, out List<Product> productsList)
        {
            List<Product> temp = lhs.data;
            foreach(var item in rhs.data)
            {
                if (!lhs.Contains(item))
                {
                    temp.Add(item);
                }
            }

            productsList = temp;
        }

        public void ChangePrice(double percents)
        {
            foreach (var item in data)
            {
                item.ChangePrice(percents);
            }

            Console.WriteLine("Prices are successfully changed");
        }

        //indexator alternative, starts from 1
        public Product At(int index)
        {
            return data[index-1];
        }

        public override string ToString()
        {
            string temp = String.Empty;

            uint indexOfProduct = 1;

            foreach (var item in data)
            {
                temp += ($"Product #{indexOfProduct}\n");
                temp += item;

                ++indexOfProduct;
            }

            return temp;
        }

        private List<Product> data;
    }

    /*class ProductComparer : IComparer<Product>
    {
        public int Compare(Product lhs, Product rhs)
        {
            return lhs.GetHashCode().CompareTo(rhs.GetHashCode());
        }
    }*/
}