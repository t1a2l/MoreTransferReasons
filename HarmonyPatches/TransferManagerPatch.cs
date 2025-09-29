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
            if (frameIndex >= 150)
            {
                __result = ExtendedTransferManager.GetExtendedFrameReason(frameIndex);
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(TransferManager), "Awake")]
        [HarmonyPostfix]
        public static void Awake(TransferManager __instance, ref TransferManager.TransferOffer[] __m_outgoingOffers, ref TransferManager.TransferOffer[] __m_incomingOffers, 
            ref ushort[] __m_outgoingCount, ref ushort[] __m_incomingCount, ref int[] __m_outgoingAmount, ref int[] __m_incomingAmount)
        {
            int newSize = 235;
            int newCountSize = (newSize + 1) * 8;
            int newOfferSize = newCountSize * 256;

            if (__m_outgoingOffers.Length < newOfferSize)
            {
                Debug.Log($"TransferManager: Resizing m_outgoingOffers from {__m_outgoingOffers.Length} to {newOfferSize}");
                __m_outgoingOffers = new TransferManager.TransferOffer[newOfferSize];
            }

            if (__m_incomingOffers.Length < newOfferSize)
            {
                Debug.Log($"TransferManager: Resizing m_incomingOffers from {__m_incomingOffers.Length} to {newOfferSize}");
                __m_incomingOffers = new TransferManager.TransferOffer[newOfferSize];
            }

            if (__m_outgoingCount.Length < newCountSize)
            {
                Debug.Log($"TransferManager: Resizing m_outgoingCount from {__m_outgoingCount.Length} to {newCountSize}");
                __m_outgoingCount = new ushort[newCountSize];
            }

            if (__m_incomingCount.Length < newCountSize)
            {
                Debug.Log($"TransferManager: Resizing m_incomingCount from {__m_incomingCount.Length} to {newCountSize}");
                __m_incomingCount = new ushort[newCountSize];
            }

            if (__m_outgoingAmount.Length < newSize)
            {
                Debug.Log($"TransferManager: Resizing m_outgoingAmount from {__m_outgoingAmount.Length} to {newSize}");
                __m_outgoingAmount = new int[newSize];
            }

            if (__m_incomingAmount.Length < newSize)
            {
                Debug.Log($"TransferManager: Resizing m_incomingAmount from {__m_incomingAmount.Length} to {newSize}");
                __m_incomingAmount = new int[newSize];
            }

        }

    }
}
