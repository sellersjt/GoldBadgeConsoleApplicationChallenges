using System;
using System.Collections.Generic;
using Badges_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Badges_UnitTest
{
    [TestClass]
    public class Badges_UnitTest
    {
        private BadgeRepository _badgeRepo = new BadgeRepository();

        [TestMethod]
        public void TestMethod1()
        {
            // test GetBadgeDictionary()
            Assert.AreEqual(0, _badgeRepo.GetBadgeDictionary().Count);

            // test AddBadge()
            Badge testBadge1 = new Badge(1007, new List<string> { "A1", "A5" }, "The Bad Badge");
            _badgeRepo.AddBadge(testBadge1);
            Assert.AreEqual(1, _badgeRepo.GetBadgeDictionary().Count);

            // test RemoveBadge()
            _badgeRepo.RemoveBadge(1007);
            Assert.AreEqual(0, _badgeRepo.GetBadgeDictionary().Count);
        }
    }
}
