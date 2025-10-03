using HarmonyLib;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class TransferManagerPatch
    {
        [HarmonyPatch(typeof(TransferManager), "GetFrameReason")]
        [HarmonyPrefix]
        public static bool GetFrameReason(TransferManager __instance, int frameIndex, ref TransferManager.TransferReason __result)
        {
            if (frameIndex % 2 == 0)
            {
                __result = ExtendedTransferManager.GetExtendedFrameReason(frameIndex);
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(TransferManager), "Awake")]
        [HarmonyPostfix]
        public static void Awake(TransferManager __instance, ref TransferManager.TransferOffer[] ___m_outgoingOffers, ref TransferManager.TransferOffer[] ___m_incomingOffers,
            ref ushort[] ___m_outgoingCount, ref ushort[] ___m_incomingCount, ref int[] ___m_outgoingAmount, ref int[] ___m_incomingAmount)
        {
            int newSize = ExtendedTransferManager.TransferReasonCount;
            int newCountSize = ExtendedTransferManager.TransferReasonCount * 8;
            int newOfferSize = newCountSize * 256;

            if (___m_outgoingOffers.Length < newOfferSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_outgoingOffers from {___m_outgoingOffers.Length} to {newOfferSize}");
                ___m_outgoingOffers = new TransferManager.TransferOffer[newOfferSize];
            }

            if (___m_incomingOffers.Length < newOfferSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_incomingOffers from {___m_incomingOffers.Length} to {newOfferSize}");
                ___m_incomingOffers = new TransferManager.TransferOffer[newOfferSize];
            }

            if (___m_outgoingCount.Length < newCountSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_outgoingCount from {___m_outgoingCount.Length} to {newCountSize}");
                ___m_outgoingCount = new ushort[newCountSize];
            }

            if (___m_incomingCount.Length < newCountSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_incomingCount from {___m_incomingCount.Length} to {newCountSize}");
                ___m_incomingCount = new ushort[newCountSize];
            }

            if (___m_outgoingAmount.Length < newSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_outgoingAmount from {___m_outgoingAmount.Length} to {newSize}");
                ___m_outgoingAmount = new int[newSize];
            }

            if (___m_incomingAmount.Length < newSize)
            {
                Debug.Log($"ExtendedTransferManager: Resizing m_incomingAmount from {___m_incomingAmount.Length} to {newSize}");
                ___m_incomingAmount = new int[newSize];
            }
        }

    }
}
