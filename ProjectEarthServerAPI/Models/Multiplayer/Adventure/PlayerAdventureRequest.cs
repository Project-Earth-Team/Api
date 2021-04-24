using System;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Models.Multiplayer.Adventure
{
    public class PlayerAdventureRequest
    {
        public Coordinate coordinate { get; set; }
        public InventoryResponse.Hotbar[] hotbar { get; set; }
        public Guid? instanceId { get; set; }
        public Guid[] scrollsToDeactivate { get;set; }
    }
}