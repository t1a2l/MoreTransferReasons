using ColossalFramework;
using MoreTransferReasons.Utils;

namespace MoreTransferReasons.AI
{
    public class ExtendedWarehouseStationAI : WarehouseStationAI, IExtendedBuildingAI
    {
        void IExtendedBuildingAI.ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            var is_parent_warehouse = GetParentWarehouse(ref data, out var extendedWarehouseAI);
            var transferType = extendedWarehouseAI.GetActualTransferReason(data.m_parentBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_parentBuilding]);
            var actual_reason_byte = (byte)(transferType - 200);
            if (is_parent_warehouse && (byte)material == actual_reason_byte)
            {
                for (int i = 0; i < offer.Amount; i++)
                {
                    VehicleInfo transferVehicleService = ExtendedWarehouseAI.GetExtendedTransferVehicleService(material, ItemClass.Level.Level1, ref Singleton<SimulationManager>.instance.m_randomizer);
                    if (transferVehicleService == null)
                    {
                        continue;
                    }
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    if (ExtedndedVehicleManager.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, transferVehicleService, data.m_position, (byte)transferType, transferToSource: false, transferToTarget: true) && transferVehicleService.m_vehicleAI is ExtendedCargoTruckAI cargoTruckAI)
                    {
                        transferVehicleService.m_vehicleAI.SetSource(vehicle, ref vehicles.m_buffer[vehicle], buildingID);
                        ((IExtendedVehicleAI)cargoTruckAI).ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], material, offer);
                    }
                }
            }
        }

        void IExtendedBuildingAI.ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max)
        {
            amount = 0;
            max = 0;
        }

        void IExtendedBuildingAI.ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {
            if (GetParentWarehouse(ref data, out var extendedWarehouseAI))
            {
                ((IExtendedBuildingAI)extendedWarehouseAI).ExtendedModifyMaterialBuffer(data.m_parentBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_parentBuilding], material, ref amountDelta);
            }
            else
            {
                amountDelta = 0;
            }
        }

        private bool GetParentWarehouse(ref Building data, out ExtendedWarehouseAI extendedWarehouseAI)
        {
            extendedWarehouseAI = null;
            if (data.m_parentBuilding != 0)
            {
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_parentBuilding].Info;
                if (info != null)
                {
                    extendedWarehouseAI = info.m_buildingAI as ExtendedWarehouseAI;
                }
            }
            return extendedWarehouseAI != null;
        }
    }
}
