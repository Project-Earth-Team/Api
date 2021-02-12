using System;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
    public class GenericUtils
    {
        public static T ParseJsonFile<T>(string playerId, string fileNameWithoutJsonExtension) where T: new()
        {
            var filepath = $"./{playerId}/{fileNameWithoutJsonExtension}.json";
            if (!File.Exists(filepath))
            {
                SetupJsonFile<T>(playerId,filepath); // Generic setup for each player specific json type
            }

            var invjson = File.ReadAllText(filepath);
            var parsedobj = JsonConvert.DeserializeObject<T>(invjson);
            return parsedobj;
        }

        private static bool SetupJsonFile<T>(string playerId,string filepath) where T: new()
        {
            try
            {
                Console.WriteLine($"Creating default json for User ID: {playerId} with Type: {typeof(T)}.");
                var obj = new T();                                                                              // TODO: Implement Default Values for each player property/json we store for them
                File.WriteAllText(filepath, JsonConvert.SerializeObject(obj));
                return true;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Creating default json failed! User ID: {playerId} Type: {typeof(T)}");
                Console.WriteLine($"Exception: {ex}");
                return false;

            }
        }

        public static bool WriteJsonFile<T>(string playerId, T objToWrite, string fileNameWithoutJsonExtension)
        {
            try
            {

                var filepath = $"./{playerId}/{fileNameWithoutJsonExtension}.json"; // Path should exist, as you cant really write to the file before reading it first

                File.WriteAllText(filepath, JsonConvert.SerializeObject(objToWrite));

                return true;

            }
            catch
            {

                return false;

            }
        }
    }
}