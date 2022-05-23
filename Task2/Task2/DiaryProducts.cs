using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class DiaryProducts : Product
    {
        public DiaryProducts(string name, double price, double weight, int days)
            : base(name, price, weight)
        {
            ExpirationDate = days;
        }

        public override void PrintInformation()
        {
            base.PrintInformation();
            Console.WriteLine("Days to expiration - " + ExpirationDate);
        }

        public override void ChangePrice(double percents)
        {
            double summaryPercents = percents;

            if (ExpirationDate <= 7)
                summaryPercents -= 10;

            Price += Price / 100 * summaryPercents;
        }

        public int ExpirationDate { get; set; }

        ~DiaryProducts() { }
    }
}
