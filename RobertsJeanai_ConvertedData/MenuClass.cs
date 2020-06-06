using System;
using System.Collections.Generic;
using System.Text;

namespace RobertsJeanai_ConvertedData
{
    class MenuClass
    {
        public static void MainMenu()
        {
            
            Console.WriteLine("Hello Admin, What would you like to do today?");

            // Create list of menu options
            List<string> mainMenuList = new List<string>();

            // Add menu options to the list
            mainMenuList.Add("Convert The Restaurant Profile Table From SQL To JSON");
            mainMenuList.Add("Showcase Our 5 Star Rating System");
            mainMenuList.Add("Showcase Our Animated Bar Graph Review System");
            mainMenuList.Add("Play A Card Game");
            mainMenuList.Add("Exit");

            for(int counter = 0; counter < mainMenuList.Count; counter++)
            {
                Console.WriteLine($"{counter + 1}: {mainMenuList[counter]}");
            }
        }
    }

    
}
