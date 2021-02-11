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

    public class TokenProperties
    {
        public string seasonId { get; set; }
        public string challengeId { get; set; }
        public string category { get; set; }

    }
}