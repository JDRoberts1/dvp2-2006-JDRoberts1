using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;


namespace RobertsJeanai_ConvertedData
{
    class JSONConvert
    {
        // output location
        static string RestaurantProfilesFolder = @"..\..\..\RobertsJeanai_ConvertedData.json";
        
        MySqlConnection _conn = null;

        public static void ConvertJSON()
        {
            // New istance of the class
            JSONConvert instance = new JSONConvert();
            
            // set an instance of the connection
            instance._conn = new MySqlConnection();
            
            // call the Connect Function which calss the Build Connection funstion inside
            instance.Connect();

            // Fields needed for query
            // id, RestaurantName, Address, Phone, HoursOfOperation, Price, USACityLocation
            // Cuisine, FoodRating, ServiceRating, AmbienceRating, ValueRating, OverallRating, OverallPossibleRating

            DataTable data = instance.DBQuery("SELECT id, RestaurantName, Address, Phone, HoursOfOperation, Price, USACityLocation, Cuisine, FoodRating, ServiceRating, AmbienceRating, ValueRating, OverallRating, OverallPossibleRating FROM RestaurantProfiles;");

            DataRowCollection rows = data.Rows;

            // Close database so it can be used again
            instance._conn.Close();

            // Send data to Convert Function
            ConvertToJSON(data);

            // Return to main menu
            Validation.ReturnToMain();
        }

        // Build connection method
        void BuildConnection()
        {
            // set variable for ipAddress
            string ip = "";

            // pull ip address from the file
            using (StreamReader sr = new StreamReader("c:/VFW/connect.txt"))
            {
                ip = sr.ReadLine();
            }

            // Hard coded login
            string conString = $"Server={ip};";
            conString += "user id = dbsAdmin;";
            conString += "password = password;";
            conString += "database=SampleRestaurantDatabase;";
            conString += "port=8889;";

            _conn.ConnectionString = conString;
        }

        // Connect to the database
        void Connect()
        {
            // connection string
            BuildConnection();

            // try to connect
            try
            {
                _conn.Open();
                Console.WriteLine("Connection Successful");
            }
            // display exception error if something goes wrong
            catch (MySqlException e)
            {
                string msg = "";
                switch (e.Number)
                {
                    case 0:
                        {
                            msg = e.ToString();
                            break;
                        }
                    case 1042:
                        {
                            msg = "Cant resolve host address.\n" + _conn.ConnectionString;
                            break;
                        }
                    case 1045:
                        {
                            msg = "invalid username/password";
                            break;
                        }
                    default:
                        {
                            msg = e.ToString();
                            break;
                        }
                }

                Console.WriteLine(msg);
            }

        }

        // build data table from information returned from query
        DataTable DBQuery(string query)
        {
            // Create connection with connection string and query 
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, _conn);

            // create the data table
            DataTable data = new DataTable();

            adapter.SelectCommand.CommandType = CommandType.Text;

            // fill the table with the returned information
            adapter.Fill(data);

            // return datatable to Main method in this class
            return data;
        }

        // Convert Data table to JSON file
        private static void ConvertToJSON(DataTable restaurantProfiles)
        {
            
            if(restaurantProfiles.Rows.Count > 0)
            {
                //Start of the JSON array
                string JSON = "[";

                // try to convert to JSON
                try
                {
                    // Alert user the the converion has begun
                    Console.WriteLine($" RestaurantProfiles table is now being converted");

                    for (int i = 0; i < restaurantProfiles.Rows.Count; i++)
                    {

                        JSON += "\n\t{";

                        

                        for (int j = 0; j < restaurantProfiles.Columns.Count; j++)
                        {
                            if (j <= restaurantProfiles.Columns.Count - 1)
                            {
                                JSON += "\"" + restaurantProfiles.Columns[j].ColumnName.ToString() + "\":" + "\"" + restaurantProfiles.Rows[i][j].ToString() + "\",";
                            }
                            
                        }

                        if (i == restaurantProfiles.Rows.Count - 1)
                        {
                            JSON += "}";
                        }
                        else
                        {
                            JSON += "},";
                        }

                    }

                    JSON += "]";

                    // End of array
                    JSON += "\n\t)";

                    // Writing file data to output.json file
                    File.WriteAllText(RestaurantProfilesFolder + restaurantProfiles + ".json", JSON);

                    // Alert user that file has been created
                    Console.WriteLine("Convert complete, File created");

                }
                // if exception occurs during conversion display the error
                catch (Exception ex)

                {

                    Console.WriteLine("Exception occured: " + ex.ToString());

                }
            }

        }

    }
}
