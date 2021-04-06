using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectEarthServerAPI.Models;
using ProjectEarthServerAPI.Models.Buildplate;
using ProjectEarthServerAPI.Models.Features;
using ProjectEarthServerAPI.Models.Multiplayer;
using ProjectEarthServerAPI.Models.Player;
using Serilog;

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

            Log.Information($"[{playerId}]: Creating new buildplate instance: Buildplate {buildplateId}");

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
            var templateId = "44e6e34f-f9a8-d92f-7f78-816f6493e753"; // Not used AFAIK
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
                playerJoinCode = "AAALbMlbaG57sSuQMe0Yek2w", // 24 letters/Numbers, probably randomly generated
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
                    //fqdn = "dns2527870c-89c6-420e-8378-996a2c40304a-azurebatch-cloudservice.westeurope.cloudapp.azure.com", // figure out why this breaks everything
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
                },
                updates = new Updates()
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
            instanceList[instanceId].result.applicationStatus = "Ready";
            instanceList[instanceId].result.serverReady = true;
            return instanceList[instanceId];
            /*
            if (instanceCheck == "Ready") {
            
                instanceList[instanceId].serverReady = true;
                instanceList[instanceId].applicationStatus = "Ready";
                return instanceList[instanceId];
            }
            */
        }

        private static HotbarTranslation[] EditHotbarForPlayer(string playerId, MultiplayerItem[] multiplayerHotbar)
        {
            if (multiplayerHotbar == null)
            {
                return null;
            }

            if (multiplayerHotbar.Length != 7)
            {
                var tempArr = new MultiplayerItem[7];
                multiplayerHotbar.CopyTo(tempArr,0);
                for (int i = 0; i < tempArr.Length; i++)
                {
                    tempArr[i] ??= new MultiplayerItem
                    {
                        category = new MultiplayerItemCategory
                        {
                            loc = ItemCategory.Invalid,
                            value = (int) ItemCategory.Invalid
                        },
                        count = 0,
                        guid = Guid.Empty,
                        owned = true,
                        rarity = new MultiplayerItemRarity
                        {
                            loc = ItemRarity.Invalid,
                            value = (int) ItemRarity.Invalid
                        }
                    };
                }

                multiplayerHotbar = tempArr;
            }

            var inv = InventoryUtils.ReadInventory(playerId);
            var hotbar = new InventoryResponse.Hotbar[multiplayerHotbar.Length];
            HotbarTranslation[] response = new HotbarTranslation[multiplayerHotbar.Length];

            for (int i = 0; i < multiplayerHotbar.Length; i++)
            {
                MultiplayerItem item = multiplayerHotbar[i];
                if (item.guid != Guid.Empty)
                {
                    var catalogItem = StateSingleton.Instance.catalog.result.items.Find(match => match.id == item.guid);
                    hotbar[i] = new InventoryResponse.Hotbar
                    {
                        count = item.count,
                        id = item.guid,
                        instanceId = item.instance_data
                    };

                    response[i] = new HotbarTranslation
                    {
                        count = item.count,
                        identifier = catalogItem.item.name,
                        meta = catalogItem.item.aux,
                        slotId = i
                    };
                }
                else
                {
                    hotbar[i] = null;
                    response[i] = new HotbarTranslation
                    {
                        count = 0,
                        identifier = "air",
                        meta = 0,
                        slotId = i
                    };
                }
            }

            InventoryUtils.EditHotbar(playerId, hotbar);

            return response;
        }

        private static HotbarTranslation[] GetHotbarForPlayer(string playerId)
        {
            var inv = InventoryUtils.ReadInventory(playerId);
            var hotbar = inv.result.hotbar;
            HotbarTranslation[] response = new HotbarTranslation[hotbar.Length];

            for (int i = 0; i < hotbar.Length; i++)
            {
                InventoryResponse.Hotbar item = hotbar[i];

                if (item != null)
                {
                    var catalogItem = StateSingleton.Instance.catalog.result.items.Find(match => match.id == item.id);

                    response[i] = new HotbarTranslation
                    {
                        count = item.count,
                        identifier = catalogItem.item.name,
                        meta = catalogItem.item.aux,
                        slotId = i
                    };
                }
                else
                {
                    response[i] = new HotbarTranslation
                    {
                        count = 0,
                        identifier = "air",
                        meta = 0,
                        slotId = i
                    };
                }
            }

            return response;
        }

        public static void EditInventoryForPlayer(string playerId, EditInventoryRequest request)
        {
            var damage = request.meta == -1 ? 0 : request.meta;
            var catalogItem =
                StateSingleton.Instance.catalog.result.items.Find(match =>
                    match.item.name == request.identifier && match.item.aux == damage);
            var isNonStackableItem = catalogItem.item.type == "Tool";
            var isHotbar = request.slotIndex <= 6;

            if (isHotbar)
            {
                var hotbar = InventoryUtils.GetHotbar(playerId).Item2;
                var slot = hotbar[request.slotIndex] ?? new InventoryResponse.Hotbar();

                if (request.removeItem) slot = null;
                else
                {
                    slot.count = request.count + 1;
                    slot.id = catalogItem.id;

                    if (isNonStackableItem) slot.instanceId.health = request.health;

                }

                hotbar[request.slotIndex] = slot;
                InventoryUtils.EditHotbar(playerId, hotbar, false);

            }
            else
            {
                // Removing items from the normal inventory should never be possible, except from the hotbar
                //if (request.removeItem) InventoryUtils.RemoveItemFromInv(playerId, catalogItem.id, request.count, request.health);
                //else 
                InventoryUtils.AddItemToInv(playerId, catalogItem.id, request.count, !isNonStackableItem);
            }
        }

        public static string ExecuteServerCommand(ServerCommandRequest request)
        {
            Log.Information($"Received command from Server {request.serverId}!");
            if (!apiKeyList.ContainsValue(request.apiKey))
            {
                var command = request.command;
                var playerId = request.playerId;
                switch (command)
                {
                    case ServerCommandType.GetBuildplate:
                        var buildplate = JsonConvert.DeserializeObject<BuildplateRequest>(request.requestData);
                        return JsonConvert.SerializeObject(GetBuildplateById(playerId, buildplate.buildplateId));

                    case ServerCommandType.GetInventoryForClient:
                        var inv = InventoryUtils.ReadInventoryForMultiplayer(playerId);
                        return JsonConvert.SerializeObject(inv);

                    case ServerCommandType.GetInventory:
                        var hotbarForServer = GetHotbarForPlayer(playerId);
                        return JsonConvert.SerializeObject(hotbarForServer);

                    case ServerCommandType.EditInventory:
                        var invEdits = JsonConvert.DeserializeObject<EditInventoryRequest>(request.requestData);
                        EditInventoryForPlayer(playerId, invEdits);
                        //foreach (EditInventoryRequest editRequest in invEdits) EditInventoryForPlayer(playerId, editRequest);
                        return "ok";

                    case ServerCommandType.EditHotbar:
                        var invData = JsonConvert.DeserializeObject<MultiplayerInventoryResponse>(request.requestData);
                        var newHotbarInfo = EditHotbarForPlayer(playerId, invData.hotbar);
                        return JsonConvert.SerializeObject(newHotbarInfo);

                    case ServerCommandType.EditBuildplate:
                        throw new NotImplementedException();

                    case ServerCommandType.MarkServerAsReady:
                        instanceList[apiKeyList.First(match => match.Value == request.apiKey).Key].result.serverReady =
                            true;
                        return "ok";

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

        public static BuildplateListResponse.Result GetBuildplateById(string playerId, string buildplateId)
        {
            var list = GetBuildplates(playerId);
            return list.result.Find(match => match.id == buildplateId);
        }
    }
}