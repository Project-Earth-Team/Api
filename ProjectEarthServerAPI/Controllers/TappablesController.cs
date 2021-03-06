using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Controllers
{
	[Authorize]
	[ApiVersion("1.1")]
	[Route("1/api/v{version:apiVersion}/tappables/{x}_{y}")]
	public class TappablesController : Controller
	{
		[HttpPost]
		public async Task<IActionResult> Post(int x, int y)
		{
			var stream = new StreamReader(Request.Body);
			var body = await stream.ReadToEndAsync();

			var req = JsonConvert.DeserializeObject<TappableRequest>(body);

			var response = new TappableResponse()
			{
				result = new TappableResponse.Result()
				{
					token = new Models.Token()
					{
						clientProperties = new Dictionary<string, string>(),
						clientType = "redeemtappable",
						lifetime = "Persistent",
						rewards = new Models.Rewards()
						{
							ExperiencePoints = 100,
							Inventory = new Models.RewardComponent[]
							{
								new Models.RewardComponent()
								{
									Amount = 10,
									Id = new Guid("1eaa0d8c-2d89-2b84-aa1f-b75ccc85faff")
								}
							}
						}
					},
					updates = new Models.Updates()
				},
				updates = new Models.Updates()
			};

			return Content(JsonConvert.SerializeObject(response), "application/json");
		}
	}
}
