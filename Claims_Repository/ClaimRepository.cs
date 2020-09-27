using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims_Repository
{
    public class ClaimRepository
    {
        private readonly List<Claim> _listOfClaims = new List<Claim>();

        // List claims
        public List<Claim> GetListOfClaims()
        {
            return _listOfClaims;
        }

        // Add claim
        public void AddClaimToList(Claim _claim)
        {
            _listOfClaims.Add(_claim);
        }

        // Remove claim
        public bool RemoveClaimFromList(int claimID)
        {
            Claim item = GetClaimByClaimID(claimID);

            if (item == null)
            {
                return false;
            }

            int initialCount = _listOfClaims.Count;
            _listOfClaims.Remove(item);

            if (initialCount > _listOfClaims.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper method
        public Claim GetClaimByClaimID(int _claimID)
        {
            foreach (Claim item in _listOfClaims)
            {
                if (item.ClaimID == _claimID)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
