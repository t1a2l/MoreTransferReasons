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
            "BeafMeat",
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
            "HighlandBeafMeat",
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

    }
}
