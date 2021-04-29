using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Features;
using Serilog;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Some simple utilities to interface with generated files from Tappy
    /// </summary>
    public class TappableUtils
    {
        private static Version4Generator version4Generator = new Version4Generator();
        //TODO: Consider turning this into a dictionary and building out a spawn-weight system?
        public static string[] TappableTypes = new[]
        {
            "genoa:stone_mound_a_tappable_map",
            "genoa:stone_mound_b_tappable_map",
            "genoa:stone_mound_c_tappable_map",
            "genoa:grass_mound_a_tappable_map",
            "genoa:grass_mound_b_tappable_map",
            "genoa:grass_mound_c_tappable_map",
            "genoa:tree_oak_a_tappable_map",
            "genoa:tree_oak_b_tappable_map",
            "genoa:tree_oak_c_tappable_map",
            "genoa:tree_birch_a_tappable_map",
            "genoa:tree_spruce_a_tappable_map",
            "genoa:chest_tappable_map",
            "genoa:sheep_tappable_map",
            "genoa:cow_tappable_map",
            "genoa:pig_tappable_map",
            "genoa:chicken_tappable_map"
        };
        private static Random random = new Random();
        //For json de-ser
        public class TappableLootTable
        {
            public string tappableID { get; set; }
            public List<List<string>> possibleDropSets { get; set; }
            
        }

        public static Dictionary<string, List<List<string>>> loadAllTappableSets()
        {
            Log.Information("[Tappables] Loading tappable data.");
            Dictionary<string, List<List<string>>> tappableData = new Dictionary<string, List<List<string>>>();
            string[] files = Directory.GetFiles("./data/tappable", "*.json");
            foreach (var file in files)
            {
               
                TappableLootTable table = JsonConvert.DeserializeObject<TappableLootTable>(File.ReadAllText(file));
                tappableData.Add(table.tappableID, table.possibleDropSets);
                Log.Information($"Loaded {table.possibleDropSets.Count} drop sets for tappable ID {table.tappableID} | Path: {file}");
            }
            
            return tappableData;
        }
      
        public static  LocationResponse.ActiveLocation createTappableInRadiusOfCordinates( double longitude, double latitude, double radius, string type )
        {
            var currentTime = DateTime.UtcNow;
            //Nab tile loc
            
            int[] cords = Tile.getTileForCords(latitude, longitude);
            LocationResponse.ActiveLocation tappable = new LocationResponse.ActiveLocation
            {
                id = Guid.NewGuid().ToString(), // Just a GUID for the tappable
                tileId = cords[0]+"_"+cords[1],
                coordinate = new Coordinate
                {
                   
                    latitude = Math.Round(latitude+random.NextDouble()*radius, 6), // Round off for the client to be happy
                    longitude = Math.Round(longitude+random.NextDouble()*radius, 6)
                },
                spawnTime = currentTime,
                expirationTime = currentTime.AddMinutes(10), //Packet captures show that typically earth keeps Tappables around for 10 minutes
                type = "Tappable", // who wouldve guessed?
                icon = type, 
                metadata = new LocationResponse.Metadata
                {
                    rarity = Item.Rarity.Common,
                    rewardId = version4Generator.NewUuid().ToString() // Seems to always be uuidv4 from official responses so generate one
                },
                encounterMetadata = null, //working captured responses have this, its fine
                tappableMetadata = new LocationResponse.TappableMetadata
                {
                    rarity = Item.Rarity.Common //assuming this and the above need to allign. Why have 2 occurances? who knows.
                }
            };

            return tappable;
        }
      
    }
}