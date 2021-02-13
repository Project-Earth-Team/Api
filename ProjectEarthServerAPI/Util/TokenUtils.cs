using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;

namespace ProjectEarthServerAPI.Util
{
    public class TokenUtils
    {
        public static Dictionary<string, Token> GetSigninTokens(string userid)
        {
            var origTokens = GetTokensForUserId(userid);
            Dictionary<string,Token> returnTokens = new Dictionary<string, Token>();
            foreach (KeyValuePair<string,Token> tok in origTokens)
            {
                if (!tok.Value.clientProperties.ContainsKey("challengeid"))
                {
                    returnTokens.Add(tok.Key,tok.Value);
                }
            }

            return returnTokens;
        }
        public static Dictionary<string, Token> GetTokensForUserId(string userid)
        {
            var serializedTokens = GetTokenResponseForUserId(userid);
            return serializedTokens.Result.tokens;
        }

        public static TokenResponse GetTokenResponseForUserId(string userid)
        {
            var returntokens = GetSerializedTokenResponse(userid);

            var serializedTokens = JsonConvert.DeserializeObject<TokenResponse>(returntokens,new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return serializedTokens;
        }

        public static string GetSerializedTokenResponse(string userid)
        {
            var tokenpath = $"./{userid}/tokens.json";

            if (!File.Exists(tokenpath))
            {
                Directory.CreateDirectory($"./{userid}");
                File.WriteAllText(tokenpath, "{\"result\":{\"tokens\":{\"9b0acea5-8eaf-4ccc-8f8d-187784dc5544\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"605dc84c-f6d1-442b-8901-69b7b89a0cc8\"}},\"c025a117-de76-49c8-924a-9c56986d7a98\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.pickup_mobs\",\"clientProperties\":{}},\"89e11f11-2582-47da-968d-764db91c7bd7\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.intro\",\"clientProperties\":{}},\"fc3028b9-36bf-473f-87ab-598ddc3d468f\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.build_with_friends.menu\",\"clientProperties\":{}},\"80c106d4-3f80-4f42-8f1c-211278b1c2ef\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"21dabe32-6d4e-4954-ab0d-8a21f5e51665\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"9a2bd5aa-015d-419b-9c3a-d80dde87e312\"}},\"7b6ecfe4-ad61-4353-af7b-cd005f0e3d1e\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2ba1bbef-fc22-42e5-b1af-81f00c130a5b\"}},\"e74024fd-6c40-42b6-ba84-1a315e1f5982\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"cc456b52-1586-4e75-b7e9-aa811f609567\"}},\"a33fd95f-f567-4c53-ae6e-4fad94975e80\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"2bf15fa5-50ed-41ce-a2c4-b870028ed991\"}},\"b730a241-3966-4731-96c4-25387493f9d2\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"98749e7b-6a14-4fac-a851-3a68511c3f77\"}},\"e23a3f79-6362-4fff-8d10-636ab4052d10\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"07f1e716-b1d5-49df-ac81-fb8ce184746e\"}},\"4fbcb172-c215-482e-870b-6d3288a42cfb\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"ba777233-973b-4536-96e9-3e8abb3ae10a\"}},\"d702f10e-d616-444a-bf4b-3e2b9cbe3d07\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"5686ddbd-3da4-46da-a719-8e95e486cdb4\"}},\"9e9addd7-812e-4135-8edc-1155bf712240\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{}},\"39a32e0f-f19d-442a-9ae3-26785dd4b14a\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3064a2c5-e5f6-44ed-9561-7e972e1c1410\"}},\"54b85e5d-1485-432f-929e-3845edb1e301\":{\"lifetime\":\"Persistent\",\"clientType\":\"empty.bucket.returned\",\"clientProperties\":{}},\"ab21c03d-18c0-441a-b78a-79fa429391d8\":{\"lifetime\":\"Persistent\",\"clientType\":\"ftue.context.adventures\",\"clientProperties\":{},\"rewards\":{\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"923f9914-34bf-4135-90ed-b418c1be7af5\":{\"lifetime\":\"Persistent\",\"clientType\":\"challenge.completed\",\"clientProperties\":{\"challengeid\":\"78618f62-aff9-4dc9-b4fc-9b50e8b5db73\",\"category\":\"oobe\"},\"rewards\":{\"experiencePoints\":150,\"inventory\":[],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}},\"98c38d85-525e-4edf-849a-58ff68219c37\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"3a99af70-bbf3-4041-8094-491bed0c3845\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"fe8a85b4-7b73-4182-8e46-adb894811f32\"}},\"af694f0c-b448-44a2-9d73-b2cc5e74b271\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"4c5bd76e-d57a-4ede-9193-eff0c12a6f8b\"}},\"878e2acc-238f-4bd3-870e-ebba2d374c68\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.welcome\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"d5f19783-57ee-4bf9-883b-736680e9237f\":{\"lifetime\":\"Persistent\",\"clientType\":\"seasons.notification\",\"clientProperties\":{\"seasonId\":\"3604cd1b-dc4b-4e69-bf8f-8774c81caa74\"}},\"9e0c471a-b7ed-4044-be1e-803e3f15cba4\":{\"lifetime\":\"Persistent\",\"clientType\":\"challenge.completed\",\"clientProperties\":{\"challengeid\":\"f77a732a-c21e-484c-aa9b-2ee41bdf8bf5\",\"category\":\"retention\"},\"rewards\":{\"inventory\":[{\"id\":\"4f16a053-4929-263a-c91a-29663e29df76\",\"amount\":1},{\"id\":\"bef8b1c2-54c6-4f69-a6c9-f1aa1ee18119\",\"amount\":1}],\"buildplates\":[],\"challenges\":[],\"personaItems\":[],\"utilityBlocks\":[]}}}},\"expiration\":null,\"continuationToken\":null,\"updates\":{}}");
                Console.WriteLine($"User with id {userid} first requested tokens, generating default.");
            }

            var returntokens = File.ReadAllText(tokenpath);
            return returntokens;
        }

        public static void AddItemToken(string playerId, string itemId)
        {
            var itemtoken = new Token
            {
                clientProperties = new Dictionary<string, string>(),
                clientType = "item.unlocked",
                lifetime = "Persistent",
                rewards = new Rewards()
            };
            itemtoken.clientProperties.Add("itemid", itemId);

            var tokenlist = GetTokensForUserId(playerId);
            tokenlist.Add("4c41bed5-c863-4494-aff5-d55c2fcf96eb", itemtoken); // TODO: Figure out token id list.

            WriteTokensForPlayer(playerId, tokenlist);

        }

        private static void WriteTokensForPlayer(string playerId, Dictionary<string, Token> tokenlist)
        {
            var tokenpath = $"./{playerId}/tokens.json";
            var response = new TokenResponse
            {
                Result = new TokenResult
                {
                    tokens = tokenlist
                }
            };

            File.WriteAllText(tokenpath,JsonConvert.SerializeObject(response));
        }
    }
}