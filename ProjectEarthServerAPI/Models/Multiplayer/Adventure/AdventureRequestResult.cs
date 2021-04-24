using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProjectEarthServerAPI.Models.Multiplayer.Adventure
{
    public class AdventureRequestResult
    {
        public LocationResponse.ActiveLocation result { get; set; }
        public Updates updates { get; set; }
    }

    public class EncounterMetadata
    {
        public string anchorId { get; set; }
        public string anchorState { get; set; }
        public string augmentedImageSetId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EncounterType encounterType { get; set; }
        public Guid locationId { get; set; }
        public Guid worldId { get; set; }
    }

    public enum EncounterType
    {
        None
    }
}