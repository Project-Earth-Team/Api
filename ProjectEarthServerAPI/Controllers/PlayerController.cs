using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/tokens")]
    public class PlayerTokenController : Controller
    {
        public ContentResult Get()
        {
            return Content("{\"result\":{\"tokens\":{\"9b0acea5-8eaf-4ccc-8f8d-187784dc5544\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"605dc84c-f6d1-442b-8901-69b7b89a0cc8\"}},\"c025a117-de76-49c8-924a-9c56986d7a98\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.pickup_mobs\",\"clientProperties\":{}},\"89e11f11-2582-47da-968d-764db91c7bd7\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.intro\",\"clientProperties\":{}},\"fc3028b9-36bf-473f-87ab-598ddc3d468f\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.menu\",\"clientProperties\":{}},\"80c106d4-3f80-4f42-8f1c-211278b1c2ef\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"21dabe32-6d4e-4954-ab0d-8a21f5e51665\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"7b6ecfe4-ad61-4353-af7b-cd005f0e3d1e\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2ba1bbef-fc22-42e5-b1af-81f00c130a5b\"}},\"e74024fd-6c40-42b6-ba84-1a315e1f5982\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"cc456b52-1586-4e75-b7e9-aa811f609567\"}},\"a33fd95f-f567-4c53-ae6e-4fad94975e80\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2bf15fa5-50ed-41ce-a2c4-b870028ed991\"}},\"b730a241-3966-4731-96c4-25387493f9d2\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"98749e7b-6a14-4fac-a851-3a68511c3f77\"}},\"e23a3f79-6362-4fff-8d10-636ab4052d10\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"07f1e716-b1d5-49df-ac81-fb8ce184746e\"}},\"4fbcb172-c215-482e-870b-6d3288a42cfb\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"ba777233-973b-4536-96e9-3e8abb3ae10a\"}},\"d702f10e-d616-444a-bf4b-3e2b9cbe3d07\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"5686ddbd-3da4-46da-a719-8e95e486cdb4\"}},\"9e9addd7-812e-4135-8edc-1155bf712240\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{}},\"39a32e0f-f19d-442a-9ae3-26785dd4b14a\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3064a2c5-e5f6-44ed-9561-7e972e1c1410\"}},\"54b85e5d-1485-432f-929e-3845edb1e301\":{\"lifetime\":\"Persistent\",\"clientType\":\"empty.bucket.returned\",\"clientProperties\":{}},\"ab21c03d-18c0-441a-b78a-79fa429391d8\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.adventures\",\"clientProperties\":{},\"rewards\":{\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"923f9914-34bf-4135-90ed-b418c1be7af5\":{\"lifetime\":\"Persistent\",\"clientType\":\"challenge.completed\",\"clientProperties\":{\"challengeid\":\"78618f62-aff9-4dc9-b4fc-9b50e8b5db73\",\"category\":\"oobe\"},\"rewards\":{\"experiencePoints\":150,\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"98c38d85-525e-4edf-849a-58ff68219c37\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"3a99af70-bbf3-4041-8094-491bed0c3845\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"af694f0c-b448-44a2-9d73-b2cc5e74b271\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"4c5bd76e-d57a-4ede-9193-eff0c12a6f8b\"}},\"878e2acc-238f-4bd3-870e-ebba2d374c68\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"d5f19783-57ee-4bf9-883b-736680e9237f\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"9e0c471a-b7ed-4044-be1e-803e3f15cba4\":{\"lifetime\":\"Persistent\",\"clientType\":\"challenge.completed\",\"clientProperties\":{\"challengeid\":\"f77a732a-c21e-484c-aa9b-2ee41bdf8bf5\",\"category\":\"retention\"},\"rewards\":{\"inventory\":[{\"id\":\"4f16a053-4929-263a-c91a-29663e29df76\",\"amount\":1},{\"id\":\"bef8b1c2-54c6-4f69-a6c9-f1aa1ee18119\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/rubies")]
    public class PlayerRubiesController : Controller
    {
        public ContentResult Get()
        {
            var responseobj = new RubyResponse();
            responseobj.result = 500;
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/splitRubies")]
    public class PlayerSplitRubiesController : Controller
    {
        public ContentResult Get()
        {
            var responseobj = new SplitRubyResponse();
            responseobj.result = new SplitRubyResult();
            responseobj.result.earned = 500;
            responseobj.result.purchased = 0;
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/utilityBlocks")]
    public class PlayerUtilityBlocksController : Controller
    {
        public ContentResult Get()
        {
            return Content("{\"result\":{\"crafting\":{\"1\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Empty\",\"boostState\":null,\"unlockPrice\":null,\"streamVersion\":4},\"2\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4},\"3\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4}},\"smelting\":{\"1\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Empty\",\"boostState\":null,\"unlockPrice\":null,\"streamVersion\":4},\"2\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4},\"3\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/profile/{profileId}")]
    public class PlayerProfileController : Controller
    {
        public ContentResult Get(string profileId)
        {
            var response = "{\"result\":{\"totalExperience\":356,\"level\":1,\"currentLevelExperience\":356,\"experienceRemaining\":144,\"health\":null,\"healthPercentage\":100.0,\"levelDistribution\":{\"2\":{\"experienceRequired\":500,\"rewards\":{\"inventory\":[{\"id\":\"730573d1-ba59-4fd4-89e0-85d4647466c2\",\"amount\":1},{\"id\":\"20dbd5fc-06b7-1aa1-5943-7ddaa2061e6a\",\"amount\":8}],\"rubies\":15,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"3\":{\"experienceRequired\":1500,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"rubies\":20,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"4\":{\"experienceRequired\":2800,\"rewards\":{\"inventory\":[{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"5\":{\"experienceRequired\":4600,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"b0633e20-bc3c-4b5f-a0e4-b5b3b20eb38e\",\"amount\":1}],\"rubies\":300,\"buildplates\":[\"f1bbd87a-1f52-11db-9470-0b3c8689f690\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"6\":{\"experienceRequired\":6100,\"rewards\":{\"inventory\":[{\"id\":\"5d788e04-8c5e-495f-81d1-69caa4001dbc\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"7\":{\"experienceRequired\":7800,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"rubies\":30,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"8\":{\"experienceRequired\":10100,\"rewards\":{\"inventory\":[{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"9\":{\"experienceRequired\":13300,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"29d26deb-3f14-410f-af59-a492a21406bb\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"10\":{\"experienceRequired\":17800,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"acb80f32-4e45-4f59-a203-c297e717f62f\",\"amount\":1}],\"buildplates\":[\"9b2680a7-9c5d-432b-0c71-32b1bb34e8db\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"11\":{\"experienceRequired\":21400,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"1b527574-8260-42f4-bd34-49d2cad6b91f\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"12\":{\"experienceRequired\":25700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"50539b69-3d2e-40c8-bef1-490d3286b9db\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"13\":{\"experienceRequired\":31300,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"14\":{\"experienceRequired\":39100,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"5a4a89e3-ef12-4af9-bf1f-9058499827ed\",\"amount\":1}],\"rubies\":50,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"15\":{\"experienceRequired\":50000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"b6bd9483-b995-413c-b9ba-447e4daf8e69\",\"amount\":1}],\"buildplates\":[\"bd8c9c76-8b16-afda-b890-62eeaf81e672\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"16\":{\"experienceRequired\":58700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"d9bbd707-8a7a-4edb-a85c-f8ec0c78a1f9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"17\":{\"experienceRequired\":68700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"18\":{\"experienceRequired\":82700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"50539b69-3d2e-40c8-bef1-490d3286b9db\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"19\":{\"experienceRequired\":101700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"rubies\":50,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"20\":{\"experienceRequired\":128700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"b0633e20-bc3c-4b5f-a0e4-b5b3b20eb38e\",\"amount\":1}],\"buildplates\":[\"bf6d1208-72bb-03e4-0535-2119a29ae231\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"21\":{\"experienceRequired\":137400,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"5d788e04-8c5e-495f-81d1-69caa4001dbc\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"22\":{\"experienceRequired\":147000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"23\":{\"experienceRequired\":157000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"24\":{\"experienceRequired\":169000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"29d26deb-3f14-410f-af59-a492a21406bb\",\"amount\":1}],\"rubies\":80,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"25\":{\"experienceRequired\":185000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"acb80f32-4e45-4f59-a203-c297e717f62f\",\"amount\":1}],\"buildplates\":[\"5c4ae0e6-4a11-3175-1d88-1054e12fc880\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}";
            return Content(response, "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/inventory/catalogv3")]
    public class PlayerInventoryController : Controller
    {
        public ContentResult Get()
        {
            var fs = new StreamReader("M:\\DevEnvironment\\DevWorkspace\\dotNETProjects\\ProjectEarthServerAPI\\ProjectEarthServerAPI\\bin\\Debug\\net5.0\\catalogv3");
            return Content(fs.ReadToEnd(), "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/inventory/survival")]
    public class PlayerSurvivalInventoryController : Controller
    {
        public ContentResult Get()
        {
            return Content("{\"result\":{\"hotbar\":[null,null,null,null,null,null,null],\"stackableItems\":[{\"owned\":1,\"id\":\"805028aa-c421-2db9-c07b-0ac3592bed4e\",\"seen\":{\"on\":\"2019-10-24T16:37:46.1904053Z\"},\"unlocked\":{\"on\":\"2019-10-24T16:37:46.1904053Z\"},\"fragments\":1},{\"owned\":11,\"id\":\"a1bf49f9-1f1f-2a4d-5f7b-c0c5ba833068\",\"seen\":{\"on\":\"2019-10-24T16:37:54.649247Z\"},\"unlocked\":{\"on\":\"2019-10-24T16:37:54.649247Z\"},\"fragments\":1},{\"owned\":4,\"id\":\"ff796546-f5f5-2c99-1118-0bb41e70d48c\",\"seen\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"9740784f-7047-0b4b-f936-8b6f8f9a1a15\",\"seen\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"fragments\":1},{\"owned\":3,\"id\":\"3d0969b4-e8c8-0a23-73d4-0c6128bfb84c\",\"seen\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:55:53.7694275Z\"},\"fragments\":1},{\"owned\":8,\"id\":\"f0617d6a-c35a-5177-fcf2-95f67d79196d\",\"seen\":{\"on\":\"2019-11-21T14:56:08.8284287Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:56:08.8284287Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"3eb631f9-fd37-705c-bd86-dec6aaf44702\",\"seen\":{\"on\":\"2019-11-21T14:56:08.8284287Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:56:08.8284287Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"88ffe819-aefb-e080-5ef5-e04244a387e1\",\"seen\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"fragments\":1},{\"owned\":4,\"id\":\"eead8c3f-638b-91fa-6e0a-f99f6906dd56\",\"seen\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"663f3282-95c6-6d22-ebf5-f1f923964d77\",\"seen\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"unlocked\":{\"on\":\"2019-11-21T14:56:20.6668018Z\"},\"fragments\":1},{\"owned\":3,\"id\":\"4f16a053-4929-263a-c91a-29663e29df76\",\"seen\":{\"on\":\"2020-10-23T14:09:16.1876553Z\"},\"unlocked\":{\"on\":\"2020-10-23T14:09:16.1876553Z\"},\"fragments\":1},{\"owned\":2,\"id\":\"1b527574-8260-42f4-bd34-49d2cad6b91f\",\"seen\":{\"on\":\"2020-10-23T14:09:16.1876553Z\"},\"unlocked\":{\"on\":\"2020-10-23T14:09:16.1876553Z\"},\"fragments\":1},{\"owned\":9,\"id\":\"9efdd49a-5bda-1191-34eb-747cf02406c0\",\"seen\":{\"on\":\"2020-10-23T14:09:38.6242099Z\"},\"unlocked\":{\"on\":\"2020-10-23T14:09:38.6242099Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"c0284797-233c-64c5-13b5-b821922ae84a\",\"seen\":{\"on\":\"2020-10-23T14:09:38.6242099Z\"},\"unlocked\":{\"on\":\"2020-10-23T14:09:38.6242099Z\"},\"fragments\":1},{\"owned\":6,\"id\":\"efea9c90-e554-3f2a-4d0e-bfd754d899f9\",\"seen\":{\"on\":\"2020-10-28T19:51:13.3746972Z\"},\"unlocked\":{\"on\":\"2020-10-28T19:51:13.3746972Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"d9bbd707-8a7a-4edb-a85c-f8ec0c78a1f9\",\"seen\":{\"on\":\"2020-11-30T07:54:56.0625597Z\"},\"unlocked\":{\"on\":\"2020-11-30T07:54:56.0625597Z\"},\"fragments\":1},{\"owned\":2,\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"seen\":{\"on\":\"2020-11-30T07:54:57.2589605Z\"},\"unlocked\":{\"on\":\"2020-11-30T07:54:57.2589605Z\"},\"fragments\":1},{\"owned\":2,\"id\":\"b6bd9483-b995-413c-b9ba-447e4daf8e69\",\"seen\":{\"on\":\"2020-12-02T21:23:53.4079699Z\"},\"unlocked\":{\"on\":\"2020-12-02T21:23:53.4079699Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"seen\":{\"on\":\"2020-12-02T21:23:54.0418279Z\"},\"unlocked\":{\"on\":\"2020-12-02T21:23:54.0418279Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"c4231eb5-8fc0-4ad7-ad18-ecfcb0734049\",\"seen\":{\"on\":\"2021-01-05T19:49:21.2877321Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:49:21.2877321Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"bef8b1c2-54c6-4f69-a6c9-f1aa1ee18119\",\"seen\":{\"on\":\"2021-01-05T19:50:18.0905966Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:50:18.0905966Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"69840d34-5fde-64f1-0710-d8f9a34e3e5e\",\"seen\":{\"on\":\"2021-01-05T19:50:20.8702008Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:50:20.8702008Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"29d15352-7a57-2c12-1885-3a783649737e\",\"seen\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"f9deeb00-51ff-714c-93be-fd2b65ef4022\",\"seen\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"fragments\":1},{\"owned\":2,\"id\":\"c2fead8a-3539-da39-047c-4c156c4438fe\",\"seen\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"unlocked\":{\"on\":\"2021-01-05T19:52:21.0649737Z\"},\"fragments\":1},{\"owned\":3,\"id\":\"408bb17b-da92-0cea-7496-ee01c6a542d7\",\"seen\":{\"on\":\"2021-01-06T21:21:29.1972348Z\"},\"unlocked\":{\"on\":\"2021-01-06T21:21:29.1972348Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"cdc029f4-c70f-7158-eb95-95c8f3b28669\",\"seen\":{\"on\":\"2021-01-06T21:32:39.6566477Z\"},\"unlocked\":{\"on\":\"2021-01-06T21:32:39.6566477Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"038c8034-1d98-96ca-8278-bc69f592549f\",\"seen\":{\"on\":\"2021-01-06T22:29:16.0124724Z\"},\"unlocked\":{\"on\":\"2021-01-06T22:29:16.0124724Z\"},\"fragments\":1},{\"owned\":1,\"id\":\"7768e3de-aea7-4171-8fdd-2b69cce45ee6\",\"seen\":{\"on\":\"2021-01-07T01:16:12.516064Z\"},\"unlocked\":{\"on\":\"2021-01-07T01:16:12.516064Z\"},\"fragments\":1}],\"nonStackableItems\":[]},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/settings")]
    public class PlayerSettingsController : Controller
    {
        public ContentResult Get()
        {
            return Content("{\"result\":{\"encounterinteractionradius\":40.0,\"tappableinteractionradius\":70.0,\"tappablevisibleradius\":-5.0,\"targetpossibletappables\":100.0,\"tile0\":10537.0,\"slowrequesttimeout\":2500.0,\"cullingradius\":50.0,\"commontapcount\":3.0,\"epictapcount\":7.0,\"speedwarningcooldown\":3600.0,\"mintappablesrequiredpertile\":22.0,\"targetactivetappables\":30.0,\"tappablecullingradius\":500.0,\"raretapcount\":5.0,\"requestwarningtimeout\":10000.0,\"speedwarningthreshold\":11.176,\"asaanchormaxplaneheightthreshold\":0.5,\"maxannouncementscount\":0.0,\"removethislater\":23.0,\"crystalslotcap\":3.0,\"crystaluncommonduration\":10.0,\"crystalrareduration\":10.0,\"crystalepicduration\":10.0,\"crystalcommonduration\":10.0,\"crystallegendaryduration\":10.0,\"maximumpersonaltimedchallenges\":3.0,\"maximumpersonalcontinuouschallenges\":3.0},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/recipes")]
    public class PlayerRecipeController : Controller
    {
        public ContentResult Get()
        {
            var fs = new StreamReader("M:\\DevEnvironment\\DevWorkspace\\dotNETProjects\\ProjectEarthServerAPI\\ProjectEarthServerAPI\\bin\\Debug\\net5.0\\recipes");
            return Content(fs.ReadToEnd(), "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/boosts")]
    public class PlayerBoostController : Controller
    {
        public ContentResult Get()
        {
            var responseobj = new BoostResponse
            {
                result = new BoostResult
                {
                    miniFigRecords = new MiniFigRecords(),
                    scenarioBoosts = new ScenarioBoosts(),
                    statusEffects = new StatusEffects()
                }
            };
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
            //return Content("{\"result\":{\"potions\":[null,null,null,null,null],\"miniFigs\":[null,null,null,null,null],\"miniFigRecords\":{},\"activeEffects\":[],\"statusEffects\":{\"tappableInteractionRadius\":null,\"experiencePointRate\":null,\"itemExperiencePointRates\":[],\"attackDamageRate\":null,\"playerDefenseRate\":null,\"blockDamageRate\":null,\"maximumPlayerHealth\":null,\"craftingSpeed\":null,\"smeltingFuelIntensity\":null,\"foodHealthRate\":null},\"scenarioBoosts\":{},\"expiration\":null}}", "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/features")]
    public class PlayerFeaturesController : Controller
    {
        public ContentResult Get()
        {
            var responseobj = new SettingsResponse();
            responseobj.result = new SettingsResult();
            responseobj.updates = new object();
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        }
    }
}
