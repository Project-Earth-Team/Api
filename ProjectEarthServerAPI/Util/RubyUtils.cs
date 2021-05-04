using System;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
	public class RubyUtils
	{
		public static SplitRubyResponse ReadRubies(string playerId)
		{
			return GenericUtils.ParseJsonFile<SplitRubyResponse>(playerId, "rubies");
		}

		public static bool WriteRubies(string playerId, SplitRubyResponse ruby)
		{
			return GenericUtils.WriteJsonFile(playerId, ruby, "rubies");
		}

		public static int SetRubies(string playerId, int count, bool shouldReplaceNotAdd)
		{
			var origRubies = ReadRubies(playerId);
			if (shouldReplaceNotAdd)
			{
				origRubies.result.earned = count;
			}
			else
			{
				origRubies.result.earned += count;
			}

			var newRubyNum = origRubies.result.earned;

			WriteRubies(playerId, origRubies);

			return newRubyNum;
		}

		public static RubyResponse GetNormalRubyResponse(string playerid)
		{
			var splitrubies = ReadRubies(playerid);
			var response = new RubyResponse() {result = splitrubies.result.earned + splitrubies.result.purchased};

			return response;
		}
	}
}
