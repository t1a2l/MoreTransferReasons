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
                var oldCitizenAI = oldAI as TouristAI;
                var oldInfo = oldCitizenAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtenedTouristAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newCitizenAI = newAI as ExtenedTouristAI;
                newCitizenAI?.m_info = oldInfo;
            }
            else if (oldAI != null && oldAI is ResidentAI && Utils.Settings.ExtenedResidentAI.value == true)
            {
                var oldCitizenAI = oldAI as ResidentAI;
                var oldInfo = oldCitizenAI?.m_info;

                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtenedResidentAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newCitizenAI = newAI as ExtenedResidentAI;
                newCitizenAI?.m_info = oldInfo;
            }
        }
    }
}
