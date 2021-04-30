using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
using Serilog;

namespace ProjectEarthServerAPI.Controllers
{
	[Authorize]
    public class TappablesController : Controller
	{
		[HttpPost]
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/tappables/{x}_{y}")]
		public async Task<IActionResult> Post(int x, int y)
		{
			   string authtoken = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var stream = new StreamReader(Request.Body);
				var body = await stream.ReadToEndAsync();

				var req = JsonConvert.DeserializeObject<TappableRequest>(body);

				Random random = new Random();

				var type = StateSingleton.Instance.activeTappableTypes[req.id];
				List<List<string>> availableDropSets = null;
				try
				{
					availableDropSets = StateSingleton.Instance.tappableData[type];
				}
				catch (Exception e)
				{
					Log.Error("[Tappables] no json file for tappable type"+type+" exists in data/tappables");
				}

				if (availableDropSets == null)
				{
					Log.Error($"[Tappables] No drop sets for {type}!");
				}
				var targetDropSet = availableDropSets[random.Next(0, availableDropSets.Count)]; 
				if (targetDropSet == null)
				{
					Log.Error($"[Tappables] targetDropSet is null! Available drop set count was {availableDropSets.Count}");
				}
				var rewards = new RewardComponent[targetDropSet.Count];
				for (int i = 0; i < targetDropSet.Count; i++)
				{
					rewards[i] = new RewardComponent()
					{
						Amount = random.Next(1, 3),
						Id = new Guid(targetDropSet[i])
					};
				}

				var response = new TappableResponse()
				{
					result = new TappableResponse.Result()
					{
						token = new Token()
						{
							clientProperties = new Dictionary<string, string>(),
							clientType = "redeemtappable",
							lifetime = "Persistent",
							rewards = new Rewards()
							{
								ExperiencePoints = 400,
								Inventory = rewards
							}
						}
					}
				};

				response.updates = RewardUtils.RedeemRewards(authtoken, response.result.token.rewards);
				response.result.updates = response.updates;

				return Content(JsonConvert.SerializeObject(response), "application/json");
			
	}
    }
}
