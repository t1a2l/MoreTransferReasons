using System;
using System.Reflection;
using ColossalFramework;
using UnityEngine;

namespace MoreTransferReasons.Serializer
{
    public class ExtendedTransferManagerSerializer
    {
        // Some magic values to check we are line up correctly on the tuple boundaries
        private const uint uiTUPLE_START = 0xFEFEFEFE;
        private const uint uiTUPLE_END = 0xFAFAFAFA;

        private const ushort iEXTENDED_TRANSFER_MANAGER_DATA_VERSION = 1;

        public static void SaveData(FastList<byte> Data)
        {
            TransferManager instance = Singleton<TransferManager>.instance;

            var m_outgoingOffers = (TransferManager.TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingOffers = (TransferManager.TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);

            // Write out metadata
            StorageData.WriteUInt16(iEXTENDED_TRANSFER_MANAGER_DATA_VERSION, Data);

            int num = ExtendedTransferManager.TransferReasonCount;

            StorageData.WriteUInt32(uiTUPLE_START, Data);

            for (int i = 150; i < num; i++)
            {
                StorageData.WriteInt32(m_incomingAmount[i], Data);
                StorageData.WriteInt32(m_outgoingAmount[i], Data);
            }

            for (int j = 150; j < num; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    int num2 = j * 8 + k;
                    StorageData.WriteUInt16(m_incomingCount[num2], Data);
                }
                for (int l = 0; l < 8; l++)
                {
                    int num3 = j * 8 + l;
                    StorageData.WriteUInt16(m_outgoingCount[num3], Data);
                }
            }

            for (int m = 150; m < num; m++)
            {
                for (int n = 0; n < 8; n++)
                {
                    int num4 = m * 8 + n;
                    uint num5 = m_incomingCount[num4];
                    num4 *= 256;
                    for (uint num6 = 0u; num6 < num5; num6++)
                    {
                        StorageData.WriteBool(m_incomingOffers[num4 + num6].Active, Data);
                    }
                }
                for (int num7 = 0; num7 < 8; num7++)
                {
                    int num8 = m * 8 + num7;
                    uint num9 = m_outgoingCount[num8];
                    num8 *= 256;
                    for (uint num10 = 0u; num10 < num9; num10++)
                    {
                        StorageData.WriteBool(m_outgoingOffers[num8 + num10].Active, Data);
                    }
                }
            }

            for (int num11 = 150; num11 < num; num11++)
            {
                for (int num12 = 0; num12 < 8; num12++)
                {
                    int num13 = num11 * 8 + num12;
                    uint num14 = m_incomingCount[num13];
                    num13 *= 256;
                    for (uint num15 = 0u; num15 < num14; num15++)
                    {
                        StorageData.WriteBool(m_incomingOffers[num13 + num15].Exclude, Data);
                    }
                }
                for (int num16 = 0; num16 < 8; num16++)
                {
                    int num17 = num11 * 8 + num16;
                    uint num18 = m_outgoingCount[num17];
                    num17 *= 256;
                    for (uint num19 = 0u; num19 < num18; num19++)
                    {
                        StorageData.WriteBool(m_outgoingOffers[num17 + num19].Exclude, Data);
                    }
                }
            }

            for (int num20 = 150; num20 < num; num20++)
            {
                for (int num21 = 0; num21 < 8; num21++)
                {
                    int num22 = num20 * 8 + num21;
                    uint num23 = m_incomingCount[num22];
                    num22 *= 256;
                    for (uint num24 = 0u; num24 < num23; num24++)
                    {
                        StorageData.WriteInt32((byte)m_incomingOffers[num22 + num24].Priority, Data);
                    }
                }
                for (int num25 = 0; num25 < 8; num25++)
                {
                    int num26 = num20 * 8 + num25;
                    uint num27 = m_outgoingCount[num26];
                    num26 *= 256;
                    for (uint num28 = 0u; num28 < num27; num28++)
                    {
                        StorageData.WriteInt32((byte)m_outgoingOffers[num26 + num28].Priority, Data);
                    }
                }
            }

            for (int num29 = 150; num29 < num; num29++)
            {
                for (int num30 = 0; num30 < 8; num30++)
                {
                    int num31 = num29 * 8 + num30;
                    uint num32 = m_incomingCount[num31];
                    num31 *= 256;
                    for (uint num33 = 0u; num33 < num32; num33++)
                    {
                        StorageData.WriteInt32((byte)m_incomingOffers[num31 + num33].Amount, Data);
                    }
                }
                for (int num34 = 0; num34 < 8; num34++)
                {
                    int num35 = num29 * 8 + num34;
                    uint num36 = m_outgoingCount[num35];
                    num35 *= 256;
                    for (uint num37 = 0u; num37 < num36; num37++)
                    {
                        StorageData.WriteInt32((byte)m_outgoingOffers[num35 + num37].Amount, Data);
                    }
                }
            }

            for (int num38 = 150; num38 < num; num38++)
            {
                for (int num39 = 0; num39 < 8; num39++)
                {
                    int num40 = num38 * 8 + num39;
                    uint num41 = m_incomingCount[num40];
                    num40 *= 256;
                    for (uint num42 = 0u; num42 < num41; num42++)
                    {
                        StorageData.WriteInt32((byte)m_incomingOffers[num40 + num42].PositionX, Data);
                    }
                }
                for (int num43 = 0; num43 < 8; num43++)
                {
                    int num44 = num38 * 8 + num43;
                    uint num45 = m_outgoingCount[num44];
                    num44 *= 256;
                    for (uint num46 = 0u; num46 < num45; num46++)
                    {
                        StorageData.WriteInt32((byte)m_outgoingOffers[num44 + num46].PositionX, Data);
                    }
                }
            }

            for (int num47 = 150; num47 < num; num47++)
            {
                for (int num48 = 0; num48 < 8; num48++)
                {
                    int num49 = num47 * 8 + num48;
                    uint num50 = m_incomingCount[num49];
                    num49 *= 256;
                    for (uint num51 = 0u; num51 < num50; num51++)
                    {
                        StorageData.WriteInt32((byte)m_incomingOffers[num49 + num51].PositionZ, Data);
                    }
                }
                for (int num52 = 0; num52 < 8; num52++)
                {
                    int num53 = num47 * 8 + num52;
                    uint num54 = m_outgoingCount[num53];
                    num53 *= 256;
                    for (uint num55 = 0u; num55 < num54; num55++)
                    {
                        StorageData.WriteInt32((byte)m_outgoingOffers[num53 + num55].PositionZ, Data);
                    }
                }
            }

            for (int num56 = 150; num56 < num; num56++)
            {
                for (int num57 = 0; num57 < 8; num57++)
                {
                    int num58 = num56 * 8 + num57;
                    uint num59 = m_incomingCount[num58];
                    num58 *= 256;
                    for (uint num60 = 0u; num60 < num59; num60++)
                    {
                        StorageData.WriteByte((byte)m_incomingOffers[num58 + num60].m_object.Type, Data);
                    }
                }
                for (int num61 = 0; num61 < 8; num61++)
                {
                    int num62 = num56 * 8 + num61;
                    uint num63 = m_outgoingCount[num62];
                    num62 *= 256;
                    for (uint num64 = 0u; num64 < num63; num64++)
                    {
                        StorageData.WriteByte((byte)m_outgoingOffers[num62 + num64].m_object.Type, Data);
                    }
                }
            }

            for (int num65 = 150; num65 < num; num65++)
            {
                for (int num66 = 0; num66 < 8; num66++)
                {
                    int num67 = num65 * 8 + num66;
                    uint num68 = m_incomingCount[num67];
                    num67 *= 256;
                    for (uint num69 = 0u; num69 < num68; num69++)
                    {
                        StorageData.WriteUInt32(m_incomingOffers[num67 + num69].m_object.Index, Data);
                    }
                }
                for (int num70 = 0; num70 < 8; num70++)
                {
                    int num71 = num65 * 8 + num70;
                    uint num72 = m_outgoingCount[num71];
                    num71 *= 256;
                    for (uint num73 = 0u; num73 < num72; num73++)
                    {
                        StorageData.WriteUInt32(m_outgoingOffers[num71 + num73].m_object.Index, Data);
                    }
                }
            }

            for (int num74 = 150; num74 < num; num74++)
            {
                for (int num75 = 0; num75 < 8; num75++)
                {
                    int num76 = num74 * 8 + num75;
                    uint num77 = m_incomingCount[num76];
                    num76 *= 256;
                    for (uint num78 = 0u; num78 < num77; num78++)
                    {
                        StorageData.WriteByte(m_incomingOffers[num76 + num78].m_isLocalPark, Data);
                    }
                }
                for (int num79 = 0; num79 < 8; num79++)
                {
                    int num80 = num74 * 8 + num79;
                    uint num81 = m_outgoingCount[num80];
                    num80 *= 256;
                    for (uint num82 = 0u; num82 < num81; num82++)
                    {
                        StorageData.WriteByte(m_outgoingOffers[num80 + num82].m_isLocalPark, Data);
                    }
                }
            }

            StorageData.WriteUInt32(uiTUPLE_END, Data);
        }

