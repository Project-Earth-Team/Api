using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Models.Buildplate
{
    /// <summary>
    /// The list of buildplates that show up on the buildplate sceen.
    /// </summary>
    public class BuildplateListResponse
    {
        public object continuationToken { get; set; }
        public object expiration { get; set; }
        public List<BuildplateData> result { get; set; }
        public Updates updates { get; set; }
        
    }

    public class BuildplateData
    {
        public double blocksPerMeter { get; set; }
        public Dimension dimension { get; set; }
        public string eTag { get; set; }
        public Guid id { get; set; }
        public bool isModified { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime lastUpdated { get; set; }
        public bool locked { get; set; }
        public string model { get; set; }
        public int numberOfBlocks { get; set; }
        public Offset offset { get; set; }
        public int order { get; set; }
        public int requiredLevel { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SurfaceOrientation surfaceOrientation { get; set; }
        public Guid templateId { get; set; }
        public string type { get; set; }
    }

    public class Dimension
    {
        public int x { get; set; }
        public int z { get; set; }
    }

    public class Offset
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class BuildplateShareResponse
    {
        public BuildplateShareInfo result { get; set; }

        public class BuildplateShareInfo
        {
            public string playerId { get; set; }
            public BuildplateData buildplateData { get; set; }
        }
    }
}
