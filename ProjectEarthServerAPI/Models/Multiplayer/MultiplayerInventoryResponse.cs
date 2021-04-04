using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Models.Multiplayer
{
    public class MultiplayerInventoryResponse
    {
        public MultiplayerItem[] hotbar { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MultiplayerItem[] inventory { get; set; }

    }

    public class MultiplayerItem
    {
        [JsonConverter(typeof(MultiplayerItemCategoryConverter))]
        public MultiplayerItemCategory category { get; set; }
        public int count { get; set; }
        public Guid guid { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public InventoryResponse.ItemInstance instance_data { get; set; }
        public bool owned { get; set; }

        [JsonConverter(typeof(MultiplayerItemRarityConverter))]
        public MultiplayerItemRarity rarity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MultiplayerItemUnlocked unlocked { get; set; }

        public static MultiplayerItem ConvertToMultiplayerItem(InventoryResponse.Hotbar item)
        {
            if (item != null) return ConvertToMultiplayerItem(item.id, item.count, true, null, item.instanceId);

            var multiplayerItem = new MultiplayerItem
            {
                category = new MultiplayerItemCategory
                {
                    loc = ItemCategory.Invalid,
                    value = (int) ItemCategory.Invalid
                },
                count = 0,
                guid = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                owned = false,
                rarity = new MultiplayerItemRarity
                {
                    loc = ItemRarity.Invalid,
                    value = 6
                }
            };

            return multiplayerItem;
        }

        public static MultiplayerItem ConvertToMultiplayerItem(InventoryResponse.StackableItem item) =>
            ConvertToMultiplayerItem(item.id, item.owned, true, item.unlocked.on);

        public static List<MultiplayerItem> ConvertToMultiplayerItems(InventoryResponse.NonStackableItem item)
        {
            return item.instances
                .Select(instance => ConvertToMultiplayerItem(item.id, item.instances.Count, true, item.unlocked.@on, instance))
                .ToList();
        }

        private static MultiplayerItem ConvertToMultiplayerItem(Guid itemId, int count, bool owned, DateTime? unlockedOn = null, InventoryResponse.ItemInstance itemInstance = null)
        {
            var catalogItem = StateSingleton.Instance.catalog.result.items.Find(match => match.id == itemId);
            ItemCategory category =
                Enum.TryParse(catalogItem.category, true, out ItemCategory itemCategory)
                    ? itemCategory
                    : ItemCategory.Invalid;

            ItemRarity rarity = Enum.TryParse(catalogItem.rarity, true, out ItemRarity itemRarity)
                ? itemRarity
                : ItemRarity.Invalid;

            var multiplayerItem = new MultiplayerItem
            {
                count = count,
                guid = itemId,
                owned = owned,

                category = new MultiplayerItemCategory
                {
                    loc = category,
                    value = (int) category
                },

                rarity = new MultiplayerItemRarity
                {
                    loc = rarity,
                    value = (int) rarity
                }
            };

            if (unlockedOn != null)
            {
                multiplayerItem.unlocked = new MultiplayerItemUnlocked
                {
                    on = EpochTime.GetIntDate(unlockedOn.Value)
                };
            }
            
            if (itemInstance != null)
            {
                multiplayerItem.instance_data = itemInstance;
            }

            return multiplayerItem;
        }
    }

    public class MultiplayerItemCategory
    {
        public ItemCategory loc { get; set; }
        public int value { get; set; }
    }

    public enum ItemCategory
    {
        Mobs = 1,
        Construction,
        Nature,
        Equipment,
        Items,
        Invalid
    }
    public class MultiplayerItemRarity
    {
        public ItemRarity loc { get; set; }
        public int value { get; set; }
    }

    public enum ItemRarity
    {
        Common = 0,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Invalid = 6
    }

    public class MultiplayerItemUnlocked
    {
        public long on { get; set; }
    }
}