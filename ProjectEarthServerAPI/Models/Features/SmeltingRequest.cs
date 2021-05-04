using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models.Features
{
	public class SmeltingRequest
	{
		[JsonProperty("input")]
		public InputItem Ingredient { get; set; }

		[JsonProperty("fuel")]
		public InputItem FuelIngredient { get; set; }

		[JsonProperty("multiplier")]
		public int Multiplier { get; set; }

		[JsonProperty("recipeId")]
		public string RecipeId { get; set; }

		[JsonProperty("sessionId")]
		public string SessionId { get; set; }
	}
}
