using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using Serilog;
using Uma.Uuid;

namespace ProjectEarthServerAPI.Util
{
    public class TokenUtils
    {
        private static readonly Version4Generator Version4Generator = new();

        public static Dictionary<Guid, Token> GetSigninTokens(string playerId)
        {
            var origTokens = ReadTokens(playerId);
            Dictionary<Guid, Token> returnTokens = new Dictionary<Guid, Token>();
            returnTokens = origTokens.Result.tokens
                .Where(pred => pred.Value.clientProperties.Count == 0)
                .ToDictionary(pred => pred.Key, pred => pred.Value);



            return returnTokens;
        }

        public static void AddItemToken(string playerId, Guid itemId)
        {
            var itemtoken = new Token
            {
                clientProperties = new Dictionary<string, string>(),
                clientType = "item.unlocked",
                lifetime = "Persistent",
                rewards = new Rewards()
            };

            itemtoken.clientProperties.Add("itemid", itemId.ToString());

            AddToken(playerId, itemtoken);

            Log.Information($"[{playerId}]: Added item token {itemId}!");
        }

        public static bool AddToken(string playerId, Token tokenToAdd)
        {
            var tokens = ReadTokens(playerId);
            if (!tokens.Result.tokens.ContainsValue(tokenToAdd))
            {
                tokens.Result.tokens.Add(Guid.NewGuid(), tokenToAdd);
                WriteTokens(playerId, tokens);
                Log.Information($"[{playerId}] Added token!");
                return true;
            }

            Log.Error($"[{playerId}] Tried to add token, but it already exists!");
            return false;
        }

        public static Token RedeemToken(string playerId, Guid tokenId)
        {
            var parsedTokens = ReadTokens(playerId);
            if (parsedTokens.Result.tokens.ContainsKey(tokenId))
            {
                var tokenToRedeem = parsedTokens.Result.tokens[tokenId];
                RewardUtils.RedeemRewards(playerId, tokenToRedeem.rewards, EventLocation.Token);

				parsedTokens.Result.tokens.Remove(tokenId);

                WriteTokens(playerId, parsedTokens);

                Log.Information($"[{playerId}]: Redeemed token {tokenId}.");

                return tokenToRedeem;
            }
            else
            {
                Log.Information($"[{playerId}] tried to redeem token {tokenId}, but it was not in the token list!");
                return null;
            }
        }

        public static TokenResponse ReadTokens(string playerId)
            => GenericUtils.ParseJsonFile<TokenResponse>(playerId, "tokens");
        private static void WriteTokens(string playerId, TokenResponse tokenList)
            => GenericUtils.WriteJsonFile(playerId, tokenList, "tokens");
    }
}
