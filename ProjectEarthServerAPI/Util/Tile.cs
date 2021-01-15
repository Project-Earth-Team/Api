using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Util
{
    /// <summary>
    /// Contains Functions for converting long/lat -> tile pos, downloading tilees, and anything else that might come up
    /// </summary>
    public class Tile
    {
        public static void downloadTile(int pos1, int pos2, string basePath)
        {
            WebClient webClient = new WebClient();
            
            try
            {

                String downloadUrl = "https://cdn.mceserv.net:443/tile/16/" + pos1 + "/" + pos1 + "_" + pos2 + "_16.png";
                webClient.DownloadFile(downloadUrl, basePath + pos1 + @"\" + pos2 + @"\" + pos1 + "_" + pos2 + "_16.png");
            }
            catch(WebException wex)
            {
                //TODO: error 502 check.
            }
        }
    }
}
