using System.Collections.Generic;
using System.Reflection.Emit;
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
            int newSize = ExtendedTransferManager.TransferReasonCount;
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

        // Target the nested Data class within TransferManager
        [HarmonyPatch(typeof(TransferManager.Data), "Serialize")]
        [HarmonyTranspiler] // Patch the Serialize method
        public static IEnumerable<CodeInstruction> SerializeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            // The value 128 is a hardcoded integer constant.
            int targetValue = 128;
            int replacementValue = ExtendedTransferManager.TransferReasonCount;

            // This flag helps us ensure we only replace the specific instruction we need.
            bool replaced = false;

            for (int i = 0; i < codes.Count; i++)
            {
                // The IL for 'int num = 128;' is 'ldc.i4.s 128' or similar.
                // OpCodes.Ldc_I4_S is 'Load integer constant (short form)'

                if (codes[i].opcode == OpCodes.Ldc_I4_S && codes[i].operand is sbyte && (sbyte)codes[i].operand == targetValue)
                {
                    // This instruction is where '128' is loaded.
                    // Replace the operand (the value being loaded) with your custom size.

                    // IMPORTANT: Since your new value (e.g., 138) might be greater than 127,
                    // you must change the opcode from Ldc_I4_S (short) to Ldc_I4 (long).
                    // Or simply use Ldc_I4, which can handle any integer.

                    codes[i].opcode = OpCodes.Ldc_I4;
                    codes[i].operand = replacementValue;
                    replaced = true;

                    // For debugging:
                    Debug.Log($"TransferManagerDataSerializePatch: Replaced IL constant 128 with {replacementValue} at instruction {i}");
                }
                // Check for the OpCodes.Ldc_I4 instruction as well, in case the compiler uses it directly for 128
                else if (codes[i].opcode == OpCodes.Ldc_I4 && codes[i].operand is int && (int)codes[i].operand == targetValue)
                {
                    codes[i].operand = replacementValue;
                    replaced = true;
                    Debug.Log($"TransferManagerDataSerializePatch: Replaced IL constant 128 (Ldc_I4) with {replacementValue} at instruction {i}");
                }
            }

            if (!replaced)
            {
                // This is critical for error checking during development
                Debug.LogError("TransferManagerDataSerializePatch failed: Could not find hardcoded value 128 to replace.");
            }

            return codes;
        }

        // Target the nested Data class within TransferManager
        [HarmonyPatch(typeof(TransferManager.Data), "Deserialize")]
        [HarmonyTranspiler] // Patch the Serialize method
        public static IEnumerable<CodeInstruction> DeserializeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            int replacementValue = ExtendedTransferManager.TransferReasonCount;

            // List of all vanilla constants that need to be replaced
            // (128, 64, 72, 80, 88, 96)
            HashSet<int> vanillaCounts = [128, 64, 72, 80, 88, 96];

            // This count helps us ensure we only replace the correct instance of 128.
            int replacementsMade = 0;

            for (int i = 0; i < codes.Count; i++)
            {
                CodeInstruction instruction = codes[i];

                // We check for instructions that load an integer constant (Ldc_I4, Ldc_I4_S)
                // and see if the value is one of the vanilla transfer reason counts.
                int? loadedValue = GetLoadedConstant(instruction);

                if (loadedValue.HasValue && vanillaCounts.Contains(loadedValue.Value))
                {
                    // This instruction is loading a vanilla transfer reason count. Replace it.
                    instruction.opcode = OpCodes.Ldc_I4; // Ensure the opcode can handle the new value (e.g., 138)
                    instruction.operand = replacementValue;
                    replacementsMade++;
                }
            }

            // The minimum number of replacements should be 6, but we'll check against a lower bound
            // to ensure the primary 128 value was found.
            if (replacementsMade < 1)
            {
                Debug.LogError("TransferManagerDataDeserializePatch failed: Could not find required constant values (128, 64, 72, 80, 88, or 96) to replace.");
            }

            return codes;
        }

        /// <summary>
        /// Helper to determine the integer value loaded by a CodeInstruction.
        /// </summary>
        private static int? GetLoadedConstant(CodeInstruction instruction)
        {
            // Check for Ldc_I4_S (short form load)
            if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand is sbyte sbyteOperand)
                return sbyteOperand;

            // Check for Ldc_I4 (long form load)
            if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand is int intOperand)
                return intOperand;

            // Check for specific single-byte opcodes (Ldc_I4_0 to Ldc_I4_8 and Ldc_I4_M1)
            if (instruction.opcode == OpCodes.Ldc_I4_M1) return -1;
            if (instruction.opcode == OpCodes.Ldc_I4_0) return 0;
            if (instruction.opcode == OpCodes.Ldc_I4_1) return 1;
            if (instruction.opcode == OpCodes.Ldc_I4_2) return 2;
            if (instruction.opcode == OpCodes.Ldc_I4_3) return 3;
            if (instruction.opcode == OpCodes.Ldc_I4_4) return 4;
            if (instruction.opcode == OpCodes.Ldc_I4_5) return 5;
            if (instruction.opcode == OpCodes.Ldc_I4_6) return 6;
            if (instruction.opcode == OpCodes.Ldc_I4_7) return 7;
            if (instruction.opcode == OpCodes.Ldc_I4_8) return 8;

            return null;
        }


    }
}
