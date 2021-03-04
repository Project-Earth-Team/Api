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
using ProjectEarthServerAPI.Models;

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
					token = new Token()
					{
						clientProperties = new Dictionary<string, string>(),
						clientType = "redeemtappable",
						lifetime = "Persistent",
						rewards = new Rewards()
						{
							ExperiencePoints = 100,
							Inventory = new RewardComponent[]
							{
								new RewardComponent()
								{
									Amount = 10,
									Id = "1eaa0d8c-2d89-2b84-aa1f-b75ccc85faff"
								},
								new Models.RewardComponent()
                                {
                                    Amount = 10,
                                    Id = "11111111-1111-1111-1111-111111111111"
								},
								new RewardComponent()
                                {
                                    Amount = 10,
                                    Id = "0d78acb3-3dc9-bbc4-01ae-8b10c76867f9"
								},
                                new RewardComponent()
                                {
                                    Amount = 10,
                                    Id = "6c5210db-3c17-6731-5abe-1853bced8b3b"
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
