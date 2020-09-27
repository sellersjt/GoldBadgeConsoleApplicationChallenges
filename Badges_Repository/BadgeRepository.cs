using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges_Repository
{
    public class BadgeRepository
    {
        private readonly Dictionary<int, Badge> _badgeDictionary = new Dictionary<int, Badge>();

        // add badge
        public void AddBadge(Badge _badge)
        {
            _badgeDictionary.Add(_badge.BadgeID, _badge);
        }

        // return badge dictionary
        public Dictionary<int, Badge> GetBadgeDictionary()
        {
            return _badgeDictionary;
        }

        // remove badge
        public bool RemoveBadge(int key)
        {
            if (_badgeDictionary.Count == 0)
            {
                return false;
            }
            int initialCount = _badgeDictionary.Count;
            _badgeDictionary.Remove(key);

            if (initialCount > _badgeDictionary.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
