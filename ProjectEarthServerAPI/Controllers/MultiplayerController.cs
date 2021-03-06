using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Buildplate;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Controllers
{
    public class MultiplayerController : Controller
    {
        [Authorize]
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/multiplayer/buildplate/{buildplateId}/instances")]
        public async Task<IActionResult> PostCreateInstance(string buildplateId)
        {
            string authtoken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stream = new StreamReader(Request.Body);
            var body = await stream.ReadToEndAsync();
            var parsedRequest = JsonConvert.DeserializeObject<BuildplateServerRequest>(body);

            var response = MultiplayerUtils.CreateBuildplateInstance(authtoken, buildplateId, parsedRequest.playerCoordinate);
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Authorize]
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/partitions/{worldId}/{instanceId}")]
        public IActionResult GetInstanceStatus(string worldId, string instanceId)
        {
            string authtoken = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = MultiplayerUtils.CheckInstanceStatus(authtoken, instanceId);
            if (response == null)
            {
                return NoContent();
            }
            else
            {
                return Content(JsonConvert.SerializeObject(response), "application/json");
            }
        }
    }
}
