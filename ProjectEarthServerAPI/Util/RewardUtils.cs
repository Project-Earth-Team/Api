﻿using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Util
{
    public class RewardUtils
    {
        public static Updates RedeemRewards(string playerId, Rewards rewards)
        {
            Updates updates = new Updates();
            uint nextStreamId = (uint) GenericUtils.GetNextStreamVersion();
            foreach (var buildplate in rewards.Buildplates)
            {
                // BuildplateUtils.AddToPlayer(playerId, buildplate.id);
                // updates.buildplates = nextStreamId;
            }

            foreach (var challenge in rewards.Challenges)
            {
                // ChallengeUtils.AddToPlayer(playerId, challenge.id);
                // updates.challenges = nextStreamId;
            }

            foreach (var item in rewards.Inventory)
            {
                InventoryUtils.AddItemToInv(playerId, item.Id, item.Amount);
                updates.inventory = nextStreamId;
            }

            foreach (var utilityBlock in rewards.UtilityBlocks) 
            {
                // TODO: This is most likely unused in the actual game, since crafting tables/furnaces dont have ids
            }

            foreach (var personaItem in rewards.PersonaItems)
            {
                // PersonaUtils.AddToPlayer(playerId, personaItem) If we can ever implement CC, this is already in place
            }

            //if (rewards.ExperiencePoints != null)
            //    PlayerUtils.AddExperience(playerId, rewards.ExperiencePoints.Value);

            return updates;
        }
    }
}