using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Util
{
	public class ProfileUtils
    {
        public static ProfileData ReadProfile(string playerId)
        {
            return GenericUtils.ParseJsonFile<ProfileData>(playerId, "profile");
        }

        public static bool WriteProfile(string playerId, ProfileData ruby)
        {
            return GenericUtils.WriteJsonFile(playerId, ruby, "profile");
        }
    }
}
