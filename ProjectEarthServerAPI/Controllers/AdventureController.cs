using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Controllers
{
    [Authorize]
    [ApiVersion("1.1")]
    [Route("1/api/v{version:apiVersion}/adventures/scrolls")]
    public class AdventureScrollsController : Controller
    {
        public ContentResult Get()
        {
            var responseobj = new ScrollsResponse();
            var response = JsonConvert.SerializeObject(responseobj);
            return Content(response, "application/json");
        } // TODO: Fixed String
    }
}
