using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ProjectEarthServerAPI.Models;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Util;
using Microsoft.AspNetCore.Authorization;
using ProjectEarthServerAPI.Models.Features;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/locations/{latitude}/{longitude}")]
    public class LocationController : Controller
    {
    //    private static Version4Generator version4Generator = new Version4Generator();
        private static Random random = new Random();
        public ContentResult Get(double latitude, double longitude)
        {
            var currentTime = DateTime.UtcNow;
            //Nab tile loc
            int[] cords = Tile.getTileForCords(latitude, longitude);
            List<LocationResponse.ActiveLocation> tappables = new List<LocationResponse.ActiveLocation>();
            int numTappablesToSpawn = random.Next(1, 10);
            for (int i = 0; i < numTappablesToSpawn; i++)
            {
                var tappable = TappableUtils.createTappableInRadiusOfCordinates(longitude, latitude, 0.0001,
                    TappableUtils.TappableTypes[random.Next(0, TappableUtils.TappableTypes.Length - 1)]);
                //add the tappable to the list
                tappables.Add(tappable);
                //add its GUID to the singleton so we can grab the correct reward pool later
                StateSingleton.Instance.activeTappableTypes.Add(Guid.Parse(tappable.id),tappable.type);
            }


            //Create our final response
            LocationResponse.Root locationResp = new LocationResponse.Root
            {
                result = new LocationResponse.Result
                {
                    killSwitchedTileIds = new List<object> { }, //havent seen this used. Debugging thing maybe?
                    activeLocations = tappables,
                },
                expiration = null,
                continuationToken = null,
                updates = new Updates()
            };

            //serialize
            var response = JsonConvert.SerializeObject(locationResp);
            //Send
            return Content(response, "application/json");
        }
    }
}
