using ColossalFramework;
using ColossalFramework.IO;
using MoreTransferReasons.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreTransferReasons
{
    public class ExtendedTransferManager : SimulationManagerBase<ExtendedTransferManager, TransferProperties>, ISimulationManager
    {
        public const ushort DataVersion = 1;

        public struct Offer
        {
            public bool Active;
            public int Amount;   
            public ushort Building;
            public uint Citizen;
            public byte m_isLocalPark;
            public InstanceID m_object;
            public Vector3 Position;
            public ushort Vehicle;
        }

        public enum TransferReason
        {
            MealsDeliveryLow = 0,
            MealsDeliveryMedium = 1,
            MealsDeliveryHigh = 2,
            FoodSupplies = 3,
            DrinkSupplies = 4,
            Bread = 5,
            CannedFish = 6,
            PoliceVanCriminalMove = 7,
            PrisonHelicopterCriminalPickup = 8,
            PrisonHelicopterCriminalMove = 9,
            MealsLow = 10,
            MealsMedium = 11,
            MealsHigh = 12,
            None = 255
        }

        public class Data : IDataContainer
        {
            public void Serialize(DataSerializer s)
            {
                LogHelper.Information("Begin Serializing ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "ExtendedTransferManager");
                s.version = DataVersion;
                ExtendedTransferManager instance = Singleton<ExtendedTransferManager>.instance;
                int num = 128;
                EncodedArray.Int integer = EncodedArray.Int.BeginWrite(s);
                for (int j = 0; j < num; j++)
                {
                    integer.Write(instance.OutgoingIndexes[j]);
                    integer.Write(instance.IncomingIndexes[j]);
                }
                integer.EndWrite();
                EncodedArray.Bool @bool = EncodedArray.Bool.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        @bool.Write(instance.OutgoingOffers[offer_material + i].Active);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        @bool.Write(instance.IncomingOffers[offer_material2 + j].Active);
                    }
                }
                @bool.EndWrite();
                EncodedArray.Byte byte2 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte2.Write((byte)instance.OutgoingOffers[offer_material + i].Amount);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte2.Write((byte)instance.IncomingOffers[offer_material2 + j].Amount);
                    }
                }
                byte2.EndWrite();
                EncodedArray.Byte byte3 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.x);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.x);
                    }
                }
                byte3.EndWrite();
                EncodedArray.Byte byte4 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.y);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.y);
                    }
                }
                byte4.EndWrite();
                EncodedArray.Byte byte5 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.z);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.z);
                    }
                }
                byte5.EndWrite();
                EncodedArray.Byte byte6 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte6.Write(instance.OutgoingOffers[offer_material + i].m_isLocalPark);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte6.Write(instance.IncomingOffers[offer_material2 + j].m_isLocalPark);
                    }
                }
                byte6.EndWrite();
                EncodedArray.Byte byte7 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte7.Write((byte)instance.OutgoingOffers[offer_material + i].m_object.Type);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte7.Write((byte)instance.IncomingOffers[offer_material2 + j].m_object.Type);
                    }
                }
                byte7.EndWrite();
                EncodedArray.UInt uInt = EncodedArray.UInt.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        uInt.Write(instance.OutgoingOffers[offer_material + i].m_object.Index);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        uInt.Write(instance.IncomingOffers[offer_material2 + j].m_object.Index);
                    }
                }
                uInt.EndWrite();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndSerialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish Serializing ExtendedTransferManager");
            }

            public void Deserialize(DataSerializer s)
            {
                LogHelper.Information("Begin Deserializing ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginDeserialize(s, "ExtendedTransferManager");
                ExtendedTransferManager instance = Singleton<ExtendedTransferManager>.instance;
                if(s.version != DataVersion)
                {
                    LogHelper.Error("ExtendedTransferManager data version mismatch. Expected: " + DataVersion + ", Actual: " + s.version);
                    return;
                }

                int num = 128;
                EncodedArray.Bool @bool = EncodedArray.Bool.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Active = @bool.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Active = @bool.Read();
                    }
                }
                @bool.EndRead();
                EncodedArray.Byte byte2 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Amount = byte2.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Amount = byte2.Read();
                    }
                }
                byte2.EndRead();
                EncodedArray.Byte byte3 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.x = byte3.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.x = byte3.Read();
                    }
                }
                byte3.EndRead();
                EncodedArray.Byte byte4 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.y = byte4.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.y = byte4.Read();
                    }
                }
                byte4.EndRead();
                EncodedArray.Byte byte5 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.z = byte5.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.z = byte5.Read();
                    }
                }
                byte5.EndRead();
                EncodedArray.Byte byte6 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_isLocalPark = byte6.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_isLocalPark = byte6.Read();
                    }
                }
                byte6.EndRead();
                EncodedArray.Byte byte7 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_object.Type = (InstanceType)byte7.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_object.Type = (InstanceType)byte7.Read();
                    }
                }
                byte7.EndRead();
                EncodedArray.UInt uInt = EncodedArray.UInt.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_object.Index = uInt.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_object.Index = uInt.Read();
                    }
                }
                uInt.EndRead();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndDeserialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish Deserializing ExtendedTransferManager");
            }

            public void AfterDeserialize(DataSerializer s)
            {
                LogHelper.Information("Begin AfterDeserialize ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginAfterDeserialize(s, "ExtendedTransferManager");
                Singleton<LoadingManager>.instance.WaitUntilEssentialScenesLoaded();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndAfterDeserialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish AfterDeserialize ExtendedTransferManager");
            }
        }

        int[] IncomingIndexes;

        int[] OutgoingIndexes;

        Offer[] OutgoingOffers;

        Offer[] IncomingOffers;

        int Transfers_Length;

        public void ClearTransferManager()
        {
            Transfers_Length = Enum.GetNames(typeof(TransferReason)).Length - 1;

            Array.Clear(OutgoingIndexes, 0, OutgoingIndexes.Length);
            Array.Clear(IncomingIndexes, 0, IncomingIndexes.Length);
            Array.Clear(OutgoingOffers, 0, OutgoingOffers.Length);
            Array.Clear(IncomingOffers, 0, IncomingOffers.Length);

            Array.Resize(ref OutgoingIndexes, Transfers_Length);
            Array.Resize(ref IncomingIndexes, Transfers_Length);
            Array.Resize(ref OutgoingOffers, Transfers_Length * 256);
            Array.Resize(ref IncomingOffers, Transfers_Length * 256);
        }

        protected override void Awake()
        {
            base.Awake();
            Transfers_Length = Enum.GetNames(typeof(TransferReason)).Length - 1;
            OutgoingIndexes = new int[Transfers_Length];
            IncomingIndexes = new int[Transfers_Length];
            OutgoingOffers = new Offer[Transfers_Length * 256];
            IncomingOffers = new Offer[Transfers_Length * 256];
        }

        public static TransferReason GetFrameReason(int frameIndex)
        {
            return frameIndex switch
            {
                1 => TransferReason.MealsDeliveryLow,
                3 => TransferReason.MealsDeliveryMedium,
                5 => TransferReason.MealsDeliveryHigh,
                7 => TransferReason.FoodSupplies,
                9 => TransferReason.DrinkSupplies,
                11 => TransferReason.Bread,
                13 => TransferReason.CannedFish,
                15 => TransferReason.PoliceVanCriminalMove,
                17 => TransferReason.PrisonHelicopterCriminalPickup,
                19 => TransferReason.PrisonHelicopterCriminalMove,
                21 => TransferReason.MealsLow,
                23 => TransferReason.MealsMedium,
                25 => TransferReason.MealsHigh,
                _ => TransferReason.None
            };
        }

        public void AddOutgoingOffer(TransferReason material, Offer offer)
        {
            int index = OutgoingIndexes[(int)material];
            if (index < 256)
            {
                OutgoingIndexes[(int)material] = index + 1;
                int FreeIndex = (int)material * 256 + index;
                OutgoingOffers[FreeIndex] = offer;
            }
        }

        public void AddIncomingOffer(TransferReason material, Offer offer)
        {
            int index = IncomingIndexes[(int)material];
            if (index < 256)
            {
                IncomingIndexes[(int)material] = index + 1;
                int FreeIndex = (int)material * 256 + index;
                IncomingOffers[FreeIndex] = offer;
            }
        }

        public void RemoveOutgoingOffer(TransferReason material, Offer offer)
        {
            int index = OutgoingIndexes[(int)material];
            for (int num3 = index - 1; num3 >= 0; num3--)
            {
                int material_index = (int)material * 256 + index;
                if (OutgoingOffers[material_index].m_object == offer.m_object && OutgoingOffers[material_index].m_isLocalPark == offer.m_isLocalPark)
                {
                    OutgoingOffers[(int)material].Amount -= OutgoingOffers[material_index].Amount;
                    int num5 = (int)material * 256 + --index;
                    ref Offer reference = ref OutgoingOffers[material_index];
                    reference = OutgoingOffers[num5];
                }
            }
            OutgoingIndexes[(int)material] = (ushort)index;
        }

        public void RemoveIncomingOffer(TransferReason material, Offer offer)
        {
            int index = IncomingIndexes[(int)material];
            for (int num3 = index - 1; num3 >= 0; num3--)
            {
                int material_index = (int)material * 256 + index;
                if (IncomingOffers[material_index].m_object == offer.m_object && IncomingOffers[material_index].m_isLocalPark == offer.m_isLocalPark)
                {
                    IncomingOffers[(int)material].Amount -= IncomingOffers[material_index].Amount;
                    int num5 = (int)material * 256 + --index;
                    ref Offer reference = ref IncomingOffers[material_index];
                    reference = IncomingOffers[num5];
                }
            }
            IncomingIndexes[(int)material] = (ushort)index;
        }

        protected override void SimulationStepImpl(int subStep)
        {
            if (subStep != 0)
            {
                int frameIndex = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 0xFF);
                MatchOffers(GetFrameReason(frameIndex));
            }
        }

        private void MatchOffers(TransferReason material)
        {
            if (material == TransferReason.None)
            {
                return;
            }
            int outgoing_matched_count = 0;
            int incoming_matched_count = 0;
            int outgoing_ocuppied_count = OutgoingIndexes[(int)material];
            int incoming_ocuppied_count = IncomingIndexes[(int)material];
            while (outgoing_matched_count < outgoing_ocuppied_count || incoming_matched_count < incoming_ocuppied_count)
            {
                if (outgoing_matched_count < outgoing_ocuppied_count)
                {
                    Offer outgoing_offer = OutgoingOffers[(int)material * 256 + outgoing_matched_count];
                    Vector3 outgoing_position = outgoing_offer.Position;
                    int outgoing_amount = outgoing_offer.Amount;
                    while (outgoing_amount != 0)
                    {
                        int chosen_index = -1;
                        double min_distance = Math.Pow(65000.0, 2.0);
                        for (int i = 0; i < incoming_ocuppied_count; i++)
                        {
                            Offer incoming_offer = IncomingOffers[(int)material * 256 + i];
                            if (outgoing_offer.Building == incoming_offer.Building)
                            {
                                continue;
                            }
                            double distance = Vector3.SqrMagnitude(incoming_offer.Position - outgoing_position);
                            if (distance < min_distance)
                            {
                                chosen_index = i;
                                min_distance = distance;
                                if (distance < 0.0)
                                {
                                    break;
                                }
                            }
                        }
                        if (chosen_index == -1)
                        {
                            break;
                        }
                        Offer chosen_incoming_offer = IncomingOffers[(int)material * 256 + chosen_index];
                        int incoming_amount = chosen_incoming_offer.Amount;
                        int min_amount = Mathf.Min(outgoing_amount, incoming_amount);
                        if (min_amount != 0)
                        {
                            StartTransfer(material, chosen_incoming_offer, outgoing_offer, min_amount);
                        }
                        outgoing_amount -= min_amount;
                        incoming_amount -= min_amount;
                        if (incoming_amount == 0)
                        {
                            int new_index = IncomingIndexes[(int)material] - 1;
                            IncomingIndexes[(int)material] = new_index;
                            ref Offer reference = ref IncomingOffers[(int)material * 256 + chosen_index];
                            reference = IncomingOffers[(int)material * 256 + new_index];
                            incoming_ocuppied_count = new_index;
                        }
                        else
                        {
                            chosen_incoming_offer.Amount = incoming_amount;
                            IncomingOffers[(int)material * 256 + chosen_index] = chosen_incoming_offer;
                        }
                        outgoing_offer.Amount = outgoing_amount;
                    }
                    if (outgoing_amount == 0)
                    {
                        outgoing_ocuppied_count--;
                        OutgoingIndexes[(int)material] = outgoing_ocuppied_count;
                        ref Offer reference2 = ref OutgoingOffers[(int)material * 256 + outgoing_matched_count];
                        reference2 = OutgoingOffers[(int)material * 256 + outgoing_ocuppied_count];
                    }
                    else
                    {
                        outgoing_offer.Amount = outgoing_amount;
                        OutgoingOffers[(int)material * 256 + outgoing_matched_count] = outgoing_offer;
                        outgoing_matched_count++;
                    }
                }
                if (incoming_matched_count >= incoming_ocuppied_count)
                {
                    continue;
                }
                Offer incoming_offer2 = IncomingOffers[(int)material * 256 + incoming_matched_count];
                Vector3 incoming_position = incoming_offer2.Position;
                int incoming_amount2 = incoming_offer2.Amount;
                while (incoming_amount2 != 0)
                {
                    int chosen_index2 = -1;
                    float min_distance2 = -1f;
                    for (int j = 0; j < outgoing_ocuppied_count; j++)
                    {
                        Offer outgoing_offer2 = OutgoingOffers[(int)material * 256 + j];
                        if (incoming_offer2.Building == outgoing_offer2.Building)
                        {
                            continue;
                        }
                        float distance2 = Vector3.SqrMagnitude(outgoing_offer2.Position - incoming_position);
                        if (distance2 > min_distance2)
                        {
                            chosen_index2 = j;
                            min_distance2 = distance2;
                            if (distance2 < 0f)
                            {
                                break;
                            }
                        }
                    }
                    if (chosen_index2 == -1)
                    {
                        break;
                    }
                    Offer chosen_outgoing_offer = OutgoingOffers[(int)material * 256 + chosen_index2];
                    int outgoing_amount2 = chosen_outgoing_offer.Amount;
                    int min_amount2 = Mathf.Min(outgoing_amount2, incoming_amount2);
                    if (min_amount2 != 0)
                    {
                        StartTransfer(material, chosen_outgoing_offer, incoming_offer2, min_amount2);
                    }
                    incoming_amount2 -= min_amount2;
                    outgoing_amount2 -= min_amount2;
                    if (outgoing_amount2 == 0)
                    {
                        int new_index2 = OutgoingIndexes[(int)material] - 1;
                        OutgoingIndexes[(int)material] = new_index2;
                        ref Offer reference3 = ref OutgoingOffers[(int)material * 256 + chosen_index2];
                        reference3 = OutgoingOffers[(int)material * 256 + new_index2];
                        outgoing_ocuppied_count = new_index2;
                    }
                    else
                    {
                        chosen_outgoing_offer.Amount = outgoing_amount2;
                        OutgoingOffers[(int)material * 256 + chosen_index2] = chosen_outgoing_offer;
                    }
                    incoming_offer2.Amount = incoming_amount2;
                }
                if (incoming_amount2 == 0)
                {
                    incoming_ocuppied_count--;
                    IncomingIndexes[(int)material] = incoming_ocuppied_count;
                    ref Offer reference4 = ref IncomingOffers[(int)material * 256 + incoming_matched_count];
                    reference4 = IncomingOffers[(int)material * 256 + incoming_ocuppied_count];
                }
                else
                {
                    incoming_offer2.Amount = incoming_amount2;
                    IncomingOffers[(int)material * 256 + incoming_matched_count] = incoming_offer2;
                    incoming_matched_count++;
                }
            }
            OutgoingIndexes[(int)material] = 0;
            IncomingIndexes[(int)material] = 0;
        }

        private void StartTransfer(TransferReason material, Offer offerOut, Offer offerIn, int delta)
        {
            bool active = offerIn.Active;
            bool active2 = offerOut.Active;
            if (active && offerIn.Vehicle != 0)
            {
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle = offerIn.Vehicle;
                offerOut.Amount = delta;
                VehicleInfo info = vehicles.m_buffer[vehicle].Info;
                if (info.m_vehicleAI is IExtendedVehicleAI extendedVehicleAI)
                {
                    extendedVehicleAI.ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], material, offerOut);
                }
            }
            else if (active2 && offerOut.Vehicle != 0)
            {
                Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle2 = offerOut.Vehicle;
                offerIn.Amount = delta;
                VehicleInfo info2 = vehicles2.m_buffer[vehicle2].Info;
                if (info2.m_vehicleAI is IExtendedVehicleAI extendedVehicleAI)
                {
                    extendedVehicleAI.ExtendedStartTransfer(vehicle2, ref vehicles2.m_buffer[vehicle2], material, offerIn);
                }
            }
            else if (active && offerIn.Citizen != 0U)
            {
                Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen = offerIn.Citizen;
                offerOut.Amount = delta;
                CitizenInfo citizenInfo = citizens.m_buffer[(int)(UIntPtr)citizen].GetCitizenInfo(citizen);
                if (citizenInfo != null)
                {
                    if (citizenInfo.m_citizenAI is IExtendedCitizenAI extendedCitizenAI)
                    {
                        extendedCitizenAI.ExtendedStartTransfer(citizen, ref citizens.m_buffer[(int)(UIntPtr)citizen], material, offerOut);
                    }
                }
            }
            else if (active2 && offerOut.Citizen != 0U)
            {
                Array32<Citizen> citizens2 = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen2 = offerOut.Citizen;
                offerIn.Amount = delta;
                CitizenInfo citizenInfo2 = citizens2.m_buffer[(int)(UIntPtr)citizen2].GetCitizenInfo(citizen2);
                if (citizenInfo2 != null)
                {
                    if (citizenInfo2.m_citizenAI is IExtendedCitizenAI extendedCitizenAI)
                    {
                        extendedCitizenAI.ExtendedStartTransfer(citizen2, ref citizens2.m_buffer[(int)(UIntPtr)citizen2], material, offerIn);
                    }
                }
            }
            else if (active2 && offerOut.Building != 0)
            {
                Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
                offerIn.Amount = delta;
                if (offerOut.m_isLocalPark != 0 && offerOut.m_isLocalPark == offerIn.m_isLocalPark)
                {
                    StartDistrictTransfer(material, offerOut, offerIn);
                }
                else
                {
                    ushort building = offerOut.Building;
                    BuildingInfo info3 = buildings.m_buffer[building].Info;
                    if (info3.m_buildingAI is IExtendedBuildingAI extendedBuildingAI)
                    {
                        extendedBuildingAI.ExtendedStartTransfer(building, ref buildings.m_buffer[building], material, offerIn);
                    }
                }
            }
            else if (active && offerIn.Building != 0)
            {
                Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
                offerOut.Amount = delta;
                if (offerIn.m_isLocalPark != 0 && offerIn.m_isLocalPark == offerOut.m_isLocalPark)
                {
                    StartDistrictTransfer(material, offerOut, offerIn);
                }
                else
                {
                    ushort building2 = offerIn.Building;
                    BuildingInfo info4 = buildings2.m_buffer[building2].Info;
                    if (info4.m_buildingAI is IExtendedBuildingAI extendedBuildingAI)
                    {
                        extendedBuildingAI.ExtendedStartTransfer(building2, ref buildings2.m_buffer[building2], material, offerOut);
                    }
                }
            }
        }

        private void StartDistrictTransfer(TransferReason material, Offer offerOut, Offer offerIn)
        {
            Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
            ushort building = offerOut.Building;
            ushort building2 = offerIn.Building;
            BuildingInfo info = buildings.m_buffer[building].Info;
            BuildingInfo info2 = buildings.m_buffer[building2].Info;
            if (info.m_buildingAI is IExtendedBuildingAI extendedBuildingAI && info2.m_buildingAI is IExtendedBuildingAI extendedBuildingAI2)
            {
                extendedBuildingAI.ExtendedGetMaterialAmount(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, out int num, out int num2);
                extendedBuildingAI2.ExtendedGetMaterialAmount(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, out int num3, out int num4);
                int num5 = Math.Min(num, num4 - num3);
                if (num5 > 0)
                {
                    num = -num5;
                    num3 = num5;
                    extendedBuildingAI.ExtendedModifyMaterialBuffer(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, ref num);
                    extendedBuildingAI2.ExtendedModifyMaterialBuffer(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, ref num3);
                }
            }
        }

        public List<TransferReason> GetExtendedTransferReason(ushort buildingId)
        {
            List<TransferReason> transferReasons = [];
            var building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId];
            var buildingAI = building.Info.GetAI();

            if(buildingAI.GetType().Name.Equals("PrisonCopterPoliceStationAI") && building.Info.m_class.m_level < ItemClass.Level.Level4 && (building.m_flags & Building.Flags.Downgrading) != 0)
            {
                transferReasons.Add(TransferReason.PoliceVanCriminalMove);
            }
            if (buildingAI.GetType().Name.Equals("PoliceHelicopterDepotAI") && (building.m_flags & Building.Flags.Downgrading) != 0)
            {
                transferReasons.Add(TransferReason.PrisonHelicopterCriminalPickup);
            }
            return transferReasons;
        }

    }
}
