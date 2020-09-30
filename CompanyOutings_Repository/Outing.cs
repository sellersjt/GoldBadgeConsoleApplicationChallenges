using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutings_Repository
{
    public enum TypeOfEvent { Golf = 1, Bowling, AmusementPark, Concert }
    public class Outing
    {
        public TypeOfEvent EventType { get; set; }
        public int Attendance { get; set; }
        public DateTime EventDate { get; set; }
        public decimal PerPersonCost { get; set; }
        public decimal TotalEventCost { get; }


        public Outing(TypeOfEvent eventType, int attendance, DateTime eventDate, decimal perPersonCost )
        {
            EventType = eventType;
            Attendance = attendance;
            EventDate = eventDate;
            PerPersonCost = perPersonCost;
            TotalEventCost = attendance * perPersonCost;
        }
    }
}
