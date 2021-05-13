using Newtonsoft.Json;
using System;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Models
{
	public class Rewards
	{
		[JsonProperty("experiencePoints", NullValueHandling = NullValueHandling.Ignore)]
		public int? ExperiencePoints { get; set; }

		[JsonProperty("inventory")]
		public RewardComponent[] Inventory { get; set; }

		[JsonProperty("rubies", NullValueHandling = NullValueHandling.Ignore)]
		public int? Rubies { get; set; }

		[JsonProperty("buildplates")]
		public RewardComponent[] Buildplates { get; set; }

		[JsonProperty("challenges")]
		public RewardComponent[] Challenges { get; set; }

		[JsonProperty("personaItems")]
		public Guid[] PersonaItems { get; set; }

		[JsonProperty("utilityBlocks")]
		public RewardComponent[] UtilityBlocks { get; set; }

		public Rewards()
		{
			Inventory = Array.Empty<RewardComponent>();
			Buildplates = Array.Empty<RewardComponent>();
			Challenges = Array.Empty<RewardComponent>();
			PersonaItems = Array.Empty<Guid>();
			UtilityBlocks = Array.Empty<RewardComponent>();
		}
	}

	public class RewardComponent
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("amount")]
		public int Amount { get; set; }
	}
}
