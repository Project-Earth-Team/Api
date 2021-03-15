using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Models
{
    public class TokenResponse
    {
        [JsonProperty("result")]
        public TokenResult Result { get; set; }

        public TokenResponse()
        {
            Result = new TokenResult
            {
                tokens = new Dictionary<Uuid, Token>()
            };
        }
    }

    public class TokenResult
    {
        public Dictionary<Uuid, Token> tokens { get; set; }
    }

    public class TokenProperties
    {
        public string seasonId { get; set; }
        public string challengeId { get; set; }
        public string category { get; set; }

    }
}