        public static void LoadData(int iGlobalVersion, byte[] Data, ref int iIndex)
        {
            if (Data != null && Data.Length > iIndex)
            {
                int iExtendedTransferManagerVersion = StorageData.ReadUInt16(Data, ref iIndex);

                Debug.Log("MoreTransferReasons ExtendedTransferManager - Global: " + iGlobalVersion + " BufferVersion: " + iExtendedTransferManagerVersion + " DataLength: " + Data.Length + " Index: " + iIndex);

                TransferManager instance = Singleton<TransferManager>.instance;

                var m_outgoingOffers = (TransferManager.TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
                var m_incomingOffers = (TransferManager.TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
                var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
                var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
                var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
                var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);

                //Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers {m_outgoingOffers.Length}");
                //Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers {m_incomingOffers.Length}");
                //Debug.Log($"LoadData ExtendedTransferManager: m_outgoingCount {m_outgoingCount.Length}");
                //Debug.Log($"LoadData ExtendedTransferManager: m_incomingCount {m_incomingCount.Length}");
                //Debug.Log($"LoadData ExtendedTransferManager: m_outgoingAmount {m_outgoingAmount.Length}");
                //Debug.Log($"LoadData ExtendedTransferManager: m_incomingAmount {m_incomingAmount.Length}");

                int num = ExtendedTransferManager.TransferReasonCount;

                CheckStartTuple($"Buffer", iExtendedTransferManagerVersion, Data, ref iIndex);

                for (int i = 150; i < num; i++)
                {
                    m_incomingAmount[i] = StorageData.ReadInt32(Data, ref iIndex);
                    // Debug.Log($"LoadData ExtendedTransferManager: m_incomingAmount[{i}] - {m_incomingAmount[i]}");

                    m_outgoingAmount[i] = StorageData.ReadInt32(Data, ref iIndex);
                    // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingAmount[{i}] - {m_outgoingAmount[i]}");
                }
                for (int j = num; j < ExtendedTransferManager.TransferReasonCount; j++)
                {
                    m_incomingAmount[j] = 0;
                    m_outgoingAmount[j] = 0;
                }

                for (int k = 150; k < num; k++)
                {
                    for (int l = 0; l < 8; l++)
                    {
                        int num2 = k * 8 + l;
                        m_incomingCount[num2] = StorageData.ReadUInt16(Data, ref iIndex);
                        // Debug.Log($"LoadData ExtendedTransferManager: m_incomingCount[{num2}] - {m_incomingCount[num2]}");
                    }
                    for (int m = 0; m < 8; m++)
                    {
                        int num3 = k * 8 + m;
                        m_outgoingCount[num3] = StorageData.ReadUInt16(Data, ref iIndex);
                        // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingCount[{num3}] - {m_outgoingCount[num3]}");
                    }
                }
                for (int n = num; n < ExtendedTransferManager.TransferReasonCount; n++)
                {
                    for (int num4 = 0; num4 < 8; num4++)
                    {
                        int num5 = n * 8 + num4;
                        m_incomingCount[num5] = 0;
                    }
                    for (int num6 = 0; num6 < 8; num6++)
                    {
                        int num7 = n * 8 + num6;
                        m_outgoingCount[num7] = 0;
                    }
                }

                for (int num8 = 150; num8 < num; num8++)
                {
                    for (int num9 = 0; num9 < 8; num9++)
                    {
                        int num10 = num8 * 8 + num9;
                        uint num11 = m_incomingCount[num10];
                        num10 *= 256;
                        for (uint num12 = 0u; num12 < num11; num12++)
                        {
                            m_incomingOffers[num10 + num12].Active = StorageData.ReadBool(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num10 + num12}].Active - {m_incomingOffers[num10 + num12].Active}");
                        }
                    }
                    for (int num13 = 0; num13 < 8; num13++)
                    {
                        int num14 = num8 * 8 + num13;
                        uint num15 = m_outgoingCount[num14];
                        num14 *= 256;
                        for (uint num16 = 0u; num16 < num15; num16++)
                        {
                            m_outgoingOffers[num14 + num16].Active = StorageData.ReadBool(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num14 + num16}].Active - {m_outgoingOffers[num14 + num16].Active}");
                        }
                    }
                }

                for (int num17 = 150; num17 < num; num17++)
                {
                    for (int num18 = 0; num18 < 8; num18++)
                    {
                        int num19 = num17 * 8 + num18;
                        uint num20 = m_incomingCount[num19];
                        num19 *= 256;
                        for (uint num21 = 0u; num21 < num20; num21++)
                        {
                            m_incomingOffers[num19 + num21].Exclude = StorageData.ReadBool(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num19 + num21}].Exclude - {m_incomingOffers[num19 + num21].Exclude}");
                        }
                    }
                    for (int num22 = 0; num22 < 8; num22++)
                    {
                        int num23 = num17 * 8 + num22;
                        uint num24 = m_outgoingCount[num23];
                        num23 *= 256;
                        for (uint num25 = 0u; num25 < num24; num25++)
                        {
                            m_outgoingOffers[num23 + num25].Exclude = StorageData.ReadBool(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num23 + num25}].Exclude - {m_outgoingOffers[num23 + num25].Exclude}");
                        }
                    }
                }

