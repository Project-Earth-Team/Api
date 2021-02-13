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
        public static readonly string recipeFileLocation = "./recipes"; // TODO: Implement into _Candelas_ config
        public static readonly string catalogFileLocation = "./catalogv3"; // TODO: Implement into _Candelas_ config

        public static Recipes recipeList = Recipes.FromFile(recipeFileLocation);
        public static CatalogResponse catelog = CatalogResponse.FromFile(catalogFileLocation);

        public static void Main(string[] args)
        {
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
