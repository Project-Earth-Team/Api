using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Controllers
{
    // TODO: Not done. Rewards need inventory implementation, timers for crafting process, and recipeId -> recipe time checks
    public class CraftingController : Controller
    {
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/crafting/{slot}/start")]
        public IActionResult PostNewCraftingJob(int slot)
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

            //var returnUpdates = CraftingUtils.StartCraftingJob(authtoken); 

            Console.WriteLine($"User with id {authtoken} initiated crafting job in slot {slot}.");

            return Accepted();
            //return Accepted(Content(returnUpdates, "application/json"));
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

            //var returnUpdates = CraftingUtils.StartCraftingJob(authtoken);

            Console.WriteLine($"User with id {authtoken} requested crafting slot {slot} status.");


            return Ok();
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

            //var returnUpdates = CraftingUtils.StartCraftingJob(authtoken); // Not sure if even needed, lets hope not

            Console.WriteLine($"User with id {authtoken} collected results of crafting slot {slot}.");


            return Ok();
            //return Accepted(Content(returnTokens, "application/json"));
        }
    }
}
