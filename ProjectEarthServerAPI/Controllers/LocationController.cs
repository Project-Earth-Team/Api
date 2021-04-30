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
		private static Version4Generator version4Generator = new Version4Generator();

		public ContentResult Get(double latitude, double longitude)
		{
			var currentTime = DateTime.UtcNow;
			//Nab tile loc
			int[] cords = Tile.getTileForCords(latitude, longitude);

			//Create Active Locations - not done inline with the LocationResponse.root because eventually this will be a randomly generated fed in list.
			LocationResponse.ActiveLocation testActiveLocation = new LocationResponse.ActiveLocation
			{
				id = Guid.NewGuid().ToString(), // Just a GUID for the tappable
				tileId = cords[0] + "_" + cords[1],
				coordinate = new Coordinate
				{
					latitude = Math.Round(latitude, 6), // Round off for the client to be happy
					longitude = Math.Round(longitude, 6)
				},
				spawnTime = currentTime,
				expirationTime = currentTime.AddMinutes(10), //Packet captures show that typically earth keeps Tappables around for 10 minutes
				type = "Tappable", // who wouldve guessed?
				icon = "genoa:chest_tappable_map", //see the github wiki link at the top for a list of these that we know about
				metadata = new LocationResponse.Metadata
				{
					rarity = Item.Rarity.Common, rewardId = version4Generator.NewUuid().ToString() // Seems to always be uuidv4 from official responses so generate one
				},
				encounterMetadata = null, //working captured responses have this, its fine
				tappableMetadata = new LocationResponse.TappableMetadata
				{
					rarity = Item.Rarity.Common //assuming this and the above need to allign. Why have 2 occurances? who knows.
				}
			};


			//Create our final response
			LocationResponse.Root locationResp = new LocationResponse.Root
			{
				result = new LocationResponse.Result
				{
					killSwitchedTileIds = new List<object> { }, //havent seen this used. Debugging thing maybe?
					activeLocations = new List<LocationResponse.ActiveLocation> {testActiveLocation},
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
