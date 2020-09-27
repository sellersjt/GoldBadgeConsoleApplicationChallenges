using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility_Repository
{
    public static class Utilites
    {
        public static int GetIntFromUser()
        {
            int input;
            do
            {
                bool parseSuccess = int.TryParse(Console.ReadLine(), out input);
                if (parseSuccess == false)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            while (input == 0);
            return input;
        }

        public static decimal GetDecimalFromUser()
        {
            decimal input;
            do
            {
                bool parseSuccess = decimal.TryParse(Console.ReadLine(), out input);
                if (parseSuccess == false)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            while (input == 0);
            return input;
        }

        public static char GetYOrNFromUser()
        {
            bool inputComplete = false;
            char output = 'n';

            Console.WriteLine("Please enter y or n:");

            while (inputComplete == false)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "y")
                {
                    inputComplete = true;
                    output = 'y';
                }
                else if (input.ToLower() == "n")
                {
                    inputComplete = true;
                    output = 'n';
                }
                else
                {
                    Console.WriteLine("Please enter y or n.");
                }
            }

            return output;
        }

        public static DateTime GetDateFromUser()
        {
            string input;
            DateTime output = new DateTime();
            bool dateCorrect = false;
            CultureInfo provider = CultureInfo.InvariantCulture;
            do
            {
                input = Console.ReadLine();
                try
                {
                    output = DateTime.ParseExact(input, "d", provider);
                    dateCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format. Try MM/dd/yyyy.", input);
                }
            } while (dateCorrect == false);
            return output;
        }
    }
}
