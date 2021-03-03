using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Player
{
	public class TappableResponse
	{
		public Result result { get; set; }
		public object expiration { get; set; }
		public object continuationToken { get; set; }
		public Updates updates { get; set; }

		public class Result
		{
			public Token token { get; set; }
			public Updates updates { get; set; }
		}
	}
}
