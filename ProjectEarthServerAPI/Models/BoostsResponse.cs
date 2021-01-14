using System.Collections.Generic;

namespace ProjectEarthServerAPI.Models
{
    public class MiniFigRecords
    {
    }

    public class StatusEffects
    {
        public object tappableInteractionRadius { get; set; }
        public object experiencePointRate { get; set; }
        public List<object> itemExperiencePointRates { get; set; }
        public object attackDamageRate { get; set; }
        public object playerDefenseRate { get; set; }
        public object blockDamageRate { get; set; }
        public object maximumPlayerHealth { get; set; }
        public object craftingSpeed { get; set; }
        public object smeltingFuelIntensity { get; set; }
        public object foodHealthRate { get; set; }
    }

    public class ScenarioBoosts
    {
    }

    public class BoostResult
    {
        public List<object> potions { get; set; }
        public List<object> miniFigs { get; set; }
        public MiniFigRecords miniFigRecords { get; set; }
        public List<object> activeEffects { get; set; }
        public StatusEffects statusEffects { get; set; }
        public ScenarioBoosts scenarioBoosts { get; set; }
        public object expiration { get; set; }
    }

    public class BoostResponse
    {
        public BoostResult result { get; set; }
    }
}