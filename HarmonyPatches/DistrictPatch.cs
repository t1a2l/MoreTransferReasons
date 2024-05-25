using ColossalFramework;
using HarmonyLib;
using MoreTransferReasons.AI;
using static DistrictPark;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class DistrictPatch
    {
        [HarmonyPatch(typeof(DistrictPark), "SimulationStep")]
        [HarmonyPostfix]
        public static void SimulationStep(DistrictPark __instance, byte parkID)
        {
            ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
            if (__instance.m_parkType == ParkType.None)
            {
                instance2.m_industryParks.m_buffer[parkID].BaseSimulationStep();
            }

            switch (__instance.m_parkType)
            {
                case ParkType.None:
                    instance2.m_industryParks.m_buffer[parkID].BaseSimulationStep();
                    break;

                case ParkType.Industry:
                    instance2.m_industryParks.m_buffer[parkID].IndustrySimulationStep();
                    break;

                case ParkType.PedestrianZone:
                    instance2.m_industryParks.m_buffer[parkID].PedestrianZoneSimulationStep(__instance, parkID);
                    break;
            } 
        }

        [HarmonyPatch(typeof(DistrictPark), nameof(DistrictPark.totalCargoFlow), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool TotalCargoFlow(DistrictPark __instance, ref uint __result)
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
                    case ExtendedTransferManager.TransferReason.SheepMilk:
                    case ExtendedTransferManager.TransferReason.CowMilk:
                    case ExtendedTransferManager.TransferReason.HighlandCowMilk:
                    case ExtendedTransferManager.TransferReason.LambMeat:
                    case ExtendedTransferManager.TransferReason.BeefMeat:
                    case ExtendedTransferManager.TransferReason.HighlandBeefMeat:
                    case ExtendedTransferManager.TransferReason.PorkMeat:
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
