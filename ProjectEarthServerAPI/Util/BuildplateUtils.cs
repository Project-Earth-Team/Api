using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models.Buildplate;
using ProjectEarthServerAPI.Models.Multiplayer;
using Serilog;

namespace ProjectEarthServerAPI.Util
{
    public class BuildplateUtils
    {
        public static BuildplateListResponse GetBuildplatesList(string playerId)
        {
            var buildplates = ReadPlayerBuildplateList(playerId);
            BuildplateListResponse list = new BuildplateListResponse { result = new List<BuildplateData>() };
            foreach (Guid id in buildplates.UnlockedBuildplates)
            {
                var bp = ReadBuildplate(id);
                bp.order = buildplates.UnlockedBuildplates.IndexOf(id);
                list.result.Add(bp.id != bp.templateId ? ReadBuildplate(id) : CloneTemplateBuildplate(playerId, bp));
            }

            buildplates.LockedBuildplates.ForEach(action =>
            {
                var bp = ReadBuildplate(action);
                bp.order = buildplates.LockedBuildplates.IndexOf(action) + buildplates.UnlockedBuildplates.Count;
                list.result.Add(bp);
            });

            return list;
        }

        public static PlayerBuildplateList ReadPlayerBuildplateList(string playerId)
            => GenericUtils.ParseJsonFile<PlayerBuildplateList>(playerId, "buildplates");

        public static void WritePlayerBuildplateList(string playerId, PlayerBuildplateList list)
            => GenericUtils.WriteJsonFile(playerId, list, "buildplates");

        public static BuildplateData CloneTemplateBuildplate(string playerId, BuildplateData templateBuildplate)
        {
            var clonedId = Guid.NewGuid();
            BuildplateData clonedBuildplate = templateBuildplate;
            clonedBuildplate.id = clonedId;
            clonedBuildplate.locked = false;

            WriteBuildplate(clonedBuildplate);

            var list = ReadPlayerBuildplateList(playerId);
            var index = list.UnlockedBuildplates.IndexOf(templateBuildplate.id);
            list.UnlockedBuildplates.Remove(templateBuildplate.id);
            list.UnlockedBuildplates.Insert(index, clonedId);

            WritePlayerBuildplateList(playerId, list);

            return clonedBuildplate;
        }

        public static BuildplateShareResponse GetBuildplateById(BuildplateRequest buildplateReq)
        {
            BuildplateData buildplate = ReadBuildplate(buildplateReq.buildplateId);

            return new BuildplateShareResponse
            {
                result = new BuildplateShareResponse.BuildplateShareInfo
                {
                    buildplateData = buildplate,
                    playerId = null
                }
            };
        }

        public static BuildplateData ReadBuildplate(Guid buildplateId)
        {
            var filepath = $"./data/buildplates/{buildplateId}.json"; // TODO: Add to config
            if (!File.Exists(filepath))
            {
                Log.Error($"Error: Tried to read buildplate that does not exist! BuildplateID: {buildplateId}");
                return null;
            }

            var buildplateJson = File.ReadAllText(filepath);
            var parsedobj = JsonConvert.DeserializeObject<BuildplateData>(buildplateJson);
            return parsedobj;
        }

        public static void WriteBuildplate(BuildplateData data)
        {
            var buildplateId = data.id;
            var filepath = $"./data/buildplates/{buildplateId}.json"; // TODO: Add to config

            data.lastUpdated = DateTime.UtcNow;

            File.WriteAllText(filepath, JsonConvert.SerializeObject(data));
        }

        public static void WriteBuildplate(BuildplateShareResponse shareResponse)
            => WriteBuildplate(shareResponse.result.buildplateData);

    }
}