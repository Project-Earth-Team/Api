using System;
using System.Collections.Generic;

namespace ProjectEarthServerAPI.Models.Features
{
	public class Item
	{
		public Guid id { get; set; }
		public ItemInfo item { get; set; }
		public string rarity { get; set; }
		public int fragmentsRequired { get; set; }
		public bool stacks { get; set; }
		public BurnRate burnRate { get; set; }
		public List<RewardComponent> fuelReturnItems { get; set; }
		public List<RewardComponent> consumeReturnItems { get; set; }
		public bool deprecated { get; set; }
		public int? experience { get; set; }
		public ExperiencePoints experiencePoints { get; set; } // This could be a Dictionary<string, int> ?
		public string category { get; set; }

		public class ItemInfo
		{
			public string name { get; set; }
			public int aux { get; set; }
			public string type { get; set; }
			public string useType { get; set; }
			public float? tapSpeed { get; set; }
			public float? heal { get; set; }
			public float? nutrition { get; set; }
			public float? mobDamage { get; set; }
			public float? blockDamage { get; set; }
			public float? health { get; set; }
			public BlockMetadata blockMetadata { get; set; }
			public ItemMetadata ItemMetadata { get; set; }
			public BoostMetadata? boostMetadata { get; set; }
			public JournalMetadata journalMetadata { get; set; }
			public AudioMetadata? audioMetadata { get; set; }
			public Dictionary<string, string> clientProperties { get; set; }
		}
		public class BurnRate
		{
			public int heatPerSecond { get; set; }
			public int burnTime { get; set; }
		}

		public class BlockMetadata
		{
			public int? health { get; set; }
			public string efficiencyCategory { get; set; }
		}

		public class ItemMetadata
		{
			public string useType { get; set; }
			public string alternativeUseType { get; set; }
			public int? mobDamage { get; set; }
			public int? blockDamage { get; set; }
			public int? weakDamage { get; set; }
			public int? nutrition { get; set; }
			public int? heal { get; set; }
			public string? efficiencyType { get; set; }
			public int? maxHealth { get; set; }
		}

		public class BoostMetadata
		{
			public string name { get; set; }
			public string attribute { get; set; }
			public string type { get; set; }
			public bool canBeDeactivated { get; set; }
			public bool canBeRemoved { get; set; }
			public DateTime? activeDuration { get; set; }
			public bool additive { get; set; }
			public object level { get; set; }
			public List<Effect> effects { get; set; }
			public string scenario { get; set; }
			public DateTime? cooldown { get; set; }
		}

		public class JournalMetadata
		{
			public string groupKey { get; set; }
			public int experience { get; set; }
			public int order { get; set; }
			public string behavior { get; set; }
			public string biome { get; set; }
		}

		public class Sounds
		{
			public string journal { get; set; }
		}

		public class AudioMetadata
		{
			public Sounds sounds { get; set; }
			public string defaultSound { get; set; }
		}

		public class ExperiencePoints
		{
			public int tappable { get; set; }
			public int? encounter { get; set; }
		}

		public class Effect
		{
			public string type { get; set; }
			public TimeSpan? duration { get; set; }
			public double? value { get; set; }
			public string unit { get; set; }
			public string targets { get; set; }
			public List<object> items { get; set; } // Which items it affects
			public List<object> itemScenarios { get; set; } // Where this effect is used
			public string activation { get; set; } // Triggered - on a trigger, Instant - directly, for food, Timed - Has a timer for completion
			public object modifiesType { get; set; } // Only health or null
		}
	}
}
