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



        //Load method
        public static ServerConfig getFromFile()
        {
            String file = File.ReadAllText("./config/apiconfig.json");
            return JsonConvert.DeserializeObject<ServerConfig>(file);
        }
    }

    


}
