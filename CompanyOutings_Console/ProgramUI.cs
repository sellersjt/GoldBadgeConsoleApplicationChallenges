using CompanyOutings_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_Repository;

namespace CompanyOutings_Console
{
    class ProgramUI
    {
        private OutingRepository _outingRepo = new OutingRepository();

        public void Run()
        {
            SeedOutingList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options to user
                Console.WriteLine("Select a menu option:\n" +
                    "1. List all outings\n" +
                    "2. Add an outing\n" +
                    "3. Show outings cost\n" +
                    "4. Exit");

                // Get user input
                string input = Console.ReadLine();

                // Evaluate user input and act
                switch (input)
                {
                    case "1":
                        // List all outings
                        Console.Clear();
                        ViewAllOutings();
                        break;
                    case "2":
                        // Add an outing
                        Console.Clear();
                        CreateNewOuting();
                        break;
                    case "3":
                        // show outing cost
                        Console.Clear();
                        ShowOutingCost();
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
                    Console.WriteLine("Please press any key to continue.");
                }
                else
                {
                    Console.WriteLine("Please press any key to close the program.");
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ViewAllOutings()
        {
            // get list of outings
            List<Outing> listOfOutings = _outingRepo.GetListOfOutings();

            // if list is empty display message and ask to add outing
            if (listOfOutings.Count == 0 || listOfOutings == null)
            {
                Console.WriteLine("There are not any outings to show. Would you like to add one?");
                char YOrN = Utilites.GetYOrNFromUser();
                if (YOrN == 'y')
                {
                    CreateNewOuting();
                }
            }
            // show outings
            else
            {
                // sort outins by date
                listOfOutings.Sort((x, y) => x.EventDate.CompareTo(y.EventDate));

                Console.WriteLine(String.Format("{0,-12} {1,-20} {2,-15}", "Event Date:", "Event Type:", "Attendance"));
                foreach (Outing item in listOfOutings)
                {
                    Console.WriteLine(String.Format("{0,-12} {1,-20} {2,-15}", String.Format("{0:MM/dd/yy}", item.EventDate), item.EventType, item.Attendance));
                }
                Console.WriteLine();
            }
        }

        private void CreateNewOuting()
        {
            // get date of outing
            Console.Write("Enter the date Of outing (MM/dd/yyyy): ");
            DateTime newOutingDate = Utilites.GetDateFromUser(); 

            // get event type and make sure it is valid
            int newEventType;
            do
            {
                Console.WriteLine("Enter the type Of outing: ");
                foreach (var eventType in Enum.GetValues(typeof(TypeOfEvent)))
                {
                    Console.WriteLine($"{(int)eventType} for {eventType}.");
                }
                newEventType = Utilites.GetIntFromUser();
                if (Enum.IsDefined(typeof(TypeOfEvent), newEventType) == false)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            } while (Enum.IsDefined(typeof(TypeOfEvent),newEventType) == false);

            // get Attendance
            Console.Write("Enter the number in attendance: ");
            int newAttendance = Utilites.GetIntFromUser();

            // get cost per person
            Console.Write("Enter the cost per person: ");
            decimal newCostPerPerson = Utilites.GetDecimalFromUser();

            // add event to list
            _outingRepo.AddOutingToList(new Outing((TypeOfEvent)newEventType, newAttendance, newOutingDate, newCostPerPerson));

            // show event was added
            Console.WriteLine("The event was added.");
        }

        private void ShowOutingCost()
        {
            Console.WriteLine("Company Outings cost:\n");

            // show cost by type
            Console.WriteLine("Cost by event type:");
            foreach (var eventType in Enum.GetValues(typeof(TypeOfEvent)))
            {
                Console.WriteLine(String.Format("{0,-15} {1,-10:C2}", eventType, _outingRepo.GetCostByOutingType((TypeOfEvent)eventType)));
            }

            // show total combined cost
            Console.WriteLine("\nCombined cost for all outings is {0:C2}.", _outingRepo.GetCombinedOutingCost());
            Console.WriteLine();
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
