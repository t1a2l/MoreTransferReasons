using HarmonyLib;
using MoreTransferReasons.AI;
using MoreTransferReasons.Utils;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(BuildingInfo), "InitializePrefab")]
    public static class InitializePrefabBuildingPatch
    {
        public static void Prefix(BuildingInfo __instance)
        {
            var oldAI = __instance.GetComponent<PrefabAI>();
            if (oldAI != null && oldAI is OutsideConnectionAI)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedOutsideConnectionAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if (oldAI != null && oldAI is ServicePointAI)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = __instance.gameObject.AddComponent<ExtendedServicePointAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if(oldAI != null && oldAI is WarehouseAI)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = __instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if(oldAI != null && oldAI is WarehouseStationAI)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = __instance.gameObject.AddComponent<ExtendedWarehouseStationAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if ((__instance.name.Contains("Warehouse Yard 01") || __instance.name.Contains("Small Warehouse 01") || __instance.name.Contains("Medium Warehouse 01") || __instance.name.Contains("Large Warehouse 01")) && __instance.GetAI() is not ExtendedWarehouseAI && !__instance.name.Contains("Sub"))
            {
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
        }
    }
}
