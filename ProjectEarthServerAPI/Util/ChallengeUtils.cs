using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Bson;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Player;

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
            else
            {
                playerChallenges.result.challenges[challengeId].state = ChallengeState.Active;
            }

            Console.WriteLine($"Activating challenge {challengeId} for player id {playerId}!");
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
            return RewardUtils.RedeemRewards(playerId, challenge.rewards);
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