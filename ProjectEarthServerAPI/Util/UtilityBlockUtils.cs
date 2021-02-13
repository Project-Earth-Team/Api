using ProjectEarthServerAPI.Models.Features;

namespace ProjectEarthServerAPI.Util
{
    public class UtilityBlockUtils
    {
        public static UtilityBlocksResponse ReadUtilityBlocks(string playerId)
        {
            return GenericUtils.ParseJsonFile<UtilityBlocksResponse>(playerId, "utilityBlocks");
        }

        public static bool WriteUtilityBlocks(string playerId, UtilityBlocksResponse obj)
        {
            return GenericUtils.WriteJsonFile(playerId, obj, "utilityBlocks");
        }

        public static bool UpdateUtilityBlocks(string playerId, CraftingJob job)
        {
            return true;
        }

    }
}