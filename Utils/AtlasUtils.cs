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
            "BeverageProducts",
            "CannedFish",
            "Cars",
            "Cloths",
            "Cotton",
            "Cows",
            "ElectronicProducts",
            "FoodProducts",
            "Footwear",
            "Fruits",
            "Furnitures",
            "HighlandCows",
            "HouseParts",
            "IndustrialSteel",
            "Milk",
            "PetroleumProducts",
            "Pigs",
            "PrintedProducts",
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
                    TextureUtils.AddSpriteToAtlas(new Rect(34 * i, 1, 32, 32), SpriteNames[i], "MoreTransferReasonsAtlas");
                }
            }
        }

    }
}
