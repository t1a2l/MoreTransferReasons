using HarmonyLib;
using MoreTransferReasons.AI;
using MoreTransferReasons.Utils;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(VehicleInfo), "InitializePrefab")]
    public static class InitializePrefabVehiclePatch
    {
        public static void Prefix(VehicleInfo __instance)
        {
            var oldAI = __instance.GetComponent<PrefabAI>();
            if (oldAI != null && oldAI is CargoTruckAI && !__instance.name.Contains("Trailer") && Utils.Settings.ExtendedCargoTruckAI.value)
            {
                UnityEngine.Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)__instance.gameObject.AddComponent<ExtendedCargoTruckAI>();
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);

                var newVehicleAI = newAI as ExtendedCargoTruckAI;
                newVehicleAI?.m_info = __instance;
            }
        }
    }
}