using ColossalFramework;
using ColossalFramework.IO;
using HarmonyLib;
using System.Reflection;
using static TransferManager;

namespace MoreTransferReasons.HarmonyPatches
{
	[HarmonyPatch(typeof(Data))]
	public class DataPatch
	{
		// Status flag - are we loading an expanded TransferManager array?
        internal static bool s_loadingExpanded = false;

		private const int OriginalTransfersCount = 128;
        internal const int NewTransfersCount = 160;


		[HarmonyPatch(typeof(Data), "Serialize")]
        [HarmonyPrefix]
		public static bool Serialize(DataSerializer s)
		{
			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "TransferManager");
			TransferManager instance = Singleton<TransferManager>.instance;
			var m_outgoingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);

			int num = NewTransfersCount;
			if (BuildConfig.SAVE_DATA_FORMAT_VERSION < 111004)
			{
				num = 96;
			}
			for (int i = 0; i < num; i++)
			{
				s.WriteInt32(m_incomingAmount[i]);
				s.WriteInt32(m_outgoingAmount[i]);
			}
			EncodedArray.UShort uShort = EncodedArray.UShort.BeginWrite(s);
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					int num2 = j * 8 + k;
					uShort.Write(m_incomingCount[num2]);
				}
				for (int l = 0; l < 8; l++)
				{
					int num3 = j * 8 + l;
					uShort.Write(m_outgoingCount[num3]);
				}
			}
			uShort.EndWrite();
			EncodedArray.Bool @bool = EncodedArray.Bool.BeginWrite(s);
			for (int m = 0; m < num; m++)
			{
				for (int n = 0; n < 8; n++)
				{
					int num4 = m * 8 + n;
					uint num5 = m_incomingCount[num4];
					num4 *= 256;
					for (uint num6 = 0u; num6 < num5; num6++)
					{
						@bool.Write(m_incomingOffers[num4 + num6].Active);
					}
				}
				for (int num7 = 0; num7 < 8; num7++)
				{
					int num8 = m * 8 + num7;
					uint num9 = m_outgoingCount[num8];
					num8 *= 256;
					for (uint num10 = 0u; num10 < num9; num10++)
					{
						@bool.Write(m_outgoingOffers[num8 + num10].Active);
					}
				}
			}
			@bool.EndWrite();
			if (BuildConfig.SAVE_DATA_FORMAT_VERSION >= 111001)
			{
				EncodedArray.Bool bool2 = EncodedArray.Bool.BeginWrite(s);
				for (int num11 = 0; num11 < num; num11++)
				{
					for (int num12 = 0; num12 < 8; num12++)
					{
						int num13 = num11 * 8 + num12;
						uint num14 = m_incomingCount[num13];
						num13 *= 256;
						for (uint num15 = 0u; num15 < num14; num15++)
						{
							bool2.Write(m_incomingOffers[num13 + num15].Exclude);
						}
					}
					for (int num16 = 0; num16 < 8; num16++)
					{
						int num17 = num11 * 8 + num16;
						uint num18 = m_outgoingCount[num17];
						num17 *= 256;
						for (uint num19 = 0u; num19 < num18; num19++)
						{
							bool2.Write(m_outgoingOffers[num17 + num19].Exclude);
						}
					}
				}
				bool2.EndWrite();
			}
			EncodedArray.Byte @byte = EncodedArray.Byte.BeginWrite(s);
			for (int num20 = 0; num20 < num; num20++)
			{
				for (int num21 = 0; num21 < 8; num21++)
				{
					int num22 = num20 * 8 + num21;
					uint num23 = m_incomingCount[num22];
					num22 *= 256;
					for (uint num24 = 0u; num24 < num23; num24++)
					{
						@byte.Write((byte)m_incomingOffers[num22 + num24].Priority);
					}
				}
				for (int num25 = 0; num25 < 8; num25++)
				{
					int num26 = num20 * 8 + num25;
					uint num27 = m_outgoingCount[num26];
					num26 *= 256;
					for (uint num28 = 0u; num28 < num27; num28++)
					{
						@byte.Write((byte)m_outgoingOffers[num26 + num28].Priority);
					}
				}
			}
			@byte.EndWrite();
			EncodedArray.Byte byte2 = EncodedArray.Byte.BeginWrite(s);
			for (int num29 = 0; num29 < num; num29++)
			{
				for (int num30 = 0; num30 < 8; num30++)
				{
					int num31 = num29 * 8 + num30;
					uint num32 = m_incomingCount[num31];
					num31 *= 256;
					for (uint num33 = 0u; num33 < num32; num33++)
					{
						byte2.Write((byte)m_incomingOffers[num31 + num33].Amount);
					}
				}
				for (int num34 = 0; num34 < 8; num34++)
				{
					int num35 = num29 * 8 + num34;
					uint num36 = m_outgoingCount[num35];
					num35 *= 256;
					for (uint num37 = 0u; num37 < num36; num37++)
					{
						byte2.Write((byte)m_outgoingOffers[num35 + num37].Amount);
					}
				}
			}
			byte2.EndWrite();
			EncodedArray.Byte byte3 = EncodedArray.Byte.BeginWrite(s);
			for (int num38 = 0; num38 < num; num38++)
			{
				for (int num39 = 0; num39 < 8; num39++)
				{
					int num40 = num38 * 8 + num39;
					uint num41 = m_incomingCount[num40];
					num40 *= 256;
					for (uint num42 = 0u; num42 < num41; num42++)
					{
						byte3.Write((byte)m_incomingOffers[num40 + num42].PositionX);
					}
				}
				for (int num43 = 0; num43 < 8; num43++)
				{
					int num44 = num38 * 8 + num43;
					uint num45 = m_outgoingCount[num44];
					num44 *= 256;
					for (uint num46 = 0u; num46 < num45; num46++)
					{
						byte3.Write((byte)m_outgoingOffers[num44 + num46].PositionX);
					}
				}
			}
			byte3.EndWrite();
			EncodedArray.Byte byte4 = EncodedArray.Byte.BeginWrite(s);
			for (int num47 = 0; num47 < num; num47++)
			{
				for (int num48 = 0; num48 < 8; num48++)
				{
					int num49 = num47 * 8 + num48;
					uint num50 = m_incomingCount[num49];
					num49 *= 256;
					for (uint num51 = 0u; num51 < num50; num51++)
					{
						byte4.Write((byte)m_incomingOffers[num49 + num51].PositionZ);
					}
				}
				for (int num52 = 0; num52 < 8; num52++)
				{
					int num53 = num47 * 8 + num52;
					uint num54 = m_outgoingCount[num53];
					num53 *= 256;
					for (uint num55 = 0u; num55 < num54; num55++)
					{
						byte4.Write((byte)m_outgoingOffers[num53 + num55].PositionZ);
					}
				}
			}
			byte4.EndWrite();
			EncodedArray.Byte byte5 = EncodedArray.Byte.BeginWrite(s);
			for (int num56 = 0; num56 < num; num56++)
			{
				for (int num57 = 0; num57 < 8; num57++)
				{
					int num58 = num56 * 8 + num57;
					uint num59 = m_incomingCount[num58];
					num58 *= 256;
					for (uint num60 = 0u; num60 < num59; num60++)
					{
						byte5.Write((byte)m_incomingOffers[num58 + num60].m_object.Type);
					}
				}
				for (int num61 = 0; num61 < 8; num61++)
				{
					int num62 = num56 * 8 + num61;
					uint num63 = m_outgoingCount[num62];
					num62 *= 256;
					for (uint num64 = 0u; num64 < num63; num64++)
					{
						byte5.Write((byte)m_outgoingOffers[num62 + num64].m_object.Type);
					}
				}
			}
			byte5.EndWrite();
			EncodedArray.UInt uInt = EncodedArray.UInt.BeginWrite(s);
			for (int num65 = 0; num65 < num; num65++)
			{
				for (int num66 = 0; num66 < 8; num66++)
				{
					int num67 = num65 * 8 + num66;
					uint num68 = m_incomingCount[num67];
					num67 *= 256;
					for (uint num69 = 0u; num69 < num68; num69++)
					{
						uInt.Write(m_incomingOffers[num67 + num69].m_object.Index);
					}
				}
				for (int num70 = 0; num70 < 8; num70++)
				{
					int num71 = num65 * 8 + num70;
					uint num72 = m_outgoingCount[num71];
					num71 *= 256;
					for (uint num73 = 0u; num73 < num72; num73++)
					{
						uInt.Write(m_outgoingOffers[num71 + num73].m_object.Index);
					}
				}
			}
			uInt.EndWrite();
			EncodedArray.Byte byte6 = EncodedArray.Byte.BeginWrite(s);
			for (int num74 = 0; num74 < num; num74++)
			{
				for (int num75 = 0; num75 < 8; num75++)
				{
					int num76 = num74 * 8 + num75;
					uint num77 = m_incomingCount[num76];
					num76 *= 256;
					for (uint num78 = 0u; num78 < num77; num78++)
					{
						byte6.Write(m_incomingOffers[num76 + num78].m_isLocalPark);
					}
				}
				for (int num79 = 0; num79 < 8; num79++)
				{
					int num80 = num74 * 8 + num79;
					uint num81 = m_outgoingCount[num80];
					num80 *= 256;
					for (uint num82 = 0u; num82 < num81; num82++)
					{
						byte6.Write(m_outgoingOffers[num80 + num82].m_isLocalPark);
					}
				}
			}
			byte6.EndWrite();
			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndSerialize(s, "TransferManager");
			return false;
		}

		[HarmonyPatch(typeof(Data), "Deserialize")]
        [HarmonyPrefix]
		public static bool Deserialize(DataSerializer s)
		{
			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginDeserialize(s, "TransferManager");
			TransferManager instance = Singleton<TransferManager>.instance;
			var m_outgoingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance);

			s_loadingExpanded = Utils.MetaData.LoadingExtended;
			
			if (s.version >= 37)
			{
				int num = DeserialiseSize();
				int max =  DeserialiseSize();
				if (s.version < 225)
				{
					num = 64;
				}
				else if (s.version < 269)
				{
					num = 72;
				}
				else if (s.version < 286)
				{
					num = 80;
				}
				else if (s.version < 110015)
				{
					num = 88;
				}
				else if (s.version < 111004)
				{
					num = 96;
				}
				for (int i = 0; i < num; i++)
				{
					m_incomingAmount[i] = s.ReadInt32();
					m_outgoingAmount[i] = s.ReadInt32();
				}
				for (int j = num; j < max; j++)
				{
					m_incomingAmount[j] = 0;
					m_outgoingAmount[j] = 0;
				}
				EncodedArray.UShort uShort = EncodedArray.UShort.BeginRead(s);
				for (int k = 0; k < num; k++)
				{
					for (int l = 0; l < 8; l++)
					{
						int num2 = k * 8 + l;
						m_incomingCount[num2] = uShort.Read();
					}
					for (int m = 0; m < 8; m++)
					{
						int num3 = k * 8 + m;
						m_outgoingCount[num3] = uShort.Read();
					}
				}
				for (int n = num; n < max; n++)
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
				uShort.EndRead();
				EncodedArray.Bool @bool = EncodedArray.Bool.BeginRead(s);
				for (int num8 = 0; num8 < num; num8++)
				{
					for (int num9 = 0; num9 < 8; num9++)
					{
						int num10 = num8 * 8 + num9;
						uint num11 = m_incomingCount[num10];
						num10 *= 256;
						for (uint num12 = 0u; num12 < num11; num12++)
						{
							m_incomingOffers[num10 + num12].Active = @bool.Read();
						}
					}
					for (int num13 = 0; num13 < 8; num13++)
					{
						int num14 = num8 * 8 + num13;
						uint num15 = m_outgoingCount[num14];
						num14 *= 256;
						for (uint num16 = 0u; num16 < num15; num16++)
						{
							m_outgoingOffers[num14 + num16].Active = @bool.Read();
						}
					}
				}
				@bool.EndRead();
				if (s.version >= 111001)
				{
					EncodedArray.Bool bool2 = EncodedArray.Bool.BeginRead(s);
					for (int num17 = 0; num17 < num; num17++)
					{
						for (int num18 = 0; num18 < 8; num18++)
						{
							int num19 = num17 * 8 + num18;
							uint num20 = m_incomingCount[num19];
							num19 *= 256;
							for (uint num21 = 0u; num21 < num20; num21++)
							{
								m_incomingOffers[num19 + num21].Exclude = bool2.Read();
							}
						}
						for (int num22 = 0; num22 < 8; num22++)
						{
							int num23 = num17 * 8 + num22;
							uint num24 = m_outgoingCount[num23];
							num23 *= 256;
							for (uint num25 = 0u; num25 < num24; num25++)
							{
								m_outgoingOffers[num23 + num25].Exclude = bool2.Read();
							}
						}
					}
					bool2.EndRead();
				}
				else
				{
					for (int num26 = 0; num26 < num; num26++)
					{
						for (int num27 = 0; num27 < 8; num27++)
						{
							int num28 = num26 * 8 + num27;
							uint num29 = m_incomingCount[num28];
							num28 *= 256;
							for (uint num30 = 0u; num30 < num29; num30++)
							{
								m_incomingOffers[num28 + num30].Exclude = false;
							}
						}
						for (int num31 = 0; num31 < 8; num31++)
						{
							int num32 = num26 * 8 + num31;
							uint num33 = m_outgoingCount[num32];
							num32 *= 256;
							for (uint num34 = 0u; num34 < num33; num34++)
							{
								m_outgoingOffers[num32 + num34].Exclude = false;
							}
						}
					}
				}
				EncodedArray.Byte @byte = EncodedArray.Byte.BeginRead(s);
				for (int num35 = 0; num35 < num; num35++)
				{
					for (int num36 = 0; num36 < 8; num36++)
					{
						int num37 = num35 * 8 + num36;
						uint num38 = m_incomingCount[num37];
						num37 *= 256;
						for (uint num39 = 0u; num39 < num38; num39++)
						{
							m_incomingOffers[num37 + num39].Priority = @byte.Read();
						}
					}
					for (int num40 = 0; num40 < 8; num40++)
					{
						int num41 = num35 * 8 + num40;
						uint num42 = m_outgoingCount[num41];
						num41 *= 256;
						for (uint num43 = 0u; num43 < num42; num43++)
						{
							m_outgoingOffers[num41 + num43].Priority = @byte.Read();
						}
					}
				}
				@byte.EndRead();
				EncodedArray.Byte byte2 = EncodedArray.Byte.BeginRead(s);
				for (int num44 = 0; num44 < num; num44++)
				{
					for (int num45 = 0; num45 < 8; num45++)
					{
						int num46 = num44 * 8 + num45;
						uint num47 = m_incomingCount[num46];
						num46 *= 256;
						for (uint num48 = 0u; num48 < num47; num48++)
						{
							m_incomingOffers[num46 + num48].Amount = byte2.Read();
						}
					}
					for (int num49 = 0; num49 < 8; num49++)
					{
						int num50 = num44 * 8 + num49;
						uint num51 = m_outgoingCount[num50];
						num50 *= 256;
						for (uint num52 = 0u; num52 < num51; num52++)
						{
							m_outgoingOffers[num50 + num52].Amount = byte2.Read();
						}
					}
				}
				byte2.EndRead();
				EncodedArray.Byte byte3 = EncodedArray.Byte.BeginRead(s);
				for (int num53 = 0; num53 < num; num53++)
				{
					for (int num54 = 0; num54 < 8; num54++)
					{
						int num55 = num53 * 8 + num54;
						uint num56 = m_incomingCount[num55];
						num55 *= 256;
						for (uint num57 = 0u; num57 < num56; num57++)
						{
							m_incomingOffers[num55 + num57].PositionX = byte3.Read();
						}
					}
					for (int num58 = 0; num58 < 8; num58++)
					{
						int num59 = num53 * 8 + num58;
						uint num60 = m_outgoingCount[num59];
						num59 *= 256;
						for (uint num61 = 0u; num61 < num60; num61++)
						{
							m_outgoingOffers[num59 + num61].PositionX = byte3.Read();
						}
					}
				}
				byte3.EndRead();
				EncodedArray.Byte byte4 = EncodedArray.Byte.BeginRead(s);
				for (int num62 = 0; num62 < num; num62++)
				{
					for (int num63 = 0; num63 < 8; num63++)
					{
						int num64 = num62 * 8 + num63;
						uint num65 = m_incomingCount[num64];
						num64 *= 256;
						for (uint num66 = 0u; num66 < num65; num66++)
						{
							m_incomingOffers[num64 + num66].PositionZ = byte4.Read();
						}
					}
					for (int num67 = 0; num67 < 8; num67++)
					{
						int num68 = num62 * 8 + num67;
						uint num69 = m_outgoingCount[num68];
						num68 *= 256;
						for (uint num70 = 0u; num70 < num69; num70++)
						{
							m_outgoingOffers[num68 + num70].PositionZ = byte4.Read();
						}
					}
				}
				byte4.EndRead();
				EncodedArray.Byte byte5 = EncodedArray.Byte.BeginRead(s);
				for (int num71 = 0; num71 < num; num71++)
				{
					for (int num72 = 0; num72 < 8; num72++)
					{
						int num73 = num71 * 8 + num72;
						uint num74 = m_incomingCount[num73];
						num73 *= 256;
						for (uint num75 = 0u; num75 < num74; num75++)
						{
							m_incomingOffers[num73 + num75].m_object.Type = (InstanceType)byte5.Read();
						}
					}
					for (int num76 = 0; num76 < 8; num76++)
					{
						int num77 = num71 * 8 + num76;
						uint num78 = m_outgoingCount[num77];
						num77 *= 256;
						for (uint num79 = 0u; num79 < num78; num79++)
						{
							m_outgoingOffers[num77 + num79].m_object.Type = (InstanceType)byte5.Read();
						}
					}
				}
				byte5.EndRead();
				EncodedArray.UInt uInt = EncodedArray.UInt.BeginRead(s);
				for (int num80 = 0; num80 < num; num80++)
				{
					for (int num81 = 0; num81 < 8; num81++)
					{
						int num82 = num80 * 8 + num81;
						uint num83 = m_incomingCount[num82];
						num82 *= 256;
						for (uint num84 = 0u; num84 < num83; num84++)
						{
							m_incomingOffers[num82 + num84].m_object.Index = uInt.Read();
						}
					}
					for (int num85 = 0; num85 < 8; num85++)
					{
						int num86 = num80 * 8 + num85;
						uint num87 = m_outgoingCount[num86];
						num86 *= 256;
						for (uint num88 = 0u; num88 < num87; num88++)
						{
							m_outgoingOffers[num86 + num88].m_object.Index = uInt.Read();
						}
					}
				}
				uInt.EndRead();
				if (s.version >= 115001)
				{
					EncodedArray.Byte byte6 = EncodedArray.Byte.BeginRead(s);
					for (int num89 = 0; num89 < num; num89++)
					{
						for (int num90 = 0; num90 < 8; num90++)
						{
							int num91 = num89 * 8 + num90;
							uint num92 = m_incomingCount[num91];
							num91 *= 256;
							for (uint num93 = 0u; num93 < num92; num93++)
							{
								m_incomingOffers[num91 + num93].m_isLocalPark = byte6.Read();
							}
						}
						for (int num94 = 0; num94 < 8; num94++)
						{
							int num95 = num89 * 8 + num94;
							uint num96 = m_outgoingCount[num95];
							num95 *= 256;
							for (uint num97 = 0u; num97 < num96; num97++)
							{
								m_outgoingOffers[num95 + num97].m_isLocalPark = byte6.Read();
							}
						}
					}
					byte6.EndRead();
				}
			}
			typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_outgoingOffers);
            typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_incomingOffers);
            typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_outgoingCount);
            typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_incomingCount);
            typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_outgoingAmount);
            typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, m_incomingAmount);
			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndDeserialize(s, "TransferManager");
			return false;
		}

		public static int DeserialiseSize()
		{
			return s_loadingExpanded ? NewTransfersCount : OriginalTransfersCount;
		}
	}
}
