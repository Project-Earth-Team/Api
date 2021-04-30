using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models.Features
{
	public class ProductCatalogResponse
	{
		public List<ProductCatalogItem> result { get; set; }
		public Updates updates { get; set; }

		public static ProductCatalogResponse FromFile(string path)
		{
			var jsontext = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<ProductCatalogResponse>(jsontext);
		}
	}

	public class ProductCatalogItem
	{
		public int id { get; set; }
		public Item.BoostMetadata boostMetadata { get; set; }
		public string name { get; set; }
		public bool deprecated { get; set; }
		public string toolsVersion { get; set; } // Possibly date? One is 200228.204513
		public string type { get; set; } // Either MiniFig or NfcMiniFig
		public Rewards rewards { get; set; }
	}
}
