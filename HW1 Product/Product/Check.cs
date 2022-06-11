using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public class Check
    {
        public void CheckPurchases(Buy purchase)
        {
            Console.WriteLine(
                purchase.Name + ":" +
                "\nFor one piece" +
                "\nPrice: " + Math.Round(purchase.Price, 2) +
                "\nWeight: " + Math.Round(purchase.Weight, 2) +
                "\nFor the whole purchase" +
                "\nPrice: " + Math.Round(purchase.FullPrice, 2) +
                "\nWeight: " + Math.Round(purchase.FullWeight, 2) +
                "\nQuantity: " + purchase.Quantity
                );
        }

        ~Check() { }
    }
}
