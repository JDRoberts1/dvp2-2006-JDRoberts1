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
        // Build connection method
        void BuildConnection()
        {
            // set variable for ipAddress
            string ip = "";

            using (StreamReader sr = new StreamReader("c:/VFW/conn.txt"))
            {
                ip = sr.ReadLine();
            }

            string conString = $"Server={ip};";
            conString += "username = dbsAdmin;";
            conString += "password = password;";
            conString += "";
        }
    }
}
