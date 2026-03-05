using UnityEngine;

namespace MoreTransferReasons.Utils
{
    public static class AtlasUtils
    {
        public static string[] SpriteNames =
        [
            "Algae",
            "Anchovy",
            "BakedGoods",
            "BeverageProducts",
            "CannedFish",
            "Cars",
            "ChemicalProducts",
            "Cloths",
            "Cotton",
            "Cows",
            "ElectronicProducts",
            "FishMeal",
            "FishOil",
            "FoodProducts",
            "Footwear",
            "Fruits",
            "Furnitures",
            "HighlandCows",
            "HouseParts",
            "IndustrialSteel",
            "Leather",
            "LiquidConcentrates",
            "Milk",
            "MixedResources",
            "Mussels",
            "PetroleumProducts",
            "Pigs",
            "Pork",
            "PrintedProducts",
            "ProcessedVegetableOil",
            "RawHides",
            "Salmon",
            "Seaweed",
            "Sheep",
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
                    TextureUtils.AddSpriteToAtlas(new Rect(32 * i, 1, 32, 32), SpriteNames[i], "MoreTransferReasonsAtlas");
                }
            }
        }

        public static string GetSpriteName(TransferManager.TransferReason transferReason, bool isStorageBuilding = false)
        {
            if (transferReason < ExtendedTransferManager.MealsDeliveryLow)
            {
                return IndustryWorldInfoPanel.ResourceSpriteName(transferReason, isStorageBuilding);
            }

            switch (transferReason)
            {
                case ExtendedTransferManager.Algae:
                    return "Algae";
                case ExtendedTransferManager.Anchovy:
                    return "Anchovy";
                case ExtendedTransferManager.BakedGoods:
                    return "BakedGoods";
                case ExtendedTransferManager.BeverageProducts:
                    return "BeverageProducts";
                case ExtendedTransferManager.CannedFish:
                    return "CannedFish";
                case ExtendedTransferManager.Cars:
                    return "Cars";
                case ExtendedTransferManager.ChemicalProducts:
                    return "ChemicalProducts";
                case ExtendedTransferManager.Cloths:
                    return "Cloths";
                case ExtendedTransferManager.Cotton:
                    return "Cotton";
                case ExtendedTransferManager.Cows:
                    return "Cows";
                case ExtendedTransferManager.ElectronicProducts:
                    return "ElectronicProducts";
                case ExtendedTransferManager.FishMeal:
                    return "FishMeal";
                case ExtendedTransferManager.FishOil:
                    return "FishOil";
                case ExtendedTransferManager.FoodProducts:
                    return "FoodProducts";
                case ExtendedTransferManager.Footwear:
                    return "Footwear";
                case ExtendedTransferManager.Fruits:
                    return "Fruits";
                case ExtendedTransferManager.Furnitures:
                    return "Furnitures";
                case ExtendedTransferManager.HighlandCows:
                    return "HighlandCows";
                case ExtendedTransferManager.HouseParts:
                    return "HouseParts";
                case ExtendedTransferManager.IndustrialSteel:
                    return "IndustrialSteel";
                case ExtendedTransferManager.Leather:
                    return "Leather";
                case ExtendedTransferManager.LiquidConcentrates:
                    return "LiquidConcentrates";
                case ExtendedTransferManager.Milk:
                    return "Milk";
                case ExtendedTransferManager.Mussels:
                    return "Mussels";
                case ExtendedTransferManager.PetroleumProducts:
                    return "PetroleumProducts";
                case ExtendedTransferManager.Pigs:
                    return "Pigs";
                case ExtendedTransferManager.Pork:
                    return "Pork";
                case ExtendedTransferManager.PrintedProducts:
                    return "PrintedProducts";
                case ExtendedTransferManager.ProcessedVegetableOil:
                    return "ProcessedVegetableOil";
                case ExtendedTransferManager.RawHides:
                    return "RawHides";
                case ExtendedTransferManager.Salmon:
                    return "Salmon";
                case ExtendedTransferManager.Seaweed:
                    return "Seaweed";
                case ExtendedTransferManager.Sheep:
                    return "Sheep";
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
