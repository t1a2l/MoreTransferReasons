using UnityEngine;

namespace MoreTransferReasons.Utils
{
    class AtlasUtils
    {
        public static string[] SpriteNames =
        [
            "Algae",
            "Anchovy",
            "BakedGoods",
            "BeefMeat",
            "BeverageProducts",
            "CannedFish",
            "Cars",
            "Cloths",
            "Cotton",
            "Cows",
            "CowMilk",
            "ElectronicProducts",
            "FoodProducts",
            "Footwear",
            "Fruits",
            "Furnitures",
            "HighlandBeefMeat",
            "HighlandCowMilk",
            "HighlandCows",
            "HouseParts",
            "IndustrialSteel",
            "LambMeat",
            "Leather",
            "PetroleumProducts",
            "Pigs",
            "PorkMeat",
            "PrintedProducts",
            "ProcessedVegetableOil",
            "Salmon",
            "Seaweed",
            "Sheep",
            "SheepMilk",
            "Shellfish",
            "Ship",
            "TissuePaper",
            "Toys",
            "Trout",
            "Tuna",
            "Tupperware",
            "Vegetables",
            "Wool"
        ];


        public static void CreateAtlas()
        {
            if (TextureUtils.GetAtlas("MoreTransferReasonsAtlas") == null)
            {
                TextureUtils.InitialiseAtlas("MoreTransferReasonsAtlas");
                for (int i = 0; i < SpriteNames.Length; i++)
                {
                    TextureUtils.AddSpriteToAtlas(new Rect(34 * i, 1, 32, 32), SpriteNames[i], "MoreTransferReasonsAtlas");
                }
            }
        }

        public static string GetSpriteName(TransferManager.TransferReason transferReason)
        {
            if (transferReason < ExtendedTransferManager.MealsDeliveryLow)
            {
                return IndustryWorldInfoPanel.ResourceSpriteName(transferReason);
            }

            switch (transferReason)
            {
                case ExtendedTransferManager.Algae:
                    return "Algae";
                case ExtendedTransferManager.Anchovy:
                    return "Anchovy";
                case ExtendedTransferManager.BakedGoods:
                    return "BakedGoods";
                case ExtendedTransferManager.BeefMeat:
                    return "BeefMeat";
                case ExtendedTransferManager.BeverageProducts:
                    return "BeverageProducts";
                case ExtendedTransferManager.CannedFish:
                    return "CannedFish";
                case ExtendedTransferManager.Cars:
                    return "Cars";
                case ExtendedTransferManager.Cloths:
                    return "Cloths";
                case ExtendedTransferManager.Cotton:
                    return "Cotton";
                case ExtendedTransferManager.Cows:
                    return "Cows";
                case ExtendedTransferManager.CowMilk:
                    return "CowMilk";
                case ExtendedTransferManager.ElectronicProducts:
                    return "ElectronicProducts";
                case ExtendedTransferManager.FoodProducts:
                    return "FoodProducts";
                case ExtendedTransferManager.Footwear:
                    return "Footwear";
                case ExtendedTransferManager.Fruits:
                    return "Fruits";
                case ExtendedTransferManager.Furnitures:
                    return "Furnitures";
                case ExtendedTransferManager.HighlandBeefMeat:
                    return "HighlandBeefMeat";
                case ExtendedTransferManager.HighlandCowMilk:
                    return "HighlandCowMilk";
                case ExtendedTransferManager.HighlandCows:
                    return "HighlandCows";
                case ExtendedTransferManager.HouseParts:
                    return "HouseParts";
                case ExtendedTransferManager.IndustrialSteel:
                    return "IndustrialSteel";
                case ExtendedTransferManager.LambMeat:
                    return "LambMeat";
                case ExtendedTransferManager.Leather:
                    return "Leather";
                case ExtendedTransferManager.PetroleumProducts:
                    return "PetroleumProducts";
                case ExtendedTransferManager.Pigs:
                    return "Pigs";
                case ExtendedTransferManager.PorkMeat:
                    return "PorkMeat";
                case ExtendedTransferManager.PrintedProducts:
                    return "PrintedProducts";
                case ExtendedTransferManager.ProcessedVegetableOil:
                    return "ProcessedVegetableOil";
                case ExtendedTransferManager.Salmon:
                    return "Salmon";
                case ExtendedTransferManager.Seaweed:
                    return "Seaweed";
                case ExtendedTransferManager.Sheep:
                    return "Sheep";
                case ExtendedTransferManager.SheepMilk:
                    return "SheepMilk";
                case ExtendedTransferManager.Shellfish:
                    return "Shellfish";
                case ExtendedTransferManager.Ship:
                    return "Ship";
                case ExtendedTransferManager.TissuePaper:
                    return "TissuePaper";
                case ExtendedTransferManager.Toys:
                    return "Toys";
                case ExtendedTransferManager.Trout:
                    return "Trout";
                case ExtendedTransferManager.Tuna:
                    return "Tuna";
                case ExtendedTransferManager.Tupperware:
                    return "Tupperware";
                case ExtendedTransferManager.Vegetables:
                    return "Vegetables";
                case ExtendedTransferManager.Wool:
                    return "Wool";
                default:   
                    return "";
            }
        }
    }
}
