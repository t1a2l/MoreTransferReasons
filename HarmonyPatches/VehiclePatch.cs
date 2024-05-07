using ColossalFramework.Math;
using ColossalFramework;
using System;
using UnityEngine;
using HarmonyLib;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class VehiclePatch
    {
        [HarmonyPatch(typeof(Vehicle), "Spawn")]
        [HarmonyPrefix]
        public static bool Spawn(Vehicle __instance, ushort vehicleID)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            VehicleInfo info = __instance.Info;
            if(__instance.m_transferType >= 200 && __instance.m_transferType != 255 && info.m_trailers != null && info.m_trailers.Length > 0)
            {
                if ((__instance.m_flags & Vehicle.Flags.Spawned) == 0)
                {
                    __instance.m_flags |= Vehicle.Flags.Spawned;
                    instance.AddToGrid(vehicleID, ref __instance, info.m_isLargeVehicle);
                }
                if (__instance.m_leadingVehicle == 0 && __instance.m_trailingVehicle != 0)
                {
                    ushort trailingVehicle = __instance.m_trailingVehicle;
                    int num = 0;
                    while (trailingVehicle != 0)
                    {
                        instance.m_vehicles.m_buffer[trailingVehicle].Spawn(trailingVehicle);
                        trailingVehicle = instance.m_vehicles.m_buffer[trailingVehicle].m_trailingVehicle;
                        if (++num > 16384)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
                if (__instance.m_leadingVehicle != 0 || __instance.m_trailingVehicle != 0 || info.m_trailers == null)
                {
                    return false;
                }
                bool flag = info.m_vehicleAI.VerticalTrailers();
                ushort num2 = vehicleID;
                bool flag2 = (instance.m_vehicles.m_buffer[num2].m_flags & Vehicle.Flags.Reversed) != 0;
                Vehicle.Frame lastFrameData = __instance.GetLastFrameData();
                float num3 = ((!flag) ? (info.m_generatedInfo.m_size.z * 0.5f) : 0f);
                num3 -= (((__instance.m_flags & Vehicle.Flags.Inverted) == 0) ? info.m_attachOffsetBack : info.m_attachOffsetFront);
                Randomizer randomizer = new(vehicleID);
                int num4 = 0;
                for (int i = 0; i < info.m_trailers.Length; i++)
                {
                    if (randomizer.Int32(100u) >= info.m_trailers[i].m_probability)
                    {
                        continue;
                    }
                    VehicleInfo info2 = info.m_trailers[i].m_info;
                    bool flag3 = randomizer.Int32(100u) < info.m_trailers[i].m_invertProbability;
                    num3 += ((!flag) ? (info2.m_generatedInfo.m_size.z * 0.5f) : info2.m_generatedInfo.m_size.y);
                    num3 -= ((!flag3) ? info2.m_attachOffsetFront : info2.m_attachOffsetBack);
                    Vector3 position = lastFrameData.m_position - lastFrameData.m_rotation * new Vector3(0f, (!flag) ? 0f : num3, (!flag) ? num3 : 0f);
                    ExtedndedVehicleManager.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, info2, position, __instance.m_transferType, transferToSource: false, transferToTarget: false);
                    if (vehicle != 0)
                    {
                        instance.m_vehicles.m_buffer[num2].m_trailingVehicle = vehicle;
                        instance.m_vehicles.m_buffer[vehicle].m_leadingVehicle = num2;
                        instance.m_vehicles.m_buffer[vehicle].m_gateIndex = __instance.m_gateIndex;
                        if (flag3)
                        {
                            instance.m_vehicles.m_buffer[vehicle].m_flags |= Vehicle.Flags.Inverted;
                        }
                        if (flag2)
                        {
                            instance.m_vehicles.m_buffer[vehicle].m_flags |= Vehicle.Flags.Reversed;
                        }
                        if (i + 1 < info.m_trailers.Length)
                        {
                            instance.m_vehicles.m_buffer[vehicle].m_flags2 |= Vehicle.Flags2.MiddleTrailer;
                        }
                        instance.m_vehicles.m_buffer[vehicle].m_frame0.m_rotation = lastFrameData.m_rotation;
                        instance.m_vehicles.m_buffer[vehicle].m_frame1.m_rotation = lastFrameData.m_rotation;
                        instance.m_vehicles.m_buffer[vehicle].m_frame2.m_rotation = lastFrameData.m_rotation;
                        instance.m_vehicles.m_buffer[vehicle].m_frame3.m_rotation = lastFrameData.m_rotation;
                        info2.m_vehicleAI.FrameDataUpdated(vehicle, ref instance.m_vehicles.m_buffer[vehicle], ref instance.m_vehicles.m_buffer[vehicle].m_frame0);
                        instance.m_vehicles.m_buffer[vehicle].Spawn(vehicle);
                        num2 = vehicle;
                    }
                    num3 += ((!flag) ? (info2.m_generatedInfo.m_size.z * 0.5f) : 0f);
                    num3 -= ((!flag3) ? info2.m_attachOffsetBack : info2.m_attachOffsetFront);
                    if (++num4 == info.m_maxTrailerCount)
                    {
                        break;
                    }
                }
                return false;
            }
            return true;
        }
    }
}
