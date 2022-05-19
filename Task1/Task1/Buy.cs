using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Buy : Product
    {
        public Buy(string productName, double productPrice, double productWeight, uint quantity)
            : base(productName, productPrice, productWeight)
        {
            Quantity = quantity;
            FullPrice = productPrice * quantity;
            FullWeight = productWeight * quantity;    
        }

        public uint Quantity { get; private set; }
        public double FullPrice { get; private set; }
        public double FullWeight { get; private set; }
        
        ~Buy() { }
    }
}
