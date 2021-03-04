using System;
using System.Collections.Generic;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Models.Features
{
    public class SmeltingSlotInfo // smelting endpoints, like crafting, also used in utilityBlocks 
    {
        public FuelInfo fuel { get; set; } // Why is this used? When used, same info as burning.fuel, but quantity always 0?
        public BurningItems burning { get; set; } // Information about currently burning items/burn rate
        public bool? hasSufficientFuel { get; set; } // If item has enough fuel to complete process. 
        public float? heatAppliedToCurrentItem { get; set; } // Heat value to cook current item.
        public string sessionId { get; set; } // See crafting variant
        public string recipeId { get; set; } // See crafting variant
        public RecipeOutput output { get; set; } // See crafting variant
        public InputItem[] escrow { get; set; } // See crafting variant
        public int completed { get; set; } // See crafting variant
        public int available { get; set; } // See crafting variant
        public int total { get; set; } // See crafting variant
        public DateTime? nextCompletionUtc { get; set; } // See crafting variant
        public DateTime? totalCompletionUtc { get; set; } // See crafting variant
        public string state { get; set; } // Active, completed, or locked (or Empty)
        public BoostState boostState { get; set; } // See crafting variant
        public UnlockPrice unlockPrice { get; set; } // See crafting variant
        public int streamVersion { get; set; } // See crafting variant
    }

    public class SmeltingSlotResponse
    {
        public SmeltingSlotInfo result { get; set; }
        public Updates updates { get; set; }
    }
    
    public class BurningItems
    {
        public DateTime? burnStartTime { get; set; } // When the items started burning
        public DateTime? burnsUntil { get; set; } // How long the items are burning
        public FuelInfo fuel { get; set; } // What is burning
        public double heatDepleted { get; set; } // How much heat has been generated
        public TimeSpan remainingBurnTime { get; set; } // Burning time remaining
    }

    public class FuelInfo
    {
        public BurnInfo burnRate { get; set; } // How fast the items are burning
        public string itemId { get; set; } // Item ID of fuel
        public InventoryResponse.ItemInstance[] itemInstanceIds { get; set; } // Instance of the fuel item (Probably used when burning tools
        public int quantity { get; set; } // How many items are used as fuel
    }

    public class BurnInfo
    {
        public int burnTime { get; set; } // How long one item burns
        public int heatPerSecond { get; set; } // How much heat it generates
    }
}