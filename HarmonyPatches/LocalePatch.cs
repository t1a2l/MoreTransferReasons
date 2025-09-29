using ColossalFramework;
using ColossalFramework.Globalization;
using HarmonyLib;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class LocalePatch
    {
        [HarmonyPatch(typeof(Locale), "Get", [typeof(string), typeof(string)], [ArgumentType.Normal, ArgumentType.Normal])]
        [HarmonyPrefix]
        public static bool Get(Locale __instance, string id, string key, ref string __result)
        {
            if(SingletonLite<LocaleManager>.exists && !string.IsNullOrEmpty(id) && id == "WAREHOUSEPANEL_RESOURCE")
            {
                var list = ExtendedTransferManager.GetExtendedTransferReasons();
                if (list.Contains(key))
                {
                    __result = ExtendedTransferManager.GetTransferReasonName(key);
                    return false;
                }
            }
            return true;
        }
    }
}
