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
        static string RestaurantProfilesFolder = @"..\..\..\RestaurantProfiles";
        MySqlConnection _conn = null;

        public static void ConvertJSON()
        {
            JSONConvert instance = new JSONConvert();
            Directory.CreateDirectory(RestaurantProfilesFolder);
            instance._conn = new MySqlConnection();
            instance.Connect();

            // id, RestaurantName, Address, Phone, HoursOfOperation, Price, USACityLocation
            // Cuisine, FoodRating, ServiceRating, AmbienceRating, ValueRating, OverallRating, OverallPossibleRating

            DataTable data = instance.DBQuery("SELECT RestaurantName, Address, Phone, HoursOfOperation, Price, USACityLocation, Cuisine, FoodRating, ServiceRating, AmbienceRating, ValueRating, OverallRating, OverallPossibleRating FROM RestaurantProfiles LIMIT 2;");

            DataRowCollection rows = data.Rows;

            // display to console to test if it works
            foreach (DataRow row in rows)
            {
                
                Console.WriteLine($"Restaurant Name: {row["RestaurantName"].ToString()}");
                Console.WriteLine($"Address: {row["Address"].ToString()}");
                Console.WriteLine($"Phone: {row["Phone"].ToString()}");
                Console.WriteLine($"Hours of Operation: {row["HoursOfOperation"].ToString()}");
                Console.WriteLine($"Price: {row["Price"].ToString()}");
                Console.WriteLine($"City Location: {row["USACityLocation"].ToString()}");
                Console.WriteLine($"Cuisine: {row["Cuisine"].ToString()}");
                Console.WriteLine($"Food Rating: {row["FoodRating"].ToString()}");
                Console.WriteLine($"Service Rating: {row["ServiceRating"].ToString()}");
                Console.WriteLine($"Ambience Rating: {row["AmbienceRating"].ToString()}");
                Console.WriteLine($"Value Rating: {row["ValueRating"].ToString()}");
                Console.WriteLine($"Overall Rating: {row["OverallRating"].ToString()}");

            }

            

            instance._conn.Close();
           

            Load(data);

            Validation.ReturnToMain();
        }

        // Build connection method
        void BuildConnection()
        {
            // set variable for ipAddress
            string ip = "";

            using (StreamReader sr = new StreamReader("c:/VFW/connect.txt"))
            {
                ip = sr.ReadLine();
            }

            string conString = $"Server={ip};";
            conString += "user id = dbsAdmin;";
            conString += "password = password;";
            conString += "database=SampleRestaurantDatabase;";
            conString += "port=8889;";

            _conn.ConnectionString = conString;
        }

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

        DataTable DBQuery(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, _conn);

            DataTable data = new DataTable();

            adapter.SelectCommand.CommandType = CommandType.Text;

            adapter.Fill(data);

            return data;
        }

        private static void Load(DataTable restaurantProfiles)
        {
            
            if(restaurantProfiles.Rows.Count > 0)
            {
                //Start of the JSON array
                string JSON = "[";

                try
                {

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

                    // Alert user that file is create
                    Console.WriteLine("Convert complete, Output file created");

                }

                catch (Exception ex)

                {

                    Console.WriteLine("Exception occured: " + ex.ToString());

                }
            }

        }

    }
}
