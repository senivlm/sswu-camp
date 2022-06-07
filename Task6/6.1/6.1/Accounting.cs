using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1
{
    public class Accounting
    {
        private const double PRICE = 0.06;
        private DateTime _date = DateTime.MinValue;
        private int flatCount = 0;
        private int quartalNumber;
        private List<FlatAccount> data;

        public struct FlatAccount
        {
            int number;
            string ownerSurname;
            int prevKilowatt;
            int nextKilowatt;
            DateTime[] captureDates;
            double debt;
        }

        public Accounting(string path)
        {
            data = new List<FlatAccount>();
            using (StreamReader sr = new StreamReader(path))
            {
                string? line = sr.ReadLine();
                if (line != null)
                {

                    string count = line.Substring(0, line.IndexOf(' '));
                    string quartal = line.Substring(line.IndexOf(' ') + 1);

                    int.TryParse(count, out flatCount);
                    int.TryParse(quartal, out quartalNumber);
                }
                else
                    throw new ArgumentNullException("Exception: File is empty");
                
                while (sr.ReadLine() != null)
                {

                }
            }
        }


    }
}
