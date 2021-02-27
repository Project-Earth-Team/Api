using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models
{
    public class LocatorResponse
    {
        public class Production
        {
            [JsonProperty(Order = 1)]
            public string serviceUri { get; set; }
            [JsonProperty(Order = 2)]
            public string cdnUri { get; set; }
            [JsonProperty(Order = 3)]
            public string playfabTitleId { get; set; }
        }

        public class ServiceEnvironments
        {
            public Production production { get; set; }
        }

        public class Result
        {
            public ServiceEnvironments serviceEnvironments { get; set; }
            public Dictionary<String, List<string>> supportedEnvironments { get; set; }
        }

        public class Updates
        {
        }

        public class Root
        {
            public Result result { get; set; }
            public object expiration { get; set; }
            public object continuationToken { get; set; }
            public Updates updates { get; set; }
        }
    }
}
