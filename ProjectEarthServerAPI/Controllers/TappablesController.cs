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

			var response = TappableUtils.RedeemTappableForPlayer(authtoken, req);

			return Content(JsonConvert.SerializeObject(response), "application/json");
		}
	}
}
