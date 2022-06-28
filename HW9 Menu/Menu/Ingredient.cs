using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    internal class Ingredient
    {
        public string Name { get; private set; }
        public double Weight { get; private set; }

        public Ingredient(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

    }
}
