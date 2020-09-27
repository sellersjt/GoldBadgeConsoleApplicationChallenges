using Claims_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_Repository;

namespace Claims_Console
{
    class ProgramUI
    {
        private ClaimRepository _unprocessedClaims = new ClaimRepository();
        private ClaimRepository _processedClaims = new ClaimRepository();

        public void Run()
        {
            SeedClaimList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                // Display options to user
                Console.WriteLine("Select a menu option:\n" +
                    "1. See all claims\n" +
                    "2. Take care of next claim\n" +
                    "3. Enter a new claim\n" +
                    "4. Exit");

                // Get user input
                string input = Console.ReadLine();

                // Evaluate user input and act
                switch (input)
                {
                    case "1":
                        // See all claims
                        Console.Clear();
                        ViewAllClaims();
                        break;
                    case "2":
                        // Take care of next claim
                        Console.Clear();
                        ProcessClaim();
                        break;
                    case "3":
                        // Enter a new claim
                        Console.Clear();
                        CreateNewClaim();
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

        private void ViewAllClaims()
        {
            // get list of claims
            List<Claim> listOfClaims = _unprocessedClaims.GetListOfClaims();

            // if list is empty display message and ask to add claim
            if (listOfClaims.Count == 0 || listOfClaims == null)
            {
                Console.WriteLine("There are not any claims to show. Would you like to add one?");
                char YOrN = Utilites.GetYOrNFromUser();
                if (YOrN == 'y')
                {
                    CreateNewClaim();
                }
            }
            // show list of claims
            else
            {
                var sb = new StringBuilder();
                sb.Append(String.Format("{0,-10} {1,-10} {2,-25} {3,-8} {4,-15} {5,-15} {6,-8}\n", "ClaimID", "Type", "Description", "Amount", "DateOfAccident", "DateOfClaim", "IsValid"));
                foreach (Claim item in listOfClaims)
                {
                    sb.Append(String.Format("{0,-10} {1,-10} {2,-25} {3,-8} {4,-15} {5,-15} {6,-8}\n", item.ClaimID, item.ClaimType, item.Description, item.ClaimAmount, String.Format("{0:MM/dd/yy}", item.DateOfIncident), String.Format("{0:MM/dd/yy}", item.DateOfClaim), item.IsValid));
                }
                Console.WriteLine(sb);
            }

        }

        private void ProcessClaim()
        {
            // get list of claims
            List<Claim> listOfClaims = _unprocessedClaims.GetListOfClaims();

            // if list is empty display message and ask to add claim
            if (listOfClaims.Count == 0 || listOfClaims == null)
            {
                Console.WriteLine("There are not any claims to show. Would you like to add one?");
                char YOrN = Utilites.GetYOrNFromUser();
                if (YOrN == 'y')
                {
                    CreateNewClaim();
                }
            }
            // show next claim
            else
            {
                Console.WriteLine("Here are the details for the next claim to be handled:");
                Console.WriteLine($"ClaimID: {listOfClaims[0].ClaimID}");
                Console.WriteLine($"Type: {listOfClaims[0].ClaimType}");
                Console.WriteLine($"Description: {listOfClaims[0].Description}");
                Console.WriteLine($"Amount: {listOfClaims[0].ClaimAmount}");
                Console.WriteLine($"DateOfAccident: {String.Format("{0:MM/dd/yy}", listOfClaims[0].DateOfIncident)}");
                Console.WriteLine($"DateOfClaim: {String.Format("{0:MM/dd/yy}", listOfClaims[0].DateOfClaim)}");
                Console.WriteLine($"IsValid: {listOfClaims[0].IsValid}");
                Console.WriteLine();
                // ask to process claim
                Console.WriteLine("Do you want to deal with this claim now(y/n)?");
                char YOrN = Utilites.GetYOrNFromUser();
                if (YOrN == 'y')
                {
                    // move claim to _processedClaims
                    _processedClaims.AddClaimToList(listOfClaims[0]);
                    // remove claim from _unprocessedClaims
                    if (_unprocessedClaims.RemoveClaimFromList(listOfClaims[0].ClaimID))
                    {
                        Console.WriteLine("The claim has been processed.");
                    }
                }
            }
        }

        private void CreateNewClaim()
        {
            // get claim ID
            Console.Write("Enter the claim id: ");
            // check if ID exist
            int newClaimID;
            bool doesClaimIDExist;
            do
            {
                newClaimID = Utilites.GetIntFromUser();
                doesClaimIDExist = _unprocessedClaims.GetClaimByClaimID(newClaimID) == null ? false : true;
                if (doesClaimIDExist)
                {
                    Console.WriteLine($"Claim ID {newClaimID} is already used. Please use another number.");
                }
            } while (doesClaimIDExist);

            //get claim type
            Console.Write("Enter the claim type");
            foreach (var item in Enum.GetValues(typeof(TypeOfClaim)))
            {
                Console.Write(" ({0} - {1})",(int)item, item.ToString());
            }
            Console.Write(": ");
            // check if typeOfClaim is defined
            TypeOfClaim newClaimType;
            bool doesClaimTypeExist;
            do
            {
                newClaimType = (TypeOfClaim)Utilites.GetIntFromUser();
                doesClaimTypeExist = Enum.IsDefined(typeof(TypeOfClaim), newClaimType);
                if (doesClaimTypeExist == false)
                {
                    Console.WriteLine("Please enter a valid claim type.");
                }
            } while (doesClaimTypeExist == false);

            // get claim description
            Console.Write("Enter a claim description: ");
            string newClaimDescription = Console.ReadLine();

            // get claim amount
            Console.Write("Amount of Damage: ");
            decimal newClaimAmount = Utilites.GetDecimalFromUser();

            // get date of accident
            Console.Write("Date Of Accident: ");
            DateTime newClaimDateOfAccident = Utilites.GetDateFromUser();

            // get date of claim
            Console.Write("Date of Claim: ");
            DateTime newClaimDateOfClaim = Utilites.GetDateFromUser();

            // show if claim is valid
            if (newClaimDateOfClaim <= newClaimDateOfAccident.AddDays(30))
            {
                Console.WriteLine("This claim is valid.");
            }
            else
            {
                Console.WriteLine("This claim is not valid.");
            }

            _unprocessedClaims.AddClaimToList(new Claim(newClaimID, newClaimType, newClaimDescription, newClaimAmount, newClaimDateOfAccident, newClaimDateOfClaim));

            Console.WriteLine("\nThe claim has been added.");
        }

        private void SeedClaimList()
        {
            Claim testClaim1 = new Claim(1, TypeOfClaim.Car, "Car accident on 465.", 400.00m, new DateTime(2018, 4, 25), new DateTime(2018, 4, 27));
            Claim testClaim2 = new Claim(2, TypeOfClaim.Home, "House fire in kitchen.", 4000.00m, new DateTime(2018, 4, 11), new DateTime(2018, 4, 12));
            Claim testClaim3 = new Claim(3, TypeOfClaim.Theft, "Stolen pancakes.", 4.00m, new DateTime(2018, 4, 27), new DateTime(2018, 6, 1));

            _unprocessedClaims.AddClaimToList(testClaim1);
            _unprocessedClaims.AddClaimToList(testClaim2);
            _unprocessedClaims.AddClaimToList(testClaim3);
        }
    }
}
