using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class Menu
    {
        private List<Dish> _dishes;
        private Dictionary<Currency, double> _courses;
        private PriceList _priceList;
        private UInt16 error_count = 0;
        private string pPrices = @"../../../Prices.txt";
        private string pCourse = @"../../../Courses.txt";
        private string pResult = @"../../../Result.txt";

        public Menu(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            _dishes = new List<Dish>();
            _courses = new Dictionary<Currency, double>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string? dishName = sr.ReadLine();
                    Dish dish = new Dish(dishName);

                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != "" && line != null)
                    {
                        string[] _ingredient = line.Split(", ");
                        string ingredientName = _ingredient[0];
                        double ingredientWeight = Convert.ToDouble(_ingredient[1]);
                        dish.Ingredients.Add(new Ingredient(ingredientName, ingredientWeight));
                    }

                    _dishes.Add(dish);
                }
            }
        }

        public void Init(Currency curr)
        {
            GetCurrency();
            Dictionary<string, double> totalIngredints = GetTotalIngredientAmount();
            try
            {
                _priceList = new PriceList(pPrices);
                CheckIfPriceExists(totalIngredints);
            }
            catch(Exception ex)
            {
                if (ex is PriceNotFoundException)
                {
                    ++error_count;
                    if (error_count > 1)
                        throw new TaskCanceledException();
                    
                    AppendPriceFile(ex.Message);
                    Init(curr);
                }
            }
            
            using (StreamWriter sw = new StreamWriter(pResult))
            {
                foreach (var item in totalIngredints)
                {
                    sw.WriteLine($"{item.Key} - {item.Value} KG - " + string.Format("{0:0.00}", item.Value * _priceList.Prices[item.Key] / _courses[curr]) + $" {curr}");
                }
            }
        }

        private void AppendPriceFile(string valueName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{valueName} doesn\'t exist in Price.txt");
            Console.WriteLine($"Enter {valueName} - price per kilo");
            Console.ResetColor();
            string line = Console.ReadLine();
            
            using (StreamWriter sw = File.AppendText(pPrices))
            {
                sw.WriteLine(line);
            }
        }

        private void CheckIfPriceExists(Dictionary<string, double> totalAmount)
        {
            foreach(var item in totalAmount)
            {
                if (!_priceList.Prices.ContainsKey(item.Key))
                {
                    throw new PriceNotFoundException(item.Key);
                }
            }
        }

        private Dictionary<string, double> GetTotalIngredientAmount()
        {
            Dictionary<string, double> totalIngredients = new Dictionary<string, double>();

            foreach(var dish in _dishes)
            {
                foreach(var item in dish.Ingredients)
                {
                    if (totalIngredients.ContainsKey(item.Name))
                    {
                        totalIngredients[item.Name] += item.Weight;
                    }
                    else
                    {
                        totalIngredients.Add(item.Name, item.Weight);
                    }
                }
            }

            return totalIngredients;
        }

        /*private bool ContainsIngredient(Dictionary<Ingredient, double> total, Ingredient ingredient)
        {
            foreach(var item in total)
            {
                if (item.Key.Name == ingredient.Name)
                    return true;
            }

            return false;
        }*/

        private void GetCurrency()
        {
            using (StreamReader sr = new StreamReader(pCourse))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(" = ");
                    Currency curr = (Currency) Enum.Parse(typeof(Currency), line[0], true);
                    double value = Convert.ToDouble(line[1]);

                    if (_courses.ContainsKey(curr))
                    {
                        _courses[curr] = value;
                    }
                    else
                    {
                        _courses.Add(curr, value);
                    }
                }   
            }
        }
    }
}
