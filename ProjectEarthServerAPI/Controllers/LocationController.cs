using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/locations/{latitude}/{longitude}")]
    public class LocationController : Controller
    {
        public ContentResult Get(double latitude, double longitude)
        {
            var response = $"{latitude} {longitude}";
            return Content(response, "application/json");
        }
    }
}
