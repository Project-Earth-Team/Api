using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Controllers
{
    // TODO: Not done. Rewards need inventory implementation, timers for crafting process, and recipeId -> recipe time checks
    public class CraftingController : Controller
    {
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}/start")]
        public async Task<IActionResult> PostNewCraftingJob(int slot)
        {
            string authtoken;
            try
            {
                authtoken = HttpContext.Request.Headers["Authorization"].ToString().Remove(0, 6);
            }
            catch
            {
                return Forbid();
            }

            var stream = new StreamReader(Request.Body);
            var body = await stream.ReadToEndAsync();

            var req = JsonConvert.DeserializeObject<CraftingRequest>(body);

            var craftingJob = CraftingUtils.StartCraftingJob(authtoken, slot, req); 

            Console.WriteLine($"User with id {authtoken} initiated crafting job in slot {slot}.");

            var updateResponse = new CraftingUpdates
            {
                updates = new Dictionary<string, int>()
            };

            var nextStreamId = GenericUtils.GetNextStreamVersion();

            updateResponse.updates.Add("crafting", nextStreamId);
            updateResponse.updates.Add("inventory", nextStreamId);

            return Content(JsonConvert.SerializeObject(updateResponse), "application/json");
            //return Accepted(Content(returnUpdates, "application/json"));
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/finish/price")]
        public IActionResult GetCraftingPrice(int slot)
        {
            TimeSpan remainingTime = TimeSpan.Parse(Request.Query["remainingTime"]);
            var returnPrice = new CraftingPriceResponse
            {
                result = new CraftingPrice{
                cost = 1,
                discount = 0,
                validTime = remainingTime
                },
                updates = new Dictionary<string, int>()
            };

            return Content(JsonConvert.SerializeObject(returnPrice), "application/json");
        }


        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}")]
        public IActionResult GetCraftingStatus(int slot)
        {
            string authtoken;
            try
            {
                authtoken = HttpContext.Request.Headers["Authorization"].ToString().Remove(0, 6);
            }
            catch
            {
                return Forbid();
            }

            var craftingStatus = CraftingUtils.GetCraftingJobInfo(authtoken, slot);

            Console.WriteLine($"User with id {authtoken} requested crafting slot {slot} status.");


            return Content(JsonConvert.SerializeObject(craftingStatus),"application/json");
            //return Accepted(Content(returnTokens, "application/json"));
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}/collectItems")]
        public IActionResult GetCollectCraftingItems(int slot)
        {
            string authtoken;
            try
            {
                authtoken = HttpContext.Request.Headers["Authorization"].ToString().Remove(0, 6);
            }
            catch
            {
                return Forbid();
            }

            var returnUpdates = CraftingUtils.FinishCraftingJob(authtoken, slot);

            Console.WriteLine($"User with id {authtoken} collected results of crafting slot {slot}.");


            return Content(JsonConvert.SerializeObject(returnUpdates), "application/json");
            //return Accepted(Content(returnTokens, "application/json"));
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}/stop")]
        public IActionResult GetStopCraftingJob(int slot)
        {
            string authtoken;
            try
            {
                authtoken = HttpContext.Request.Headers["Authorization"].ToString().Remove(0, 6);
            }
            catch
            {
                return Forbid();
            }

            var returnUpdates = CraftingUtils.CancelCraftingJob(authtoken, slot);

            Console.WriteLine($"User with id {authtoken} cancelled crafting job in slot {slot}.");

            return Accepted();

            //return Content(JsonConvert.SerializeObject(returnUpdates), "application/json");
            //return Accepted(Content(returnTokens, "application/json"));
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}/unlock")]
        public IActionResult GetUnlockCraftingSlot(int slot)
        {
            string authtoken;
            try
            {
                authtoken = HttpContext.Request.Headers["Authorization"].ToString().Remove(0, 6);
            }
            catch
            {
                return Forbid();
            }

            var returnUpdates = CraftingUtils.UnlockCraftingSlot(authtoken, slot);

            Console.WriteLine($"User with id {authtoken} cancelled crafting job in slot {slot}.");

            return Content(JsonConvert.SerializeObject(returnUpdates),"application/json");
        }
    }
}
