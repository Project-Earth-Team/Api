using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Buildplate;
using ProjectEarthServerAPI.Models.Multiplayer;
using ProjectEarthServerAPI.Models.Player;

namespace ProjectEarthServerAPI.Util
{
    public class MultiplayerUtils
    {
        private static Dictionary<string, BuildplateServerResponse> instanceList = new();
        private static Dictionary<string, Guid> apiKeyList = new();

        public static BuildplateServerResponse CreateBuildplateInstance(string playerId, string buildplateId,
            PlayerCoordinate playerCoords)
        {
            // TODO: Actually start the server

            Console.WriteLine($"Creating new buildplate instance: Player {playerId} Buildplate {buildplateId}");

            var ServerIp = "192.168.2.100";
            var ServerPort = 19132;
            var BlocksPerMeter = 14.0;
            var serverInstanceId = Guid.NewGuid().ToString();
            var BuildplateOffset = new BuildplateServerResponse.Offset {x = 0.0, y = 34.0, z = 0.0};
            var instanceMetadata = new BuildplateServerResponse.InstanceMetadata
            {
                buildplateid = buildplateId
            };

            var dimensions = new BuildplateServerResponse.Dimension
            {
                x = 8,
                z = 8
            };
            var templateId = "00000000-0000-0000-0000-000000000000"; // Not used AFAIK
            var surfaceOrientation = "Horizontal"; // Can also be vertical

            var buildplateData = new BuildplateServerResponse.GameplayMetadata
            {
                augmentedImageSetId = null,
                blocksPerMeter = BlocksPerMeter,
                breakableItemToItemLootMap = new BuildplateServerResponse.BreakableItemToItemLootMap(),
                dimension = dimensions, // TODO: BuildplateInfo
                gameplayMode = "Buildplate",
                isFullSize = (dimensions.x >= 32 && dimensions.z >= 32), // TODO: BuildplateInfo
                offset = BuildplateOffset, // Same for all buildplates
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
                    applicationStatus = "Ready",
                    fqdn = "hi mojang :)", // hope this doesnt break anything
                    gameplayMetadata = buildplateData,
                    hostCoordinate = playerCoords, 
                    instanceId = serverInstanceId,
                    ipV4Address = ServerIp,
                    metadata = JsonConvert.SerializeObject(instanceMetadata),
                    partitionId = playerId,
                    port = ServerPort,
                    roleInstance = "776932eeeb69", // Maybe randomly generated for each instance?
                    serverReady = true,           // Maybe we can get this from our own server, but right now, it 100% will never be ready on request lol
                    serverStatus = "Running"
                }
            };

            var apiKey = Guid.NewGuid();
            apiKeyList.Add(serverInstanceId,apiKey);
            instanceList.Add(serverInstanceId,result);


            return result;
        }

        public static BuildplateServerResponse CheckInstanceStatus(string playerId, string instanceId)
        {
            // TODO: Refresh instance list from server & check allocator if instance is ready or not
            //return null;
            return instanceList[instanceId];
            /*
            if (instanceCheck == "Ready") {
            
                instanceList[instanceId].serverReady = true;
                instanceList[instanceId].applicationStatus = "Ready";
                return instanceList[instanceId];
            }
            */
        }

        public static InventoryResponse GetInventoryForPlayer(string playerId, Guid apiKey)
        {
            return apiKeyList.ContainsValue(apiKey) ? InventoryUtils.ReadInventory(playerId) : new InventoryResponse();
        }

        private static void EditInventoryForPlayer(string playerId, string itemIdentifier, int count = 1,
            bool removeItem = false, bool isStackableItem = true, string instanceId = null)
        {
            if (removeItem) InventoryUtils.RemoveItemFromInv(playerId, itemIdentifier, count, instanceId);
            else InventoryUtils.AddItemToInv(playerId, itemIdentifier, count, isStackableItem, instanceId);
        }

        public static string ExecuteServerCommand(ServerCommandRequest request)
        {
            if (apiKeyList.ContainsValue(request.apiKey))
            {
                var command = request.command;
                var playerId = request.playerId;
                switch (command)
                {
                    case ServerCommandType.GetBuildplate:
                        var buildplate = JsonConvert.DeserializeObject<BuildplateRequest>(request.requestData);
                        return JsonConvert.SerializeObject(GetBuildplateById(playerId, buildplate.buildplateId));

                    case ServerCommandType.GetInventory:
                        return JsonConvert.SerializeObject(InventoryUtils.ReadInventory(playerId));

                    case ServerCommandType.EditInventory:
                        var invData = JsonConvert.DeserializeObject<InventoryItemRequest>(request.requestData);
                        EditInventoryForPlayer(playerId, invData.itemIdentifier, invData.count, invData.removeItem);
                        return "ok";
                        break;

                    case ServerCommandType.EditBuildplate:
                        throw new NotImplementedException();
                        break;

                    default:
                        return null;
                }
            }
            else return null;
        }

        public static BuildplateListResponse GetBuildplates(string playerId)
        {
            var buildplates = GenericUtils.ParseJsonFile<BuildplateListResponse>(playerId, "buildplates");

            return buildplates;

        }

        public static BuildplateListResponse.Result GetBuildplateById(string playerId,string buildplateId)
        {
            var list = GetBuildplates(playerId);
            return list.result.Find(match => match.id == buildplateId);
        }
    }
}