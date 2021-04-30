using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OpenApi.Extensions;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Player;
using Serilog;

namespace ProjectEarthServerAPI.Util
{
	public class ChallengeUtils
	{
		private static readonly ChallengesList ChallengeList = StateSingleton.Instance.seasonChallenges.result;

		public static bool ActivateChallengeForPlayer(string playerId, Guid challengeId)
		{
			var challenge = ChallengeList.challenges.First(pred => pred.Key == challengeId).Value;
			var playerChallenges = ReadChallenges(playerId);
			bool shouldBeActivated = false;

			foreach (KeyValuePair<Guid, ChallengeInfo> prereqChallenge in playerChallenges.result.challenges.Where(pred =>
				challenge.prerequisiteIds.Contains(pred.Key)))
			{
				if (!shouldBeActivated)
				{
					switch (challenge.prerequisiteLogicalCondition)
					{
						case ChallengeLogicCondition.And:
							if (!prereqChallenge.Value.isComplete)
								return false;
							break;

						case ChallengeLogicCondition.Or:
							if (prereqChallenge.Value.isComplete)
								shouldBeActivated = true;
							break;
					}
				}
				else break;
			}

			if (challenge.duration == ChallengeDuration.Season)
				playerChallenges.result.activeSeasonChallenge = challengeId;

			playerChallenges.result.challenges[challengeId].state = ChallengeState.Active;

			Log.Information($"[{playerId}]: Activating challenge {challengeId}!");
			WriteChallenges(playerId, playerChallenges);

			return true;
		}

		public static Updates RedeemChallengeForPlayer(string playerId, Guid challengeId)
		{
			var challenge = ChallengeList.challenges.First(pred => pred.Key == challengeId).Value;
			var playerChallenges = ReadChallenges(playerId);

			playerChallenges.result.challenges[challengeId].isComplete = true;
			playerChallenges.result.challenges[challengeId].state = ChallengeState.Completed;
			playerChallenges.result.challenges[challengeId].percentComplete = 100;


			WriteChallenges(playerId, playerChallenges);

			var completionToken = new Token {clientProperties = new Dictionary<string, string>(), clientType = "challenge.completed", lifetime = "Persistent", rewards = challenge.rewards};
			completionToken.clientProperties.Add("challengeid", challengeId.ToString());
			completionToken.clientProperties.Add("category", challenge.category.GetDisplayName());
			completionToken.clientProperties.Add("expirationtimeutc", playerChallenges.result.challenges[challengeId].endTimeUtc.Value.ToString(CultureInfo.InvariantCulture));

			var returnUpdates = RewardUtils.RedeemRewards(playerId, challenge.rewards);
			if (TokenUtils.AddToken(playerId, completionToken))
				returnUpdates.tokens = 1; // Not the actual stream id ofc, but we just need to tell the game to reload the tokens

			return returnUpdates;
		}

		public static Updates ProgressChallenge(string playerId, ChallengeEventType challengeEvent, Guid eventId, int amount = 1)
		{
			var playerChallenges = ReadChallenges(playerId);
			var activeChallenges =
				playerChallenges.result.challenges.Where(pred => pred.Value.state == ChallengeState.Active).ToDictionary(pred => pred.Key, pred => pred.Value);

			// TODO: Implement challenge backend, since challenge requirements are not set in the response
			return new Updates() {challenges = GenericUtils.GetNextStreamVersion()};
		}

		public static ChallengesResponse ReloadChallenges(string playerId)
		{
			var playerChallenges = ReadChallenges(playerId);
			return playerChallenges;
		}

		private static ChallengesResponse ReadChallenges(string playerId)
		{
			return GenericUtils.ParseJsonFile<ChallengesResponse>(playerId, "challenges");
		}

		private static bool WriteChallenges(string playerId, ChallengesResponse challenges)
		{
			return GenericUtils.WriteJsonFile(playerId, challenges, "challenges");
		}
	}
}
