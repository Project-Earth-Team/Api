using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models
{
    public class CraftingRequest
    {
        [JsonProperty("ingredients")]
        public CraftingIngredient[] Ingredients { get; set; }
        [JsonProperty("multiplier")]
        public int Multiplier { get; set; }
        [JsonProperty("recipeId")]
        public string RecipeId { get; set; }
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }

    public class CraftingIngredient
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
        [JsonProperty("itemInstanceIds")]
        public string[] ItemInstanceIds { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}