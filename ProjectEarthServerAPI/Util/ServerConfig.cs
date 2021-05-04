using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Represents the server configuration as json, while also having a load method
    /// </summary>
    public class ServerConfig
    {
        //Properties
        public string baseServerIP { get; set; }
        public string itemsFolderLocation { get; set; }
        public string efficiencyCategoriesFolderLocation { get; set; }
        public string journalCatalogFileLocation { get; set; }
        public string recipesFileLocation { get; set; }
        public string settingsFileLocation { get; set; }
        public string seasonChallengesFileLocation { get; set; }
        public string productCatalogFileLocation { get; set; }
        public Dictionary<string, string> multiplayerAuthKeys { get; set; }
        //tappable settings
        public int minTappableSpawnAmount { get; set; }
        public int maxTappableSpawnAmount { get; set; }
        public double tappableSpawnRadius { get; set; }
        
        //Load method

        /// <summary>
        /// Get the server config from the configuration file.
        /// </summary>
        /// <returns></returns>
        public static ServerConfig getFromFile()
        {
            String file = File.ReadAllText("./data/config/apiconfig.json");
            return JsonConvert.DeserializeObject<ServerConfig>(file);
        }
    }
}
