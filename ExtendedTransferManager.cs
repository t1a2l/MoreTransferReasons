using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;

namespace MoreTransferReasons
{
    public class ExtendedTransferManager : TransferManager
    {
        public const int TransferReasonCount = 236;

        public const TransferReason MealsDeliveryLow = (TransferReason)150;

        public const TransferReason MealsDeliveryMedium = (TransferReason)151;

        public const TransferReason MealsDeliveryHigh = (TransferReason)152;

        public const TransferReason Anchovy = (TransferReason)153;

        public const TransferReason Salmon = (TransferReason)154;

        public const TransferReason Shellfish = (TransferReason)155;

        public const TransferReason Tuna = (TransferReason)156;

        public const TransferReason Algae = (TransferReason)157;

        public const TransferReason Seaweed = (TransferReason)158;

        public const TransferReason Trout = (TransferReason)159;

        public const TransferReason Milk = (TransferReason)160;

        public const TransferReason RawHides = (TransferReason)161;

        public const TransferReason Pork = (TransferReason)162;

        public const TransferReason Fruits = (TransferReason)163;

        public const TransferReason Vegetables = (TransferReason)164;

        public const TransferReason Wool = (TransferReason)165;

        public const TransferReason Cotton = (TransferReason)166;

        public const TransferReason Cows = (TransferReason)167;

        public const TransferReason HighlandCows = (TransferReason)168;

        public const TransferReason Sheep = (TransferReason)169;

        public const TransferReason Pigs = (TransferReason)170;

        public const TransferReason ProcessedVegetableOil = (TransferReason)171;

        public const TransferReason LiquidConcentrates = (TransferReason)172;

        public const TransferReason ChemicalProducts = (TransferReason)173;

        public const TransferReason Leather = (TransferReason)174;

        public const TransferReason FoodProducts = (TransferReason)175;

        public const TransferReason BeverageProducts = (TransferReason)176;

        public const TransferReason BakedGoods = (TransferReason)177;

        public const TransferReason CannedFish = (TransferReason)178;

        public const TransferReason Furnitures = (TransferReason)179;

        public const TransferReason ElectronicProducts = (TransferReason)180;

        public const TransferReason IndustrialSteel = (TransferReason)181;

        public const TransferReason Tupperware = (TransferReason)182;

        public const TransferReason Toys = (TransferReason)183;

        public const TransferReason PrintedProducts = (TransferReason)184;

        public const TransferReason TissuePaper = (TransferReason)185;

        public const TransferReason Cloths = (TransferReason)186;

        public const TransferReason PetroleumProducts = (TransferReason)187;

        public const TransferReason Cars = (TransferReason)188;

        public const TransferReason Footwear = (TransferReason)189;

        public const TransferReason HouseParts = (TransferReason)190;

        public const TransferReason Ship = (TransferReason)191;

        public const TransferReason ConstructionResources = (TransferReason)192;

        public const TransferReason OperationResources = (TransferReason)193;

        // 193 - 86, 194 - 88, 195 - 90,
        // 196 - 92, 197 - 94, 198 - 96, 199 - 98, 200 - 100, 201 - 102, 202 - 104, 203 - 106, 204 - 108, 205 - 110,
        // 206 - 112, 207 - 114, 208 - 116, 209 - 118, 210 - 120, 211 - 122, 212 - 124, 213 - 126, 214 - 128, 215 - 130,
        // 216 - 132, 217 - 134, 218 - 136, 219 - 138

        public const TransferReason MealsLow = (TransferReason)220;

        public const TransferReason MealsMedium = (TransferReason)221;

        public const TransferReason MealsHigh = (TransferReason)222;

        public const TransferReason PoliceVanCriminalMove = (TransferReason)223;

        public const TransferReason PrisonHelicopterCriminalPickup = (TransferReason)224;

        public const TransferReason PrisonHelicopterCriminalMove = (TransferReason)225;

        public const TransferReason CarRent = (TransferReason)226;

        public const TransferReason CarBuy = (TransferReason)227;

        public const TransferReason CarSell = (TransferReason)228;

        public const TransferReason VehicleFuel = (TransferReason)229;

        public const TransferReason VehicleFuelElectric = (TransferReason)230;

        public const TransferReason VehicleWash = (TransferReason)231;

        public const TransferReason VehicleMinorRepair = (TransferReason)232;

