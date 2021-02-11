using System;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
    public class RubyUtils
    {
        public static SplitRubyResponse ReadRubies(string playerid)
        {
            var rubiesfile = $"./{playerid}/rubies.json";
            if (!File.Exists(rubiesfile))
            {
                SetupRubies(playerid); // Default rubies
            }

            var rubyjson = File.ReadAllText(rubiesfile);
            var rubies = JsonConvert.DeserializeObject<SplitRubyResponse>(rubyjson);
            return rubies;
        }

        public static bool WriteRubies(string playerid, SplitRubyResponse ruby)
        {
            try
            {
                var rubiesfile = $"./{playerid}/rubies.json"; // Path should exist, as you cant really write to the file before reading it first

                File.WriteAllText(rubiesfile, JsonConvert.SerializeObject(ruby));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetupRubies(string playerid)
        {
            try
            {
                var rubiesfile = $"./{playerid}/rubies.json";
                var obj = new SplitRubyResponse()
                {
                    result = new SplitRubyResult()
                    {
                        earned = 100,  // <- Hardcoded default Ruby count, change later if we want
                        purchased = 0
                    }
                };
                File.WriteAllText(rubiesfile, JsonConvert.SerializeObject(obj));
                return true;
            }
            catch
            {
                Console.WriteLine($"Creating default ruby counts failed! User ID: {playerid}");
                return false;
            }
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
            var response = new RubyResponse()
            {
                result = splitrubies.result.earned + splitrubies.result.purchased
            };

            return response;
        }
    }
}