using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace MoreTransferReasons.Code
{
	public static class CustomStartTransfer
	{
		public static void BuildingAIStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
		{
			VehicleInfo vehicleInfo = GetSelectedVehicle(buildingID);
			if (vehicleInfo == null)
			{
				vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, data.Info.m_class.m_service, data.Info.m_class.m_subService, data.Info.m_class.m_level, VehicleInfo.VehicleType.Car);
			}
			if (vehicleInfo == null)
			{
				return;
			}
			Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
			if (CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, vehicleInfo, data.m_position, material, transferToSource: false, transferToTarget: true))
			{
				vehicleInfo.m_vehicleAI.SetSource(vehicle, ref vehicles.m_buffer[vehicle], buildingID);
				VehicleAIStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], offer);
			}
		}

		public static void VehicleAIStartTransfer(ushort vehicleID, ref Vehicle data, ExtendedTransferManager.Offer offer)
		{
			if ((data.m_flags & Vehicle.Flags.WaitingTarget) != 0)
			{
				data.Info.m_vehicleAI.SetTarget(vehicleID, ref data, offer.Building);
			}
		}

		public static VehicleInfo GetSelectedVehicle(ushort buildingId)
		{
			if (!Singleton<BuildingManager>.instance.TryGetSelectedServiceBuildingVehicle(buildingId, out var prefabIndex))
			{
				return null;
			}
			return PrefabCollection<VehicleInfo>.GetPrefab((uint)prefabIndex);
		}

		public static bool CreateVehicle(out ushort vehicle, ref Randomizer r, VehicleInfo info, Vector3 position, ExtendedTransferManager.TransferReason type, bool transferToSource, bool transferToTarget)
		{
			if (Singleton<VehicleManager>.instance.m_vehicles.CreateItem(out ushort num, ref r))
			{
				vehicle = num;
				Vehicle.Frame frame = new Vehicle.Frame(position, Quaternion.identity);
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_flags = Vehicle.Flags.Created;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_flags2 = (Vehicle.Flags2)0;
				if (transferToSource)
				{
					Vehicle[] buffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
					ushort num2 = vehicle;
					buffer[(int)num2].m_flags = buffer[(int)num2].m_flags | Vehicle.Flags.TransferToSource;
				}
				if (transferToTarget)
				{
					Vehicle[] buffer2 = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
					ushort num3 = vehicle;
					buffer2[(int)num3].m_flags = buffer2[(int)num3].m_flags | Vehicle.Flags.TransferToTarget;
				}
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].Info = info;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_frame0 = frame;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_frame1 = frame;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_frame2 = frame;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_frame3 = frame;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_targetPos0 = Vector4.zero;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_targetPos1 = Vector4.zero;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_targetPos2 = Vector4.zero;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_targetPos3 = Vector4.zero;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_sourceBuilding = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_targetBuilding = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_transferType = (byte)type;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_transferSize = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_waitCounter = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_blockCounter = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_nextGridVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_nextOwnVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_nextGuestVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_nextLineVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_transportLine = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_leadingVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_trailingVehicle = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_cargoParent = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_firstCargo = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_nextCargo = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_citizenUnits = 0U;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_path = 0U;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_lastFrame = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_pathPositionIndex = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_lastPathOffset = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_gateIndex = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_waterSource = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_touristCount = 0;
				Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_custom = 0;
				info.m_vehicleAI.CreateVehicle(vehicle, ref Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle]);
				info.m_vehicleAI.FrameDataUpdated(vehicle, ref Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle], ref Singleton<VehicleManager>.instance.m_vehicles.m_buffer[(int)vehicle].m_frame0);
				Singleton<VehicleManager>.instance.m_vehicleCount = (int)(Singleton<VehicleManager>.instance.m_vehicles.ItemCount() - 1U);
				return true;
			}
			vehicle = 0;
			return false;
		}

	}
}
