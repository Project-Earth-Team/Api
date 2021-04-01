using System;
using System.Data;

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
        public string buildplateId { get; set; }
    }

    public class InventoryItemRequest
    {
        public string itemIdentifier { get; set; }
        public int count { get; set; }
        public bool removeItem { get; set; }
    }

    public enum ServerCommandType
    {
        GetInventory,
        GetBuildplate,
        EditInventory,
        EditBuildplate
    }
}