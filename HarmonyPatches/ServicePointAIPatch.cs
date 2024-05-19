using ColossalFramework;
using HarmonyLib;
using MoreTransferReasons.AI;
using System;
using System.Reflection;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class ServicePointAIPatch
    {
        private delegate string PlayerBuildingAIGetDebugStringDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building data);
        private static PlayerBuildingAIGetDebugStringDelegate BaseGetDebugString = AccessTools.MethodDelegate<PlayerBuildingAIGetDebugStringDelegate>(typeof(PlayerBuildingAI).GetMethod("GetDebugString", BindingFlags.Instance | BindingFlags.Public), null, false);

        [HarmonyPatch(typeof(ServicePointAI), "GetDebugString")]
        [HarmonyPrefix]
        public static bool GetDebugString(ServicePointAI __instance, ushort buildingID, ref Building data, ref string __result)
        {
            string debugString = BaseGetDebugString(__instance, buildingID, ref data);
            debugString = StringUtils.SafeFormat("{0}\nPosition: ({1},{2})", debugString, Mathf.Round(data.m_position.x), Mathf.Round(data.m_position.z));
            byte park = Singleton<DistrictManager>.instance.GetPark(data.m_position);
            debugString = StringUtils.SafeFormat("{0}\nPark: {1}", debugString, park);
            string debugString2 = debugString;
            if (park != 0)
            {
                Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
                DistrictPark[] buffer2 = Singleton<DistrictManager>.instance.m_parks.m_buffer;
                Vehicle[] buffer3 = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
                ExtendedDistrictPark[] buffer4 = Singleton<ExtendedDistrictManager>.instance.m_industryParks.m_buffer;
                for (int i = 0; i < DistrictPark.pedestrianReasonsCount; i++)
                {
                    TransferManager.TransferReason material = DistrictPark.kPedestrianZoneTransferReasons[i].m_material;
                    int count = buffer2[park].m_materialSuggestion[i].Count;
                    int count2 = buffer2[park].m_materialRequest[i].Count;
                    int num = 0;
                    foreach (ushort item in buffer2[park].m_materialSuggestion[i])
                    {
                        int amount = 0;
                        int max = 0;
                        buffer[item].Info.m_buildingAI.GetMaterialAmount(item, ref buffer[item], material, out amount, out max);
                        num += amount;
                    }
                    int num2 = 0;
                    foreach (ushort item2 in buffer2[park].m_materialRequest[i])
                    {
                        int amount2 = 0;
                        int max2 = 0;
                        buffer[item2].Info.m_buildingAI.GetMaterialAmount(item2, ref buffer[item2], material, out amount2, out max2);
                        num2 += max2 - amount2;
                    }
                    int num3 = 0;
                    for (int j = 0; j < buffer2[park].m_finalGateCount; j++)
                    {
                        ushort num4 = buffer2[park].m_finalServicePointList[j];
                        if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        ushort num5 = buffer[num4].m_guestVehicles;
                        int num6 = 0;
                        while (num5 != 0)
                        {
                            if ((TransferManager.TransferReason)buffer3[num5].m_transferType == material)
                            {
                                num3++;
                            }
                            num5 = buffer3[num5].m_nextGuestVehicle;
                            if (++num6 > 16384)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                        }
                    }
                    if (count == 0 && count2 == 0)
                    {
                        continue;
                    }
                    ushort num7 = 0;
                    for (int k = 0; k < DistrictPark.kDeliveryCategories.Length; k++)
                    {
                        if (DistrictPark.kDeliveryCategories[k] == DistrictPark.kPedestrianZoneTransferReasons[i].m_deliveryCategory)
                        {
                            num7 = buffer2[park].m_randomServicePoints[k];
                            break;
                        }
                    }
                    string text = material.ToString();
                    if (text.Length > 15)
                    {
                        text = text.Substring(0, 15);
                    }
                    else if (text.Length < 15)
                    {
                        for (int l = text.Length; l < 15; l++)
                        {
                            text = " " + text;
                        }
                    }
                    if (count != 0 && count2 != 0)
                    {
                        debugString = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): SUG Am={4} Bld={5} Avg={6}\tREQ Am={7} Bld={8} Avg={9}", debugString, text, num7, num3, num, count, num / count, num2, count2, num2 / count2);
                    }
                    else if (count != 0)
                    {
                        debugString = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): SUG Am={4} Bld={5} Avg={6}", debugString, text, num7, num3, num, count, num / count);
                    }
                    else if (count2 != 0)
                    {
                        debugString = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): REQ Am={4} Bld={5} Avg={6}", debugString, text, num7, num3, num2, count2, num2 / count2);
                    }
                }

                for (int i = 0; i < ExtendedDistrictPark.pedestrianExtendedReasonsCount; i++)
                {
                    ExtendedTransferManager.TransferReason material = ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[i].m_material;
                    int count = buffer4[park].m_extendedMaterialSuggestion[i].Count;
                    int count2 = buffer4[park].m_extendedMaterialRequest[i].Count;
                    int num = 0;
                    foreach (ushort item in buffer2[park].m_materialSuggestion[i])
                    {
                        ((IExtendedBuildingAI)buffer[item].Info.m_buildingAI).ExtendedGetMaterialAmount(item, ref buffer[item], material, out int amount, out int max);
                        num += amount;
                    }
                    int num2 = 0;
                    foreach (ushort item2 in buffer2[park].m_materialRequest[i])
                    {
                        ((IExtendedBuildingAI)buffer[item2].Info.m_buildingAI).ExtendedGetMaterialAmount(item2, ref buffer[item2], material, out int amount2, out int max2);
                        num2 += max2 - amount2;
                    }
                    int num3 = 0;
                    for (int j = 0; j < buffer2[park].m_finalGateCount; j++)
                    {
                        ushort num4 = buffer2[park].m_finalServicePointList[j];
                        if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        ushort num5 = buffer[num4].m_guestVehicles;
                        int num6 = 0;
                        while (num5 != 0)
                        {
                            if ((ExtendedTransferManager.TransferReason)buffer3[num5].m_transferType == material)
                            {
                                num3++;
                            }
                            num5 = buffer3[num5].m_nextGuestVehicle;
                            if (++num6 > 16384)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                        }
                    }
                    if (count == 0 && count2 == 0)
                    {
                        continue;
                    }
                    ushort num7 = 0;
                    for (int k = 0; k < DistrictPark.kDeliveryCategories.Length; k++)
                    {
                        if (DistrictPark.kDeliveryCategories[k] == DistrictPark.kPedestrianZoneTransferReasons[i].m_deliveryCategory)
                        {
                            num7 = buffer2[park].m_randomServicePoints[k];
                            break;
                        }
                    }
                    string text = material.ToString();
                    if (text.Length > 15)
                    {
                        text = text.Substring(0, 15);
                    }
                    else if (text.Length < 15)
                    {
                        for (int l = text.Length; l < 15; l++)
                        {
                            text = " " + text;
                        }
                    }
                    if (count != 0 && count2 != 0)
                    {
                        debugString2 = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): SUG Am={4} Bld={5} Avg={6}\tREQ Am={7} Bld={8} Avg={9}", debugString, text, num7, num3, num, count, num / count, num2, count2, num2 / count2);
                    }
                    else if (count != 0)
                    {
                        debugString2 = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): SUG Am={4} Bld={5} Avg={6}", debugString, text, num7, num3, num, count, num / count);
                    }
                    else if (count2 != 0)
                    {
                        debugString2 = StringUtils.SafeFormat("{0}\n{1}\t(SP={2} Veh={3}): REQ Am={4} Bld={5} Avg={6}", debugString, text, num7, num3, num2, count2, num2 / count2);
                    }
                }


            }
            if ((__instance.m_deliveryCategories & DistrictPark.DeliveryCategories.Cargo) != 0)
            {
                debugString = StringUtils.SafeFormat("{0}\nCargo traffic rate: {1} {2} {3} {4}, avg: {5:0.0}", debugString, (data.m_cargoTrafficRate >> 24) & 0xFFu, (data.m_cargoTrafficRate >> 16) & 0xFFu, (data.m_cargoTrafficRate >> 8) & 0xFFu, (data.m_cargoTrafficRate >> 0) & 0xFFu, __instance.GetAvgTrafficRate(buildingID, ref data, DistrictPark.DeliveryCategories.Cargo));
                debugString2 = StringUtils.SafeFormat("{0}\nCargo traffic rate: {1} {2} {3} {4}, avg: {5:0.0}", debugString2, (data.m_cargoTrafficRate >> 24) & 0xFFu, (data.m_cargoTrafficRate >> 16) & 0xFFu, (data.m_cargoTrafficRate >> 8) & 0xFFu, (data.m_cargoTrafficRate >> 0) & 0xFFu, __instance.GetAvgTrafficRate(buildingID, ref data, DistrictPark.DeliveryCategories.Cargo));
            }
            if ((__instance.m_deliveryCategories & DistrictPark.DeliveryCategories.Garbage) != 0)
            {
                debugString = StringUtils.SafeFormat("{0}\nGarbage traffic rate: {1} {2} {3} {4}, avg: {5:0.0}", debugString, (data.m_garbageTrafficRate >> 24) & 0xFFu, (data.m_garbageTrafficRate >> 16) & 0xFFu, (data.m_garbageTrafficRate >> 8) & 0xFFu, (data.m_garbageTrafficRate >> 0) & 0xFFu, __instance.GetAvgTrafficRate(buildingID, ref data, DistrictPark.DeliveryCategories.Garbage));
                debugString2 = StringUtils.SafeFormat("{0}\nGarbage traffic rate: {1} {2} {3} {4}, avg: {5:0.0}", debugString2, (data.m_garbageTrafficRate >> 24) & 0xFFu, (data.m_garbageTrafficRate >> 16) & 0xFFu, (data.m_garbageTrafficRate >> 8) & 0xFFu, (data.m_garbageTrafficRate >> 0) & 0xFFu, __instance.GetAvgTrafficRate(buildingID, ref data, DistrictPark.DeliveryCategories.Garbage));
            }
            __result = debugString + Environment.NewLine + debugString2;
            return false;
        }

    }
}
