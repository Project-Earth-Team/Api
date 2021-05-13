using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProjectEarthServerAPI.Models
{
	public class SigninResponse
	{
		[JsonObject(ItemNullValueHandling = NullValueHandling.Include)]
		public class ResponseTemplate
		{
			public Result result { get; set; }
			public Updates updates { get; set; }
		}

		[JsonObject(ItemNullValueHandling = NullValueHandling.Include)]
		public class Result
		{
			[JsonProperty("authenticationToken")]
			public string AuthenticationToken { get; set; }

			[JsonProperty("basePath")]
			public string BasePath { get; set; }

			[JsonProperty("clientProperties")]
			public object ClientProperties { get; set; }

			[JsonProperty("mixedReality")]
			public object MixedReality { get; set; }

			[JsonProperty("mrToken")]
			public object MrToken { get; set; }

			[JsonProperty("streams")]
			public object Streams { get; set; }

            [JsonProperty("tokens")]
            public Dictionary<Guid, Token> Tokens { get; set; }

			[JsonProperty("updates")]
			public Updates Updates { get; set; }
		}
	}
}
