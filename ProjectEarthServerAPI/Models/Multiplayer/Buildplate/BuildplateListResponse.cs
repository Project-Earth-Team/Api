using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Buildplate
{
    /// <summary>
    /// The list of buildplates that show up on the buildplate sceen.
    /// </summary>
    public class BuildplateListResponse
    {
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

        public class BuildplateData
        {
            public double blocksPerMeter { get; set; }
            public Dimension dimension { get; set; }
            public string eTag { get; set; }
            public string id { get; set; }
            public bool isModified { get; set; }
            public DateTime lastUpdated { get; set; }
            public bool locked { get; set; }
            public string model { get; set; }
            public int numberOfBlocks { get; set; }
            public Offset offset { get; set; }
            public int order { get; set; }
            public int requiredLevel { get; set; }
            public string surfaceOrientation { get; set; }
            public string templateId { get; set; }
            public string type { get; set; }
        }

        public object continuationToken { get; set; }
        public object expiration { get; set; }
        public List<BuildplateData> result { get; set; }
        public Updates updates { get; set; }
        
    }

    public class BuildplateShareResponse
    {
        public BuildplateShareInfo result { get; set; }

        public class BuildplateShareInfo
        {
            public string playerId { get; set; }
            public BuildplateListResponse.BuildplateData buildplateData { get; set; }
        }
    }
}
