using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Buildplate
{
    public class BuildplateServerResponse
    {
        public class BreakableItemToItemLootMap
        {
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

        public class SaveState
        {
            public bool boosts { get; set; }
            public bool experiencePoints { get; set; }
            public bool health { get; set; }
            public bool inventory { get; set; }
            public bool model { get; set; }
            public bool world { get; set; }
        }

        public class SnapshotOptions
        {
            public SaveState saveState { get; set; }
            public string snapshotTriggerConditions { get; set; }
            public string snapshotWorldStorage { get; set; }
            public List<string> triggerConditions { get; set; }
            public string triggerInterval { get; set; }
        }

        public class GameplayMetadata
        {
            public object augmentedImageSetId { get; set; }
            public double blocksPerMeter { get; set; }
            public BreakableItemToItemLootMap breakableItemToItemLootMap { get; set; }
            public Dimension dimension { get; set; }
            public string gameplayMode { get; set; }
            public bool isFullSize { get; set; }
            public Offset offset { get; set; }
            public string playerJoinCode { get; set; }
            public object rarity { get; set; }
            public List<string> shutdownBehavior { get; set; }
            public SnapshotOptions snapshotOptions { get; set; }
            public string spawningClientBuildNumber { get; set; }
            public string spawningPlayerId { get; set; }
            public string surfaceOrientation { get; set; }
            public string templateId { get; set; }
            public string worldId { get; set; }
        }

        public class HostCoordinate
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Result
        {
            public string applicationStatus { get; set; }
            public string fqdn { get; set; }
            public GameplayMetadata gameplayMetadata { get; set; }
            public HostCoordinate hostCoordinate { get; set; }
            public string instanceId { get; set; }
            public string ipV4Address { get; set; }
            public string metadata { get; set; }
            public string partitionId { get; set; }
            public int port { get; set; }
            public string roleInstance { get; set; }
            public bool serverReady { get; set; }
            public string serverStatus { get; set; }
        }
      
        public object continuationToken { get; set; }
        public object expiration { get; set; }
        public Result result { get; set; }
        public Updates updates { get; set; }
        


    }
}
