using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models
{
    public class ResourcePackResponse
    {
        public class Result {
            public int order { get; set; } 
            public List<int> parsedResourcePackVersion { get; set; } 
            public string relativePath { get; set; } 
            public string resourcePackId { get; set; } 
            public string resourcePackVersion { get; set; }
        }
        public class Updates { }

        public object continuationToken { get; set; } 
        public object expiration { get; set; } 
        public List<Result> result { get; set; } 
        public Updates updates { get; set; } 
        
    }
}

