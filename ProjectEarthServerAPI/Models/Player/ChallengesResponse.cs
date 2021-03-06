using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Models.Player
{
    public class ChallengesResponse
    {
        public ChallengesList result { get; set; }
        public Updates updates { get; set; }
    }

    public class ChallengesList
    {
        public Dictionary<Uuid,ChallengeInfo> challenges { get; set; }
        public Uuid activeSeasonChallenge { get; set; }
    }

    public class ChallengeInfo
    {
        public Uuid referenceId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeDuration duration { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeType type { get; set; }
        public DateTime? endTimeUtc { get; set; }
        public Rewards rewards { get; set; }
        public int percentComplete { get; set; }
        public bool isComplete { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeState state { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeCategory category { get; set; }
        public int currentCount { get; set; }
        public int totalThreshold { get; set; }
        public Uuid parentId { get; set; }
        public int order { get; set; }
        public string rarity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeLogicCondition prerequisiteLogicalCondition { get; set; }
        public List<Uuid> prerequisiteIds { get; set; }
        public Uuid groupId { get; set; }
        public ChallengeProperties clientProperties { get; set; }
    }

    public class ChallengeProperties {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string topDecorationTexture { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string challengeCollectedAudioEvent { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string challengeSelectedAudioEvent { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string frameTexture { get; set; }
    }

    public enum ChallengeDuration
    {
        Career,
        Season,
        SignIn,
        OOBE,
        PersonalTimed,
        PersonalContinuous
    }

    public enum ChallengeType
    {
        Journal,
        Regular
    }

    public enum ChallengeState
    {
        Active,
        Completed,
        Locked
    }

    public enum ChallengeCategory
    {
        Building,
        Collection,
        cow,
        chicken,
        oobe,
        pig,
        rabbit,
        retention,
        season_17,
        sheep,
        Smelting
    }

    public enum ChallengeLogicCondition
    {
        And,
        Or
    }
}