using System;
using System.Security.Cryptography.X509Certificates;
using Claims_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Claims_UnitTest
{
    [TestClass]
    public class Claims_UnitTest
    {
        public ClaimRepository _claimRepo = new ClaimRepository();

        [TestMethod]
        public void TestMethod1()
        {
            // test claim isValid
            Claim testClaim1 = new Claim(1, TypeOfClaim.Car, "test", 555.00m, new DateTime(2018,4,27), new DateTime(2018,6,1));
            Assert.IsFalse(testClaim1.IsValid);

            Claim testClaim2 = new Claim(2, TypeOfClaim.Car, "test", 555.00m, new DateTime(2018, 4, 25), new DateTime(2018, 4, 27));
            Assert.IsTrue(testClaim2.IsValid);

            // test GetListOfClaims()
            Assert.AreEqual(0, _claimRepo.GetListOfClaims().Count);

            // test AddClaimToList()
            _claimRepo.AddClaimToList(testClaim1);
            _claimRepo.AddClaimToList(testClaim2);
            Assert.AreEqual(2, _claimRepo.GetListOfClaims().Count);

            // test GetClaimByClaimID()
            Assert.AreEqual(1, _claimRepo.GetClaimByClaimID(1).ClaimID);

            // test RemoveClaimFromList()
            Assert.IsTrue(_claimRepo.RemoveClaimFromList(1));
        }
    }
}