                for (int num35 = 150; num35 < num; num35++)
                {
                    for (int num36 = 0; num36 < 8; num36++)
                    {
                        int num37 = num35 * 8 + num36;
                        uint num38 = m_incomingCount[num37];
                        num37 *= 256;
                        for (uint num39 = 0u; num39 < num38; num39++)
                        {
                            m_incomingOffers[num37 + num39].Priority = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num37 + num39}].Priority - {m_incomingOffers[num37 + num39].Priority}");
                        }
                    }
                    for (int num40 = 0; num40 < 8; num40++)
                    {
                        int num41 = num35 * 8 + num40;
                        uint num42 = m_outgoingCount[num41];
                        num41 *= 256;
                        for (uint num43 = 0u; num43 < num42; num43++)
                        {
                            m_outgoingOffers[num41 + num43].Priority = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num41 + num43}].Priority - {m_outgoingOffers[num41 + num43].Priority}");
                        }
                    }
                }

                for (int num44 = 150; num44 < num; num44++)
                {
                    for (int num45 = 0; num45 < 8; num45++)
                    {
                        int num46 = num44 * 8 + num45;
                        uint num47 = m_incomingCount[num46];
                        num46 *= 256;
                        for (uint num48 = 0u; num48 < num47; num48++)
                        {
                            m_incomingOffers[num46 + num48].Amount = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num46 + num48}].Amount - {m_incomingOffers[num46 + num48].Amount}");
                        }
                    }
                    for (int num49 = 0; num49 < 8; num49++)
                    {
                        int num50 = num44 * 8 + num49;
                        uint num51 = m_outgoingCount[num50];
                        num50 *= 256;
                        for (uint num52 = 0u; num52 < num51; num52++)
                        {
                            m_outgoingOffers[num50 + num52].Amount = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num50 + num52}].Amount - {m_outgoingOffers[num50 + num52].Amount}");
                        }
                    }
                }

                for (int num53 = 150; num53 < num; num53++)
                {
                    for (int num54 = 0; num54 < 8; num54++)
                    {
                        int num55 = num53 * 8 + num54;
                        uint num56 = m_incomingCount[num55];
                        num55 *= 256;
                        for (uint num57 = 0u; num57 < num56; num57++)
                        {
                            m_incomingOffers[num55 + num57].PositionX = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num55 + num57}].PositionX - {m_incomingOffers[num55 + num57].PositionX}");
                        }
                    }
                    for (int num58 = 0; num58 < 8; num58++)
                    {
                        int num59 = num53 * 8 + num58;
                        uint num60 = m_outgoingCount[num59];
                        num59 *= 256;
                        for (uint num61 = 0u; num61 < num60; num61++)
                        {
                            m_outgoingOffers[num59 + num61].PositionX = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num59 + num61}].PositionX - {m_outgoingOffers[num59 + num61].PositionX}");
                        }
                    }
                }

                for (int num62 = 150; num62 < num; num62++)
                {
                    for (int num63 = 0; num63 < 8; num63++)
                    {
                        int num64 = num62 * 8 + num63;
                        uint num65 = m_incomingCount[num64];
                        num64 *= 256;
                        for (uint num66 = 0u; num66 < num65; num66++)
                        {
                            m_incomingOffers[num64 + num66].PositionZ = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num64 + num66}].PositionZ - {m_incomingOffers[num64 + num66].PositionZ}");
                        }
                    }
                    for (int num67 = 0; num67 < 8; num67++)
                    {
                        int num68 = num62 * 8 + num67;
                        uint num69 = m_outgoingCount[num68];
                        num68 *= 256;
                        for (uint num70 = 0u; num70 < num69; num70++)
                        {
                            m_outgoingOffers[num68 + num70].PositionZ = StorageData.ReadInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num68 + num70}].PositionZ - {m_outgoingOffers[num68 + num70].PositionZ}");
                        }
                    }
                }

                for (int num71 = 150; num71 < num; num71++)
                {
                    for (int num72 = 0; num72 < 8; num72++)
                    {
                        int num73 = num71 * 8 + num72;
                        uint num74 = m_incomingCount[num73];
                        num73 *= 256;
                        for (uint num75 = 0u; num75 < num74; num75++)
                        {
                            m_incomingOffers[num73 + num75].m_object.Type = (InstanceType)StorageData.ReadByte(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num73 + num75}].InstanceType - {(InstanceType)m_incomingOffers[num73 + num75].m_object.Type}");
                        }
                    }
                    for (int num76 = 0; num76 < 8; num76++)
                    {
                        int num77 = num71 * 8 + num76;
                        uint num78 = m_outgoingCount[num77];
                        num77 *= 256;
                        for (uint num79 = 0u; num79 < num78; num79++)
                        {
                            m_outgoingOffers[num77 + num79].m_object.Type = (InstanceType)StorageData.ReadByte(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num77 + num79}].InstanceType - {(InstanceType)m_outgoingOffers[num77 + num79].m_object.Type}");
                        }
                    }
                }

                for (int num80 = 150; num80 < num; num80++)
                {
                    for (int num81 = 0; num81 < 8; num81++)
                    {
                        int num82 = num80 * 8 + num81;
                        uint num83 = m_incomingCount[num82];
                        num82 *= 256;
                        for (uint num84 = 0u; num84 < num83; num84++)
                        {
                            m_incomingOffers[num82 + num84].m_object.Index = StorageData.ReadUInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num82 + num84}].m_object.Index - {m_incomingOffers[num82 + num84].m_object.Index}");
                        }
                    }
                    for (int num85 = 0; num85 < 8; num85++)
                    {
                        int num86 = num80 * 8 + num85;
                        uint num87 = m_outgoingCount[num86];
                        num86 *= 256;
                        for (uint num88 = 0u; num88 < num87; num88++)
                        {
                            m_outgoingOffers[num86 + num88].m_object.Index = StorageData.ReadUInt32(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num86 + num88}].m_object.Index - {m_outgoingOffers[num86 + num88].m_object.Index}");
                        }
                    }
                }

                for (int num89 = 150; num89 < num; num89++)
                {
                    for (int num90 = 0; num90 < 8; num90++)
                    {
                        int num91 = num89 * 8 + num90;
                        uint num92 = m_incomingCount[num91];
                        num91 *= 256;
                        for (uint num93 = 0u; num93 < num92; num93++)
                        {
                            m_incomingOffers[num91 + num93].m_isLocalPark = StorageData.ReadByte(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_incomingOffers[{num91 + num93}].m_isLocalPark - {m_incomingOffers[num91 + num93].m_isLocalPark}");
                        }
                    }
                    for (int num94 = 0; num94 < 8; num94++)
                    {
                        int num95 = num89 * 8 + num94;
                        uint num96 = m_outgoingCount[num95];
                        num95 *= 256;
                        for (uint num97 = 0u; num97 < num96; num97++)
                        {
                            m_outgoingOffers[num95 + num97].m_isLocalPark = StorageData.ReadByte(Data, ref iIndex);
                            // Debug.Log($"LoadData ExtendedTransferManager: m_outgoingOffers[{num95 + num97}].m_isLocalPark - {m_outgoingOffers[num95 + num97].m_isLocalPark}");
                        }
                    }
                }

                CheckEndTuple($"Buffer", iExtendedTransferManagerVersion, Data, ref iIndex);

                typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_outgoingOffers);
                typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_incomingOffers);
                typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_outgoingCount);
                typeof(TransferManager).GetField("m_incomingCount", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_incomingCount);
                typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_outgoingAmount);
                typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, m_incomingAmount);
            }
        }

        private static void CheckStartTuple(string sTupleLocation, int iDataVersion, byte[] Data, ref int iIndex)
        {
            if (iDataVersion >= 1)
            {
                uint iTupleStart = StorageData.ReadUInt32(Data, ref iIndex);
                if (iTupleStart != uiTUPLE_START)
                {
                    throw new Exception($"ExtendedTransferManager Buffer start tuple not found at: {sTupleLocation}");
                }
            }
        }

        private static void CheckEndTuple(string sTupleLocation, int iDataVersion, byte[] Data, ref int iIndex)
        {
            if (iDataVersion >= 1)
            {
                uint iTupleEnd = StorageData.ReadUInt32(Data, ref iIndex);
                if (iTupleEnd != uiTUPLE_END)
                {
                    throw new Exception($"ExtendedTransferManager Buffer end tuple not found at: {sTupleLocation}");
                }
            }
        }

    }
}
