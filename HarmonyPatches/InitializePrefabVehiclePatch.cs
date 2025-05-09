using System;
using HarmonyLib;
using UnityEngine;
using MoreTransferReasons.AI;
using MoreTransferReasons.Utils;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(VehicleInfo), "InitializePrefab")]
    public static class InitializePrefabVehiclePatch
    {
        public static void Prefix(VehicleInfo __instance)
        {
            try
            {
                var oldAI = __instance.GetComponent<PrefabAI>();
                if ((__instance.m_class.m_service == ItemClass.Service.PlayerIndustry || __instance.m_class.m_service == ItemClass.Service.Industrial || __instance.m_class.m_service == ItemClass.Service.Fishing) && !__instance.name.Contains("Trailer") && __instance.m_vehicleType == VehicleInfo.VehicleType.Car && Utils.Settings.ExtendedCargoTruckAI.value == true)
                {
                    UnityEngine.Object.DestroyImmediate(oldAI);
                    var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedCargoTruckAI>();
                    PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

    }
}