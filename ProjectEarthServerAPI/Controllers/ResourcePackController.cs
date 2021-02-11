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

    //Wheres the resource pack?

    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/resourcepacks/2020.1217.02/default")]
    public class ResourcePackController : ControllerBase
    {
        [HttpGet]
        public ContentResult Get()
        {
            ResourcePackResponse response = new ResourcePackResponse
            {
                result = new List<ResourcePackResponse.Result>() {
                    new ResourcePackResponse.Result
                    {
                        order = 0,
                        parsedResourcePackVersion = new List<int>() { 2020, 1214, 4 },
                        relativePath = "cdn/resourcepacks/dba38e59-091a-4826-b76a-a08d7de5a9e2-1301b0c257a311678123b9e7325d0d6c61db3c35", //Naming the endpoint the same thing for consistency. this might not be needed. 
                        resourcePackVersion = "2020.1214.04"
                    }
                },
                updates = new ResourcePackResponse.Updates(),
                continuationToken = null,
                expiration = null
            };
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }

    //Heres the resource pack!
    [Route("cdn/resourcepacks/dba38e59-091a-4826-b76a-a08d7de5a9e2-1301b0c257a311678123b9e7325d0d6c61db3c35")]
    public class ResourcePackCdnController : ControllerBase
    {
        public IActionResult Get() 
        {

            String resourcePackFilePath = @".\resourcepacks\vanilla.zip"; //resource packs are distributed as renamed zip files containing an MCpack

            if (!System.IO.File.Exists(resourcePackFilePath))
            {
                Console.WriteLine("[Resourcepacks] Error! Resource pack file not found.");
                return BadRequest(); //we cannot serve you.
            }

            byte[] fileData = System.IO.File.ReadAllBytes(resourcePackFilePath);//Namespaces
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "dba38e59-091a-4826-b76a-a08d7de5a9e2-1301b0c257a311678123b9e7325d0d6c61db3c35",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());


            return File(fileData, "application/octet-stream", "dba38e59-091a-4826-b76a-a08d7de5a9e2-1301b0c257a311678123b9e7325d0d6c61db3c35");
        }
    }
}
