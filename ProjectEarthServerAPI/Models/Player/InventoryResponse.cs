using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Player
{
    /// <summary>
    /// Models the player inventory endpoint (inventory/survival)
    /// </summary>
    public class InventoryResponse
    {
        #region Model
        public Result result { get; set; }
        public object expiration { get; set; }
        public object continuationToken { get; set; }
        public Updates updates { get; set; }
        public class Hotbar
        {
            //I havent done adventures recently. Health is **Probably wrong**
            public string id { get; set; }
            public ItemInstance instanceId { get; set; }
            public int count { get; set; }
        }

        public class ItemInstance
        {
            public string id { get; set; }
            public double health { get; set; }
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
            public int owned { get; set; } // How many you have
            public string id { get; set; } // Item UUID 
            public Seen seen { get; set; } // When you last used/got this item
            public Unlocked unlocked { get; set; } // When you first unlocked the item
            public int fragments { get; set; } // Not used
        }

        public class Instance
        {
            public string id { get; set; } // Maybe? Instance id of last multiplayer instance you visited
            public double health { get; set; } // Same as above, just health of the tool
        }

        public class NonStackableItem
        {
            public List<Instance> instances { get; set; } // List of Instances, see above explanation
            public string id { get; set; } // Item UUID
            public Seen seen { get; set; } // When you last used/got this item
            public Unlocked unlocked { get; set; } // When you first unlocked the item
            public int fragments { get; set; } // Not used
        }

        public class Result
        {
            public Hotbar[] hotbar { get; set; } // Items you have in your hotbar
            public List<StackableItem> stackableItems { get; set; } // Stackable items (dirt, cobble, torches)
            public List<NonStackableItem> nonStackableItems { get; set; } // Unstackable items (picks, axes, swords)
        }

        #endregion
        #region Functions

        public InventoryResponse()
        {
            result = new Result
            {
                hotbar = new Hotbar[7],
                nonStackableItems = new List<NonStackableItem>(1),
                stackableItems = new List<StackableItem>(1)
            };
        }

        #endregion

    }
}
