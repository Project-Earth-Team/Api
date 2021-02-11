using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
namespace ProjectEarthServerAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("player/environment")]
    public class LocatorController : Controller
    {

        private readonly ILogger<LocatorController> _logger;

        public LocatorController(ILogger<LocatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ContentResult Get()
        {
            string baseServerIP = ServerConfig.getFromFile().baseServerIP;

            LocatorResponse.Root response = new LocatorResponse.Root()
            {
                result = new LocatorResponse.Result()
                {
                    serviceEnvironments = new LocatorResponse.ServiceEnvironments()
                    {
                        production = new LocatorResponse.Production()
                        {
                            playfabTitleId = "11509",
                            serviceUri = baseServerIP,
                            cdnUri = baseServerIP+"/cdn",
                            //playfabTitleId = "F0DE2" //maybe make our own soon? - Mojang could kill this anytime after server sunset with no warning. 
                        }
                    },
                    supportedEnvironments = new LocatorResponse.SupportedEnvironments()
                    {
                        _2020121702 = new List<string>()

                    }
                },
                //updates = new LocatorResponse.Updates()
            };

            response.result.supportedEnvironments._2020121702.Add("production");
            var resp = JsonConvert.SerializeObject(response);
            return Content(resp,"application/json");

        }
    }
}