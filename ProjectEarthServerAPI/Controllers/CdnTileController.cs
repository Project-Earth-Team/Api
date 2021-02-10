using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using ProjectEarthServerAPI.Util;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Controllers
{
    [ApiVersion("1.1")]
    [Route("cdn/tile/16/{_}/{tilePos1}_{tilePos2}_16.png")]
    [ResponseCache(Duration = 11200)]
    public class CdnTileController : ControllerBase
    {
        public IActionResult Get(int _, int tilePos1, int tilePos2) // _ used because we dont care :|
        {

            String targetTilePath = $"./tiles/16/{tilePos1}/{tilePos1}_{tilePos2}_16.png"; 

            if (!System.IO.File.Exists(targetTilePath))
            {

                var boo = Tile.DownloadTile(tilePos1, tilePos2, @".\tiles\16\");

                //Lets download that lovely tile now, Shall we?
                if (boo == false)
                {
                    return Content("hi!");
                } // Error 400 on Tile download error
            }
            //String targetTilePath = $"./tiles/creeper_tile.png";
            byte[] fileData = System.IO.File.ReadAllBytes(targetTilePath);//Namespaces
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = tilePos1 + "_" + tilePos2 + "_16.png",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());


            return File(fileData, "application/octet-stream", tilePos1 + "_" + tilePos2 + "_16.png");
        }
    }
}
