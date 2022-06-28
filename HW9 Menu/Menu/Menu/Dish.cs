using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    internal class Dish
    {
        public string Name { get; private set; }
        public List<Ingredient> Ingredients { get; set; }

        public Dish(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
        }
    }
}
