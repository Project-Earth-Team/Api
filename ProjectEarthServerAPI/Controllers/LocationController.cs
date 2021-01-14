using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;
using Newtonsoft.Json;
namespace ProjectEarthServerAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/locations/{latitude}/{longitude}")]
    public class LocationController : Controller
    {
        public ContentResult Get(double latitude, double longitude)
        {
            //see https://github.com/Luminoso-256/project-earth-tiles/wiki/Tappables

            //Create Active Locations - not done inline with the LocationResponse.root because eventually this will be a randomly generated fed in list.
            LocationResponse.ActiveLocation testActiveLocation = new LocationResponse.ActiveLocation
            {
                id = "8c7a46b2-7e21-674a-4c05-6583e5a34f88", //Copypasted from a known good response. TODO: Figure out what this actually is. MCE loves it's UUIDs 
                tileId = "16288_25136", //Once again. copypasted. Tile IDs are based on Open Street Map. Perhaps we can construct a Long/Lat -> Tile ID converter? - https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames could be of use
                coordinate = new LocationResponse.Coordinate
                {
                    latitude = latitude,
                    longitude = longitude
                },
                spawnTime = DateTime.Now,
                expirationTime = DateTime.Now.AddMinutes(10), //Packet captures show that typically earth keeps Tappables around for 10 minutes
                type = "Tappable", //who wouldve guessed?
                icon = "genoa:chest_tappable_map", //see the github wiki link at the top for a list of these that we know about
                metadata = new LocationResponse.Metadata
                {
                    rarity = "Epic",
                    rewardId = "9a3b1169-34e6-4fec-8157-a66bf7f8e7eb" //another "known-good" UUID that we need to figure out
                },
                encounterMetadata = null, //working captured responses have this, its fine
                tappableMetadata = new LocationResponse.TappableMetadata
                {
                    rarity = "Epic" //assuming this and the above need to allign. Why have 2 occurances? who knows.
                }
            };


            //Create our final response
            LocationResponse.Root locationResp = new LocationResponse.Root
            {
                result = new LocationResponse.Result
                {
                    killSwitchedTileIds = new List<object> { }, //havent seen this used. Debugging thing maybe?
                    activeLocations = new List<LocationResponse.ActiveLocation> { testActiveLocation },
                },
                expiration = null,
                continuationToken = null,
                updates = new LocationResponse.Updates()
            };

            //serialize
            var response = JsonConvert.SerializeObject(locationResp);
            //Send
            return Content(response, "application/json");
        }
    }
}
