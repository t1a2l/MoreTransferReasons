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
            if (oldAI != null && oldAI is CommercialBuildingAI && Utils.Settings.ExtendedCommercialBuildingAI.value)
            {
                var oldBuildingAI = oldAI as CommercialBuildingAI;
                var oldInfo = oldBuildingAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedCommercialBuildingAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newBuildingAI = newAI as ExtendedCommercialBuildingAI;
                newBuildingAI?.m_info = oldInfo;
            }
            else if(oldAI != null && oldAI is OutsideConnectionAI && Utils.Settings.ExtendedOutsideConnectionAI.value)
            {
                var oldBuildingAI = oldAI as OutsideConnectionAI;
                var oldInfo = oldBuildingAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedOutsideConnectionAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newBuildingAI = newAI as ExtendedOutsideConnectionAI;
                newBuildingAI?.m_info = oldInfo;
            }
            else if (oldAI != null && oldAI is ServicePointAI && Utils.Settings.ExtendedServicePointAI.value)
            {
                var oldBuildingAI = oldAI as ServicePointAI;
                var oldInfo = oldBuildingAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedServicePointAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newBuildingAI = newAI as ExtendedServicePointAI;
                newBuildingAI?.m_info = oldInfo;
            }
            else if (oldAI != null && oldAI is WarehouseStationAI && Utils.Settings.ExtendedWarehouseAI.value)
            {
                var oldBuildingAI = oldAI as WarehouseStationAI;
                var oldInfo = oldBuildingAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedWarehouseStationAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newBuildingAI = newAI as ExtendedWarehouseStationAI;
                newBuildingAI?.m_info = oldInfo;
            }
            else if (oldAI != null && oldAI is WarehouseAI && Utils.Settings.ExtendedWarehouseAI.value)
            {
                var oldBuildingAI = oldAI as WarehouseAI;
                var oldInfo = oldBuildingAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedWarehouseAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newBuildingAI = newAI as ExtendedWarehouseAI;
                newBuildingAI?.m_info = oldInfo;
            }
            
        }
    }
}
