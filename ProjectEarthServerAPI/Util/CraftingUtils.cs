using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Features;

namespace ProjectEarthServerAPI.Util
{
    public class CraftingUtils
    {
        private static Recipes recipeList = Program.recipeList;
        private static Dictionary<string,Dictionary<int, CraftingSlotInfo>> craftingJobs = new Dictionary<string, Dictionary<int, CraftingSlotInfo>>();
        public static bool StartCraftingJob(string playerId, int slot,CraftingRequest request) // TODO: Check if slot not unlocked (not a big priority)
        {
            recipeList ??= Recipes.FromFile("./recipes");


            var recipe = recipeList.result.crafting.Find(match => match.id == request.RecipeId);

            if (recipe != null)
            {
                var itemsToReturn = recipe.returnItems.ToList();

                foreach (RecipeIngredients ingredient in recipe.ingredients)
                {
                    if (itemsToReturn.Find(match =>
                        match.id == ingredient.items[0] && match.amount == ingredient.quantity) == null)
                    {
                        InventoryUtils.RemoveItemFromInv(playerId, ingredient.items[0], null, ingredient.quantity);
                    }
                }

                CraftingSlotInfo job = new CraftingSlotInfo
                {
                    available = 0,
                    boostState = null,
                    completed = 0,
                    escrow = request.Ingredients,
                    nextCompletionUtc = DateTime.UtcNow.Add(recipe.duration.TimeOfDay),
                    output = recipe.output,
                    recipeId = recipe.id,
                    sessionId = request.SessionId,
                    state = "Active",
                    streamVersion = 1, // TODO: Stream ID
                    total = request.Multiplier,
                    totalCompletionUtc = DateTime.UtcNow.Add(recipe.duration.TimeOfDay*request.Multiplier),
                    unlockPrice = null

                };

                job.output.quantity *= request.Multiplier;

                if (!craftingJobs.ContainsKey(playerId))
                {
                    craftingJobs.Add(playerId,new Dictionary<int, CraftingSlotInfo>());
                    craftingJobs[playerId].Add(1,new CraftingSlotInfo());
                    craftingJobs[playerId].Add(2, new CraftingSlotInfo());
                    craftingJobs[playerId].Add(3, new CraftingSlotInfo());
                }

                craftingJobs[playerId][slot] = job;
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
                var updates = new Dictionary<string,int>();

                if (job.totalCompletionUtc != null && job.completed == 0 && DateTime.Compare(job.totalCompletionUtc.Value,DateTime.UtcNow) < 0)
                {
                    if (job.nextCompletionUtc.Value.Second == job.totalCompletionUtc.Value.Second)
                    {
                        job.completed = 1;
                        job.available = 1;
                        job.nextCompletionUtc = null;
                        job.state = "Completed";
                        job.streamVersion = 2; // TODO: Stream ID
                        job.escrow = new InputItem[0];
                    }
                    else
                    {
                        job.available = 1;
                        job.state = "Available";
                        job.streamVersion = 2; // TODO: Stream ID
                        job.nextCompletionUtc = job.nextCompletionUtc.Value.Add(recipe.duration.TimeOfDay);

                        for (int i = 0; i < job.escrow.Length-1; i++)
                        {
                            job.escrow[i].quantity -= recipe.ingredients[i].quantity;
                        }

                    }
                    updates.Add("crafting", 2); // TODO: Stream ID
                }

                var returnResponse = new CraftingSlotResponse
                {
                    result = job,
                    updates = updates
                };

                return returnResponse;
            }
            catch (Exception e )
            {
                Console.WriteLine($"Error while getting crafting job info: Player ID: {playerId} Crafting Slot: {slot}");
                Console.WriteLine($"Exception: {e}");
                return null;
            }

        }

        public static CollectItemsResponse FinishCraftingJob(string playerId, int slot)
        {
            var job = craftingJobs[playerId][slot];

            InventoryUtils.AddItemToInv(playerId, job.output.itemId, job.output.quantity);
            // TODO: Add to challenges, tokens, journal (when implemented)


            var returnResponse = new CollectItemsResponse
            {
                rewards = new Rewards(),
                updates = new Dictionary<string, int>()
            };


            returnResponse.rewards.Inventory = returnResponse.rewards.Inventory.Append(new RewardComponent
            {
                Amount = job.output.quantity,
                Id = job.output.itemId
            }).ToArray();

            if (!TokenUtils.GetTokenResponseForUserId(playerId).Result.tokens.Any(match => match.Value.clientProperties.ContainsKey("itemid") && match.Value.clientProperties["itemid"] == job.output.itemId))
            {
                TokenUtils.AddItemToken(playerId, job.output.itemId);
                returnResponse.updates.Add("tokens", 3); // TODO: Stream ID
            }

            job.nextCompletionUtc = null;
            job.available = 0;
            job.completed = 0;
            job.recipeId = null;
            job.sessionId = null;
            job.state = null; // This ones probably not right
            job.total = 0;
            job.boostState = null;
            job.totalCompletionUtc = null;
            job.unlockPrice = null;
            job.output = null;
            //craftingJobs[playerId].Remove(slot);


            returnResponse.updates.Add("crafting",3); // TODO: Stream ID
            returnResponse.updates.Add("inventory",3); // TODO: Stream ID

            return returnResponse;

        }
    }

    public class CraftingJob
    {
        public string recipeId { get; set; }
        public string sessionId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime timeRemaining { get; set; }
        public RecipeOutput output { get; set; }
    }
}