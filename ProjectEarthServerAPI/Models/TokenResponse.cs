using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models
{
    public class TokenResponse
    {
        [JsonProperty("result")]
        public TokenResult Result { get; set; }
    }

    public class TokenResult
    {
        public Dictionary<string,Token> tokens { get; set; }
    }

    public class Token
    {
        public string lifetime { get; set; }
        public string clientType { get; set; }
        public Dictionary<string,string> clientProperties { get; set; } // Needs to be optional, so Dictionary<string,string> instead of fixed class
        public TokenRewards rewards { get; set; } // TODO: Weird assignment, sometimes not there, sometimes with experiencePoints, most times without; Figure out how to display
    }

    public class TokenProperties
    {
        public string seasonId { get; set; }
        public string challengeId { get; set; }
        public string category { get; set; }

    }

    public class TokenRewards
    {
        public RewardComponent[] inventory { get; set; }
        public RewardComponent[] buildplates { get; set; }
        public RewardComponent[] challenges { get; set; }
        public RewardComponent[] personaItems { get; set; }
        public RewardComponent[] utilityBlocks { get; set; }
    }

    public class RewardComponent
    {
        public string id { get; set; }
        public int amount { get; set; }
    }
}