using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models.Features
{
	public class TappableRequest
	{
		public Guid id { get; set; }
		public Coordinate playerCoordinate { get; set; }
    }
}
