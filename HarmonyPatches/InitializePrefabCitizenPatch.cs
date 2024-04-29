using HarmonyLib;
using MoreTransferReasons.Code;
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

        }
    }
}
