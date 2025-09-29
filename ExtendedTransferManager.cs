namespace MoreTransferReasons
{
    public class ExtendedTransferManager : TransferManager
    {
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

        public const TransferReason SheepMilk = (TransferReason)160;

        public const TransferReason CowMilk = (TransferReason)161;

        public const TransferReason HighlandCowMilk = (TransferReason)162;

        public const TransferReason LambMeat = (TransferReason)163;

        public const TransferReason BeefMeat = (TransferReason)164;

        public const TransferReason HighlandBeefMeat = (TransferReason)165;

        public const TransferReason PorkMeat = (TransferReason)166;

        public const TransferReason Fruits = (TransferReason)167;

        public const TransferReason Vegetables = (TransferReason)168;

        public const TransferReason Wool = (TransferReason)169;

        public const TransferReason Cotton = (TransferReason)170;

        public const TransferReason Cows = (TransferReason)171;

        public const TransferReason HighlandCows = (TransferReason)172;

        public const TransferReason Sheep = (TransferReason)173;

        public const TransferReason Pigs = (TransferReason)174;

        public const TransferReason ProcessedVegetableOil = (TransferReason)175;

        public const TransferReason Leather = (TransferReason)176;

        public const TransferReason FoodProducts = (TransferReason)177;

        public const TransferReason BeverageProducts = (TransferReason)178;

        public const TransferReason BakedGoods = (TransferReason)179;

        public const TransferReason CannedFish = (TransferReason)180;

        public const TransferReason Furnitures = (TransferReason)181;

        public const TransferReason ElectronicProducts = (TransferReason)182;

        public const TransferReason IndustrialSteel = (TransferReason)183;

        public const TransferReason Tupperware = (TransferReason)184;

        public const TransferReason Toys = (TransferReason)185;

        public const TransferReason PrintedProducts = (TransferReason)186;

        public const TransferReason TissuePaper = (TransferReason)187;

        public const TransferReason Cloths = (TransferReason)188;

        public const TransferReason PetroleumProducts = (TransferReason)189;

        public const TransferReason Cars = (TransferReason)190;

        public const TransferReason Footwear = (TransferReason)191;

        public const TransferReason HouseParts = (TransferReason)192;

        public const TransferReason Ship = (TransferReason)193;

        public const TransferReason ConstructionResources = (TransferReason)194;

        public const TransferReason OperationResources = (TransferReason)195;

        //196 - 92, 197 - 94, 198 - 96, 199 - 98, 200 - 100, 201 - 102, 202 - 104, 203 - 106, 204 - 108, 205 - 110,
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
                20 => SheepMilk,
                22 => CowMilk,
                24 => HighlandCowMilk,
                26 => LambMeat,
                28 => BeefMeat,
                30 => HighlandBeefMeat,
                32 => PorkMeat,
                34 => Fruits,
                36 => Vegetables,
                38 => Wool,
                40 => Cotton,
                42 => Cows,
                44 => HighlandCows,
                46 => Sheep,
                48 => Pigs,
                50 => ProcessedVegetableOil,
                52 => Leather,
                54 => FoodProducts,
                56 => BeverageProducts,
                58 => BakedGoods,
                60 => CannedFish,
                62 => Furnitures,
                64 => ElectronicProducts,
                66 => IndustrialSteel,
                68 => Tupperware,
                70 => Toys,
                72 => PrintedProducts,
                74 => TissuePaper,
                76 => Cloths,
                78 => PetroleumProducts,
                80 => Cars,
                82 => Footwear,
                84 => HouseParts,
                86 => Ship,
                88 => ConstructionResources,
                90 => OperationResources,
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

    }
}
