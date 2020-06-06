using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertsJeanai_ConvertedData
{
    class Validation
    {
        public static int GetInt(string input)
        {
            int validatedInt;

            while (!(int.TryParse(input, out validatedInt)))
            {
                Console.WriteLine("Please enter a valid value");
                input = Console.ReadLine();
            }

            return validatedInt;
        }

        public static float GetFloat(string input)
        {
            float validatedFloat;
            
            while(!(float.TryParse(input, out validatedFloat)))
            {
                Console.WriteLine("Please enter a valid value");
                input = Console.ReadLine();
            }

            return validatedFloat;
        }

        public static string IsEmpty(string input)
        {
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Please do not leave blank");
                input = Console.ReadLine();
            }

            return input;
        }

        public static void ReturnToMain()
        {
            Console.WriteLine("Press any key to reutn to Menu");
            Console.ReadKey();
        }

    }
}
