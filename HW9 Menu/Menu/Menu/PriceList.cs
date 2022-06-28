using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    
    internal class PriceList
    {
        public Dictionary<string, double> Prices { get; set; }

        public PriceList(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            Prices = new Dictionary<string, double>();
            
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != "" && line != null)
                    {
                        string[] priceLine = line.Split(" - ");
                        Prices.Add(priceLine[0], Convert.ToDouble(priceLine[1]));
                    }
                }
            }
        }

        public bool Contains(string name)
        {
            foreach (var item in Prices)
            {
                if (item.Key.ToLower() == name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
