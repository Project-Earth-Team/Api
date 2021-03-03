using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models.Features
{
    /// <summary>
    /// Models an item catalog
    /// </summary>
    public class CatalogResponse
    {
        //===Model
        #region Model
        //Efficiency Classes
        public class EfficiencyMap
        {
            public double hand { get; set; }
            public double hoe { get; set; }
            public double axe { get; set; }
            public double shovel { get; set; }
            public double pickaxe_1 { get; set; }
            public double pickaxe_2 { get; set; }
            public double pickaxe_3 { get; set; }
            public double pickaxe_4 { get; set; }
            public double pickaxe_5 { get; set; }
            public double sword { get; set; }
            public double sheers { get; set; }
        }

        //TODO: Actually initialize these with values
        public class Rock2
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Web
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Wood
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Metal1
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Wool
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Metal3
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Ground
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Metal2
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Other
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Leaves
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Equal
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Snow
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Instant
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Rail
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Plants
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Rock4
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Rock3
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Ice
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class Rock1
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class AdventureChest
        {
            public EfficiencyMap efficiencyMap { get; set; }
        }

        public class EfficiencyCategories
        {
            public Rock2 rock_2 { get; set; }
            public Web web { get; set; }
            public Wood wood { get; set; }
            public Metal1 metal_1 { get; set; }
            public Wool wool { get; set; }
            public Metal3 metal_3 { get; set; }
            public Ground ground { get; set; }
            public Metal2 metal_2 { get; set; }
            public Other other { get; set; }
            public Leaves leaves { get; set; }
            public Equal equal { get; set; }
            public Snow snow { get; set; }
            public Instant instant { get; set; }
            public Rail rail { get; set; }
            public Plants plants { get; set; }
            public Rock4 rock_4 { get; set; }
            public Rock3 rock_3 { get; set; }
            public Ice ice { get; set; }
            public Rock1 rock_1 { get; set; }
            public AdventureChest adventure_chest { get; set; }
        }

        public class BlockMetadata
        {
            public double? health { get; set; }
            public string efficiencyCategory { get; set; }
        }
        public class ItemMetadata
        {
            public string useType { get; set; }
            public string alternativeUseType { get; set; }
            public double? mobDamage { get; set; }
            public double? blockDamage { get; set; }
            public object weakDamage { get; set; }
            public double? nutrition { get; set; }
            public double? heal { get; set; }
            public object efficiencyType { get; set; }
            public double? maxHealth { get; set; }
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

        public class ClientProperties
        {
        }
        public class BurnRate
        {
            public int heatPerSecond { get; set; }
            public int burnTime { get; set; }
        }

        public class ExperiencePoints
        {
            public int tappable { get; set; }
            public int? encounter { get; set; }
        }
        public class Item
        {
            public string id { get; set; }
            public ItemInfo item { get; set; }
            public string rarity { get; set; }
            public int fragmentsRequired { get; set; }
            public bool stacks { get; set; }
            public BurnRate burnRate { get; set; }
            public List<object> fuelReturnItems { get; set; }
            public List<object> consumeReturnItems { get; set; }
            public bool deprecated { get; set; }
            public int? experience { get; set; }
            public Dictionary<string, int> experiencePoints { get; set; }
            public string category { get; set; }
        }

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
            public BoostMetadata boostMetadata { get; set; }
            public JournalMetadata journalMetadata { get; set; }
            public AudioMetadata audioMetadata { get; set; }
            public ClientProperties clientProperties { get; set; }

        }
        public class Result
        {
            public EfficiencyCategories efficiencyCategories { get; set; }
            public List<Item> items { get; set; }
        }

        public Result result { get; set; }
        public object expiration { get; set; }
        public object continuationToken { get; set; }
        public Updates updates { get; set; }
        #endregion
        #region Functions

        //===Load and save
        public static CatalogResponse FromFile(string catalogFilePath)
        {
            string file = File.ReadAllText(catalogFilePath);
            return JsonConvert.DeserializeObject<CatalogResponse>(file);
        }

        #endregion
    }
}
