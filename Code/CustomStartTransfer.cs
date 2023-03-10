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
			Array16<Vehicle> m_vehicles = Singleton<VehicleManager>.instance.m_vehicles;
			VehicleManager instance2 = Singleton<VehicleManager>.instance;
			if (m_vehicles.CreateItem(out var item, ref r))
			{
				vehicle = item;
				Vehicle.Frame frame = new Vehicle.Frame(position, Quaternion.identity);
				m_vehicles.m_buffer[vehicle].m_flags = Vehicle.Flags.Created;
				m_vehicles.m_buffer[vehicle].m_flags2 = (Vehicle.Flags2)0;
				if (transferToSource)
				{
					m_vehicles.m_buffer[vehicle].m_flags |= Vehicle.Flags.TransferToSource;
				}
				if (transferToTarget)
				{
					m_vehicles.m_buffer[vehicle].m_flags |= Vehicle.Flags.TransferToTarget;
				}
				m_vehicles.m_buffer[vehicle].Info = info;
				m_vehicles.m_buffer[vehicle].m_frame0 = frame;
				m_vehicles.m_buffer[vehicle].m_frame1 = frame;
				m_vehicles.m_buffer[vehicle].m_frame2 = frame;
				m_vehicles.m_buffer[vehicle].m_frame3 = frame;
				m_vehicles.m_buffer[vehicle].m_targetPos0 = Vector4.zero;
				m_vehicles.m_buffer[vehicle].m_targetPos1 = Vector4.zero;
				m_vehicles.m_buffer[vehicle].m_targetPos2 = Vector4.zero;
				m_vehicles.m_buffer[vehicle].m_targetPos3 = Vector4.zero;
				m_vehicles.m_buffer[vehicle].m_sourceBuilding = 0;
				m_vehicles.m_buffer[vehicle].m_targetBuilding = 0;
				m_vehicles.m_buffer[vehicle].m_transferType = (byte)type;
				m_vehicles.m_buffer[vehicle].m_transferSize = 0;
				m_vehicles.m_buffer[vehicle].m_waitCounter = 0;
				m_vehicles.m_buffer[vehicle].m_blockCounter = 0;
				m_vehicles.m_buffer[vehicle].m_nextGridVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_nextOwnVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_nextGuestVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_nextLineVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_transportLine = 0;
				m_vehicles.m_buffer[vehicle].m_leadingVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_trailingVehicle = 0;
				m_vehicles.m_buffer[vehicle].m_cargoParent = 0;
				m_vehicles.m_buffer[vehicle].m_firstCargo = 0;
				m_vehicles.m_buffer[vehicle].m_nextCargo = 0;
				m_vehicles.m_buffer[vehicle].m_citizenUnits = 0u;
				m_vehicles.m_buffer[vehicle].m_path = 0u;
				m_vehicles.m_buffer[vehicle].m_lastFrame = 0;
				m_vehicles.m_buffer[vehicle].m_pathPositionIndex = 0;
				m_vehicles.m_buffer[vehicle].m_lastPathOffset = 0;
				m_vehicles.m_buffer[vehicle].m_gateIndex = 0;
				m_vehicles.m_buffer[vehicle].m_waterSource = 0;
				m_vehicles.m_buffer[vehicle].m_touristCount = 0;
				m_vehicles.m_buffer[vehicle].m_custom = 0;
				info.m_vehicleAI.CreateVehicle(vehicle, ref m_vehicles.m_buffer[vehicle]);
				info.m_vehicleAI.FrameDataUpdated(vehicle, ref m_vehicles.m_buffer[vehicle], ref m_vehicles.m_buffer[vehicle].m_frame0);
				instance2.m_vehicleCount = (int)(m_vehicles.ItemCount() - 1);
				return true;
			}
			vehicle = 0;
			return false;
		}
	}
}