        public const TransferReason VehicleMajorRepair = (TransferReason)233;

        public const TransferReason VehicleOutOfFuel = (TransferReason)234;

        public const TransferReason VehicleBrokenDown = (TransferReason)235;

        public static TransferReason GetExtendedFrameReason(int frameIndex)
        {
            return frameIndex switch
            {
                0 => MealsDeliveryLow,
                2 => MealsDeliveryMedium,
                4 => MealsDeliveryHigh, // deliver high end food - vehicle
                6 => Anchovy,
                8 => Salmon,
                10 => Shellfish,
                12 => Tuna,
                14 => Algae,
                16 => Seaweed,
                18 => Trout,
                20 => Milk,
                22 => RawHides,
                24 => Pork,
                26 => Fruits,
                28 => Vegetables,
                30 => Wool,
                32 => Cotton,
                34 => Cows,
                36 => HighlandCows,
                38 => Sheep,
                40 => Pigs,
                42 => ProcessedVegetableOil,
                44 => LiquidConcentrates,
                46 => ChemicalProducts,
                48 => Leather,
                50 => FoodProducts,
                52 => BeverageProducts,
                54 => BakedGoods,
                56 => CannedFish,
                58 => Furnitures,
                60 => ElectronicProducts,
                62 => IndustrialSteel,
                64 => Tupperware,
                66 => Toys,
                68 => PrintedProducts,
                70 => TissuePaper,
                72 => Cloths,
                74 => PetroleumProducts,
                76 => Cars,
                78 => Footwear,
                80 => HouseParts,
                82 => Ship,
                84 => ConstructionResources,
                86 => OperationResources,
                // 88 90
                // 92 94 96 98 100 102 104 106 108
                // 110 112 114 116 118 120 122 124 126 128 130
                // 132 134 136 138
                // until 138 only transfer types that needs vehicle to transport them
                140 => MealsLow,
                142 => MealsMedium,
                144 => MealsHigh,
                146 => PoliceVanCriminalMove,
                148 => PrisonHelicopterCriminalPickup,
                150 => PrisonHelicopterCriminalMove,
                152 => CarRent,
                154 => CarBuy,
                156 => CarSell,
                158 => VehicleFuel,
                160 => VehicleFuelElectric,
                162 => VehicleWash,
                164 => VehicleMinorRepair,
                166 => VehicleMajorRepair,
                168 => VehicleOutOfFuel,
                170 => VehicleBrokenDown,
                _ => TransferReason.None,
            };
        }

        public static List<string> GetExtendedTransferReasons()
        {
            return
            [
                "MealsDeliveryLow",
                "MealsDeliveryMedium",
                "MealsDeliveryHigh",
                "Anchovy",
                "Salmon",
                "Shellfish",
                "Tuna",
                "Algae",
                "Seaweed",
                "Trout",
                "Milk",
                "RawHides",
                "Pork",
                "Fruits",
                "Vegetables",
                "Wool",
                "Cotton",
                "Cows",
                "HighlandCows",
                "Sheep",
                "Pigs",
                "ProcessedVegetableOil",
                "LiquidConcentrates",
                "ChemicalProducts",
                "Leather",
                "FoodProducts",
                "BeverageProducts",
                "BakedGoods",
                "CannedFish",
                "Furnitures",
                "ElectronicProducts",
                "IndustrialSteel",
                "Tupperware",
                "Toys",
                "PrintedProducts",
                "TissuePaper",
                "Cloths",
                "PetroleumProducts",
                "Cars",
                "Footwear",
                "HouseParts",
                "Ship",
                "ConstructionResources",
                "OperationResources",
                "MealsLow",
                "MealsMedium",
                "MealsHigh",
                "PoliceVanCriminalMove",
                "PrisonHelicopterCriminalPickup",
                "PrisonHelicopterCriminalMove",
                "CarRent",
                "CarBuy",
                "CarSell",
                "VehicleFuel",
                "VehicleFuelElectric",
                "VehicleWash",
                "VehicleMinorRepair",
                "VehicleMajorRepair",
                "VehicleOutOfFuel",
                "VehicleBrokenDown"
            ];
        }

