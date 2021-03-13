using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Global state information
    /// </summary>
    public sealed class StateSingleton
    {
        private StateSingleton()
        {
        }
        private static StateSingleton instance = null;
        public static StateSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateSingleton();
                }
                return instance;
            }
        }
        public CatalogResponse catalog { get; set; }
        public ServerConfig config { get; set; }
        public Recipes recipes { get; set; }
        public SettingsResponse settings { get; set; }
        public ChallengesResponse seasonChallenges { get; set; }
        public ProductCatalogResponse productCatalog { get; set; }

    }
}
