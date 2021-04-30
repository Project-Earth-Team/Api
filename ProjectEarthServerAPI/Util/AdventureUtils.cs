using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Multiplayer;
using ProjectEarthServerAPI.Models.Multiplayer.Adventure;

namespace ProjectEarthServerAPI.Util
{
	public class AdventureUtils
	{
		public Dictionary<Guid, Item.Rarity> crystalRarityList = StateSingleton.Instance.catalog.result.items
			.FindAll(select => select.item.type == "AdventureScroll")
			.ToDictionary(pred => pred.id, pred => pred.rarity);

		public static AdventureRequestResult RedeemCrystal(string playerId, PlayerAdventureRequest adventureRequest, Guid crystalId)
		{
			InventoryUtils.RemoveItemFromInv(playerId, crystalId);

			var selectedAdventureIcon = "genoa:adventure_generic_map_b";
			var selectedAdventureId = "b7335819-c123-49b9-83fb-8a0ec5032779";

			var adventureLocation = new LocationResponse.ActiveLocation
			{
				coordinate = adventureRequest.coordinate,
				encounterMetadata = new EncounterMetadata
				{
					anchorId = "",
					anchorState = "Off",
					augmentedImageSetId = "",
					encounterType = EncounterType.None,
					locationId = Guid.Empty,
					worldId = Guid.NewGuid() // TODO: Replace this with actual adventure id
				},
				expirationTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10.00)),
				spawnTime = DateTime.UtcNow,
				icon = selectedAdventureIcon,
				id = selectedAdventureId,
				metadata = new(),
				tileId = string.Join("-",
					Tile.getTileForCords(adventureRequest.coordinate.latitude, adventureRequest.coordinate.longitude)),
				type = "PlayerAdventure"
			};

			return new AdventureRequestResult {result = adventureLocation, updates = new Updates()};
		}
	}
}
