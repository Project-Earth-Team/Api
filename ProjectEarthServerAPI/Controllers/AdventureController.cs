using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Multiplayer.Adventure;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.1")]
    public class AdventureScrollsController : Controller
    {
        [Route("1/api/v{version:apiVersion}/adventures/scrolls")]
        public ContentResult Get()
        {
            var responseobj = new ScrollsResponse();
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        } // TODO: Fixed String

        [Route("1/api/v{version:apiVersion}/adventures/scrolls/{crystalId}")]
        public async Task<ContentResult> PostRedeemCrystal(Guid crystalId)
        {
            var playerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stream = new StreamReader(Request.Body);
            var body = await stream.ReadToEndAsync();

            var req = JsonConvert.DeserializeObject<PlayerAdventureRequest>(body);
            var resp = AdventureUtils.RedeemCrystal(playerId, req, crystalId);

            return Content(JsonConvert.SerializeObject(resp), "application/json");
        }
    }
}
