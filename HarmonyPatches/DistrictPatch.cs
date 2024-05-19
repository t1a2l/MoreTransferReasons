using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using static DistrictPark;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class DistrictPatch
    {
        public static Array8<ExtendedDistrictPark> IndustryParks;

        [HarmonyPatch(typeof(DistrictManager), "Awake")]
        [HarmonyPostfix]
        public static void Awake()
        {
            IndustryParks = new Array8<ExtendedDistrictPark>(128u);
        }

        [HarmonyPatch(typeof(DistrictManager), "CreatePark")]
        [HarmonyPostfix]
        public static void CreatePark(DistrictManager __instance, ref byte park, DistrictPark.ParkType type, DistrictPark.ParkLevel level, ref bool __result)
        {
            if(__result)
            {
                IndustryParks.m_buffer[park].m_milkData = default;
                IndustryParks.m_buffer[park].m_fruitsData = default;
                IndustryParks.m_buffer[park].m_vegetablesData = default;
                IndustryParks.m_buffer[park].m_cowsData = default;
                IndustryParks.m_buffer[park].m_highlandCowsData = default;
                IndustryParks.m_buffer[park].m_sheepData = default;
                IndustryParks.m_buffer[park].m_pigsData = default;
                IndustryParks.m_buffer[park].m_foodProductsData = default;
                IndustryParks.m_buffer[park].m_beverageProductsData = default;
                IndustryParks.m_buffer[park].m_bakedGoodsData = default;
                IndustryParks.m_buffer[park].m_cannedFishData = default;
                IndustryParks.m_buffer[park].m_furnituresData = default;
                IndustryParks.m_buffer[park].m_electronicProductsData = default;
                IndustryParks.m_buffer[park].m_industrialSteelData = default;
                IndustryParks.m_buffer[park].m_tupperwareData = default;
                IndustryParks.m_buffer[park].m_toysData = default;
                IndustryParks.m_buffer[park].m_printedProductsData = default;
                IndustryParks.m_buffer[park].m_tissuePaperData = default;
                IndustryParks.m_buffer[park].m_clothsData = default;
                IndustryParks.m_buffer[park].m_petroleumProductsData = default;
                IndustryParks.m_buffer[park].m_carsData = default;
                IndustryParks.m_buffer[park].m_footwearData = default;
                IndustryParks.m_buffer[park].m_housePartsData = default;
                IndustryParks.m_buffer[park].m_shipData = default;
                IndustryParks.m_buffer[park].m_woolData = default;
                IndustryParks.m_buffer[park].m_cottonData = default;
                if(type == ParkType.PedestrianZone)
                {
                    IndustryParks.m_buffer[park].m_tempIncome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];
                    IndustryParks.m_buffer[park].m_tempOutcome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];

                    IndustryParks.m_buffer[park].m_extendedMaterialRequest = (from i in Enumerable.Range(0, ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                              select new Queue<ushort>()).ToArray();
                    IndustryParks.m_buffer[park].m_extendedMaterialSuggestion = (from i in Enumerable.Range(0, ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                                 select new Queue<ushort>()).ToArray();
                }
            }
        }

        [HarmonyPatch(typeof(DistrictPark), "SimulationStep")]
        [HarmonyPostfix]
        public static void SimulationStep(DistrictPark __instance, byte parkID)
        {
            if (__instance.m_parkType == ParkType.None)
            {
                IndustryParks.m_buffer[parkID].BaseSimulationStep();
            }

            switch (__instance.m_parkType)
            {
                case ParkType.None:
                    IndustryParks.m_buffer[parkID].BaseSimulationStep();
                    break;

                case ParkType.Industry:
                    IndustryParks.m_buffer[parkID].IndustrySimulationStep();
                    break;

                case ParkType.PedestrianZone:
                    IndustryParks.m_buffer[parkID].PedestrianZoneSimulationStep(__instance, parkID);
                    break;
            }
        }

        [HarmonyPatch(typeof(DistrictPark), nameof(DistrictPark.totalCargoFlow), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool TotalCargoFlow(DistrictPark __instance, byte parkID, ref uint __result)
        {
            uint num = 0u;
            for (int i = 0; i < pedestrianReasonsCount; i++)
            {
                switch (kPedestrianZoneTransferReasons[i].m_material)
                {
                    case TransferManager.TransferReason.Oil:
                    case TransferManager.TransferReason.Ore:
                    case TransferManager.TransferReason.Logs:
                    case TransferManager.TransferReason.Grain:
                    case TransferManager.TransferReason.Goods:
                    case TransferManager.TransferReason.Coal:
                    case TransferManager.TransferReason.Petrol:
                    case TransferManager.TransferReason.Food:
                    case TransferManager.TransferReason.Lumber:
                    case TransferManager.TransferReason.LuxuryProducts:
                    case TransferManager.TransferReason.Fish:
                        num += __instance.m_finalIncome[i] + __instance.m_finalOutcome[i];
                        break;
                    case TransferManager.TransferReason.Mail:
                        num += (__instance.m_finalIncome[i] + __instance.m_finalOutcome[i]) / 1000;
                        break;
                }
            }
            for (int i = 0; i < ExtendedDistrictPark.pedestrianExtendedReasonsCount; i++)
            {
                switch (ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[i].m_material)
                {
                    case ExtendedTransferManager.TransferReason.Anchovy:
                    case ExtendedTransferManager.TransferReason.Salmon:
                    case ExtendedTransferManager.TransferReason.Shellfish:
                    case ExtendedTransferManager.TransferReason.Tuna:
                    case ExtendedTransferManager.TransferReason.Algae:
                    case ExtendedTransferManager.TransferReason.Seaweed:
                    case ExtendedTransferManager.TransferReason.Trout:
                    case ExtendedTransferManager.TransferReason.Milk:
                    case ExtendedTransferManager.TransferReason.Fruits:
                    case ExtendedTransferManager.TransferReason.Vegetables:
                    case ExtendedTransferManager.TransferReason.Cows:
                    case ExtendedTransferManager.TransferReason.HighlandCows:
                    case ExtendedTransferManager.TransferReason.Sheep:
                    case ExtendedTransferManager.TransferReason.Pigs:
                    case ExtendedTransferManager.TransferReason.Wool:
                    case ExtendedTransferManager.TransferReason.Cotton:
                    case ExtendedTransferManager.TransferReason.FoodProducts:
                    case ExtendedTransferManager.TransferReason.BeverageProducts:
                    case ExtendedTransferManager.TransferReason.BakedGoods:
                    case ExtendedTransferManager.TransferReason.CannedFish:
                    case ExtendedTransferManager.TransferReason.Furnitures:
                    case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    case ExtendedTransferManager.TransferReason.Tupperware:
                    case ExtendedTransferManager.TransferReason.Toys:
                    case ExtendedTransferManager.TransferReason.PrintedProducts:
                    case ExtendedTransferManager.TransferReason.TissuePaper:
                    case ExtendedTransferManager.TransferReason.Cloths:
                    case ExtendedTransferManager.TransferReason.Footwear:
                    case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    case ExtendedTransferManager.TransferReason.Cars:
                    case ExtendedTransferManager.TransferReason.HouseParts:
                        num += __instance.m_finalIncome[i] + __instance.m_finalOutcome[i];
                        break;
                }
            }
            __result = num;
            return false;
        }

    }
}
