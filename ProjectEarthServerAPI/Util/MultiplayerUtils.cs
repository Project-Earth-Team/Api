using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Buildplate;

namespace ProjectEarthServerAPI.Util
{
    public class MultiplayerUtils
    {
        private static Dictionary<string, BuildplateServerResponse> instanceList =
            new Dictionary<string, BuildplateServerResponse>();
        public static BuildplateServerResponse CreateBuildplateInstance(string playerId, string buildplateId,
            PlayerCoordinate playerCoords)
        {
            // TODO: Actually start the server

            var ServerIp = "192.168.2.100";
            var ServerPort = 19132;
            var BlocksPerMeter = 14.0;
            var serverInstanceId = Guid.NewGuid().ToString();
            var instanceMetadata = new BuildplateServerResponse.InstanceMetadata
            {
                buildplateid = buildplateId
            };

            var dimensions = new BuildplateServerResponse.Dimension
            {
                x = 8,
                z = 8
            };
            var templateId = "InsertBuildplateTemplateIdHere";
            var surfaceOrientation = "Horizontal"; // Can also be vertical

            var buildplateData = new BuildplateServerResponse.GameplayMetadata
            {
                augmentedImageSetId = null,
                blocksPerMeter = BlocksPerMeter,
                breakableItemToItemLootMap = new BuildplateServerResponse.BreakableItemToItemLootMap(),
                dimension = dimensions, // TODO: BuildplateInfo
                gameplayMode = "Buildplate",
                isFullSize = (dimensions.x >= 32 && dimensions.z >= 32), // TODO: BuildplateInfo
                playerJoinCode = "AAAAAAAAAAAAAAAAAAAAAAAA", // 24 letters/Numbers, probably randomly generated
                rarity = null, // Why even is this here?
                shutdownBehavior = new List<string>() { "ServerShutdownWhenAllPlayersQuit", "ServerShutdownWhenHostPlayerQuits"}, // Own instance server needs to respect this
                snapshotOptions = new BuildplateServerResponse.SnapshotOptions
                {
                    saveState = new BuildplateServerResponse.SaveState // Should be the same for all buildplates
                    {
                        inventory = true,
                        model = true,
                        world = true
                    },
                    snapshotTriggerConditions = "None",
                    snapshotWorldStorage = "Buildplate",
                    triggerConditions = new List<string>() { "Interval", "PlayerExits"},
                    triggerInterval = new TimeSpan(00,00,30)
                },
                spawningClientBuildNumber = "2020.1217.02", // How should we figure this out? Should probably just be the latest every time
                spawningPlayerId = playerId,
                surfaceOrientation = surfaceOrientation, // TODO: BuildplateInfo
                templateId = templateId, // TODO: BuildplateInfo
                worldId = buildplateId

            };

            var result = new BuildplateServerResponse
            {
                result = new BuildplateServerResponse.Result
                {
                    applicationStatus = "Unknown",
                    fqdn = "hi mojang :)", // hope this doesnt break anything
                    gameplayMetadata = buildplateData,
                    hostCoordinate = playerCoords, 
                    instanceId = serverInstanceId,
                    ipV4Address = ServerIp,
                    metadata = JsonConvert.SerializeObject(instanceMetadata),
                    partitionId = playerId,
                    port = ServerPort,
                    roleInstance = "776932eeeb69", // Maybe randomly generated for each instance?
                    serverReady = false,           // Maybe we can get this from our own server, but right now, it 100% will never be ready on request lol
                    serverStatus = "Running"
                }
            };

            instanceList.Add(serverInstanceId,result);

            return result;
        }

        public static BuildplateServerResponse CheckInstanceStatus(string playerId, string instanceId)
        {
            // TODO: Refresh instance list from server & check allocator if instance is ready or not
            return null;
            /*
            if (instanceCheck == "Ready") {
            
                instanceList[instanceId].serverReady = true;
                instanceList[instanceId].applicationStatus = "Ready";
                return instanceList[instanceId];
            }
            */
        }
    }
}