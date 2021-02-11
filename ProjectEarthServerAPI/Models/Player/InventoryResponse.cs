using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Player
{
    /// <summary>
    /// Models the player inventory endpoint (inventory/survival)
    /// </summary>
    public class InventoryResponse
    {
        public class Hotbar
        {
            //I havent done adventures recently. Health is **Probably wrong**
            public object health { get; set; }
            public string id { get; set; }
            public object instanceId { get; set; }
            public int count { get; set; }
        }

        public class Seen
        {
            public DateTime on { get; set; }
        }

        public class Unlocked
        {
            public DateTime on { get; set; }
        }

        public class StackableItem
        {
            public int owned { get; set; }
            public string id { get; set; }
            public Seen seen { get; set; }
            public Unlocked unlocked { get; set; }
            public int fragments { get; set; }
        }

        public class Instance
        {
            public string id { get; set; }
            public double health { get; set; }
        }

        public class NonStackableItem
        {
            public List<Instance> instances { get; set; }
            public string id { get; set; }
            public Seen seen { get; set; }
            public Unlocked unlocked { get; set; }
            public int fragments { get; set; }
        }

        public class Result
        {
            public List<Hotbar> hotbar { get; set; }
            public List<StackableItem> stackableItems { get; set; }
            public List<NonStackableItem> nonStackableItems { get; set; }
        }

        public class Updates
        {
        }

     
        public Result result { get; set; }
        public object expiration { get; set; }
        public object continuationToken { get; set; }
        public Updates updates { get; set; }
        

    }
}
