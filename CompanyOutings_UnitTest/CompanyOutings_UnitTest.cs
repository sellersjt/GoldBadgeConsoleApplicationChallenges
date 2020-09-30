using System;
using CompanyOutings_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyOutings_UnitTest
{
    [TestClass]
    public class CompanyOutings_UnitTest
    {
        private OutingRepository _outingRepo = new OutingRepository();

        [TestMethod]
        public void TotalEventCostTest()
        {
            // populate outing
            Outing testOuting = new Outing(TypeOfEvent.Concert, 200, new DateTime(2020, 09, 12), 35.70m);

            // test TotalEventCost set
            Assert.AreEqual(7140, testOuting.TotalEventCost);
        }

        [TestMethod]
        public void GetTest()
        {
            // populate outings
            SeedOutingList();

            // test GetListOfOutings()
            Assert.AreEqual(6, _outingRepo.GetListOfOutings().Count);
        }

        [TestMethod]
        public void AddTest()
        {
            // populate outings
            SeedOutingList();

            // test AddOutingToList()
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Concert, 200, new DateTime(2020, 09, 12), 35.70m));
            Assert.AreEqual(7, _outingRepo.GetListOfOutings().Count);
        }

        [TestMethod]
        public void CombinedOutingCostTest()
        {
            // populate outings
            SeedOutingList();

            // test GetCombinedOutingCost()
            Assert.AreEqual(11817, _outingRepo.GetCombinedOutingCost());
        }

        [TestMethod]
        public void GetCostByOutingTypeTest()
        {
            // populate outings
            SeedOutingList();

            // test GetCostByOutingType()
            Assert.AreEqual(550, _outingRepo.GetCostByOutingType(TypeOfEvent.Bowling));
            Assert.AreEqual(2687, _outingRepo.GetCostByOutingType(TypeOfEvent.Golf));
        }

        public void SeedOutingList()
        {
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Golf, 50, new DateTime(2020, 07, 14), 45.70m));
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Concert, 200, new DateTime(2020, 09, 12), 35.70m));
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Bowling, 10, new DateTime(2020, 05, 10), 25.00m));
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Bowling, 12, new DateTime(2020, 06, 16), 25.00m));
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.Golf, 8, new DateTime(2020, 07, 10), 50.25m));
            _outingRepo.AddOutingToList(new Outing(TypeOfEvent.AmusementPark, 45, new DateTime(2020, 08, 20), 32.00m));
        }
    }
}
