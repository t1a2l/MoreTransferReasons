using ColossalFramework;
using ColossalFramework.IO;
using MoreTransferReasons.Utils;
using System;
using UnityEngine;

namespace MoreTransferReasons
{
	public class ExtendedTransferManager : SimulationManagerBase<ExtendedTransferManager, TransferProperties>, ISimulationManager
	{
		public struct Offer
		{
			public byte m_isLocalPark;
			public Vector3 Position;
			public int Amount;
			public bool Active;
			public ushort Building;
			public ushort Vehicle;
			public uint Citizen;
			public InstanceID m_object;
			public bool IsWarehouse;
		}

		public enum TransferReason
		{
			MealsDeliveryLow = 0, // deliver low end food
			MealsDeliveryMedium = 1, // deliver regular food
			MealsDeliveryHigh = 2, // deliver high end food
			FoodSupplies = 3, // food materials for restaurants
			DrinkSupplies = 4, // drink materials for restaurants
			Bread = 5,
			CannedFish = 6,
			PoliceVanCriminalMove = 7, // carry prisoners from small police stations to big ones
			PrisonHelicopterCriminalPickup = 8, // pick up from big police stations
			PrisonHelicopterCriminalMove = 9, // transfer to prison
			MealsLow = 10, // serve low end food
			MealsMedium = 11, // serve normal food
			MealsHigh = 12,  // serve high end food
			Anchovy = 13,
			Salmon = 14,
			Shellfish = 15,
			Tuna = 16,
			Algae = 17,
			Seaweed = 18,	
			Trout = 19,
			Fruits = 20,
			Vegetables = 21,
			Cows = 22,
			HighlandCows = 23,
			Sheep = 24,
			Pigs = 25,
			Furnitures = 26,
			ElectronicProducts = 27,
			IndustrialSteel = 28,
			Tupperware = 29,
			Toys = 30,
			PrintedProducts = 31,
			TissuePaper = 32,
			Cloths = 33,
			PetroleumProducts = 34,
			Cars = 35,
			Footwear = 36,
			Houses = 37,
			None = 255
		}

		public class Data : IDataContainer
		{
			public void Serialize(DataSerializer s)
			{
				LogHelper.Information("Begin Serializing ExtendedTransferManager");
				Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "ExtendedTransferManager");
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

		protected override void Awake()
		{
			base.Awake();
			Transfers_Length = Enum.GetNames(typeof(TransferReason)).Length - 1;
			OutgoingIndexes = new int[Transfers_Length];
			IncomingIndexes = new int[Transfers_Length];
			OutgoingOffers = new Offer[Transfers_Length*256];
			IncomingOffers = new Offer[Transfers_Length*256];
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
				_ => TransferReason.None, 
			};
		}

		public void AddOutgoingOffer(TransferReason material, Offer offer)
		{
			int index = OutgoingIndexes[(int)material];
			if(index < 256)
			{
				OutgoingIndexes[(int)material] = index + 1;
				int FreeIndex = (int)material * 256 + index;
				OutgoingOffers[FreeIndex] = offer;
			}
		}

