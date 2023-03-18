using ColossalFramework;

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
			TransferManager.TransferReason transferReason = TransferManager.TransferReason.None;
			if(material == ExtendedTransferManager.TransferReason.FoodSupplies || material == ExtendedTransferManager.TransferReason.DrinkSupplies || material == ExtendedTransferManager.TransferReason.Bread)
			{
				transferReason = TransferManager.TransferReason.LuxuryProducts;
			}
			if (Singleton<VehicleManager>.instance.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, vehicleInfo, data.m_position, transferReason, transferToSource: false, transferToTarget: true))
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

	}
}
