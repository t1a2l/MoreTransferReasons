using ColossalFramework;
using UnityEngine;

namespace MoreTransferReasons.AI
{
    public class ExtendedOutsideConnectionAI : OutsideConnectionAI, IExtendedBuildingAI
    {
        public void ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            StartConnectionTransfer(buildingID, ref data, material, offer);
        }

        public void ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max)
        {
            amount = 0;
            max = 0;
        }

        public void ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {

        }

        public static bool StartConnectionTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            bool flag = false;
            int amount = offer.Amount;
            offer.Amount = 1;
            for (int i = 0; i < amount; i++)
            {
                flag |= StartConnectionTransferImpl(buildingID, ref data, material, offer);
            }
            return flag;
        }

        private static bool StartConnectionTransferImpl(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            bool flag2 = false;
            bool flag3 = false;
            VehicleInfo vehicleInfo = ExtendedWarehouseAI.GetExtendedTransferVehicleService(material, ItemClass.Level.Level1, ref Singleton<SimulationManager>.instance.m_randomizer);
            if (vehicleInfo != null)
            {
                if(material == ExtendedTransferManager.TransferReason.Cars && vehicleInfo.GetClassLevel() == ItemClass.Level.Level2)
                {
                    return false;
                }
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                byte transferType = (byte)(material + 200);
                if (ExtendedVehicleManager.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, vehicleInfo, data.m_position, transferType, flag3, !flag3))
                {
                    vehicleInfo.m_vehicleAI.SetSource(vehicle, ref vehicles.m_buffer[vehicle], buildingID);

                    ((IExtendedVehicleAI)vehicleInfo.m_vehicleAI).ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], material, offer);
                    if (!flag2)
                    {
                        ushort building4 = offer.Building;
                        if (building4 != 0)
                        {
                            vehicleInfo.m_vehicleAI.GetSize(vehicle, ref vehicles.m_buffer[vehicle], out var size, out _);
                            if (!flag3)
                            {
                                IndustryBuildingManager.ImportResource(building4, ref buffer[building4], material, size);
                            }
                        }
                    }
                }
            }
            return true;
        }

        public override void ReleaseBuilding(ushort buildingID, ref Building data)
        {
            RemoveExtendedConnectionOffers(buildingID, ref data);
            base.ReleaseBuilding(buildingID, ref data);
        }

        public static void RemoveExtendedConnectionOffers(ushort buildingID, ref Building data)
        {
            ExtendedTransferManager instance = Singleton<ExtendedTransferManager>.instance;
            if ((data.m_flags & Building.Flags.Outgoing) != 0)
            {
                ExtendedTransferManager.Offer offer = default;
                offer.Building = buildingID;
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Anchovy, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Salmon, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Shellfish, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Tuna, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Algae, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Seaweed, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Trout, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.SheepMilk, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.CowMilk, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.HighlandCowMilk, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.LambMeat, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.BeefMeat, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.HighlandBeefMeat, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.PorkMeat, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Fruits, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Vegetables, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Cows, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.HighlandCows, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Sheep, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Pigs, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Toys, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Cloths, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Cars, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.Footwear, offer);
                instance.RemoveOutgoingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer);
            }
            if ((data.m_flags & Building.Flags.Incoming) != 0)
            {
                ExtendedTransferManager.Offer offer2 = default;
                offer2.Building = buildingID;
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Toys, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Cloths, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Cars, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.Footwear, offer2);
                instance.RemoveIncomingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer2);
            }
        }

        public override void SimulationStep(ushort buildingID, ref Building data)
        {
            base.SimulationStep(buildingID, ref data);
            if ((Singleton<ToolManager>.instance.m_properties.m_mode & ItemClass.Availability.Game) != 0)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(m_info.m_class);
                int productionRate = GetProductionRate(100, budget);
                AddConnectionOffers(buildingID, ref data, productionRate, m_cargoCapacity);
            }
        }

        public static void AddConnectionOffers(ushort buildingID, ref Building data, int productionRate, int cargoCapacity)
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            ExtendedTransferManager instance2 = Singleton<ExtendedTransferManager>.instance;
            cargoCapacity = 10;
            cargoCapacity = (cargoCapacity * productionRate + 99) / 100;
            int num2 = (cargoCapacity + instance.m_randomizer.Int32(16u)) / 16;
            if ((data.m_flags & Building.Flags.Outgoing) != 0)
            {
                int num5 = TickPathfindStatus(buildingID, ref data, PathFindType.LeavingCargo);
                ExtendedTransferManager.Offer offer = default;
                offer.Building = buildingID;
                offer.Position = data.m_position * (instance.m_randomizer.Int32(100, 400) * 0.01f);
                offer.Active = true;
                int num7 = num2;
                if (num7 != 0)
                {
                    if (num7 * num5 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        offer.Amount = 1;
                        
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Toys, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Cloths, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Cars, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Footwear, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer);
                        }
                    }
                    else
                    {
                        offer.Amount = num2;
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Toys, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Cloths, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Cars, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.Footwear, offer);
                        instance2.AddOutgoingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer);
                    }
                }
            }
            
            if ((data.m_flags & Building.Flags.Incoming) == 0)
            {
                return;
            }
            int num17 = TickPathfindStatus(buildingID, ref data, PathFindType.EnteringCargo);
            ExtendedTransferManager.Offer offer2 = default;
            offer2.Building = buildingID;
            offer2.Position = data.m_position * ((float)instance.m_randomizer.Int32(100, 400) * 0.01f);
            offer2.Active = false;
            int num19 = num2;
            if (num19 != 0)
            {
                if (num19 * num17 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    offer2.Amount = 1;
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Anchovy, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Salmon, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Shellfish, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Tuna, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Algae, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Seaweed, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Trout, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.SheepMilk, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.CowMilk, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandCowMilk, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.LambMeat, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BeefMeat, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandBeefMeat, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PorkMeat, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Fruits, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Vegetables, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cows, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandCows, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Sheep, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Pigs, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Toys, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cloths, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cars, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Footwear, offer2);
                    }
                    if (instance.m_randomizer.Int32(16u) == 0)
                    {
                        instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer2);
                    }
                }
                else
                {
                    offer2.Amount = num2;
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Anchovy, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Salmon, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Shellfish, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Tuna, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Algae, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Seaweed, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Trout, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.SheepMilk, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.CowMilk, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandCowMilk, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.LambMeat, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BeefMeat, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandBeefMeat, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PorkMeat, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Fruits, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Vegetables, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cows, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HighlandCows, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Sheep, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Pigs, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.FoodProducts, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BeverageProducts, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.BakedGoods, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.CannedFish, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Furnitures, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.ElectronicProducts, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.IndustrialSteel, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Tupperware, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Toys, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PrintedProducts, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.TissuePaper, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cloths, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.PetroleumProducts, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Cars, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.Footwear, offer2);
                    instance2.AddIncomingOffer(ExtendedTransferManager.TransferReason.HouseParts, offer2);
                }
            }
        }

        private static int TickPathfindStatus(ushort buildingID, ref Building data, PathFindType type)
        {
            return type switch
            {
                PathFindType.EnteringCargo => TickPathfindStatus(ref data.m_education3, ref data.m_adults),
                PathFindType.LeavingCargo => TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer),
                _ => 0,
            };
        }

        private static int TickPathfindStatus(ref byte success, ref byte failure)
        {
            int result = (success << 8) / Mathf.Max(1, success + failure);
            if (success > failure)
            {
                success = (byte)(success + 1 >> 1);
                failure >>= 1;
            }
            else
            {
                success >>= 1;
                failure = (byte)(failure + 1 >> 1);
            }
            return result;
        }
    }
}
