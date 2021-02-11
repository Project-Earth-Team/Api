using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Util;
using ProjectEarthServerAPI.Models.Features;
namespace ProjectEarthServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Initialize singleton with loaded information

            StateSingleton.Instance.catalog = CatalogResponse.fromFile(StateSingleton.Instance.config.catalogFileLocation);
            //Start api
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
