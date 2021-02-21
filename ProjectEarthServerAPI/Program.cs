using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
using ProjectEarthServerAPI.Models.Features;
namespace ProjectEarthServerAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //Initialize state singleton from config
            StateSingleton.Instance.config = ServerConfig.getFromFile();
            StateSingleton.Instance.catalog = CatalogResponse.FromFile(StateSingleton.Instance.config.catalogFileLocation);
            StateSingleton.Instance.recipes = Recipes.FromFile(StateSingleton.Instance.config.recipesFileLocation);
            StateSingleton.Instance.settings = SettingsResponse.FromFile(StateSingleton.Instance.config.settingsFileLocation);
            StateSingleton.Instance.productCatalog = ProductCatalogResponse.FromFile(StateSingleton.Instance.config.productCatalogFileLocation);

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
