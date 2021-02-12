using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Player;
using ProjectEarthServerAPI.Util;

namespace ProjectEarthServerAPI.Controllers
{
    public class PlayerTokenController : Controller
    {
        [ApiVersion("1.1")]
        [ResponseCache(Duration = 11200)]
        [Route("1/api/v{version:apiVersion}/player/tokens")]
        public IActionResult Get()
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

            var returnTokens = TokenUtils.GetSerializedTokenResponse(authtoken);

            Console.WriteLine($"User with id {authtoken} requested tokens.");

            return Content(returnTokens, "application/json");
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/player/tokens/{token}/redeem")] // TODO: Proper testing
        public IActionResult RedeemToken(string token)
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

            var tokenpath = $"./{authtoken}/tokens.json";
            var tokentext = System.IO.File.ReadAllText(tokenpath);
            var parsedTokens = JsonConvert.DeserializeObject<TokenResponse>(tokentext);
            if (parsedTokens.Result.tokens.ContainsKey(token))
            {
                var tokenToDelete = parsedTokens.Result.tokens[token];
                if (tokenToDelete.rewards != null)
                {
                    var rewardsToGive = tokenToDelete.rewards; // TODO: Implement Rewards 1/5
                    foreach (RewardComponent reward in rewardsToGive.Inventory)
                    {
                        InventoryUtils.AddItemToInv(authtoken, reward.Id, reward.Amount,
                            StateSingleton.Instance.catalog.result.items.Find(match => match.id == reward.Id).stacks); // If this catalog entry is a null reference we have a big problem
                    }
                }

                parsedTokens.Result.tokens.Remove(token);

                Console.WriteLine($"Redeemed token {token} for user with id {authtoken}.");

                return Content(JsonConvert.SerializeObject(tokenToDelete), "application/json");
            }

