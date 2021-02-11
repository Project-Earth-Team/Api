using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models
{
    public class LocationResponse
    {
        public class Coordinate
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Metadata
        {
            public string rarity { get; set; }
            public string rewardId { get; set; }
        }

        public class TappableMetadata
        {
            public string rarity { get; set; }
        }

        public class ActiveLocation
        {
            public string id { get; set; }
            public string tileId { get; set; }
            public Coordinate coordinate { get; set; }
            public DateTime spawnTime { get; set; }
            public DateTime expirationTime { get; set; }
            public string type { get; set; }
            public string icon { get; set; }
            public Metadata metadata { get; set; }
            public object encounterMetadata { get; set; }
            public TappableMetadata tappableMetadata { get; set; }
        }

        public class Result
        {
            public List<object> killSwitchedTileIds { get; set; }
            public List<ActiveLocation> activeLocations { get; set; }
        }

        public class Updates
        {
        }

        public class Root
        {
            public Result result { get; set; }
            public object expiration { get; set; }
            public object continuationToken { get; set; }
            public Updates updates { get; set; }
        }
    }
}
