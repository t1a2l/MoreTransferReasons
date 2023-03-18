using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;

namespace MoreTransferReasons.Code
{
	public class ExtendedTransferManager : SimulationManagerBase<TransferManager, TransferProperties>, ISimulationManager
	{
		public struct Offer
		{
			public byte m_isLocalPark;
			public Vector3 Position;
			public int Amount;
			public bool Active;
			public ushort Building;
			public ushort Vehicle;
			public ushort Citizen;
		}

		public enum TransferReason
		{
			DeliveryLow = 0, // 0 - 255
			DeliveryNormal = 1, // 256 - 511
			DeliveryHigh = 2, // 512 - 767
			FoodSupplies = 3, // 768 - 1023
			DrinkSupplies = 4, // 1024 - 1279
			Bread = 5, // 1280 - 1535
			None = 128
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
				1 => TransferReason.DeliveryLow, 
				3 => TransferReason.DeliveryNormal, 
				5 => TransferReason.DeliveryHigh, 
				7 => TransferReason.FoodSupplies, 
				9 => TransferReason.DrinkSupplies, 
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
						int chosen_index = 0;
						float min_distance = -1f;
						for(int i = 0; i < incoming_ocuppied_count; i++)
						{
							Offer incoming_offer = IncomingOffers[(int)material * 256 + i];
							float distance = Vector3.SqrMagnitude(incoming_offer.Position - outgoing_position);
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
					Offer incoming_offer = IncomingOffers[(int)material * 256 + outgoing_matched_count];
					Vector3 incoming_position = incoming_offer.Position;
					int incoming_amount = incoming_offer.Amount;
					while(incoming_amount != 0)
					{
						int chosen_index = 0;
						float min_distance = -1f;
						for(int i = 0; i < outgoing_ocuppied_count; i++)
						{
							Offer outgoing_offer = OutgoingOffers[(int)material * 256 + i];
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
			for(int k = 0; k < Transfers_Length; k++)
			{
				OutgoingIndexes[k] = 0;
				IncomingIndexes[k] = 0;
			}
		}

		private void StartTransfer(TransferReason material, Offer offerOut, Offer offerIn, int delta)
		{
			bool active = offerIn.Active;
			bool active2 = offerOut.Active;
			if (active && offerIn.Vehicle != 0)
			{
				Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
				ushort vehicle = offerIn.Vehicle;
				CustomStartTransfer.VehicleAIStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], offerOut);
				offerOut.Amount = delta;
			}
			else if (active2 && offerOut.Vehicle != 0)
			{
				Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
				ushort vehicle2 = offerOut.Vehicle;
				CustomStartTransfer.VehicleAIStartTransfer(vehicle2, ref vehicles2.m_buffer[vehicle2], offerIn);
				offerIn.Amount = delta;
			}
			else if (active2 && offerOut.Building != 0)
			{
				Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
				if (offerOut.m_isLocalPark != 0 && offerOut.m_isLocalPark == offerIn.m_isLocalPark)
				{
					StartDistrictTransfer(material, offerOut, offerIn);
					return;
				}
				ushort building = offerOut.Building;
				CustomStartTransfer.BuildingAIStartTransfer(building, ref buildings.m_buffer[building], material, offerIn);
				offerIn.Amount = delta;
			}
			else if (active && offerIn.Building != 0)
			{
				if (offerIn.m_isLocalPark != 0 && offerIn.m_isLocalPark == offerOut.m_isLocalPark)
				{
					StartDistrictTransfer(material, offerOut, offerIn);
					return;
				}
				Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
				ushort building2 = offerIn.Building;
				CustomStartTransfer.BuildingAIStartTransfer(building2, ref buildings2.m_buffer[building2], material, offerOut);
				offerOut.Amount = delta;
			}
		}

		private void StartDistrictTransfer(TransferReason material, Offer offerOut, Offer offerIn)
		{
			Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
			ushort building = offerOut.Building;
			ushort building2 = offerIn.Building;
			GetMaterialAmount(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, out var amount, out var _);
			GetMaterialAmount(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, out var amount2, out var max2);
			int num = Math.Min(amount, max2 - amount2);
			if (num > 0)
			{
				amount = -num;
				amount2 = num;
				ModifyMaterialBuffer(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, ref amount);
				ModifyMaterialBuffer(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, ref amount2);
			}
		}

		public void GetMaterialAmount(ushort buildingID, ref Building data, TransferReason material, out int amount, out int max)
		{
			int width = data.Width;
			int length = data.Length;
			int num = 4000;
			amount = data.m_customBuffer1;
			int num2;
			if (data.Info.m_buildingAI.GetType().Name.Equals("RestaurantAI"))
			{
				num2 = CalculateVisitplaceCount(new Randomizer(buildingID), width, length);
				max = Mathf.Max(num2 * 500, num * 4);
			}
			else
			{
				num2 = CalculateProductionCapacity(new Randomizer(buildingID), width, length);
				int consumptionDivider = 1;
				max = Mathf.Max(num2 * 500 / consumptionDivider, num * 4);
			}
		}

		public void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferReason material, ref int amountDelta)
		{
			int width = data.Width;
			int length = data.Length;
			int num = 4000;
			if (data.Info.m_buildingAI.GetType().Name.Equals("RestaurantAI"))
			{
				int num2 = CalculateVisitplaceCount(new Randomizer(buildingID), width, length);
				int num3 = Mathf.Max(num2 * 500, num * 4);
				int customBuffer2 = data.m_customBuffer1;
				amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer2);
				data.m_customBuffer1 = (ushort)(customBuffer2 + amountDelta);
			}
			else
			{
				int num2 = CalculateProductionCapacity(new Randomizer(buildingID), width, length);
				int consumptionDivider = 1;
				int num3 = Mathf.Max(num2 * 500 / consumptionDivider, num * 4);
				int customBuffer = data.m_customBuffer1;
				amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
				data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
			}
		}

		public int CalculateVisitplaceCount(Randomizer r, int width, int length)
		{
			int num = Mathf.Max(200, width * length * 250 + r.Int32(100u)) / 100;
			return num;
		}

		public int CalculateProductionCapacity(Randomizer r, int width, int length)
		{
			int num = Mathf.Max(100, width * length * 160 + r.Int32(100u)) / 100;
			return num;
		}

	}
}
