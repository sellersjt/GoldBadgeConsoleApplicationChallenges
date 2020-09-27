using System;
using Cafe_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Security;

namespace Cafe_UnitTest
{
    [TestClass]
    public class Cafe_UnitTest
    {
        public MenuRepository _menuRepo = new MenuRepository();

        [TestMethod]
        public void TestMethod1()
        {
            // seed menu list
            Menu combo1 = new Menu(1, "Cheeseburger Combo", "Cheeseburger with frys and drink", new List<string> { "Cheeseburger", "Frys", "Drink" }, 5.99m);
            Menu combo2 = new Menu(2, "Hamburger Combo", "Hamburger with frys and drink", new List<string> { "Hamburger", "Frys", "Drink" }, 5.49m);
            Menu combo3 = new Menu(3, "Double Cheeseburger Combo", "Double Cheeseburger with frys and drink", new List<string> { "Double Cheeseburger", "Frys", "Drink" }, 6.49m);

            _menuRepo.AddMenuToList(combo1);
            _menuRepo.AddMenuToList(combo2);
            _menuRepo.AddMenuToList(combo3);



            // check GetCMenuList()
            List<Menu> listOfMenu = _menuRepo.GetMenuList();
            int menuCount = listOfMenu.Count;
            Assert.AreEqual(3, menuCount);

            // check AddMenuToList()
            Menu combo4 = new Menu(4, "Chicken Sandwich Combo", "Chicken Sandwich with waffle frys and drink", new List<string> { "Chicken Sandwich", "Waffle Frys", "Drink" }, 7.50m);
            _menuRepo.AddMenuToList(combo4);
            menuCount = _menuRepo.GetMenuList().Count;
            Assert.AreEqual(4, menuCount);

            // check RemoveMenuFromList()
            Assert.IsTrue(_menuRepo.RemoveMenuFromList(1));

            // check GetMenuByMenuNumber()
            Assert.AreEqual("Hamburger Combo", _menuRepo.GetMenuByMenuNumber(2).MealName);
        }
    }
}
