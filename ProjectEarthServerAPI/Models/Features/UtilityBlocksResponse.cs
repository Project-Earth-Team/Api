using System;
using System.Collections.Generic;

namespace ProjectEarthServerAPI.Models.Features
{
    public class UtilityBlocksResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public Dictionary<string,CraftingSlotInfo> crafting { get; set; }
            public Dictionary<string,SmeltingResponse> smelting { get; set; }
        }
    }
}