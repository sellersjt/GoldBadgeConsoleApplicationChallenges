using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutings_Repository
{
    public class OutingRepository
    {
        private readonly List<Outing> _listOfOutings = new List<Outing>();

        //return list of outings
        public List<Outing> GetListOfOutings()
        {
            return _listOfOutings;
        }

        // add outing to list
        public void AddOutingToList( Outing _outing)
        {
            _listOfOutings.Add(_outing);
        }

        // return combined cost for all outings
        public decimal GetCombinedOutingCost()
        {
            decimal _totalCost = 0;
            foreach (Outing item in _listOfOutings)
            {
                _totalCost += item.TotalEventCost;
            }
            return _totalCost;
        }

        // return cost by outing type
        public decimal GetCostByOutingType(TypeOfEvent _type)
        {
            decimal _eventTypeCost = 0;
            foreach (Outing item in _listOfOutings)
            {
                if (item.EventType == _type)
                {
                    _eventTypeCost += item.TotalEventCost;
                }
            }
            return _eventTypeCost;
        }
    }
}