        public static string GetTransferReasonName(int transferInt)
        {
            return transferInt switch
            {
                150 => "MealsDeliveryLow",
                151 => "MealsDeliveryMedium",
                152 => "MealsDeliveryHigh",
                153 => "Anchovy",
                154 => "Salmon",
                155 => "Shellfish",
                156 => "Tuna",
                157 => "Algae",
                158 => "Seaweed",
                159 => "Trout",
                160 => "Milk",
                161 => "RawHides",
                162 => "Pork",
                163 => "Fruits",
                164 => "Vegetables",
                165 => "Wool",
                166 => "Cotton",
                167 => "Cows",
                168 => "HighlandCows",
                169 => "Sheep",
                170 => "Pigs",
                171 => "ProcessedVegetableOil",
                172 => "LiquidConcentrates",
                173 => "ChemicalProducts",
                174 => "Leather",
                175 => "FoodProducts",
                176 => "BeverageProducts",
                177 => "BakedGoods",
                178 => "CannedFish",
                179 => "Furnitures",
                180 => "ElectronicProducts",
                181 => "IndustrialSteel",
                182 => "Tupperware",
                183 => "Toys",
                184 => "PrintedProducts",
                185 => "TissuePaper",
                186 => "Cloths",
                187 => "PetroleumProducts",
                188 => "Cars",
                189 => "Footwear",
                190 => "HouseParts",
                191 => "Ship",
                192 => "ConstructionResources",
                193 => "OperationResources",
                220 => "MealsLow",
                221 => "MealsMedium",
                222 => "MealsHigh",
                223 => "PoliceVanCriminalMove",
                224 => "PrisonHelicopterCriminalPickup",
                225 => "PrisonHelicopterCriminalMove",
                226 => "CarRent",
                227 => "CarBuy",
                228 => "CarSell",
                229 => "VehicleFuel",
                230 => "VehicleFuelElectric",
                231 => "VehicleWash",
                232 => "VehicleMinorRepair",
                233 => "VehicleMajorRepair",
                234 => "VehicleOutOfFuel",
                235 => "VehicleBrokenDown",
                _ => null
            }; 
        }

        public static string GetTransferReasonDescription(int transferInt)
        {
            return transferInt switch
            {
                153 => "Anchovy is gathered by Anchovy fish harbor.",
                154 => "Salmon is gathered by Salmon fish harbor.",
                155 => "Shellfish is gathered by Shellfish fish harbor.",
                156 => "Tuna is gathered by Tuna fish harbor.",
                157 => "Algae is gathered by Algae fish farm.",
                158 => "Seaweed is gathered by Seaweed fish farm.",
                159 => "Trout is gathered by Trout fish farm.",
                160 => "Milk is produced by Milking Parlours.",
                161 => "RawHides are produced by Slaughterhouses to create Leather.",
                162 => "Pork is produced by Slaughterhouses.",
                163 => "Fruits are produced by Fruit Fields.",
                164 => "Vegetables are produced by Potatoes Fields, Corn Fields and Greeenhouses.",
                165 => "Wool is produced from Sheep in Animal Pastures.",
                166 => "Cotton is produced by Cotton Fields.",
                171 => "Processed Vegetable Oil is producted in a Vegetable Oil Mill and require Crops and Vegetables.",
                172 => "Liquid Concentrates is producted in a Pressing Plant and require Fruits and Vegetables.",
                173 => "Chemical Products are producted in a Chemical Plant and require Processed Vegetable Oil, Petroleum and Metals.",
                174 => "Leather is producted in a Tannery and require Raw Hides and Chemical Products.",
                175 => "FoodProducts are producted in a Food Factory and require Red Meat/Pork, Flour, Milk, Processed Vegetable Oil, Vegetables/Fruits, Paper and Plastics.",
                176 => "BeverageProducts are producted in a Beverage Factory and require Liquid Concentrates/Milk, Crops, Glass and Plastics.",
                177 => "BakedGoods are producted in a Bakery and require Flour, Milk and Fruits.",
                178 => "CannedFish is producted in a Seafood Factory and require Salmon/Tuna/Trout, Processed Vegetable Oil, Algae/Seaweed, Plastics and Metals.",
                179 => "Furnitures are producted in a Furniture Factory and require Planed Timber, Leather/Cotton, Chemical Products and Paper.",
                180 => "ElectronicProducts are producted in a Electronics Factory and require Metals, Glass and Plastics.",
                181 => "IndustrialSteel is producted in a Industrial Steel Plant and require Metals.",
                182 => "Tupperware is producted in a Household Plastic Factory and require Chemical Products, Processed Vegetable Oil and Plastics.",
                183 => "Toys are producted in a Toy Factory and require Planed Timber, Cotton/Wool, Chemical Products and Plastics.",
                184 => "PrintedProducts are producted in a Printing Press and require Paper, Chemical Products, Processed Vegetable Oil and Plastics.",
                185 => "TissuePaper is producted in a Soft Paper Factory and require Cotton, Paper, Chemical Products and Plastics.",
                186 => "Cloths are producted in a Clothing Factory and require Cotton/Wool, Leather and Plastics/Paper.",
                187 => "PetroleumProducts are producted in a Petroleum Refinery and require Metals, Patroleum and Plastics.",
                188 => "Cars are producted in a Car Factory and require Metals, Leather, Plastics, Chemical Products and Glass.",
                189 => "Footwear is producted in a Sneaker Factory and require Planed Timber, Cotton/Leather, Plastics and Chemical Products.",
                190 => "HouseParts are producted in a Modular House Factory and require Chemical Products, Metals/Planed Timber, Paper/Plastics and Glass.",
                191 => "Ship is producted in a Shipyard and require Planed Timber/Metals, Plastics/Glass, Chemical Products and Leather / Cotton.",
                _ => null
            };
        }

