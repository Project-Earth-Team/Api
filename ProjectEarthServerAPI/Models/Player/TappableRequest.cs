using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Features
{
	public class TappableRequest
	{
		public Guid id { get; set; }
		public PlayerCoordinate playerCoordinate { get; set; }

		public class PlayerCoordinate
		{
			public double latitude { get; set; }
			public double longitude { get; set; }
		}
	}
}
