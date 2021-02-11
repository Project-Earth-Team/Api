using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Utilities for interfacing with Player Inventory
    /// </summary>
    public class InventoryUtils
    {

        public static InventoryUtilResult RemoveItemFromInv(string playerid, string itemIdToRemove,
            [Optional] string unstackableItemId, int countToRemove = 1)
        {
            var inv = ReadInventory(playerid);

            var itementry = inv.result.stackableItems.Find(match => match.id == itemIdToRemove);
            if (itementry != null)
            {
                if (countToRemove > itementry.owned)
                {
                    return InventoryUtilResult.NotEnoughItemsAvailable;
                }
                else
                {
                    itementry.owned -= countToRemove;
                    itementry.seen.on = DateTime.UtcNow;
                }
                WriteInventory(playerid, inv);
                return InventoryUtilResult.Success;
            }
            else
            {
                var unstackableItem = inv.result.nonStackableItems.Find(match => match.id == itemIdToRemove);
                if (unstackableItem != null)
                {
                    var instance = unstackableItem.instances.Find(match => match.id == unstackableItemId);
                    unstackableItem.instances.Remove(instance);
                    unstackableItem.seen.on = DateTime.UtcNow;

                    WriteInventory(playerid, inv);
                    return InventoryUtilResult.Success;
                }

                return InventoryUtilResult.ItemNotFoundInInv; // Item not in inventory, so not able to be removed
            }
        }

        public static Tuple<InventoryUtilResult,int> GetItemCountFromInv(string playerid, string itemId)
        {
            var inv = ReadInventory(playerid);

            var itementry = inv.result.stackableItems.Find(match => match.id == itemId);

            if (itementry != null)
            {
                return new Tuple<InventoryUtilResult, int>(InventoryUtilResult.Success,itementry.owned);
            }
            else
            {
                var unstackableItem = inv.result.nonStackableItems.Find(match => match.id == itemId);
                if (unstackableItem != null)
                {
                    return new Tuple<InventoryUtilResult, int>(InventoryUtilResult.Success, 1); // unstackable Item, so count is always 1
                }
            }

            return new Tuple<InventoryUtilResult, int>(InventoryUtilResult.ItemNotFoundInInv, 0); // Item not in inventory, so count 0
        }

        public static InventoryUtilResult AddItemToInv(string playerid, string itemIdToAdd, int count = 1, bool isStackableItem = true)
        {
            try
            {
                var inv = ReadInventory(playerid);

                if (!isStackableItem)
                {
                    inv.result.nonStackableItems.Add(new InventoryResponse.NonStackableItem()
                    {
                        fragments = 1,
                        instances = new List<InventoryResponse.Instance>(),
                        id = itemIdToAdd,
                        seen = new InventoryResponse.Seen(){on = DateTime.UtcNow},
                        unlocked = new InventoryResponse.Unlocked(){on = DateTime.UtcNow}
                    });
                }
                else
                {
                    var itementry = inv.result.stackableItems.Find(match => match.id == itemIdToAdd);

                    if (itementry != null)
                    {
                        itementry.owned += count;
                        itementry.seen.on = DateTime.UtcNow;
                    }
                    else
                    {
                        inv.result.stackableItems.Add(new InventoryResponse.StackableItem()
                        {
                            fragments = 1,
                            id = itemIdToAdd,
                            owned = count,
                            seen = new InventoryResponse.Seen(){on = DateTime.UtcNow},
                            unlocked = new InventoryResponse.Unlocked(){on = DateTime.UtcNow}
                        });
                    }
                }

                WriteInventory(playerid,inv);

                Console.WriteLine($"Added item {itemIdToAdd} to inventory. User ID: {playerid}");
                return InventoryUtilResult.Success;

            }
            catch
            {
                Console.WriteLine($"Adding item to inventory failed! User ID: {playerid} Item to add: {itemIdToAdd}");
                return InventoryUtilResult.NoSpecificError;
            }
        }

        public static Tuple<InventoryUtilResult, double> EditHealthOfItem(string playerid, string itemId, string unstackableItemInstanceId, double newHealth) // TODO: Actually Edit lmao
        {
            try
            {
                var inv = ReadInventory(playerid);

                var unstackableItem = inv.result.nonStackableItems.Find(result => result.id == itemId);
                var unstackableItemInstance =
                    unstackableItem?.instances.Find(match => match.id == unstackableItemInstanceId);

                if (unstackableItemInstance != null)
                    return new Tuple<InventoryUtilResult, double>(InventoryUtilResult.Success,
                        unstackableItemInstance.health);

                return new Tuple<InventoryUtilResult, double>(InventoryUtilResult.UnstackableItemInstanceNotFound, 0.0);
            }
            catch
            {
                return new Tuple<InventoryUtilResult, double>(InventoryUtilResult.NoSpecificError, 0.0);
            }
        }

        private static InventoryUtilResult SetupInventoryForId(string playerid)
        {
            try
            {
                var inventoryfile = $"./{playerid}/inventory.json";
                var obj = new InventoryResponse()
                {
                    result = new InventoryResponse.Result() // TODO: Implement Default Values for each player property/json we store for them
                };
                File.WriteAllText(inventoryfile, JsonConvert.SerializeObject(obj));
                return InventoryUtilResult.Success;
            }
            catch
            {
                Console.WriteLine($"Creating default inventory failed! User ID: {playerid}");
                return InventoryUtilResult.NoSpecificError;
            }
        }

        public static InventoryResponse ReadInventory(string playerid)
        {
            var inventoryfile = $"./{playerid}/inventory.json";
            if (!File.Exists(inventoryfile))
            {
                SetupInventoryForId(playerid); // New inventory, no items will be available
            }

            var invjson = File.ReadAllText(inventoryfile);
            var inv = JsonConvert.DeserializeObject<InventoryResponse>(invjson);
            return inv;
        }

        public static InventoryUtilResult WriteInventory(string playerid, InventoryResponse inv)
        {
            try
            {
                var inventoryfile = $"./{playerid}/inventory.json"; // Path should exist, as you cant really write to the file before reading it first

                File.WriteAllText(inventoryfile, JsonConvert.SerializeObject(inv));
                return InventoryUtilResult.Success;
            }
            catch
            {
                return InventoryUtilResult.NoSpecificError;
            }
        }

        public enum InventoryUtilResult
        {
            Success = 1,
            NotEnoughItemsAvailable,
            ItemNotFoundInInv,
            InventoryCreated,
            NoSpecificError,
            UnstackableItemInstanceNotFound
        }
    }                                   
}
