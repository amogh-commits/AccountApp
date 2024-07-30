using System;
using System.Collections.Generic;
using System.IO;
using AccountApp.Model;
using System.Xml;

using Newtonsoft.Json;
using AccountLibrary.Model;

namespace AccountApp.Model
{
    public class SerialDeserial
    {
        public static void SerializeData(List<Account> accounts)
        {
            string fileName = "AccountData.json";
            string json = JsonConvert.SerializeObject(accounts, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fileName, json);
            Console.WriteLine("Data serialized and saved to AccountData.json.");
        }

        public static List<Account> DeserializeData()
        {
            List<Account> accounts = new List<Account>();
            string fileName = "AccountData.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                Console.WriteLine("Data deserialized from AccountData.json.");
            }
            else
            {
                Console.WriteLine("AccountData.json not found. Returning empty list.");
            }
            return accounts;
        }
    }
}
