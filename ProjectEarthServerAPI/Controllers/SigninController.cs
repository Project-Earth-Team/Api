using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/player/profile/signin")]
    public class SigninController : Controller
    {
        [HttpPost]
        public ContentResult Post()
        {
            var response = new SigninResponse.ResponseTemplate()
            {
                result = new SigninResponse.Result()
                {
                    AuthenticationToken =
                        "CfDJ8L6Ty7FtvPNMhWU6CQQMcb5b1o8grZXzxMwEiJa2FrLTR4tjA31k49sDku6mxqPPgdBNzLZ90aJbAfuqdfnm-aePgF7HqTgNWSAmDf4YK5Cxx3cs35mdTXEfU74GU3UW2_w94dZ119jpWcrNPKREL-6zkEmCVW-bOZ2KyHyfzAsw6QbqqP01NHQVHOu_cWd6NCOiqO9PH0G1keOsbyx90CsRgW5uWsU-_KGWmi_LoKZkpFfoVb_DxH7RmMmgdEfGS0dXA6Of59A_J2kkmwL3l8DcyJlA-WbX_wuJaHPxIqhcDdq9z4CIHesplMjUasxiFVmcSh_ABZ2OqQrj3aRMY0vpkbCXA9ChFTHOgofQbjIF8ZYgdebGGrooMp3qwZ0XSyiTb4Zk4BprH7KFlBEKWwCRpMJbSE8lrQCXsihAy4w6X-zAoG66ckW1CTvQNqMuNEjC4UebZvA9JrHbUEZQLxbnfIKs2KZTBejqg5cESILf9kffhaY5avwAev6jnhtGgU7w_nOLITt50eH_-dXBIBeLMpBQS6VDgPZmXx24xDm8afj6BwPGSLHVYOg7uF3u--LryUWug6FfqDIqMJAUZn0LOJxUSF395mU2avUMPY-B8BaDF8MHtTw8BXGjK_Yi4wuDMD2KI5SfodoJRx24Cuu3PadOYxWT_RvO02L0ukIG8FtU1gg6G0jVaSlWipMYx8qvRrDaHyWhvKIvT7rUMGdLA_IEYdyaLIF9vZdFvjnq6zv1nMOvT8wQ7eF4xMB_5e8vtB_HvJTEfZNoR6GjuV-kG7t1aBrsUWIblK70Wv6Nhbj6KcN4zpuiT5GpIM6XrljPjWg-KgVXWmmYQ8t_rjnKuaho5btQ1EfecTgdvyrqI5nmMkYCMB-Q7k5_Pn7_DK2xcnrvuTW77XKl2_19of7Bgx4z0LFlh4wperGcAQbnPjMzmE5bloh_MTusGY0knc9qAQyiwocEdHUJNdw",
                    BasePath = "/1367834360",
                    Tokens = new object()
                }
            };
            //var resp = JsonConvert.SerializeObject(response);
            //return Content(resp, "application/json");
            return Content(
                "{\"result\":{\"authenticationToken\":\"abcdtest123\",\"tokens\":{\"9b0acea5-8eaf-4ccc-8f8d-187784dc5544\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"605dc84c-f6d1-442b-8901-69b7b89a0cc8\"}},\"c025a117-de76-49c8-924a-9c56986d7a98\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.pickup_mobs\",\"clientProperties\":{}},\"89e11f11-2582-47da-968d-764db91c7bd7\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.intro\",\"clientProperties\":{}},\"fc3028b9-36bf-473f-87ab-598ddc3d468f\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.menu\",\"clientProperties\":{}},\"80c106d4-3f80-4f42-8f1c-211278b1c2ef\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"21dabe32-6d4e-4954-ab0d-8a21f5e51665\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"7b6ecfe4-ad61-4353-af7b-cd005f0e3d1e\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2ba1bbef-fc22-42e5-b1af-81f00c130a5b\"}},\"e74024fd-6c40-42b6-ba84-1a315e1f5982\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"cc456b52-1586-4e75-b7e9-aa811f609567\"}},\"a33fd95f-f567-4c53-ae6e-4fad94975e80\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2bf15fa5-50ed-41ce-a2c4-b870028ed991\"}},\"b730a241-3966-4731-96c4-25387493f9d2\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"98749e7b-6a14-4fac-a851-3a68511c3f77\"}},\"e23a3f79-6362-4fff-8d10-636ab4052d10\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"07f1e716-b1d5-49df-ac81-fb8ce184746e\"}},\"4fbcb172-c215-482e-870b-6d3288a42cfb\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"ba777233-973b-4536-96e9-3e8abb3ae10a\"}},\"d702f10e-d616-444a-bf4b-3e2b9cbe3d07\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"5686ddbd-3da4-46da-a719-8e95e486cdb4\"}},\"9e9addd7-812e-4135-8edc-1155bf712240\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{}},\"39a32e0f-f19d-442a-9ae3-26785dd4b14a\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3064a2c5-e5f6-44ed-9561-7e972e1c1410\"}},\"54b85e5d-1485-432f-929e-3845edb1e301\":{\"lifetime\":\"Persistent\",\"clientType\":\"empty.bucket.returned\",\"clientProperties\":{}},\"ab21c03d-18c0-441a-b78a-79fa429391d8\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.adventures\",\"clientProperties\":{},\"rewards\":{\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"98c38d85-525e-4edf-849a-58ff68219c37\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"3a99af70-bbf3-4041-8094-491bed0c3845\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"af694f0c-b448-44a2-9d73-b2cc5e74b271\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"4c5bd76e-d57a-4ede-9193-eff0c12a6f8b\"}},\"878e2acc-238f-4bd3-870e-ebba2d374c68\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"d5f19783-57ee-4bf9-883b-736680e9237f\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}}},\"updates\":{},\"basePath\":\"/1\",\"mrToken\":null,\"mixedReality\":null,\"streams\":null,\"clientProperties\":{}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}",
                "application/json");

        }
    }

    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/resourcepacks/2020.1217.02/default")]
    public class PlayerResourcePackController : Controller
    {
        public ContentResult Get()
        {
            return Content(
                "{\"result\":[{\"parsedResourcePackVersion\":[2020,1214,4],\"resourcePackVersion\":\"2020.1214.04\",\"resourcePackId\":\"dba38e59-091a-4826-b76a-a08d7de5a9e2\",\"order\":0,\"relativePath\":\"availableresourcepack/resourcepacks/dba38e59-091a-4826-b76a-a08d7de5a9e2-1301b0c257a311678123b9e7325d0d6c61db3c35\"}]}",
                "application/json");
        }
    }
}
