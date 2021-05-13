using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Util;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Player;
using Serilog;
using Serilog.Core;
using Uma.Uuid;

namespace ProjectEarthServerAPI
{

    public class Program
    {

        public static void Main(string[] args)
        {
            TypeDescriptor.AddAttributes(typeof(Uuid), new TypeConverterAttribute(typeof(StringToUuidConv)));
        
            // Init Logging
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/debug.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 8338607, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();

            Log.Logger = log;

            //Initialize state singleton from config
            StateSingleton.Instance.config = ServerConfig.getFromFile();
            StateSingleton.Instance.catalog = CatalogResponse.FromFiles(StateSingleton.Instance.config.itemsFolderLocation, StateSingleton.Instance.config.efficiencyCategoriesFolderLocation);
            StateSingleton.Instance.recipes = Recipes.FromFile(StateSingleton.Instance.config.recipesFileLocation);
            StateSingleton.Instance.settings = SettingsResponse.FromFile(StateSingleton.Instance.config.settingsFileLocation);
            StateSingleton.Instance.challengeStorage = ChallengeStorage.FromFiles(StateSingleton.Instance.config.challengeStorageFolderLocation);
            StateSingleton.Instance.productCatalog = ProductCatalogResponse.FromFile(StateSingleton.Instance.config.productCatalogFileLocation);
            StateSingleton.Instance.tappableData = TappableUtils.loadAllTappableSets();
            StateSingleton.Instance.activeTappables = new();
            //Start api
            CreateHostBuilder(args).Build().Run();

            Log.Information("Server started!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}
