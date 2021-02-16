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
        public string catalogFileLocation { get; set; }
        public string journalCatalogFileLocation { get; set; }
        public string recipiesFileLocation { get; set; }
        //Load method

        /// <summary>
        /// Get the server config from the configuration file.
        /// </summary>
        /// <returns></returns>
        public static ServerConfig getFromFile()
        {
            String file = File.ReadAllText("./config/apiconfig.json");
            return JsonConvert.DeserializeObject<ServerConfig>(file);
        }
    }

    


}
