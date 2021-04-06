using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Features;
using Serilog;

namespace ProjectEarthServerAPI.Util
{
    public class CraftingUtils // TODO: Bug Fix: Cancelling a crafting task that you already collected some results
                               // TODO: for returns the entire crafting cost, not just the remaining part
    {
        private static Recipes recipeList = StateSingleton.Instance.recipes;
        private static Dictionary<string, Dictionary<int, CraftingSlotInfo>> craftingJobs = new Dictionary<string, Dictionary<int, CraftingSlotInfo>>();
        public static bool StartCraftingJob(string playerId, int slot, CraftingRequest request) // TODO: Check if slot not unlocked (not a big priority)
        {
            recipeList ??= Recipes.FromFile("./data/recipes");

            var recipe = recipeList.result.crafting.Find(match => match.id == request.RecipeId);

            if (recipe != null)
            {
                var itemsToReturn = recipe.returnItems.ToList();

                foreach (RecipeIngredients ingredient in recipe.ingredients)
                {
                    if (itemsToReturn.Find(match =>
                        match.id == ingredient.items[0] && match.amount == ingredient.quantity) == null)
                    {
                        InventoryUtils.RemoveItemFromInv(playerId, ingredient.items[0], ingredient.quantity * request.Multiplier);
                    }
                }

                var nextStreamId = GenericUtils.GetNextStreamVersion();

                CraftingSlotInfo job = new CraftingSlotInfo
                {
                    available = 0,
                    boostState = null,
                    completed = 0,
                    escrow = request.Ingredients,
                    nextCompletionUtc = null,
                    output = recipe.output,
                    recipeId = recipe.id,
                    sessionId = request.SessionId,
                    state = "Active",
                    streamVersion = nextStreamId,
                    total = request.Multiplier,
                    totalCompletionUtc = DateTime.UtcNow.Add(recipe.duration.TimeOfDay*request.Multiplier),
                    unlockPrice = null

                };

                if (request.Multiplier != 1)
                {
                    job.nextCompletionUtc = DateTime.UtcNow.Add(recipe.duration.TimeOfDay);
                }

                if (!craftingJobs.ContainsKey(playerId))
                {
                    craftingJobs.Add(playerId, new Dictionary<int, CraftingSlotInfo>());
                    craftingJobs[playerId].Add(1, new CraftingSlotInfo());
                    craftingJobs[playerId].Add(2, new CraftingSlotInfo());
                    craftingJobs[playerId].Add(3, new CraftingSlotInfo());
                }

                craftingJobs[playerId][slot] = job;

                UtilityBlockUtils.UpdateUtilityBlocks(playerId, slot, job);

                Log.Debug($"[{playerId}]: Initiated crafting job in slot {slot}.");

                return true;
            }

            return false;

        }

        public static CraftingSlotResponse GetCraftingJobInfo(string playerId, int slot)
        {

            try
            {
                var job = craftingJobs[playerId][slot];
                var recipe = recipeList.result.crafting.Find(match => match.id == job.recipeId & !match.deprecated);
                var updates = new Updates();
                var nextStreamId = GenericUtils.GetNextStreamVersion();

                job.streamVersion = nextStreamId;

                if (job.totalCompletionUtc != null && DateTime.Compare(job.totalCompletionUtc.Value, DateTime.UtcNow) < 0 && job.recipeId != null) 
                {                                                                                                                                  
                                                                                                                                                   
                    job.available = job.total - job.completed;
                    job.completed += job.available;
                    job.nextCompletionUtc = null;
                    job.state = "Completed";
                    job.escrow = new InputItem[0];
                }
                /*else
                {

                    job.available++;
                    //job.completed++;
                    job.state = "Available";
                    job.streamVersion = nextStreamId;
                    job.nextCompletionUtc = job.nextCompletionUtc.Value.Add(recipe.duration.TimeOfDay);

                    for (int i = 0; i < job.escrow.Length - 1; i++)
                    {
                        job.escrow[i].quantity -= recipe.ingredients[i].quantity;
                    }

                }*/

                updates.crafting = (uint) nextStreamId;

                var returnResponse = new CraftingSlotResponse
                {
                    result = job,
                    updates = updates
                };

                UtilityBlockUtils.UpdateUtilityBlocks(playerId, slot, job);

                Log.Debug($"[{playerId}]: Requested crafting slot {slot} status.");

                return returnResponse;
            }
            catch (Exception e )
            {
                Log.Error($"[{playerId}]: Error while getting crafting job info! Crafting Slot: {slot}");
                Log.Debug($"Exception: {e.StackTrace}");
                return null;
            }

        }

        public static CollectItemsResponse FinishCraftingJob(string playerId, int slot)
        {
            var job = craftingJobs[playerId][slot];
            var recipe = recipeList.result.crafting.Find(match => match.id == job.recipeId & !match.deprecated);
            int craftedAmount = 0;

            var nextStreamId = GenericUtils.GetNextStreamVersion();

            var returnResponse = new CollectItemsResponse
            {
                result = new CollectItemsInfo
                {
                    rewards = new Rewards(),
                },
                updates = new Dictionary<string, int>()
            };

            if (job.completed != job.total && job.nextCompletionUtc != null)
            {
                if (DateTime.UtcNow >= job.nextCompletionUtc)
                {
                    craftedAmount++;
                    while (DateTime.UtcNow >= job.nextCompletionUtc && job.nextCompletionUtc.Value.Add(recipe.duration.TimeOfDay) < job.totalCompletionUtc && craftedAmount < job.total-job.completed)
                    {
                        job.nextCompletionUtc = job.nextCompletionUtc.Value.Add(recipe.duration.TimeOfDay);
                        craftedAmount++;
                    }

                    job.nextCompletionUtc = job.nextCompletionUtc.Value.Add(recipe.duration.TimeOfDay);
                    job.completed += craftedAmount;
                    //job.available -= craftedAmount;
                    for (int i = 0; i < job.escrow.Length-1; i++)
                    {
                        job.escrow[i].quantity -= recipe.ingredients[i].quantity*craftedAmount;
                    }

                    job.streamVersion = nextStreamId;

                    InventoryUtils.AddItemToInv(playerId, job.output.itemId, job.output.quantity*craftedAmount);
                }
            }
            else
            {
                craftedAmount = job.available;
                InventoryUtils.AddItemToInv(playerId, job.output.itemId, job.output.quantity * craftedAmount);
                // TODO: Add to challenges, tokens, journal (when implemented)
            }

            if (!TokenUtils.GetTokenResponseForUserId(playerId).Result.tokens.Any(match => match.Value.clientProperties.ContainsKey("itemid") && match.Value.clientProperties["itemid"] == job.output.itemId.ToString()))
            {
                //TokenUtils.AddItemToken(playerId, job.output.itemId); -> List of item tokens not known. Could cause issues later, for now we just disable it.
                returnResponse.updates.Add("tokens", nextStreamId);
            }

            returnResponse.result.rewards.Inventory = returnResponse.result.rewards.Inventory.Append(new RewardComponent
            {
                Amount = job.output.quantity*craftedAmount,
                Id = job.output.itemId
            }).ToArray();

            returnResponse.updates.Add("crafting", nextStreamId);
            returnResponse.updates.Add("inventory", nextStreamId);


            if (job.completed == job.total || job.nextCompletionUtc == null)
            {
                job.nextCompletionUtc = null;
                job.available = 0;
                job.completed = 0;
                job.recipeId = null;
                job.sessionId = null;
                job.state = "Empty";
                job.total = 0;
                job.boostState = null;
                job.totalCompletionUtc = null;
                job.unlockPrice = null;
                job.output = null;
                job.streamVersion = nextStreamId;
            }

            UtilityBlockUtils.UpdateUtilityBlocks(playerId, slot, job);

            Log.Debug($"[{playerId}]: Collected results of crafting slot {slot}.");

            return returnResponse;

        }

        public static CraftingSlotResponse CancelCraftingJob(string playerId, int slot)
        {
            var job = craftingJobs[playerId][slot];
            var nextStreamId = GenericUtils.GetNextStreamVersion();
            var resp = new CraftingSlotResponse
            {
                result = new CraftingSlotInfo(),
                updates = new Updates()
        };

            foreach (InputItem item in job.escrow)
            {
                InventoryUtils.AddItemToInv(playerId, item.itemId, item.quantity);
            }

            job.nextCompletionUtc = null;
            job.available = 0;
            job.completed = 0;
            job.recipeId = null;
            job.sessionId = null;
            job.state = "Empty";
            job.total = 0;
            job.boostState = null;
            job.totalCompletionUtc = null;
            job.unlockPrice = null;
            job.output = null;
            job.streamVersion = nextStreamId;

            UtilityBlockUtils.UpdateUtilityBlocks(playerId, slot, job);

            Log.Debug($"[{playerId}]: Cancelled crafting job in slot {slot}.");

            resp.updates.crafting = (uint) nextStreamId;
            return resp;
        }

        public static CraftingUpdates UnlockCraftingSlot(string playerId, int slot)
        {
            var job = craftingJobs[playerId][slot];

            RubyUtils.SetRubies(playerId, job.unlockPrice.cost - job.unlockPrice.discount, false);
            job.state = "Empty";
            job.unlockPrice = null;

            var nextStreamId = GenericUtils.GetNextStreamVersion();
            var returnUpdates = new CraftingUpdates
            {
                updates = new Dictionary<string, int>()
            };

            UtilityBlockUtils.UpdateUtilityBlocks(playerId, slot, job);

            Log.Debug($"[{playerId}]: Unlocked crafting slot {slot}.");

            returnUpdates.updates.Add("crafting", nextStreamId);

            return returnUpdates;

        }
    }
}