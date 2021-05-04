using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEarthServerAPI.Models
{
	public class Coordinate
	{
		public double latitude { get; set; }
		public double longitude { get; set; }
	}

	public class SigninRequest
	{
		public string advertisingId { get; set; }
		public string appsFlyerId { get; set; }
		public string buildNumber { get; set; }
		public string clientVersion { get; set; }
		public Coordinate coordinate { get; set; }
		public string deviceId { get; set; }
		public string deviceOS { get; set; }
		public string deviceToken { get; set; }
		public string language { get; set; }
		public string sessionTicket { get; set; }
		public object streams { get; set; }
	}
}
