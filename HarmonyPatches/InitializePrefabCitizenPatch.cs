using MoreTransferReasons.AI;
using MoreTransferReasons.Utils;
using HarmonyLib;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(CitizenInfo), "InitializePrefab")]
    public static class InitializePrefabCitizenPatch
    {
        public static void Prefix(CitizenInfo __instance)
        {
            var oldAI = __instance.GetComponent<PrefabAI>();
            if (oldAI != null && oldAI is TouristAI && Utils.Settings.ExtenedTouristAI.value == true)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtenedTouristAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
            else if (oldAI != null && oldAI is ResidentAI && Utils.Settings.ExtenedResidentAI.value == true)
            {
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtenedResidentAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
            }
        }
    }
}
