using System.Collections.Generic;

namespace ProjectEarthServerAPI.Models
{
    public class Token
    {
        public string lifetime { get; set; }
        public string clientType { get; set; }
        public Dictionary<string, string> clientProperties { get; set; } // Needs to be optional, so Dictionary<string,string> instead of fixed class
        public Rewards rewards { get; set; }
    }
}