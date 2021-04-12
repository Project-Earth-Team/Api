using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Multiplayer;
using ProjectEarthServerAPI.Models.Player;
using ProjectEarthServerAPI.Util;
using Serilog;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Utilities for interfacing with Player Inventory
    /// </summary>
    public class InventoryUtils
    {
        public static InventoryUtilResult RemoveItemFromInv(string playerId, Guid itemIdToRemove, int count,
            float health)
        {
            var inv = ReadInventory(playerId);
            var item = inv.result.nonStackableItems.Find(match =>
                match.id == itemIdToRemove && match.instances.Any(match => match.health == health));
            if (item == null)
            {
                InventoryResponse.Hotbar instanceItem = Array.Find(inv.result.hotbar, match =>
                    match.id == itemIdToRemove && match.instanceId.health == health);
                return RemoveItemFromInv(playerId, itemIdToRemove, count, instanceItem.instanceId.id);
            }
            else
            {
                return RemoveItemFromInv(playerId, itemIdToRemove, count,
                    item.instances.Find(match => match.health == health).id);
            }
        }

        public static InventoryUtilResult RemoveItemFromInv(string playerId, Guid itemIdToRemove,
            int count = 1, string unstackableItemId = null, bool includeHotbar = true)
        {
            var inv = ReadInventory(playerId);

            if (includeHotbar)
            {
                InventoryResponse.Hotbar hotbarItem = null;
                foreach (InventoryResponse.Hotbar item in inv.result.hotbar)
                {
                    if (item != null && item.id == itemIdToRemove && item.count == count)
                    {
                        if (unstackableItemId == null || item.instanceId.id == unstackableItemId)
                            hotbarItem = item;
                        break;
                    }
                }

                if (hotbarItem != null)
                {
                    var hotbar = inv.result.hotbar;
                    var index = Array.IndexOf(hotbar, hotbarItem);

                    if (hotbarItem.count == count) hotbarItem = null;
                    else hotbarItem.count -= count;

                    hotbar[index] = hotbarItem;

                    EditHotbar(playerId, hotbar, false);

                    return InventoryUtilResult.Success;

                }

            }

            var itementry = inv.result.stackableItems.Find(match => match.id == itemIdToRemove && match.owned >= count);
            if (itementry != null)
            {
                itementry.owned -= count;
                itementry.seen.on = DateTime.UtcNow;

                WriteInventory(playerId, inv);
            }
            else
            {
                var unstackableItem = inv.result.nonStackableItems.Find(match => match.id == itemIdToRemove);
                if (unstackableItem != null)
                {
                    var instance = unstackableItem.instances.Find(match => match.id == unstackableItemId);
                    unstackableItem.instances.Remove(instance);
                    unstackableItem.seen.on = DateTime.UtcNow;

                    WriteInventory(playerId, inv);
                }
                else
                {
                    return InventoryUtilResult.NotEnoughItemsAvailable;
                }

            }

            return InventoryUtilResult.Success;
        }

        public static InventoryUtilResult RemoveItemFromInv(string playerId, string itemIdentifier, int count = 1,
            string unstackableItemId = null)
        {
            var itemId = StateSingleton.Instance.catalog.result.items.Find(match => match.item.name == itemIdentifier)
                .id;

            return RemoveItemFromInv(playerId, itemId, count, unstackableItemId);
        }

        public static Tuple<InventoryUtilResult, int> GetItemCountFromInv(string playerId, Guid itemId)
        {
            var inv = ReadInventory(playerId);;

            var itementry = inv.result.stackableItems.Find(match => match.id == itemId);

            if (itementry != null)
            {
                return new Tuple<InventoryUtilResult, int>(InventoryUtilResult.Success, itementry.owned);
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

        public static InventoryUtilResult AddItemToInv(string playerId, Guid itemIdToAdd, int count = 1, bool isStackableItem = true, string instanceId = null)
        {
            try
            {
                var inv = ReadInventory(playerId);

                if (!isStackableItem)
                {
                    var itementry = inv.result.nonStackableItems.Find(match => match.id == itemIdToAdd);

                    if (itementry != null && instanceId != null)
                    {
                        itementry.instances.Add(new InventoryResponse.ItemInstance{health = 100.00, id = instanceId});
                        itementry.seen.on = DateTime.UtcNow;
                    }
                    else
                    {
                        itementry?.instances.Add(new InventoryResponse.ItemInstance{health = 100.00, id = Guid.NewGuid().ToString()});
                    }
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
                            seen = new InventoryResponse.DateTimeOn(){on = DateTime.UtcNow},
                            unlocked = new InventoryResponse.DateTimeOn(){on = DateTime.UtcNow}
                        });
                    }
                }


                WriteInventory(playerId, inv);

                TokenUtils.AddItemToken(playerId, itemIdToAdd);

                Log.Information($"[{playerId}]: Added item {itemIdToAdd} to inventory.");
                return InventoryUtilResult.Success;

            }
            catch
            {
                Log.Error($"[{playerId}]: Adding item to inventory failed! Item to add: {itemIdToAdd}");
                return InventoryUtilResult.NoSpecificError;
            }
        }

        public static InventoryUtilResult AddItemToInv(string playerId, string itemIdentifier, int count = 1,
            bool isStackableItem = true, string instanceId = null)
        {
            var itemId = StateSingleton.Instance.catalog.result.items.Find(match => match.item.name == itemIdentifier)
                .id;
            return AddItemToInv(playerId, itemId, count, isStackableItem, instanceId);
        }

        public static Tuple<InventoryUtilResult, double> EditHealthOfItem(string playerId, Guid itemId, string unstackableItemInstanceId, double newHealth) // TODO: Actually Edit lmao
        {
            try
            {
                var inv = ReadInventory(playerId);

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

        public static Tuple<InventoryUtilResult, InventoryResponse.Hotbar[]> GetHotbar(string playerId)
        {
            var inv = ReadInventory(playerId);
            return new Tuple<InventoryUtilResult, InventoryResponse.Hotbar[]>(InventoryUtilResult.Success,
                inv.result.hotbar);
        }

        public static Tuple<InventoryUtilResult, InventoryResponse.Hotbar[]> EditHotbar(string playerId, InventoryResponse.Hotbar[] newHotbar, bool moveItemsToInventory = true)
        {
            var inv = ReadInventory(playerId);

            for (int i = 0; i < inv.result.hotbar.Length; i++)
            {
                if (newHotbar[i]?.id != inv.result.hotbar[i]?.id |
                    newHotbar[i]?.count != inv.result.hotbar[i]?.count)
                {
                    if (newHotbar[i] == null)
                    {
                        if (moveItemsToInventory)
                        {
                            if (inv.result.hotbar[i].instanceId == null)
                            {
                                AddItemToInv(playerId, inv.result.hotbar[i].id, inv.result.hotbar[i].count);
                            }
                            else
                            {
                                AddItemToInv(playerId, inv.result.hotbar[i].id, 1, false,
                                    inv.result.hotbar[i].instanceId.id);
                            }
                        }
                    }
                    else
                    {

                        if (moveItemsToInventory)
                        {
                            if (inv.result.hotbar[i] != null)
                            {
                                RemoveItemFromInv(playerId, newHotbar[i].id,
                                    newHotbar[i].count - inv.result.hotbar[i].count, newHotbar[i].instanceId?.id, false);
                            }
                            else
                            {
                                RemoveItemFromInv(playerId, newHotbar[i].id, newHotbar[i].count,
                                    newHotbar[i].instanceId?.id, false);
                            }
                        }
                        else // Not adding the actual item, just the item id since earth can only transfer items already in the inventory item lists
                        {
                            if (newHotbar[i].instanceId == null)
                            {
                                AddItemToInv(playerId, newHotbar[i].id, 0);
                            }
                            else
                            {
                                AddItemToInv(playerId, newHotbar[i].id, 0, false,
                                    newHotbar[i].instanceId.id);
                            }
                        }
                    }
                }

            }

            var newinv = ReadInventory(playerId);
            newinv.result.hotbar = newHotbar;

            WriteInventory(playerId, newinv);

            return new Tuple<InventoryUtilResult, InventoryResponse.Hotbar[]>(InventoryUtilResult.Success, newHotbar);
        }

        /*
         * Theoretically we can just replace these function with their generic variants,
         * but I thought keeping them for ease of use would be nice.
         */

        public static InventoryResponse ReadInventory(string playerId)
        {
            return GenericUtils.ParseJsonFile<InventoryResponse>(playerId, "inventory");
        }

        public static MultiplayerInventoryResponse ReadInventoryForMultiplayer(string playerId)
        {
            var normalInv = ReadInventory(playerId);
            var multiplayerInv = new MultiplayerInventoryResponse();

            var hotbar = new MultiplayerItem[normalInv.result.hotbar.Length];

            for (int i = 0; i < normalInv.result.hotbar.Length; i++)
            {
                hotbar[i] = MultiplayerItem.ConvertToMultiplayerItem(normalInv.result.hotbar[i]);
            }

            var inv = new List<MultiplayerItem>();

            for (int i = 0; i < normalInv.result.stackableItems.Count; i++)
            {
                var item = normalInv.result.stackableItems[i];
                inv.Add(MultiplayerItem.ConvertToMultiplayerItem(item));
            }

            for (int i = 0; i < normalInv.result.nonStackableItems.Count; i++)
            {
                var item = normalInv.result.nonStackableItems[i];
                MultiplayerItem.ConvertToMultiplayerItems(item)
                    .ForEach(match => inv.Add(match));
            }

            multiplayerInv.hotbar = hotbar;
            multiplayerInv.inventory = inv.ToArray();

            return multiplayerInv;
        }

        private static bool WriteInventory(string playerId, InventoryResponse inv)
        {
            return GenericUtils.WriteJsonFile(playerId, inv, "inventory");
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
