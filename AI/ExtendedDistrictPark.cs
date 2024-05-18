using ColossalFramework;
using System;
using System.Reflection;
using static DistrictPark;
using static MoreTransferReasons.PedestrianZoneExtendedTransferReasonManager;

namespace MoreTransferReasons
{
    public class ExtendedDistrictPark
    {
        public DistrictAreaResourceData m_milkData;

        public DistrictAreaResourceData m_fruitsData;

        public DistrictAreaResourceData m_vegetablesData;

        public DistrictAreaResourceData m_cowsData;

        public DistrictAreaResourceData m_highlandCowsData;

        public DistrictAreaResourceData m_sheepData;

        public DistrictAreaResourceData m_pigsData;

        public DistrictAreaResourceData m_foodProductsData;

        public DistrictAreaResourceData m_beverageProductsData;

        public DistrictAreaResourceData m_bakedGoodsData;

        public DistrictAreaResourceData m_cannedFishData;

        public DistrictAreaResourceData m_furnituresData;

        public DistrictAreaResourceData m_electronicProductsData;

        public DistrictAreaResourceData m_industrialSteelData;

        public DistrictAreaResourceData m_tupperwareData;

        public DistrictAreaResourceData m_toysData;

        public DistrictAreaResourceData m_printedProductsData;

        public DistrictAreaResourceData m_tissuePaperData;

        public DistrictAreaResourceData m_clothsData;

        public DistrictAreaResourceData m_petroleumProductsData;

        public DistrictAreaResourceData m_carsData;

        public DistrictAreaResourceData m_footwearData;

        public DistrictAreaResourceData m_housePartsData;

        public DistrictAreaResourceData m_shipData;

        public DistrictAreaResourceData m_woolData;

        public DistrictAreaResourceData m_cottonData;

