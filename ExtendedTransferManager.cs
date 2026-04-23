using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;

namespace MoreTransferReasons
{
    public class ExtendedTransferManager : TransferManager
    {
        public const int TransferReasonCount = 240;

        public const TransferReason MealsDeliveryLow = (TransferReason)150;

        public const TransferReason MealsDeliveryMedium = (TransferReason)151;

        public const TransferReason MealsDeliveryHigh = (TransferReason)152;

        public const TransferReason Anchovy = (TransferReason)153;

        public const TransferReason Salmon = (TransferReason)154;

        public const TransferReason Shellfish = (TransferReason)155;

        public const TransferReason Tuna = (TransferReason)156;

        public const TransferReason Algae = (TransferReason)157;

        public const TransferReason Seaweed = (TransferReason)158;

        public const TransferReason Mussels = (TransferReason)159;

        public const TransferReason Trout = (TransferReason)160;

        public const TransferReason Milk = (TransferReason)161;

        public const TransferReason RawHides = (TransferReason)162;

        public const TransferReason Pork = (TransferReason)163;

        public const TransferReason Fruits = (TransferReason)164;

        public const TransferReason Vegetables = (TransferReason)165;

        public const TransferReason Wool = (TransferReason)166;

        public const TransferReason Cotton = (TransferReason)167;

        public const TransferReason Cows = (TransferReason)168;

        public const TransferReason HighlandCows = (TransferReason)169;

        public const TransferReason Sheep = (TransferReason)170;

        public const TransferReason Pigs = (TransferReason)171;

        public const TransferReason ProcessedVegetableOil = (TransferReason)172;

        public const TransferReason LiquidConcentrates = (TransferReason)173;

        public const TransferReason FishMeal = (TransferReason)174;

        public const TransferReason FishOil = (TransferReason)175;

        public const TransferReason ChemicalProducts = (TransferReason)176;

        public const TransferReason Leather = (TransferReason)177;

        public const TransferReason FoodProducts = (TransferReason)178;

        public const TransferReason BeverageProducts = (TransferReason)179;

        public const TransferReason BakedGoods = (TransferReason)180;

        public const TransferReason CannedFish = (TransferReason)181;

        public const TransferReason Furnitures = (TransferReason)182;

        public const TransferReason ElectronicProducts = (TransferReason)183;

        public const TransferReason IndustrialSteel = (TransferReason)184;

        public const TransferReason Tupperware = (TransferReason)185;

        public const TransferReason Toys = (TransferReason)186;

        public const TransferReason PrintedProducts = (TransferReason)187;

        public const TransferReason TissuePaper = (TransferReason)188;

        public const TransferReason Cloths = (TransferReason)189;

        public const TransferReason PetroleumProducts = (TransferReason)190;

        public const TransferReason Cars = (TransferReason)191;

        public const TransferReason Footwear = (TransferReason)192;

        public const TransferReason HouseParts = (TransferReason)193;

        public const TransferReason Ship = (TransferReason)194;

        public const TransferReason ConstructionResources = (TransferReason)195;

        public const TransferReason OperationResources = (TransferReason)196;

        // 197 - 94, 198 - 96, 199 - 98, 200 - 100, 201 - 102, 202 - 104, 203 - 106, 204 - 108, 205 - 110,
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

        public const TransferReason Crime2 = (TransferReason)236;

        public const TransferReason TaxiMove = (TransferReason)237;

        public const TransferReason Mail2 = (TransferReason)238;

