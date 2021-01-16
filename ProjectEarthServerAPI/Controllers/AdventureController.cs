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
        [Route("1/api/v{version:apiVersion}/adventure/scrolls")]
        public class AdventureScrollsController : Controller
        {
            public ContentResult Get()
            {
                var responseobj = new ScrollsResponse();
                var response = JsonConvert.SerializeObject(responseobj);
                return Content(response, "application/json");
            }
        }
    }
