using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
using Serilog;

namespace ProjectEarthServerAPI.Controllers
{
	[ApiVersion("1.1")]
	public class SigninController : ControllerBase
	{
		[Authorize]
		[HttpGet]
		[Route("1/api/v{version:apiVersion}/player/profile/{profileID}")]
		public IActionResult Get(string profileID)
		{
			var response = new ProfileResponse(ProfileUtils.ReadProfile(profileID));
			return Content(JsonConvert.SerializeObject(response), "application/json");
		}

		[Route("api/v{version:apiVersion}/player/profile/{profileID}")]
		[HttpPost]
		public async Task<IActionResult> Post(string jsonstring, string profileID)
		{
			if (profileID != "signin")
			{
				return BadRequest();
			}

			var httprequest = Request.Body;
			StreamReader sr = new StreamReader(httprequest);
			var request = JsonConvert.DeserializeObject<SigninRequest>(await sr.ReadToEndAsync());

			var playerid = request.sessionTicket.Split("-")[0];

			var response = new SigninResponse.ResponseTemplate()
			{
				result = new SigninResponse.Result()
				{
					AuthenticationToken = playerid,
					BasePath = "/1",
					Tokens = TokenUtils.GetSigninTokens(playerid),
					ClientProperties = new object(),
					Updates = new Updates()
				},
				updates = new Updates()
			};

			var resp = JsonConvert.SerializeObject(response, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

			Log.Information($"[{playerid}]: Logged in.");

			return Content(resp, "application/json");
			//return Content(
			//    "{\"result\":{\"authenticationToken\":\"" + playerid + "\",\"tokens\":{\"9b0acea5-8eaf-4ccc-8f8d-187784dc5544\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"605dc84c-f6d1-442b-8901-69b7b89a0cc8\"}},\"c025a117-de76-49c8-924a-9c56986d7a98\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.pickup_mobs\",\"clientProperties\":{}},\"89e11f11-2582-47da-968d-764db91c7bd7\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.intro\",\"clientProperties\":{}},\"fc3028b9-36bf-473f-87ab-598ddc3d468f\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.menu\",\"clientProperties\":{}},\"80c106d4-3f80-4f42-8f1c-211278b1c2ef\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"21dabe32-6d4e-4954-ab0d-8a21f5e51665\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"7b6ecfe4-ad61-4353-af7b-cd005f0e3d1e\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2ba1bbef-fc22-42e5-b1af-81f00c130a5b\"}},\"e74024fd-6c40-42b6-ba84-1a315e1f5982\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"cc456b52-1586-4e75-b7e9-aa811f609567\"}},\"a33fd95f-f567-4c53-ae6e-4fad94975e80\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2bf15fa5-50ed-41ce-a2c4-b870028ed991\"}},\"b730a241-3966-4731-96c4-25387493f9d2\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"98749e7b-6a14-4fac-a851-3a68511c3f77\"}},\"e23a3f79-6362-4fff-8d10-636ab4052d10\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"07f1e716-b1d5-49df-ac81-fb8ce184746e\"}},\"4fbcb172-c215-482e-870b-6d3288a42cfb\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"ba777233-973b-4536-96e9-3e8abb3ae10a\"}},\"d702f10e-d616-444a-bf4b-3e2b9cbe3d07\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"5686ddbd-3da4-46da-a719-8e95e486cdb4\"}},\"9e9addd7-812e-4135-8edc-1155bf712240\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{}},\"39a32e0f-f19d-442a-9ae3-26785dd4b14a\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3064a2c5-e5f6-44ed-9561-7e972e1c1410\"}},\"54b85e5d-1485-432f-929e-3845edb1e301\":{\"lifetime\":\"Persistent\",\"clientType\":\"empty.bucket.returned\",\"clientProperties\":{}},\"ab21c03d-18c0-441a-b78a-79fa429391d8\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.adventures\",\"clientProperties\":{},\"rewards\":{\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"98c38d85-525e-4edf-849a-58ff68219c37\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"3a99af70-bbf3-4041-8094-491bed0c3845\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"af694f0c-b448-44a2-9d73-b2cc5e74b271\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"4c5bd76e-d57a-4ede-9193-eff0c12a6f8b\"}},\"878e2acc-238f-4bd3-870e-ebba2d374c68\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"d5f19783-57ee-4bf9-883b-736680e9237f\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}}},\"updates\":{},\"basePath\":\"/1\",\"mrToken\":null,\"mixedReality\":null,\"streams\":null,\"clientProperties\":{}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}",
			//    "application/json");
		}
	}
}
