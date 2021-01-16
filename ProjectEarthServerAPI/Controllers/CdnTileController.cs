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
    public class CdnTileController : ControllerBase
    {
        public IActionResult Get(int _, int tilePos1, int tilePos2) // _ used because we dont care :|
        {

            String targetTilePath = @".\tiles\16\" + tilePos1 + @"\" + tilePos2 + @"\" + tilePos1 + "_" + tilePos2 + "_16.png"; 

            if (!System.IO.File.Exists(targetTilePath)){

                //Lets download that lovely tile now, Shall we?
                if (!Tile.DownloadTile(tilePos1, tilePos2, @".\tiles\16\")) return BadRequest(); // Error 400 on Tile download error
            
            }

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
