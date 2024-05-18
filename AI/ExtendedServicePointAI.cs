using ColossalFramework;
using UnityEngine;

namespace MoreTransferReasons.AI
{
    public class ExtendedServicePointAI : ServicePointAI, IExtendedBuildingAI
    {
        public void ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {
            int amountDelta2 = amountDelta;
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte park = instance.GetPark(data.m_position);
            if (park != 0 && instance.m_parks.m_buffer[park].IsPedestrianZone && ExtendedDistrictPark.TryGetPedestrianReason(material, out var reason))
            {
                amountDelta -= amountDelta2;
                if (IsReachedTrafficLimit(buildingID, ref data, reason.m_deliveryCategory))
                {
                    amountDelta = (int)Mathf.Clamp(amountDelta, (float)(-reason.m_averageTruckCapacity) / 2f, (float)reason.m_averageTruckCapacity / 2f);
                }
                ExtendedDistrictManager.IndustryParks[park].ModifyMaterialBuffer(material, ref amountDelta);
                amountDelta += amountDelta2;
                if (amountDelta != 0)
                {
                    bool flag = HasAvailableCapacity(buildingID, ref data, reason.m_deliveryCategory, out int left);
                    IncreaseTrafficRate(buildingID, ref data, reason.m_deliveryCategory);
                    if (flag && left == 1 && instance.m_parks.m_buffer[park].TryGetRandomServicePoint(reason.m_deliveryCategory, out var buildingID2) && buildingID == buildingID2)
                    {
                        instance.m_parks.m_buffer[park].SetRandomServicePoint(reason.m_deliveryCategory, 0);
                    }
                }
            }
            else
            {
                amountDelta = amountDelta2;
            }
        }

        public void ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte park = instance.GetPark(data.m_position);
            byte park2 = instance.GetPark(offer.Position);
            if (park == 0)
            {
                return;
            }
            if (park == park2)
            {
                ExtendedDistrictManager.IndustryParks[park].StartLocalTransfer(material, offer);
            }
            else
            {
                if (!ExtendedDistrictPark.TryGetPedestrianReason(material, out var reason) || IsReachedCriticalTrafficLimit(buildingID, ref data, reason.m_deliveryCategory) || (!HasAvailableCapacity(buildingID, ref data, reason.m_deliveryCategory, out var left) && left == 0))
                {
                    return;
                }

                bool flag = false;
                VehicleInfo vehicleInfo;
                switch (material)
                {
                    case ExtendedTransferManager.TransferReason.Anchovy:
                    case ExtendedTransferManager.TransferReason.Salmon:
                    case ExtendedTransferManager.TransferReason.Shellfish:
                    case ExtendedTransferManager.TransferReason.Tuna:
                    case ExtendedTransferManager.TransferReason.Algae:
                    case ExtendedTransferManager.TransferReason.Seaweed:
                    case ExtendedTransferManager.TransferReason.Trout:
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Fishing, ItemClass.SubService.None, ItemClass.Level.Level1, VehicleInfo.VehicleType.Car);
                        break;
                    case ExtendedTransferManager.TransferReason.Milk:
                    case ExtendedTransferManager.TransferReason.Fruits:
                    case ExtendedTransferManager.TransferReason.Vegetables:
                    case ExtendedTransferManager.TransferReason.Cows:
                    case ExtendedTransferManager.TransferReason.HighlandCows:
                    case ExtendedTransferManager.TransferReason.Sheep:
                    case ExtendedTransferManager.TransferReason.Pigs:
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialFarming, ItemClass.Level.Level2);
                        break;
                    case ExtendedTransferManager.TransferReason.FoodProducts:
                    case ExtendedTransferManager.TransferReason.BeverageProducts:
                    case ExtendedTransferManager.TransferReason.BakedGoods:
                    case ExtendedTransferManager.TransferReason.CannedFish:
                    case ExtendedTransferManager.TransferReason.Furnitures:
                    case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    case ExtendedTransferManager.TransferReason.Tupperware:
                    case ExtendedTransferManager.TransferReason.Toys:
                    case ExtendedTransferManager.TransferReason.PrintedProducts:
                    case ExtendedTransferManager.TransferReason.TissuePaper:
                    case ExtendedTransferManager.TransferReason.Cloths:
                    case ExtendedTransferManager.TransferReason.Footwear:
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level1);
                        break;
                    case ExtendedTransferManager.TransferReason.IndustrialSteel: // shipyard, car factory, construction
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level3);
                        break;
                    case ExtendedTransferManager.TransferReason.PetroleumProducts: // gas stations, boiler stations, airport fuel, plastic factory
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level1);
                        break;
                    case ExtendedTransferManager.TransferReason.Cars: // 1 -> 2 -> rental, buy, export
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level4);
                        break;
                    case ExtendedTransferManager.TransferReason.HouseParts: // 9 -> to build houses
                        vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level5);
                        break;
                    default:
                        return;
                }
                if (vehicleInfo == null || (DistrictPark.GetDeliveryCategories(vehicleInfo.vehicleCategory) & m_deliveryCategories) == 0)
                {
                    return;
                }
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                if (Singleton<VehicleManager>.instance.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, vehicleInfo, Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].m_position, material, flag, !flag))
                {
                    vehicleInfo.m_vehicleAI.SetSource(vehicle, ref vehicles.m_buffer[vehicle], buildingID);
                    ((IExtendedVehicleAI)vehicleInfo.m_vehicleAI).ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], material, offer);
                    ushort building = offer.Building;
                    if (building != 0 && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[building].m_flags & Building.Flags.IncomingOutgoing) != 0)
                    {
                        vehicleInfo.m_vehicleAI.GetSize(vehicle, ref vehicles.m_buffer[vehicle], out var size, out _);
                        IndustryBuildingManager.ExportResource(buildingID, ref data, material, size);
                    }
                }
            }
        }

        public void ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max)
        {
            amount = 0;
            max = 0;
        }
    }
}
