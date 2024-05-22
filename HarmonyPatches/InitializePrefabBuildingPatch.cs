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
            switch(__instance.name)
            {
                case "Warehouse Yard 01":
                case "Small Warehouse 01":
                case "Medium Warehouse 01":
                case "Large Warehouse 01":
                case "Warehouse with Railway Connection":
                case "Grain Silo 01":
                case "Grain Silo 02":
                case "Barn 01":
                case "Barn 02":
                    if(__instance.GetAI() is not ExtendedWarehouseAI)
                    {
                        Object.DestroyImmediate(oldAI);
                        var newAI = __instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                        PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
                    }
                    break;
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
                    switch (__instance.name)
                    {
                        case "Warehouse Yard 01":
                        case "Small Warehouse 01":
                        case "Medium Warehouse 01":
                        case "Large Warehouse 01":
                        case "Warehouse with Railway Connection":
                            extendedWarehouseAI.m_extendedStorageType = ExtendedTransferManager.TransferReason.None;
                            extendedWarehouseAI.m_storageType = TransferManager.TransferReason.None;
                            extendedWarehouseAI.m_isFarmIndustry = false;
                            extendedWarehouseAI.m_isFishIndustry = false;
                            break;
                        case "Grain Silo 01":
                        case "Grain Silo 02":
                        case "Barn 01":
                        case "Barn 02":
                            extendedWarehouseAI.m_storageType = TransferManager.TransferReason.None;
                            extendedWarehouseAI.m_extendedStorageType = ExtendedTransferManager.TransferReason.None;
                            extendedWarehouseAI.m_isFarmIndustry = true;
                            extendedWarehouseAI.m_isFishIndustry = false;
                            break;
                    }

                }
            }
            
        }

    }
}