		public void AddIncomingOffer(TransferReason material, Offer offer)
		{
			int index = IncomingIndexes[(int)material];
			if(index < 256)
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
				int materail_index = (int)material * 256 + index;
				if (OutgoingOffers[materail_index].m_object == offer.m_object && OutgoingOffers[materail_index].m_isLocalPark == offer.m_isLocalPark)
				{
					OutgoingOffers[(int)material].Amount -= OutgoingOffers[materail_index].Amount;
					int num5 = (int)material * 256 + --index;
					ref Offer reference = ref OutgoingOffers[materail_index];
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
				int materail_index = (int)material * 256 + index;
				if (IncomingOffers[materail_index].m_object == offer.m_object && IncomingOffers[materail_index].m_isLocalPark == offer.m_isLocalPark)
				{
					IncomingOffers[(int)material].Amount -= IncomingOffers[materail_index].Amount;
					int num5 = (int)material * 256 + --index;
					ref Offer reference = ref IncomingOffers[materail_index];
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
			while(outgoing_matched_count < outgoing_ocuppied_count || incoming_matched_count < incoming_ocuppied_count)
			{
				if(outgoing_matched_count < outgoing_ocuppied_count)
				{
					Offer outgoing_offer = OutgoingOffers[(int)material * 256 + outgoing_matched_count];
					Vector3 outgoing_position = outgoing_offer.Position;
					int outgoing_amount = outgoing_offer.Amount;
					while(outgoing_amount != 0)
					{
						int chosen_index = -1;
						double min_distance = Math.Pow(65000, 2);
						for(int i = 0; i < incoming_ocuppied_count; i++)
						{
							Offer incoming_offer = IncomingOffers[(int)material * 256 + i];
							if(outgoing_offer.Building == incoming_offer.Building)
							{
								continue;
							}
							if(outgoing_offer.IsWarehouse && incoming_offer.IsWarehouse)
							{
								continue;
							}
							double distance = Vector3.SqrMagnitude(incoming_offer.Position - outgoing_position);
							if(distance < min_distance)
							{
								chosen_index = i;
								min_distance = distance;
								if(distance < 0f)
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
				if(incoming_matched_count < incoming_ocuppied_count)
				{
					Offer incoming_offer = IncomingOffers[(int)material * 256 + incoming_matched_count];
					Vector3 incoming_position = incoming_offer.Position;
					int incoming_amount = incoming_offer.Amount;
					while(incoming_amount != 0)
					{
						int chosen_index = -1;
						float min_distance = -1f;
						for(int i = 0; i < outgoing_ocuppied_count; i++)
						{
							Offer outgoing_offer = OutgoingOffers[(int)material * 256 + i];
							if(incoming_offer.Building == outgoing_offer.Building)
							{
								continue;
							}
							if(incoming_offer.IsWarehouse && outgoing_offer.IsWarehouse)
							{
								continue;
							}
							float distance = Vector3.SqrMagnitude(outgoing_offer.Position - incoming_position);
							if(distance > min_distance)
							{
								chosen_index = i;
								min_distance = distance;
								if(distance < 0f)
								{
									break;
								}
							}
						}
						if (chosen_index == -1)
						{
							break;
						}
						Offer chosen_outgoing_offer = OutgoingOffers[(int)material * 256 + chosen_index];
						int outgoing_amount = chosen_outgoing_offer.Amount;
						int min_amount = Mathf.Min(outgoing_amount, incoming_amount);
						if (min_amount != 0)
						{
							StartTransfer(material, chosen_outgoing_offer, incoming_offer, min_amount);
						}
						incoming_amount -= min_amount;
						outgoing_amount -= min_amount;
						if (outgoing_amount == 0)
						{
							int new_index = OutgoingIndexes[(int)material] - 1;
							OutgoingIndexes[(int)material] = new_index;
							ref Offer reference = ref OutgoingOffers[(int)material * 256 + chosen_index];
							reference = OutgoingOffers[(int)material * 256 + new_index];
							outgoing_ocuppied_count = new_index;
						}
						else
						{
							chosen_outgoing_offer.Amount = outgoing_amount;
							OutgoingOffers[(int)material * 256 + chosen_index] = chosen_outgoing_offer;
						}
						incoming_offer.Amount = incoming_amount;
					}
					if (incoming_amount == 0)
					{
						incoming_ocuppied_count--;
						IncomingIndexes[(int)material] = incoming_ocuppied_count;
						ref Offer reference2 = ref IncomingOffers[(int)material * 256 + incoming_matched_count];
						reference2 = IncomingOffers[(int)material * 256 + incoming_ocuppied_count];
					}
					else
					{
						incoming_offer.Amount = incoming_amount;
						IncomingOffers[(int)material * 256 + incoming_matched_count] = incoming_offer;
						incoming_matched_count++;
					}
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
				else
				{
					 throw new Exception("ExtendedVehicleAI Interface not found");
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
				else
				{
					 throw new Exception("ExtendedVehicleAI Interface not found");
				}
				
			}
			else if (active && offerIn.Citizen != 0U)
			{
				Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
				uint citizen = offerIn.Citizen;
				offerOut.Amount = delta;
				CitizenInfo citizenInfo = citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
				if (citizenInfo != null)
				{
					if (citizenInfo.m_citizenAI is IExtendedCitizenAI extendedCitizenAI) 
					{
						extendedCitizenAI.ExtendedStartTransfer(citizen, ref citizens.m_buffer[(int)((UIntPtr)citizen)], material, offerOut);
					}
					else
					{
						 throw new Exception("ExtendedCitizenAI Interface not found");
					}
				}
			}
			else if (active2 && offerOut.Citizen != 0U)
			{
				Array32<Citizen> citizens2 = Singleton<CitizenManager>.instance.m_citizens;
				uint citizen2 = offerOut.Citizen;
				offerIn.Amount = delta;
				CitizenInfo citizenInfo2 = citizens2.m_buffer[(int)((UIntPtr)citizen2)].GetCitizenInfo(citizen2);
				if (citizenInfo2 != null)
				{
					if (citizenInfo2.m_citizenAI is IExtendedCitizenAI extendedCitizenAI) 
					{
						extendedCitizenAI.ExtendedStartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
					}	
					else
					{
						 throw new Exception("ExtendedCitizenAI Interface not found");
					}
				}
			}
			else if (active2 && offerOut.Building != 0)
			{
				Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
				offerIn.Amount = delta;
				if (offerOut.m_isLocalPark != 0 && offerOut.m_isLocalPark == offerIn.m_isLocalPark)
				{
					this.StartDistrictTransfer(material, offerOut, offerIn);
				}
				else
				{
					ushort building = offerOut.Building;
					BuildingInfo info3 = buildings.m_buffer[(int)building].Info;
					if (info3.m_buildingAI is IExtendedBuildingAI extendedBuildingAI) 
					{
						extendedBuildingAI.ExtendedStartTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
					}	
					else
					{
						 throw new Exception("ExtendedBuildingAI Interface not found");
					}
				}
			}
			else if (active && offerIn.Building != 0)
			{
				Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
				offerOut.Amount = delta;
				if (offerIn.m_isLocalPark != 0 && offerIn.m_isLocalPark == offerOut.m_isLocalPark)
				{
					this.StartDistrictTransfer(material, offerOut, offerIn);
				}
				else
				{
					ushort building2 = offerIn.Building;
					BuildingInfo info4 = buildings2.m_buffer[(int)building2].Info;
					if (info4.m_buildingAI is IExtendedBuildingAI extendedBuildingAI) 
					{
						extendedBuildingAI.ExtendedStartTransfer(building2, ref buildings2.m_buffer[(int)building2], material, offerOut);
					}
					else
					{
						 throw new Exception("ExtendedBuildingAI Interface not found");
					}
				}
			}
		}

		private void StartDistrictTransfer(TransferReason material, Offer offerOut, Offer offerIn)
		{
			Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
			ushort building = offerOut.Building;
			ushort building2 = offerIn.Building;
			BuildingInfo info = buildings.m_buffer[(int)building].Info;
			BuildingInfo info2 = buildings.m_buffer[(int)building2].Info;
			if (info.m_buildingAI is IExtendedBuildingAI extendedBuildingAI && info2.m_buildingAI is IExtendedBuildingAI extendedBuildingAI2) 
			{
				extendedBuildingAI.ExtendedGetMaterialAmount(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building], material, out int num, out int num2);
				extendedBuildingAI2.ExtendedGetMaterialAmount(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building2], material, out int num3, out int num4);
				int num5 = Math.Min(num, num4 - num3);
				if (num5 > 0)
				{
					num = -num5;
					num3 = num5;
					extendedBuildingAI.ExtendedModifyMaterialBuffer(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building], material, ref num);
					extendedBuildingAI2.ExtendedModifyMaterialBuffer(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building2], material, ref num3);
				}
			}
			else
			{
					throw new Exception("ExtendedBuildingAI Interface not found");
			}
		}

	}
}