        public override void UpdateData(SimulationManager.UpdateMode mode)
        {
            Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginLoading("ExtendedTransferManager.UpdateData");
            base.UpdateData(mode);
            VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
            CitizenManager citizenManager = Singleton<CitizenManager>.instance;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;

            TransferManager instance = Singleton<TransferManager>.instance;

            var m_outgoingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);

            for (int i = 150; i < TransferReasonCount; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int num = i * 8 + j;
                    int num2 = m_incomingCount[num];
                    for (int num3 = num2 - 1; num3 >= 0; num3--)
                    {
                        int num4 = num * 256 + num3;
                        bool flag = false;
                        switch (m_incomingOffers[num4].m_object.Type)
                        {
                            case InstanceType.Vehicle:
                                flag = vehicleManager.m_vehicles.m_buffer[m_incomingOffers[num4].m_object.Vehicle].Info is null;
                                break;
                            case InstanceType.Citizen:
                                {
                                    uint citizen = m_incomingOffers[num4].m_object.Citizen;
                                    flag = citizenManager.m_citizens.m_buffer[citizen].GetCitizenInfo(citizen) is null;
                                    break;
                                }
                            case InstanceType.Building:
                                flag = buildingManager.m_buildings.m_buffer[m_incomingOffers[num4].m_object.Building].Info is null;
                                break;
                        }
                        if (flag)
                        {
                            m_incomingAmount[i] -= m_incomingOffers[num4].Amount;
                            ref TransferOffer reference = ref m_incomingOffers[num4];
                            reference = m_incomingOffers[num * 256 + --num2];
                        }
                    }
                    m_incomingCount[num] = (ushort)num2;
                    int num5 = m_outgoingCount[num];
                    for (int num6 = num5 - 1; num6 >= 0; num6--)
                    {
                        int num7 = num * 256 + num6;
                        bool flag2 = false;
                        switch (m_outgoingOffers[num7].m_object.Type)
                        {
                            case InstanceType.Vehicle:
                                flag2 = vehicleManager.m_vehicles.m_buffer[m_outgoingOffers[num7].m_object.Vehicle].Info is null;
                                break;
                            case InstanceType.Citizen:
                                {
                                    uint citizen2 = m_outgoingOffers[num7].m_object.Citizen;
                                    flag2 = citizenManager.m_citizens.m_buffer[citizen2].GetCitizenInfo(citizen2) is null;
                                    break;
                                }
                            case InstanceType.Building:
                                flag2 = buildingManager.m_buildings.m_buffer[m_outgoingOffers[num7].m_object.Building].Info is null;
                                break;
                        }
                        if (flag2)
                        {
                            m_outgoingAmount[i] -= m_outgoingOffers[num7].Amount;
                            ref TransferOffer reference2 = ref m_outgoingOffers[num7];
                            reference2 = m_outgoingOffers[num * 256 + --num5];
                        }
                    }
                    m_outgoingCount[num] = (ushort)num5;
                }
            }
            Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndLoading();
        }

    }
}
