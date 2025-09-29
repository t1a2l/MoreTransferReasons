using HarmonyLib;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class TransferManagerPatch
    {
        [HarmonyPatch(typeof(TransferManager), "GetFrameReason")]
        [HarmonyPrefix]
        public static bool GetFrameReason(TransferManager __instance, int frameIndex, ref TransferManager.TransferReason __result)
        {
            if (frameIndex >= 150)
            {
                __result = ExtendedTransferManager.GetExtendedFrameReason(frameIndex);
                return false;
            }
            return true;
        }

    }
}