            return BadRequest();
        }
    }

    [ApiVersion("1.1")]
    [ResponseCache(Duration = 11200)]
    [Route("1/api/v{version:apiVersion}/player/rubies")]
    public class PlayerRubiesController : Controller
    {
        public IActionResult Get()
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

            var obj = RubyUtils.GetNormalRubyResponse(authtoken);
            var response = JsonConvert.SerializeObject(obj);
            return Content(response, "application/json");
        }
    }

    [ApiVersion("1.1")]
    [ResponseCache(Duration = 11200)]
    [Route("1/api/v{version:apiVersion}/player/splitRubies")]
    public class PlayerSplitRubiesController : Controller
    {
        public IActionResult Get()
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

            var obj = RubyUtils.ReadRubies(authtoken);
            var response = JsonConvert.SerializeObject(obj);
            return Content(response, "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/utilityBlocks")]
    public class PlayerUtilityBlocksController : Controller
    {
        public IActionResult Get()
        {
            return Content("{\"result\":{\"crafting\":{\"1\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Empty\",\"boostState\":null,\"unlockPrice\":null,\"streamVersion\":4},\"2\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4},\"3\":{\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4}},\"smelting\":{\"1\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Empty\",\"boostState\":null,\"unlockPrice\":null,\"streamVersion\":4},\"2\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4},\"3\":{\"fuel\":null,\"burning\":null,\"hasSufficientFuel\":null,\"heatAppliedToCurrentItem\":null,\"sessionId\":null,\"recipeId\":null,\"output\":null,\"escrow\":[],\"completed\":0,\"available\":0,\"total\":0,\"nextCompletionUtc\":null,\"totalCompletionUtc\":null,\"state\":\"Locked\",\"boostState\":null,\"unlockPrice\":{\"cost\":1,\"discount\":0},\"streamVersion\":4}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    }  // TODO: Fixed String

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/player/profile/{profileId}")]
    public class PlayerProfileController : Controller
    {
        public IActionResult Get(string profileId)
        {
            var response = "{\"result\":{\"totalExperience\":356,\"level\":1,\"currentLevelExperience\":356,\"experienceRemaining\":144,\"health\":null,\"healthPercentage\":100.0,\"levelDistribution\":{\"2\":{\"experienceRequired\":500,\"rewards\":{\"inventory\":[{\"id\":\"730573d1-ba59-4fd4-89e0-85d4647466c2\",\"amount\":1},{\"id\":\"20dbd5fc-06b7-1aa1-5943-7ddaa2061e6a\",\"amount\":8}],\"rubies\":15,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"3\":{\"experienceRequired\":1500,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"rubies\":20,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"4\":{\"experienceRequired\":2800,\"rewards\":{\"inventory\":[{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"5\":{\"experienceRequired\":4600,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"b0633e20-bc3c-4b5f-a0e4-b5b3b20eb38e\",\"amount\":1}],\"rubies\":300,\"buildplates\":[\"f1bbd87a-1f52-11db-9470-0b3c8689f690\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"6\":{\"experienceRequired\":6100,\"rewards\":{\"inventory\":[{\"id\":\"5d788e04-8c5e-495f-81d1-69caa4001dbc\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"7\":{\"experienceRequired\":7800,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"rubies\":30,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"8\":{\"experienceRequired\":10100,\"rewards\":{\"inventory\":[{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"9\":{\"experienceRequired\":13300,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"29d26deb-3f14-410f-af59-a492a21406bb\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"10\":{\"experienceRequired\":17800,\"rewards\":{\"inventory\":[{\"id\":\"81a84b7e-928f-7157-254c-6543e90dbc59\",\"amount\":1},{\"id\":\"acb80f32-4e45-4f59-a203-c297e717f62f\",\"amount\":1}],\"buildplates\":[\"9b2680a7-9c5d-432b-0c71-32b1bb34e8db\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"11\":{\"experienceRequired\":21400,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"1b527574-8260-42f4-bd34-49d2cad6b91f\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"12\":{\"experienceRequired\":25700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"50539b69-3d2e-40c8-bef1-490d3286b9db\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"13\":{\"experienceRequired\":31300,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"14\":{\"experienceRequired\":39100,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"5a4a89e3-ef12-4af9-bf1f-9058499827ed\",\"amount\":1}],\"rubies\":50,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"15\":{\"experienceRequired\":50000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"b6bd9483-b995-413c-b9ba-447e4daf8e69\",\"amount\":1}],\"buildplates\":[\"bd8c9c76-8b16-afda-b890-62eeaf81e672\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"16\":{\"experienceRequired\":58700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"d9bbd707-8a7a-4edb-a85c-f8ec0c78a1f9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"17\":{\"experienceRequired\":68700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"18\":{\"experienceRequired\":82700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"50539b69-3d2e-40c8-bef1-490d3286b9db\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"19\":{\"experienceRequired\":101700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"234432db-03c6-46f3-85c6-289cc710ca90\",\"amount\":1}],\"rubies\":50,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"20\":{\"experienceRequired\":128700,\"rewards\":{\"inventory\":[{\"id\":\"123dd088-ba76-71ce-b40c-2f05b948f303\",\"amount\":1},{\"id\":\"b0633e20-bc3c-4b5f-a0e4-b5b3b20eb38e\",\"amount\":1}],\"buildplates\":[\"bf6d1208-72bb-03e4-0535-2119a29ae231\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"21\":{\"experienceRequired\":137400,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"5d788e04-8c5e-495f-81d1-69caa4001dbc\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"22\":{\"experienceRequired\":147000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"75a749d3-f6a0-4f54-a785-9099403ea7e9\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"23\":{\"experienceRequired\":157000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"77103bc4-d7ba-4d1b-8e5e-16e6f61ef2f1\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"24\":{\"experienceRequired\":169000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"29d26deb-3f14-410f-af59-a492a21406bb\",\"amount\":1}],\"rubies\":80,\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"25\":{\"experienceRequired\":185000,\"rewards\":{\"inventory\":[{\"id\":\"1d549957-68d0-730a-56f3-d33996738d84\",\"amount\":1},{\"id\":\"acb80f32-4e45-4f59-a203-c297e717f62f\",\"amount\":1}],\"buildplates\":[\"5c4ae0e6-4a11-3175-1d88-1054e12fc880\"],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}";
            return Content(response, "application/json");
        }
    } // TODO: Fixed String

    public class PlayerInventoryController : Controller
    {
        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/inventory/catalogv3")]
        public IActionResult GetCatalog()
        {
            var fs = new StreamReader("./catalogv3");
            return Content(fs.ReadToEnd(), "application/json");
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/inventory/survival")]
        public IActionResult GetSurvivalInventory()
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

            var inv = InventoryUtils.ReadInventory(authtoken);
            var jsonstring = JsonConvert.SerializeObject(inv);
            return Content(jsonstring, "application/json");
        }

        [ApiVersion("1.1")]
        [Route("1/api/v{version:apiVersion}/inventory/survival/hotbar")]
        public async Task<IActionResult> PutItemInHotbar()
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

            var stream = new StreamReader(Request.Body);
            var body = await stream.ReadToEndAsync();

            var newhotbar = JsonConvert.DeserializeObject<InventoryResponse.Hotbar[]>(body);
            var inv = InventoryUtils.ReadInventory(authtoken);

            for (int i = 0; i < inv.result.hotbar.Length-1; i++)
            {
                if (newhotbar[i]?.id != inv.result.hotbar[i]?.id | newhotbar[i]?.count != inv.result.hotbar[i]?.count)
                {
                    if (newhotbar[i] == null)
                    {
                        if (inv.result.hotbar[i].instanceId == null)
                        {
                            InventoryUtils.AddItemToInv(authtoken, inv.result.hotbar[i].id, inv.result.hotbar[i].count,
                                true);
                        }
                        else
                        {
                            InventoryUtils.AddItemToInv(authtoken, inv.result.hotbar[i].id, 1, false, inv.result.hotbar[i].instanceId.id);
                        }
                    }
                    else
                    {
                        if (inv.result.hotbar[i] != null)
                        {
                            InventoryUtils.RemoveItemFromInv(authtoken, newhotbar[i].id,
                                newhotbar[i].instanceId?.id, newhotbar[i].count - inv.result.hotbar[i].count);
                        }
                        else
                        {
                            InventoryUtils.RemoveItemFromInv(authtoken, newhotbar[i].id,
                                newhotbar[i].instanceId?.id, newhotbar[i].count);
                        }
                    }
                }
            }

            var newinv = InventoryUtils.ReadInventory(authtoken);
            newinv.result.hotbar = newhotbar;

            InventoryUtils.WriteInventory(authtoken, newinv);

            return Content(JsonConvert.SerializeObject(newinv.result.hotbar));
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/settings")]
    public class PlayerSettingsController : Controller
    {
        public IActionResult Get()
        {
            return Content("{\"result\":{\"encounterinteractionradius\":40.0,\"tappableinteractionradius\":70.0,\"tappablevisibleradius\":-5.0,\"targetpossibletappables\":100.0,\"tile0\":10537.0,\"slowrequesttimeout\":2500.0,\"cullingradius\":50.0,\"commontapcount\":3.0,\"epictapcount\":7.0,\"speedwarningcooldown\":3600.0,\"mintappablesrequiredpertile\":22.0,\"targetactivetappables\":30.0,\"tappablecullingradius\":500.0,\"raretapcount\":5.0,\"requestwarningtimeout\":10000.0,\"speedwarningthreshold\":11.176,\"asaanchormaxplaneheightthreshold\":0.5,\"maxannouncementscount\":0.0,\"removethislater\":23.0,\"crystalslotcap\":3.0,\"crystaluncommonduration\":10.0,\"crystalrareduration\":10.0,\"crystalepicduration\":10.0,\"crystalcommonduration\":10.0,\"crystallegendaryduration\":10.0,\"maximumpersonaltimedchallenges\":3.0,\"maximumpersonalcontinuouschallenges\":3.0},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}", "application/json");
        }
    } // TODO: Fixed String

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/recipes")]
    public class PlayerRecipeController : Controller
    {
        public IActionResult Get()
        {
            var fs = new StreamReader("./recipes");
            return Content(fs.ReadToEnd(), "application/json");
        }
    }

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/boosts")]
    public class PlayerBoostController : Controller
    {
        public IActionResult Get()
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
    } // TODO: Fixed String/Object

    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/features")]
    public class PlayerFeaturesController : Controller
    {
        public IActionResult Get()
        {
            var responseobj = new SettingsResponse {result = new SettingsResult(), updates = new object()};
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        }
    } // TODO: Fixed String
}
