using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Product
    {
        public Product(string productName, double productPrice, double productWeight)
        {
           Name = productName;
           Price = productPrice;
           Weight = productWeight;
        }

        public string Name { get; private set; }
        public double Price { get; private set; }
        public double Weight { get; private set; }

        ~Product() { }
    }
}