        public void AddExtendedExportAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempExport += (uint)amount;
                    break;
            }
        }

        public void AddExtendedImportAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempImport += (uint)amount;
                    break;
            }
        }

        public void AddBufferStatus(ExtendedTransferManager.TransferReason material, int amount, int incoming, int capacity)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.Add(amount, incoming, capacity);
                    break;
            }
        }

        public void AddProductionAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            var m_mainGate = (ushort)typeof(DistrictPark).GetField("m_mainGate", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
            if (m_mainGate != 0 && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[m_mainGate].m_flags & Building.Flags.Active) != 0)
            {
                var m_totalProductionAmount = (ulong)typeof(DistrictPark).GetField("m_totalProductionAmount", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
                m_totalProductionAmount += (uint)amount;
                typeof(DistrictPark).GetField("m_totalProductionAmount", BindingFlags.Instance | BindingFlags.Public).SetValue(null, m_totalProductionAmount);
            }
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempProduction += (uint)amount;
                    break;
            }
        }

        public void AddConsumptionAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempConsumption += (uint)amount;
                    break;
            }
        }

        public void IndustrySimulationStep()
        {
            if ((Singleton<SimulationManager>.instance.m_currentFrameIndex & 0xFFF) >= 3840)
            {
                m_milkData.Add(ref m_milkData);
                m_fruitsData.Add(ref m_fruitsData);
                m_vegetablesData.Add(ref m_vegetablesData);
                m_cowsData.Add(ref m_cowsData);
                m_highlandCowsData.Add(ref m_highlandCowsData);
                m_sheepData.Add(ref m_sheepData);
                m_pigsData.Add(ref m_pigsData);
                m_foodProductsData.Add(ref m_foodProductsData);
                m_beverageProductsData.Add(ref m_beverageProductsData);
                m_bakedGoodsData.Add(ref m_bakedGoodsData);
                m_cannedFishData.Add(ref m_cannedFishData);
                m_furnituresData.Add(ref m_furnituresData);
                m_electronicProductsData.Add(ref m_electronicProductsData);
                m_industrialSteelData.Add(ref m_milkData);
                m_tupperwareData.Add(ref m_tupperwareData);
                m_toysData.Add(ref m_toysData);
                m_printedProductsData.Add(ref m_printedProductsData);
                m_tissuePaperData.Add(ref m_tissuePaperData);
                m_clothsData.Add(ref m_clothsData);
                m_petroleumProductsData.Add(ref m_petroleumProductsData);
                m_carsData.Add(ref m_carsData);
                m_footwearData.Add(ref m_footwearData);
                m_housePartsData.Add(ref m_housePartsData);
                m_shipData.Add(ref m_shipData);
                m_woolData.Add(ref m_woolData);
                m_cottonData.Add(ref m_cottonData);
                m_milkData.Update();
                m_fruitsData.Update();
                m_vegetablesData.Update();
                m_cowsData.Update();
                m_highlandCowsData.Update();
                m_sheepData.Update();
                m_pigsData.Update();
                m_foodProductsData.Update();
                m_beverageProductsData.Update();
                m_bakedGoodsData.Update();
                m_cannedFishData.Update();
                m_furnituresData.Update();
                m_electronicProductsData.Update();
                m_industrialSteelData.Update();
                m_tupperwareData.Update();
                m_toysData.Update();
                m_printedProductsData.Update();
                m_tissuePaperData.Update();
                m_clothsData.Update();
                m_petroleumProductsData.Update();
                m_carsData.Update();
                m_footwearData.Update();
                m_housePartsData.Update();
                m_shipData.Update();
                m_woolData.Update();
                m_cottonData.Update();
                m_milkData.Reset();
                m_fruitsData.Reset();
                m_vegetablesData.Reset();
                m_cowsData.Reset();
                m_highlandCowsData.Reset();
                m_sheepData.Reset();
                m_pigsData.Reset();
                m_foodProductsData.Reset();
                m_beverageProductsData.Reset();
                m_bakedGoodsData.Reset();
                m_cannedFishData.Reset();
                m_furnituresData.Reset();
                m_electronicProductsData.Reset();
                m_industrialSteelData.Reset();
                m_tupperwareData.Reset();
                m_toysData.Reset();
                m_printedProductsData.Reset();
                m_tissuePaperData.Reset();
                m_clothsData.Reset();
                m_petroleumProductsData.Reset();
                m_carsData.Reset();
                m_footwearData.Reset();
                m_housePartsData.Reset();
                m_shipData.Reset();
                m_woolData.Reset();
                m_cottonData.Reset();
            }
            else
            {
                m_milkData.ResetBuffers();
                m_fruitsData.ResetBuffers();
                m_vegetablesData.ResetBuffers();
                m_cowsData.ResetBuffers();
                m_highlandCowsData.ResetBuffers();
                m_sheepData.ResetBuffers();
                m_pigsData.ResetBuffers();
                m_foodProductsData.ResetBuffers();
                m_beverageProductsData.ResetBuffers();
                m_bakedGoodsData.ResetBuffers();
                m_cannedFishData.ResetBuffers();
                m_furnituresData.ResetBuffers();
                m_electronicProductsData.ResetBuffers();
                m_industrialSteelData.ResetBuffers();
                m_tupperwareData.ResetBuffers();
                m_toysData.ResetBuffers();
                m_printedProductsData.ResetBuffers();
                m_tissuePaperData.ResetBuffers();
                m_clothsData.ResetBuffers();
                m_petroleumProductsData.ResetBuffers();
                m_carsData.ResetBuffers();
                m_footwearData.ResetBuffers();
                m_housePartsData.ResetBuffers();
                m_shipData.ResetBuffers();
                m_woolData.ResetBuffers();
                m_cottonData.ResetBuffers();
            }
        }

        public void BaseSimulationStep()
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            if ((instance.m_currentFrameIndex & 0xFFF) >= 3840)
            {
                m_milkData.Update();
                m_fruitsData.Update();
                m_vegetablesData.Update();
                m_cowsData.Update();
                m_highlandCowsData.Update();
                m_sheepData.Update();
                m_pigsData.Update();
                m_foodProductsData.Update();
                m_beverageProductsData.Update();
                m_bakedGoodsData.Update();
                m_cannedFishData.Update();
                m_furnituresData.Update();
                m_electronicProductsData.Update();
                m_industrialSteelData.Update();
                m_tupperwareData.Update();
                m_toysData.Update();
                m_printedProductsData.Update();
                m_tissuePaperData.Update();
                m_clothsData.Update();
                m_petroleumProductsData.Update();
                m_carsData.Update();
                m_footwearData.Update();
                m_housePartsData.Update();
                m_shipData.Update();
                m_woolData.Update();
                m_cottonData.Update();
                m_milkData.Reset();
                m_fruitsData.Reset();
                m_vegetablesData.Reset();
                m_cowsData.Reset();
                m_highlandCowsData.Reset();
                m_sheepData.Reset();
                m_pigsData.Reset();
                m_foodProductsData.Reset();
                m_beverageProductsData.Reset();
                m_bakedGoodsData.Reset();
                m_cannedFishData.Reset();
                m_furnituresData.Reset();
                m_electronicProductsData.Reset();
                m_industrialSteelData.Reset();
                m_tupperwareData.Reset();
                m_toysData.Reset();
                m_printedProductsData.Reset();
                m_tissuePaperData.Reset();
                m_clothsData.Reset();
                m_petroleumProductsData.Reset();
                m_carsData.Reset();
                m_footwearData.Reset();
                m_housePartsData.Reset();
                m_shipData.Reset();
                m_woolData.Reset();
                m_cottonData.Reset();
            }
        }

        public void CreatePark()
        {
            m_milkData = default;
            m_fruitsData = default;
            m_vegetablesData = default;
            m_cowsData = default;
            m_highlandCowsData = default;
            m_sheepData = default;
            m_pigsData = default;
            m_foodProductsData = default;
            m_beverageProductsData = default;
            m_bakedGoodsData = default;
            m_cannedFishData = default;
            m_furnituresData = default;
            m_electronicProductsData = default;
            m_industrialSteelData = default;
            m_tupperwareData = default;
            m_toysData = default;
            m_printedProductsData = default;
            m_tissuePaperData = default;
            m_clothsData = default;
            m_petroleumProductsData = default;
            m_carsData = default;
            m_footwearData = default;
            m_housePartsData = default;
            m_shipData = default;
            m_woolData = default;
            m_cottonData = default;
        }

        public bool TryGetRandomServicePoint(ExtendedTransferManager.TransferReason material, out ushort buildingID)
        {
            if (TryGetPedestrianReason(material, out var reason) && TryGetRandomServicePoint(reason.m_deliveryCategory, out buildingID))
            {
                ServicePointAI servicePointAI = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI as ServicePointAI;
                if (servicePointAI != null && !servicePointAI.IsReachedCriticalTrafficLimit(buildingID, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID], reason.m_deliveryCategory))
                {
                    return true;
                }
            }
            buildingID = 0;
            return false;
        }

        public bool TryGetRandomServicePoint(DeliveryCategories deliveryCategory, out ushort buildingID)
        {
            if (deliveryCategory != 0 && IsServicePointDelivery(deliveryCategory, out var index))
            {
                var m_randomServicePoints = (ushort[])typeof(DistrictPark).GetField("m_randomServicePoints", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
                buildingID = m_randomServicePoints[index];
                return buildingID != 0;
            }
            buildingID = 0;
            return false;
        }

        public static bool TryGetPedestrianReason(ExtendedTransferManager.TransferReason material, out PedestrianZoneExtendedTransferReason reason)
        {
            if (IsPedestrianReason(material, out var index))
            {
                reason = kPedestrianZoneExtendedTransferReasons[index];
                return true;
            }
            reason = default;
            return false;
        }

        public static bool IsPedestrianReason(ExtendedTransferManager.TransferReason material, out int index)
        {
            index = Array.FindIndex(kPedestrianZoneExtendedTransferReasons, (PedestrianZoneExtendedTransferReason i) => i.m_material == material);
            return index >= 0;
        }

    }
}
