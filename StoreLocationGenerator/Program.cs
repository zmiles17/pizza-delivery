using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace StoreLocationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader streamReader = new StreamReader("C:/Users/ZMiles/Documents/mcdonalds-locations.json"))
            {
                using (TextWriter writer = new StreamWriter("C:/Users/ZMiles/Documents/stores-insert.sql"))
                {
                    string json = streamReader.ReadToEnd();
                    JsonTextReader reader = new JsonTextReader(new StringReader(json));
                    Dictionary<string, string> v = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        if (reader.Value != null)
                        {

                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                var property = reader.Value;
                                reader.Read();
                                if (reader.Value != null)
                                {
                                    var value = reader.Value.ToString().Replace("'", "''");
                                    switch (property)
                                    {
                                        case "storeNumber":
                                            v.Add("StoreNumber", $"'{value}'");
                                            break;
                                        case "address":
                                            v.Add("Address", $"'{value}'");
                                            break;
                                        case "city":
                                            v.Add("City", $"'{value}'");
                                            break;
                                        case "state":
                                            v.Add("State", $"'{value}'");
                                            break;
                                        case "zip":
                                            v.Add("Zip", $"'{value}'");
                                            break;
                                        case "phone":
                                            v.Add("Phone", $"'{value}'");
                                            break;
                                    }

                                    if (v.Count == 6)
                                    {
                                        string insertStatement = $"INSERT INTO Stores (StoreNumber, Address, City, State, Zip, Phone) VALUES ({v["StoreNumber"]},{v["Address"]},{v["City"]},{v["State"]},{v["Zip"]},{v["Phone"]});";
                                        writer.WriteLine(insertStatement);
                                        v.Clear();
                                    }
                                }



                            }
                        }

                    }
                }





            }
        }
    }
}
