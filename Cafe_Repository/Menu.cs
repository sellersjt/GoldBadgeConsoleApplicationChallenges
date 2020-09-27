using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Repository
{
    public class Menu
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }

        private List<string> _ingredients = new List<string>();
        public List<string> Ingredients
        {
            get
            {
                return _ingredients;
            }
            set
            {
                _ingredients = value;
            }
        }
        public void AddIngredient(string line)
        {
            Ingredients.Add(line);
        }
        public decimal MealPrice { get; set; }



        public Menu() { }

        public Menu(int mealNumber, string mealName, string mealDescription, List<string> ingredients, decimal mealPrice)
        {
            MealNumber = mealNumber;
            MealName = mealName;
            MealDescription = mealDescription;
            _ingredients = ingredients;
            MealPrice = mealPrice;
        }

    }
}