        public const TransferReason IntercityBus = (TransferReason)239;

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
                18 => Mussels,
                20 => Trout,
                22 => Milk,
                24 => RawHides,
                26 => Pork,
                28 => Fruits,
                30 => Vegetables,
                32 => Wool,
                34 => Cotton,
                36 => Cows,
                38 => HighlandCows,
                40 => Sheep,
                42 => Pigs,
                44 => ProcessedVegetableOil,
                46 => LiquidConcentrates,
                48 => FishMeal,
                50 => FishOil,
                52 => ChemicalProducts,
                54 => Leather,
                56 => FoodProducts,
                58 => BeverageProducts,
                60 => BakedGoods,
                62 => CannedFish,
                64 => Furnitures,
                66 => ElectronicProducts,
                68 => IndustrialSteel,
                70 => Tupperware,
                72 => Toys,
                74 => PrintedProducts,
                76 => TissuePaper,
                78 => Cloths,
                80 => PetroleumProducts,
                82 => Cars,
                84 => Footwear,
                86 => HouseParts,
                88 => Ship,
                90 => ConstructionResources,
                92 => OperationResources,
                // 94 96 98 100 102 104 106 108
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
                172 => Crime2,
                174 => TaxiMove,
                176 => Mail2,
                178 => IntercityBus,
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
                "Mussels",
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
                "FishMeal",
                "FishOil",
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
                "VehicleBrokenDown",
                "Crime2",
                "TaxiMove",
                "Mail2",
                "IntercityBus"
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
                159 => "Mussels",
                160 => "Trout",
                161 => "Milk",
                162 => "RawHides",
                163 => "Pork",
                164 => "Fruits",
                165 => "Vegetables",
                166 => "Wool",
                167 => "Cotton",
                168 => "Cows",
                169 => "HighlandCows",
                170 => "Sheep",
                171 => "Pigs",
                172 => "ProcessedVegetableOil",
                173 => "LiquidConcentrates",
                174 => "FishMeal",
                175 => "FishOil",
                176 => "ChemicalProducts",
                177 => "Leather",
                178 => "FoodProducts",
                179 => "BeverageProducts",
                180 => "BakedGoods",
                181 => "CannedFish",
                182 => "Furnitures",
                183 => "ElectronicProducts",
                184 => "IndustrialSteel",
                185 => "Tupperware",
                186 => "Toys",
                187 => "PrintedProducts",
                188 => "TissuePaper",
                189 => "Cloths",
                190 => "PetroleumProducts",
                191 => "Cars",
                192 => "Footwear",
                193 => "HouseParts",
                194 => "Ship",
                195 => "ConstructionResources",
                196 => "OperationResources",
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
                236 => "Crime2",
                237 => "TaxiMove",
                238 => "Mail2",
                239 => "IntercityBus",
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
                159 => "Mussels is gathered by Mussel fish farm.",
                160 => "Trout is gathered by Trout fish farm.",
                161 => "Milk is produced by Milking Parlours.",
                162 => "RawHides are produced by Slaughterhouses to create Leather.",
                163 => "Pork is produced by Slaughterhouses.",
                164 => "Fruits are produced by Fruit Fields.",
                165 => "Vegetables are produced by Potatoes Fields, Corn Fields and Greeenhouses.",
                166 => "Wool is produced from Sheep in Animal Pastures.",
                167 => "Cotton is produced by Cotton Fields.",
                172 => "Processed Vegetable Oil is produced in a Vegetable Oil Mill and require Crops and Vegetables.",
                173 => "Liquid Concentrates is produced in a Pressing Plant and require Fruits and Vegetables.",
                174 => "Fish Meal is produced in a Fish Meal Factory from raw fish. Used as input for Fish Hatcheries.",
                175 => "Fish Oil is produced in a Fish Meal Factory as a byproduct of fish processing.",
                176 => "Chemical Products are produced in a Chemical Plant and require Processed Vegetable Oil, Petroleum and Metals.",
                177 => "Leather is produced in a Tannery and require Raw Hides and Chemical Products.",
                178 => "FoodProducts are produced in a Food Factory and require Red Meat/Pork, Flour, Milk, Processed Vegetable Oil, Vegetables/Fruits, Paper and Plastics.",
                179 => "BeverageProducts are produced in a Beverage Factory and require Liquid Concentrates/Milk, Crops, Glass and Plastics.",
                180 => "BakedGoods are produced in a Bakery and require Flour, Milk and Fruits.",
                181 => "CannedFish is produced in a Seafood Factory and require Salmon/Tuna/Trout, Processed Vegetable Oil, Algae/Seaweed, Plastics and Metals.",
                182 => "Furnitures are produced in a Furniture Factory and require Planed Timber, Leather/Cotton, Chemical Products and Paper.",
                183 => "ElectronicProducts are produced in a Electronics Factory and require Metals, Glass and Plastics.",
                184 => "IndustrialSteel is produced in a Industrial Steel Plant and require Metals.",
                185 => "Tupperware is produced in a Household Plastic Factory and require Chemical Products, Processed Vegetable Oil and Plastics.",
                186 => "Toys are produced in a Toy Factory and require Planed Timber, Cotton/Wool, Chemical Products and Plastics.",
                187 => "PrintedProducts are produced in a Printing Press and require Paper, Chemical Products, Processed Vegetable Oil and Plastics.",
                188 => "TissuePaper is produced in a Soft Paper Factory and require Cotton, Paper, Chemical Products and Plastics.",
                189 => "Cloths are produced in a Clothing Factory and require Cotton/Wool, Leather and Plastics/Paper.",
                190 => "PetroleumProducts are produced in a Petroleum Refinery and require Metals, Patroleum and Plastics.",
                191 => "Cars are produced in a Car Factory and require Metals, Leather, Plastics, Chemical Products and Glass.",
                192 => "Footwear is produced in a Sneaker Factory and require Planed Timber, Cotton/Leather, Plastics and Chemical Products.",
                193 => "HouseParts are produced in a Modular House Factory and require Chemical Products, Metals/Planed Timber, Paper/Plastics and Glass.",
                194 => "Ship is produced in a Shipyard and require Planed Timber/Metals, Plastics/Glass, Chemical Products and Leather / Cotton.",
                _ => null
            };
        }

        public static int GetResourcePrice(TransferReason material, ItemClass.Service sourceService = ItemClass.Service.None)
        {
            if (material < MealsDeliveryLow)
            {
                return IndustryBuildingAI.GetResourcePrice(material, sourceService);
            }
                
            return UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, material switch
            {
                // ── Raw agricultural (= Grain tier 200) ──────────────────
                Fruits => 200,
                Vegetables => 200,
                Cotton => 200,

                // ── Live animals (= Ore tier 300) ────────────────────────
                Cows => 300,
                HighlandCows => 300,
                Sheep => 300,
                Pigs => 300,

                // ── Raw animal products (= Oil tier 400) ─────────────────
                Milk => 400,
                RawHides => 400,
                Wool => 400,

                // ── Fish variants (= Fish tier 600) ──────────────────────
                Anchovy => 600,
                Salmon => 600,
                Shellfish => 600,
                Tuna => 600,
                Algae => 600,
                Seaweed => 600,
                Mussels => 600,
                Trout => 600,

                // ── Tier 1 processed (= AnimalProducts/Paper tier 1500) ──
                Pork => 1500,
                ProcessedVegetableOil => 1500,
                FoodProducts => 1500,
                BeverageProducts => 1500,
                BakedGoods => 1500,
                TissuePaper => 1500,
                PrintedProducts => 1500,
                Tupperware => 1500,

                // ── Tier 2 processed (= Glass/Metals tier 2250) ──────────
                IndustrialSteel => 2250,
                HouseParts => 2250,
                CannedFish => 2000,
                Leather => 2000,
                LiquidConcentrates => 2000,
                FishMeal => 2000,
                FishOil => 2000,
                Cloths => 2500,
                Footwear => 2500,

                // ── Tier 3 processed (= Petroleum/Plastics tier 3000) ────
                ChemicalProducts => 3000,
                PetroleumProducts => 3000,
                Furnitures => 3000,
                Toys => 3000,

                // ── High value (= LuxuryProducts tier 10000) ─────────────
                ElectronicProducts => 5000,
                Cars => 8000,
                Ship => 10000,

                _ => 0
            });
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
