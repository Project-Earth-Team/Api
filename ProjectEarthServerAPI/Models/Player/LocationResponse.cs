using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Multiplayer.Adventure;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Models
{
	public class LocationResponse
	{
		public class Metadata : TappableMetadata
		{
			public string rewardId { get; set; }
		}

		public class TappableMetadata
		{
			[JsonConverter(typeof(StringEnumConverter))]
			public Item.Rarity rarity { get; set; }
		}

		public class ActiveLocationStorage
		{
			public ActiveLocation location { get; set; }
			public Rewards rewards { get; set; }
		}

		public class ActiveLocation
		{
			public Guid id { get; set; }
			public string tileId { get; set; }
			public Coordinate coordinate { get; set; }

			[JsonConverter(typeof(DateTimeConverter))]
			public DateTime spawnTime { get; set; }

			[JsonConverter(typeof(DateTimeConverter))]
			public DateTime expirationTime { get; set; }

			public string type { get; set; }
			public string icon { get; set; }
			public Metadata metadata { get; set; }
			public EncounterMetadata encounterMetadata { get; set; }
			public TappableMetadata tappableMetadata { get; set; }
		}

		public class Result
		{
			public List<object> killSwitchedTileIds { get; set; }
			public List<ActiveLocation> activeLocations { get; set; }
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
