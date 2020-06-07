using System;

namespace RobertsJeanai_ConvertedData
{
    class Program
    {
        static void Main(string[] args)
        {
            // verify code works on MAC

            //Database Location
            //string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";

            //Output Location
            //string _directory = @"../../output/";

            bool programIsRunning = true;
            while (programIsRunning)
            {
                MenuClass.MainMenu();
                Console.WriteLine("Please select an option");
                string userOption = Validation.IsEmpty();

                switch (userOption)
                {
                    case "1":
                    case "convert the restaurant profile table from sql to json":
                        {
                            // Call the Convert class
                            JSONConvert.ConvertJSON();
                        }
                        break;
                    case "2":
                    case "showcase our 5 star rating system":
                        {
                            
                        }
                        break;
                    case "3":
                    case "showcase our animated bar graph review system":
                        {
                            
                        }
                        break;
                    case "4":
                    case "play a card game":
                        {

                        }
                        break;
                    case "5":
                    case "exit":
                        {
                            Console.Clear();
                            programIsRunning = false;
                        }
                        break;
                }
                
            }
            

        }
    }
}
