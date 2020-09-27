using Badges_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_Repository;

namespace Badges_Console
{
    class ProgramUI
    {
        private BadgeRepository _badgeRepo = new BadgeRepository();

        public void Run()
        {
            SeedBadgeDictionary();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options to user
                Console.WriteLine("Select a menu option:\n" +
                    "1. List all Badges\n" +
                    "2. Add a badge\n" +
                    "3. Remove a badge\n" +
                    "4. Edit a badge\n" +
                    "5. Exit");

                // Get user input
                string input = Console.ReadLine();

                // Evaluate user input and act
                switch (input)
                {
                    case "1":
                        // List all Badges
                        Console.Clear();
                        ViewAllBadges();
                        break;
                    case "2":
                        // Add a badge
                        Console.Clear();
                        CreateNewBadge();
                        break;
                    case "3":
                        // remove a badge
                        Console.Clear();
                        RemoveBadge();
                        break;
                    case "4":
                        // Edit a badge
                        Console.Clear();
                        EditBadge();
                        break;
                    case "5":
                        // Exit
                        Console.WriteLine("Goodbye!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;
                }
                if (input != "5")
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

        private void RemoveBadge()
        {
            // get dictionary of badges
            Dictionary<int, Badge> _badgeDictionary = _badgeRepo.GetBadgeDictionary();

            // get badge ID to remove and check if ID exist
            int badgeIDToRemove;
            bool doesBadgeIDExist;
            do
            {
                Console.Write("What is the badge number to remove? ");
                badgeIDToRemove = Utilites.GetIntFromUser();
                doesBadgeIDExist = _badgeDictionary.ContainsKey(badgeIDToRemove) ? true : false;
                if (doesBadgeIDExist == false)
                {
                    Console.WriteLine($"Badge ID {badgeIDToRemove} does not exist. Would you loke to choose another number?");
                    if (Utilites.GetYOrNFromUser() == 'n')
                    {
                        return;
                    }
                }
            } while (doesBadgeIDExist == false);

            // remove badge from dictionary
            if (_badgeRepo.RemoveBadge(badgeIDToRemove))
            {
                Console.WriteLine($"Badge {badgeIDToRemove} was removed\n.");
            }
            else
            {
                Console.WriteLine("The badge could not be removed.");
            }
        }

        private void ViewAllBadges()
        {
            // get dictionary of badges
            Dictionary<int, Badge> _badgeDictionary = _badgeRepo.GetBadgeDictionary();

            // check if dictionary is empty, if so ask to add badge
            if (_badgeDictionary.Count == 0)
            {
                Console.WriteLine("There are not any badges to show. Would you like to add one?");
                char YOrN = Utilites.GetYOrNFromUser();
                if (YOrN == 'y')
                {
                    CreateNewBadge();
                }
            }
            // show badges
            else
            {
                Console.WriteLine(String.Format("{0,-10} {1,-25}", "Badge #", "Door Access"));
                foreach (var badge in _badgeDictionary.Values)
                {
                    Console.WriteLine(String.Format("{0,-10} {1,-25}", badge.BadgeID, String.Join(", ", badge.DoorNames)));
                }
                Console.WriteLine();
            }
        }

        private void EditBadge()
        {
            // get dictionary of badges
            Dictionary<int, Badge> _badgeDictionary = _badgeRepo.GetBadgeDictionary();

            // get badge ID to edit and check if ID exist
            int badgeIDToEdit;
            bool doesBadgeIDExist;
            do
            {
                Console.Write("What is the badge number to update? ");
                badgeIDToEdit = Utilites.GetIntFromUser();
                doesBadgeIDExist = _badgeDictionary.ContainsKey(badgeIDToEdit) ? true : false;
                if (doesBadgeIDExist == false)
                {
                    Console.WriteLine($"Badge ID {badgeIDToEdit} does not exist. Would you loke to choose another number?");
                    if (Utilites.GetYOrNFromUser() == 'n')
                    {
                        return;
                    }
                }
            } while (doesBadgeIDExist == false);

            // show door access
            Console.WriteLine($"{badgeIDToEdit} has access to doors {String.Join(", ", _badgeDictionary[badgeIDToEdit].DoorNames)}.");

            // ask for edit choice
            Console.WriteLine("What would you like to do?\n" +
                "1. Remove a door\n" +
                "2. Add a door");

            // Get user input
            string input = Console.ReadLine();

            // Evaluate user input and act
            switch (input)
            {
                case "1":
                    // Remove a door

                    //get door to remove and check if door exist
                    string doorToRemove;
                    bool doesDoorExist;
                    do
                    {
                        Console.Write("Which door would you like to remove? ");
                        doorToRemove = Console.ReadLine();
                        doesDoorExist = _badgeDictionary[badgeIDToEdit].DoorNames.Contains(doorToRemove);
                        if (doesDoorExist == false)
                        {
                            Console.WriteLine($"{badgeIDToEdit} does not have access to door {doorToRemove}. Would you like to choose another door?");
                            if (Utilites.GetYOrNFromUser() == 'n')
                            {
                                return;
                            }
                        }

                    } while (doesDoorExist == false);

                    // remove door from list
                    _badgeDictionary[badgeIDToEdit].DoorNames.Remove(doorToRemove);
                    Console.WriteLine($"Access to door {doorToRemove} has been removed from badge {badgeIDToEdit}.\n");

                    break;
                case "2":
                    // Add a door

                    //get door to add
                    Console.Write($"Which door would you like to add access to badge {badgeIDToEdit}? ");
                    string doorToAdd = Console.ReadLine();

                    // add door to list
                    _badgeDictionary[badgeIDToEdit].DoorNames.Add(doorToAdd);
                    Console.WriteLine($"Access to door {doorToAdd} has been added to badge {badgeIDToEdit}.\n");

                    break;
                default:
                    Console.WriteLine("Please enter a valid number");
                    break;
            }
        }

        private void CreateNewBadge()
        {
            // get dictionary of badges
            Dictionary<int, Badge> _badgeDictionary = _badgeRepo.GetBadgeDictionary();

            // get badge ID
            Console.Write("What is the number on the badge: ");

            // check if ID exist
            int newBadgeID;
            bool doesBadgeIDExist;

            do
            {
                newBadgeID = Utilites.GetIntFromUser();
                doesBadgeIDExist = _badgeDictionary.ContainsKey(newBadgeID) ? true : false;
                if (doesBadgeIDExist)
                {
                    Console.WriteLine($"Badge ID {newBadgeID} is already used. Please use another number.");
                }
            } while (doesBadgeIDExist);

            // get door access list
            List<string> newDoorList = new List<string>();
            Char inputNotComplete = 'y';
            do
            {
                Console.Write("List a door that it needs access to: ");
                newDoorList.Add(Console.ReadLine());
                Console.WriteLine("Any other doors(y/n)?");
                inputNotComplete = Utilites.GetYOrNFromUser();
            } while (inputNotComplete != 'n');

            // add badge to dictionary
            _badgeRepo.AddBadge(new Badge(newBadgeID, newDoorList, ""));

            Console.WriteLine("\nThe badge was added.");
        }

        private void SeedBadgeDictionary()
        {
            _badgeRepo.AddBadge(new Badge(1007, new List<string> { "A1", "A5" }, "The Bond Badge"));
            _badgeRepo.AddBadge(new Badge(12345, new List<string> { "A7" }, "Tempory Badge"));
            _badgeRepo.AddBadge(new Badge(22345, new List<string> { "A1","A7", "B4" }, "Tempory Badge"));
        }
    }
}
