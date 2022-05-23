using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public abstract class Product
    {
        public Product(string productName, double productPrice, double productWeight)
        {
           Name = productName;
           Price = productPrice;
           Weight = productWeight;
        }

        public virtual void ChangePrice(double percents)
        {
            Price += Price / 100 * percents;
        }

        public virtual void PrintInformation()
        {
            Console.WriteLine("Name - " + Name +
                              "\nPrice - " + Price +
                              "\nWeight - " + Weight);
        }

        public string Name { get; protected set; }
        public double Price { get; protected set; }
        public double Weight { get; protected set; }

        ~Product() { }
    }
}
