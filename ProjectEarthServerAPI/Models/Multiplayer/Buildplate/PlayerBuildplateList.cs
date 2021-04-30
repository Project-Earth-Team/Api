using System;
using System.Collections.Generic;

namespace ProjectEarthServerAPI.Models.Buildplate
{
	public class PlayerBuildplateList
	{
		public List<Guid> UnlockedBuildplates { get; set; }
		public List<Guid> LockedBuildplates { get; set; }
	}
}
