using ColossalFramework.Math;
using HarmonyLib;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class VehicleManagerPatch
    {
        [HarmonyPatch(typeof(VehicleManager), "CreateVehicle")]
        [HarmonyPostfix]
        public static void CreateVehicle(VehicleManager __instance, ref ushort vehicle, ref Randomizer r, VehicleInfo info, Vector3 position, TransferManager.TransferReason type, bool transferToSource, bool transferToTarget, ref bool __result)
        {
            if(__result && (type == ExtendedTransferManager.Cars || type == ExtendedTransferManager.HouseParts))
            {
                if (type == ExtendedTransferManager.Cars)
                {
                    __instance.m_vehicles.m_buffer[vehicle].m_gateIndex = 1;
                }
                else if (type == ExtendedTransferManager.HouseParts)
                {
                    __instance.m_vehicles.m_buffer[vehicle].m_gateIndex = 9;
                }
            }
        }

    }
}
