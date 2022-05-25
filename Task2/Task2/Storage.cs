using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Storage
    {
        public Storage()
        {
            storage = new List<Product>();
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
            var name = Console.ReadLine();

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

                storage.Add(new Meat(name, weight, price, sort, type));
            }

            else if (typeOfProduct?.ToUpper() == "DIARY_PRODUCTS")
            {
                Console.WriteLine("Enter days to expiration >> ");
                int days = Console.Read();

                storage.Add(new DiaryProducts(name, price, weight, days));
            }

            else
            {
                throw new ArrayTypeMismatchException("Error: list type mismatch");  
            }
        }

        public List<Meat> GetMeats()
        {
            List<Meat> temp = new List<Meat>();

            foreach(var item in storage)
            {
                if (item is Meat)
                {
                    temp.Add((Meat)item);
                }
            }

            return temp;
        }

        public void PrintProducts()
        {
            uint indexOfProduct = 1;

            foreach (var item in storage)
            {
                Console.WriteLine("Product #{0}", indexOfProduct);

                item.PrintInformation();

                ++indexOfProduct;
            }
        }

        public void ChangePrice(double percents)
        {
            foreach (var item in storage)
            {
                item.ChangePrice(percents);
            }

            Console.WriteLine("Prices are successfully changed");
        }

        public Product At(int index)
        {
            return storage[index-1];
        }
        
        private List<Product> storage;
    }
}
