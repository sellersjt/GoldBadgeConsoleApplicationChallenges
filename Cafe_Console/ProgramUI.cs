using Cafe_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_Repository;

namespace Cafe_Console
{
    class ProgramUI
    {
        private MenuRepository _menuRepo = new MenuRepository();

        public void Run()
        {
            SeedMenuList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options to user
                Console.WriteLine("Select a menu option:\n" +
                    "1. View all Menu Items\n" +
                    "2. Add Menu Item\n" +
                    "3. Delete Menu Item\n" +
                    "4. Exit");

                // Get user input
                string input = Console.ReadLine();

                // Evaluate user input and act
                switch (input)
                {
                    case "1":
                        // View all Menu Items
                        Console.Clear();
                        ViewAllMenuItems();
                        break;
                    case "2":
                        // Add menu item
                        Console.Clear();
                        CreateNewMenuItem();
                        break;
                    case "3":
                        // Delete Menu Item
                        Console.Clear();
                        DeleteExistingMenuItem();
                        break;
                    case "4":
                        // Exit
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;
                }
                if (input != "4")
                {
                    Console.WriteLine("Please press any key to continue..");
                }
                else
                {
                    Console.WriteLine("Please press any key to close the program..");
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CreateNewMenuItem()
        {
            Menu newMenuItem = new Menu();

            // Get meal number
            bool inputComplete = false;
            do
            {
                Console.WriteLine("Enter the number for the meal:");
                int mealNumberInput = Utilites.GetIntFromUser();

                if (_menuRepo.GetMenuByMenuNumber(mealNumberInput) == null)
                {
                    newMenuItem.MealNumber = mealNumberInput;
                    inputComplete = true;
                }
                else
                {
                    Console.WriteLine("That number is already used. Please enter another number.");
                }
            } while (inputComplete == false);
            
            
            

            // Get meal name
            Console.WriteLine("Enter the name of the meal:");
            newMenuItem.MealName = Console.ReadLine();

            // Get meal description
            Console.WriteLine("Enter the description of the meal:");
            newMenuItem.MealDescription = Console.ReadLine();

            // Get meal ingredients
            Console.WriteLine("Enter the ingredients of the meal: When done enter a blank line.");
            List<string> _ingredients = new List<string>();
            string input;
            do
            {
                input = Console.ReadLine();
                if (input != "")
                {
                    _ingredients.Add(input);
                }
            }
            while (!string.IsNullOrEmpty(input));

            newMenuItem.Ingredients = _ingredients;

            // Get meal price
            Console.WriteLine("Enter the price for the meal:");
            newMenuItem.MealPrice = Utilites.GetDecimalFromUser();

            _menuRepo.AddMenuToList(newMenuItem);

            Console.WriteLine("Your meal has been added!");
        }

        private void ViewAllMenuItems()
        {
            List<Menu> listOfMenu = _menuRepo.GetMenuList();

            if (listOfMenu.Count == 0)
            {
                Console.WriteLine("Please create a menu.");
            }
            else
            {
                foreach (Menu item in listOfMenu)
                {
                    Console.WriteLine($"Meal Number: {item.MealNumber}\n" +
                        $"Meal Name: {item.MealName}\n" +
                        $"Meal Description: {item.MealDescription}\n" +
                        $"Meal Ingredients: {string.Join(", ", item.Ingredients)}\n" +
                        $"Meal Price: ${item.MealPrice}");
                    Console.WriteLine();
                }
            }
        }

        private void DeleteExistingMenuItem()
        {
            // Show all menu items
            ViewAllMenuItems();

            // Get the number of the item to remove
            Console.WriteLine("Enter the number of the Menu Item to remove.");
            int mealNumberInput = Utilites.GetIntFromUser();

            // Call the delete method
            bool wasDeleted = _menuRepo.RemoveMenuFromList(mealNumberInput);

            // If the item was deleted, say so. otherwise say so.
            if (wasDeleted)
            {
                Console.WriteLine("The menu item was successfully deleted.");
            }
            else
            {
                Console.WriteLine("The menu item could not be deleted");
            }
        }

        private void SeedMenuList()
        {
            Menu combo1 = new Menu(1, "Cheeseburger Combo", "Cheeseburger with frys and drink", new List<string> { "Cheeseburger", "Frys", "Drink" }, 5.99m);
            Menu combo2 = new Menu(2, "Hamburger Combo", "Hamburger with frys and drink", new List<string> { "Hamburger", "Frys", "Drink" }, 5.49m);
            Menu combo3 = new Menu(3, "Double Cheeseburger Combo", "Double Cheeseburger with frys and drink", new List<string> { "Double Cheeseburger", "Frys", "Drink" }, 6.49m);

            combo1.AddIngredient("Icecream Cone");
            combo1.MealDescription += " and Icecream Cone";

            _menuRepo.AddMenuToList(combo1);
            _menuRepo.AddMenuToList(combo2);
            _menuRepo.AddMenuToList(combo3);
        }
    }
}
