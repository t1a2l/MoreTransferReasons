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
                var newAI = __instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if ((__instance.name.Contains("Grain Silo 01") || __instance.name.Contains("Grain Silo 02") || __instance.name.Contains("Barn 01") || __instance.name.Contains("Barn 02")) && __instance.GetAI() is not ExtendedWarehouseAI && !__instance.name.Contains("Sub"))
            {
                Object.DestroyImmediate(oldAI);
                var newAI = __instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
        }

        public static void Postfix(BuildingInfo __instance)
        {
            uint index = 0U;
            for (; PrefabCollection<BuildingInfo>.LoadedCount() > index; ++index)
            {
                BuildingInfo buildingInfo = PrefabCollection<BuildingInfo>.GetLoaded(index);

                if (buildingInfo != null && buildingInfo.GetAI() is ExtendedWarehouseAI extendedWarehouseAI)
                {
                    if (__instance.name.Contains("Warehouse Yard 01") || __instance.name.Contains("Small Warehouse 01") || __instance.name.Contains("Medium Warehouse 01") || __instance.name.Contains("Large Warehouse 01")) 
                    {
                        extendedWarehouseAI.m_extendedStorageType = ExtendedTransferManager.TransferReason.None;
                        extendedWarehouseAI.m_storageType = TransferManager.TransferReason.None;
                        extendedWarehouseAI.m_isFarmIndustry = false;
                        extendedWarehouseAI.m_isFishIndustry = false;
                    }
                    if (__instance.name.Contains("Grain Silo 01") || __instance.name.Contains("Grain Silo 02") || __instance.name.Contains("Barn 01") || __instance.name.Contains("Barn 02"))
                    {
                        extendedWarehouseAI.m_storageType = TransferManager.TransferReason.None;
                        extendedWarehouseAI.m_extendedStorageType = ExtendedTransferManager.TransferReason.None;
                        extendedWarehouseAI.m_isFarmIndustry = true;
                        extendedWarehouseAI.m_isFishIndustry = false;
                    }

                }
            }
            
        }

    }
}
