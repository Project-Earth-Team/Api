using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models.Features
{
	/// <summary>
	/// Models an item catalog
	/// </summary>
	public class CatalogResponse
	{
		//===Model

		#region Model

		//Efficiency Classes
		public class EfficiencyMap
		{
			public double hand { get; set; }
			public double hoe { get; set; }
			public double axe { get; set; }
			public double shovel { get; set; }
			public double pickaxe_1 { get; set; }
			public double pickaxe_2 { get; set; }
			public double pickaxe_3 { get; set; }
			public double pickaxe_4 { get; set; }
			public double pickaxe_5 { get; set; }
			public double sword { get; set; }
			public double sheers { get; set; }
		}

		public class EfficiencyCategory
		{
			public EfficiencyMap efficiencyMap { get; set; }
		}

		public class Result
		{
			public Dictionary<string, EfficiencyCategory> efficiencyCategories { get; set; }
			public List<Item> items { get; set; }
		}

		public Result result { get; set; }
		public object expiration { get; set; }
		public object continuationToken { get; set; }
		public Updates updates { get; set; }

		#endregion

		#region Functions

		//===Load and save
		public static CatalogResponse FromFiles(string itemsFolderLocation, string efficiencyCategoriesFolderLocation)
		{
			Dictionary<string, EfficiencyCategory> efficiencyCategories = new Dictionary<string, EfficiencyCategory>();
			foreach (string efficiencyCategoryFile in Directory.GetFiles(efficiencyCategoriesFolderLocation))
			{
				string efficiencyCategory = File.ReadAllText(efficiencyCategoryFile);
				EfficiencyCategory cat = new EfficiencyCategory() {efficiencyMap = JsonConvert.DeserializeObject<EfficiencyMap>(efficiencyCategory)};
				efficiencyCategories.Add(Path.GetFileNameWithoutExtension(efficiencyCategoryFile), cat);
			}

			List<Item> items = new List<Item>();
			foreach (string itemFile in Directory.GetFiles(itemsFolderLocation))
			{
				string item = File.ReadAllText(itemFile);
				items.Add(JsonConvert.DeserializeObject<Item>(item));
			}

			return new CatalogResponse() {result = new Result() {efficiencyCategories = efficiencyCategories, items = items}, updates = new Updates()};
		}

		#endregion
	}
}
