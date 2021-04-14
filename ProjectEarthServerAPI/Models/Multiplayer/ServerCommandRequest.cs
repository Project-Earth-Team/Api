using System;
using System.Data;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models.Multiplayer
{
    public class ServerCommandRequest
    {
        public ServerCommandType command { get; set; }
        public string playerId { get; set; }
        public Guid apiKey { get; set; }
        public Guid serverId { get; set; }
        public string requestData { get; set; }

    }

    public class BuildplateRequest
    {
        public Guid buildplateId { get; set; }
        public string playerId { get; set; }
    }

    public class HotbarTranslation
    {
        public string identifier { get; set; }
        public int meta { get; set; }
        public int count { get; set; }
        public int slotId { get; set; }
    }

    public class EditInventoryRequest
    {
        public string identifier { get; set; }
        public int meta { get; set; }
        public int count { get; set; }
        public int slotIndex { get; set; }
        public float health { get; set; }
        public bool removeItem { get; set; }
    }

    public class ServerInstanceInfo
    {
        public Guid instanceId { get; set; }
        public Guid buildplateId { get; set; }
    }

    public class ServerInstanceRequestInfo
    {
        public Guid instanceId { get; set; }
        public Guid buildplateId { get; set; }
        public string playerId { get; set; }
    }

    public enum ServerCommandType
    {
        GetInventory,
        GetInventoryForClient,
        EditInventory,
        EditHotbar,
        GetBuildplate,
        EditBuildplate,
        MarkServerAsReady
    }
}