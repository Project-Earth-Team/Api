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
            public string serviceUri { get; set; }
            public string cdnUri { get; set; }
            public string playfabTitleId { get; set; }
        }

        public class ServiceEnvironments
        {
            public Production production { get; set; }
        }

        public class SupportedEnvironments
        {
            [JsonProperty("2020.1217.02")]
            public List<string> _2020121702 { get; set; }
        }

        public class Result
        {
            public ServiceEnvironments serviceEnvironments { get; set; }
            public SupportedEnvironments supportedEnvironments { get; set; }
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
