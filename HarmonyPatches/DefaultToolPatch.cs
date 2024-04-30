using HarmonyLib;
using UnityEngine;
using MoreTransferReasons.AI;
using MoreTransferReasons.UI;
using ColossalFramework;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(DefaultTool))]
    public static class DefaultToolPatch
    {
        [HarmonyPatch(typeof(DefaultTool), "OpenWorldInfoPanel")]
        [HarmonyPrefix]
        public static bool OpenWorldInfoPanel(DefaultTool __instance, InstanceID id, Vector3 position)
        {
            if (id.Building != 0)
            {
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[id.Building].Info;
                ExtendedWarehouseAI extendedWarehouseAI = info.m_buildingAI as ExtendedWarehouseAI;
                if (Singleton<InstanceManager>.instance.SelectInstance(id))
                {
                    if(extendedWarehouseAI != null)
		            {
		                WorldInfoPanel.Show<ExtendedWarehouseWorldInfoPanel>(position, id);
                        return false;
		            }
                    else
                    {
                        WorldInfoPanel.Hide<ExtendedWarehouseWorldInfoPanel>();
                    }
                }
            }
            else
            {
                WorldInfoPanel.Hide<ExtendedWarehouseWorldInfoPanel>();
            }
            return true;
        }
    }
}
