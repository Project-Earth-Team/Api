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
        public static Dictionary<Uuid, Token> GetSigninTokens(string playerId)
        {
            var origTokens = GetTokensForUserId(playerId);
            Dictionary<Uuid, Token> returnTokens = new Dictionary<Uuid, Token>();
            foreach (KeyValuePair<Uuid, Token> tok in origTokens)
            {
                if (tok.Value.clientProperties.Count == 0)
                {
                    returnTokens.Add(tok.Key, tok.Value);
                }
            }

            return returnTokens;
        }

        public static Dictionary<Uuid, Token> GetTokensForUserId(string playerId)
        {
            var serializedTokens = GetTokenResponseForUserId(playerId);
            return serializedTokens.Result.tokens;
        }

        public static TokenResponse GetTokenResponseForUserId(string playerId)
        {
            var returntokens = GetSerializedTokenResponse(playerId);

            var serializedTokens = JsonConvert.DeserializeObject<TokenResponse>(returntokens, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return serializedTokens;
        }

        public static string GetSerializedTokenResponse(string playerId)
        {
            return JsonConvert.SerializeObject(
                GenericUtils.ParseJsonFile<TokenResponse>(playerId, "tokens"));
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

            var tokenlist = GetTokensForUserId(playerId);

            if (tokenlist.All(pred => pred.Value.clientProperties["itemid"] != itemId.ToString()))
                tokenlist.Add(Version4Generator.NewUuid(), itemtoken);

            Log.Information($"[{playerId}]: Added item token {itemId}!");

            WriteTokensForPlayer(playerId, tokenlist);

        }

        public static bool AddToken(string playerId, Token tokenToAdd)
        {
            var tokenlist = GetTokensForUserId(playerId);
            if (!tokenlist.ContainsValue(tokenToAdd))
            {
                tokenlist.Add(Version4Generator.NewUuid(), tokenToAdd);
                return true;
            }
            else return false;
        }

        public static Token RedeemToken(string playerId, Uuid tokenId)
        {
            var parsedTokens = GetTokenResponseForUserId(playerId);
            if (parsedTokens.Result.tokens.ContainsKey(tokenId))
            {
                var tokenToRedeem = parsedTokens.Result.tokens[tokenId];
                RewardUtils.RedeemRewards(playerId, tokenToRedeem.rewards);

                parsedTokens.Result.tokens.Remove(tokenId);

                Log.Information($"[{playerId}]: Redeemed token {tokenId}.");

                return tokenToRedeem;
            }

            return null;
        }

        private static void WriteTokensForPlayer(string playerId, Dictionary<Uuid, Token> tokenlist)
        {
            var tokenResp = new TokenResponse
            {
                Result = new TokenResult
                {
                    tokens = tokenlist
                },
                updates = new Updates()
            };
            GenericUtils.WriteJsonFile(playerId, tokenResp,"tokens");
        }
    }
}