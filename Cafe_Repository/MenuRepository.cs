using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Repository
{
    public class MenuRepository
    {
        private readonly List<Menu> _listOfMenu = new List<Menu>();


        // List
        public List<Menu> GetMenuList()
        {
            return _listOfMenu;
        }

        // Add
        public void AddMenuToList(Menu menu)
        {
            _listOfMenu.Add(menu);
        }

        // Delete
        public bool RemoveMenuFromList(int mealNumber)
        {
            Menu item = GetMenuByMenuNumber(mealNumber);

            if (item == null)
            {
                return false;
            }

            int initialCount = _listOfMenu.Count;
            _listOfMenu.Remove(item);

            if (initialCount > _listOfMenu.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method
        public Menu GetMenuByMenuNumber(int menuNumber)
        {
            foreach (Menu item in _listOfMenu)
            {
                if (item.MealNumber == menuNumber)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
