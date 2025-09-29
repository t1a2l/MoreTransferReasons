using System.Linq;
using HarmonyLib;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class WarehouseWorldInfoPanelPatch
    {
        [HarmonyPatch(typeof(WarehouseWorldInfoPanel), "Start")]
        [HarmonyPostfix]
        public static void Start(WarehouseWorldInfoPanel __instance, ref TransferManager.TransferReason[] ___m_transferReasons)
        {
            var transferReasons = ___m_transferReasons.ToList();

            transferReasons.Add(ExtendedTransferManager.MealsDeliveryLow);
            transferReasons.Add(ExtendedTransferManager.MealsDeliveryMedium);
            transferReasons.Add(ExtendedTransferManager.MealsDeliveryHigh);
            transferReasons.Add(ExtendedTransferManager.Anchovy);
            transferReasons.Add(ExtendedTransferManager.Salmon);
            transferReasons.Add(ExtendedTransferManager.Shellfish);
            transferReasons.Add(ExtendedTransferManager.Tuna);
            transferReasons.Add(ExtendedTransferManager.Algae);
            transferReasons.Add(ExtendedTransferManager.Seaweed);
            transferReasons.Add(ExtendedTransferManager.Trout);
            transferReasons.Add(ExtendedTransferManager.SheepMilk);
            transferReasons.Add(ExtendedTransferManager.CowMilk);
            transferReasons.Add(ExtendedTransferManager.HighlandCowMilk);
            transferReasons.Add(ExtendedTransferManager.LambMeat);
            transferReasons.Add(ExtendedTransferManager.BeefMeat);
            transferReasons.Add(ExtendedTransferManager.HighlandBeefMeat);
            transferReasons.Add(ExtendedTransferManager.PorkMeat);
            transferReasons.Add(ExtendedTransferManager.Fruits);
            transferReasons.Add(ExtendedTransferManager.Vegetables);
            transferReasons.Add(ExtendedTransferManager.Wool);
            transferReasons.Add(ExtendedTransferManager.Cotton);
            transferReasons.Add(ExtendedTransferManager.Cows);
            transferReasons.Add(ExtendedTransferManager.HighlandCows);
            transferReasons.Add(ExtendedTransferManager.Sheep);
            transferReasons.Add(ExtendedTransferManager.Pigs);
            transferReasons.Add(ExtendedTransferManager.ProcessedVegetableOil);
            transferReasons.Add(ExtendedTransferManager.Leather);
            transferReasons.Add(ExtendedTransferManager.FoodProducts);
            transferReasons.Add(ExtendedTransferManager.BeverageProducts);
            transferReasons.Add(ExtendedTransferManager.BakedGoods);
            transferReasons.Add(ExtendedTransferManager.CannedFish);
            transferReasons.Add(ExtendedTransferManager.Furnitures);
            transferReasons.Add(ExtendedTransferManager.ElectronicProducts);
            transferReasons.Add(ExtendedTransferManager.IndustrialSteel);
            transferReasons.Add(ExtendedTransferManager.Tupperware);
            transferReasons.Add(ExtendedTransferManager.Toys);
            transferReasons.Add(ExtendedTransferManager.PrintedProducts);
            transferReasons.Add(ExtendedTransferManager.TissuePaper);
            transferReasons.Add(ExtendedTransferManager.Cloths);
            transferReasons.Add(ExtendedTransferManager.PetroleumProducts);
            transferReasons.Add(ExtendedTransferManager.Cars);
            transferReasons.Add(ExtendedTransferManager.Footwear);
            transferReasons.Add(ExtendedTransferManager.HouseParts);
            ___m_transferReasons = [.. transferReasons];
        }
    }
}
