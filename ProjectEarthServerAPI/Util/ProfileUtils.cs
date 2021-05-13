using System.Dynamic;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Util
{
	public class ProfileUtils
	{
		public static ProfileData ReadProfile(string playerId)
		{
			return GenericUtils.ParseJsonFile<ProfileData>(playerId, "profile");
		}

		public static void AddExperienceToPlayer(string playerId, int experiencePoints)
		{
			var playerProfile = ReadProfile(playerId);
			var currentLvl = playerProfile.level;
			playerProfile.totalExperience += experiencePoints;
			while (currentLvl < 25 && playerProfile.experienceRemaining <= 0)
			{
				playerProfile.level++;
				RewardLevelupRewards(playerId, playerProfile.level);
			}

			WriteProfile(playerId, playerProfile);
		}

		private static void RewardLevelupRewards(string playerId, int level)
		{
			RewardUtils.RedeemRewards(playerId, ProfileResponse.levels[level.ToString()].rewards, EventLocation.LevelUp);
		}

		private static bool WriteProfile(string playerId, ProfileData playerProfile)
		{
			return GenericUtils.WriteJsonFile(playerId, playerProfile, "profile");
		}
	}
}
