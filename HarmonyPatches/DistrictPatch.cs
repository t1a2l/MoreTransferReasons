using ColossalFramework.Math;
using ColossalFramework;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static DistrictPark;
using UnityEngine;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch]
    public static class DistrictPatch
    {
        [HarmonyPatch(typeof(DistrictManager), "CreatePark")]
        [HarmonyPrefix]
        public static bool CreatePark(DistrictManager __instance, out byte park, DistrictPark.ParkType type, DistrictPark.ParkLevel level, ref bool __result)
        {
            if (__instance.m_parks.CreateItem(out var item))
            {
                park = item;
                __instance.m_parks.m_buffer[park].m_flags = DistrictPark.Flags.Created;
                __instance.m_parks.m_buffer[park].m_totalAlpha = 0u;
                __instance.m_parks.m_buffer[park].m_randomSeed = Singleton<SimulationManager>.instance.m_randomizer.ULong64();
                __instance.m_parks.m_buffer[park].m_nameLocation = Vector3.zero;
                __instance.m_parks.m_buffer[park].m_nameSize = Vector2.zero;
                __instance.m_parks.m_buffer[park].m_mainGate = 0;
                __instance.m_parks.m_buffer[park].m_randomGate = 0;
                __instance.m_parks.m_buffer[park].m_condition = 0;
                __instance.m_parks.m_buffer[park].m_tempEntertainmentAccumulation = 0;
                __instance.m_parks.m_buffer[park].m_finalEntertainmentAccumulation = 0;
                __instance.m_parks.m_buffer[park].m_tempAttractivenessAccumulation = 0;
                __instance.m_parks.m_buffer[park].m_finalAttractivenessAccumulation = 0;
                __instance.m_parks.m_buffer[park].m_tempHappinessAccumulation = 0u;
                __instance.m_parks.m_buffer[park].m_finalHappinessAccumulation = 0u;
                __instance.m_parks.m_buffer[park].m_tempMaxHappinessAccumulation = 0u;
                __instance.m_parks.m_buffer[park].m_finalMaxHappinessAccumulation = 0u;
                __instance.m_parks.m_buffer[park].m_totalVisitorCount = 0u;
                __instance.m_parks.m_buffer[park].m_lastVisitorCount = 0u;
                __instance.m_parks.m_buffer[park].m_tempTicketIncome = 0u;
                __instance.m_parks.m_buffer[park].m_finalTicketIncome = 0u;
                __instance.m_parks.m_buffer[park].m_ticketPrice = GetDefaultTicketPrice(type);
                __instance.m_parks.m_buffer[park].m_tempResidentCount = 0;
                __instance.m_parks.m_buffer[park].m_finalResidentCount = 0;
                __instance.m_parks.m_buffer[park].m_tempTouristCount = 0;
                __instance.m_parks.m_buffer[park].m_finalTouristCount = 0;
                __instance.m_parks.m_buffer[park].m_parkPolicies = __instance.m_parks.m_buffer[0].m_parkPolicies;
                __instance.m_parks.m_buffer[park].m_eventPolicies = __instance.m_parks.m_buffer[0].m_eventPolicies;
                __instance.m_parks.m_buffer[park].m_specializationPolicies = __instance.m_parks.m_buffer[0].m_specializationPolicies;
                __instance.m_parks.m_buffer[park].m_parkPoliciesEffect = DistrictPolicies.Park.None;
                __instance.m_parks.m_buffer[park].m_parkType = DistrictPark.ParkType.None;
                __instance.m_parks.m_buffer[park].m_parkLevel = DistrictPark.ParkLevel.None;
                __instance.m_parks.m_buffer[park].m_tempGateCount = 0;
                __instance.m_parks.m_buffer[park].m_finalGateCount = 0;
                __instance.m_parks.m_buffer[park].m_tempVisitorCapacity = 0;
                __instance.m_parks.m_buffer[park].m_finalVisitorCapacity = 0;
                __instance.m_parks.m_buffer[park].m_tempMainCapacity = 0;
                __instance.m_parks.m_buffer[park].m_finalMainCapacity = 0;
                __instance.m_parks.m_buffer[park].m_dayNightCount = 0;
                __instance.m_parks.m_buffer[park].m_propCount = 0;
                __instance.m_parks.m_buffer[park].m_treeCount = 0u;
                __instance.m_parks.m_buffer[park].m_decorationCount = 0u;
                __instance.m_parks.m_buffer[park].m_decorationCount2 = 0u;
                __instance.m_parks.m_buffer[park].m_areaFactor = 0;
                __instance.m_parks.m_buffer[park].m_totalProductionAmount = 0uL;
                __instance.m_parks.m_buffer[park].m_tempWorkerCount = 0;
                __instance.m_parks.m_buffer[park].m_finalWorkerCount = 0;
                __instance.m_parks.m_buffer[park].m_grainData = default;
                __instance.m_parks.m_buffer[park].m_logsData = default;
                __instance.m_parks.m_buffer[park].m_oreData = default;
                __instance.m_parks.m_buffer[park].m_oilData = default;
                __instance.m_parks.m_buffer[park].m_animalProductsData = default;
                __instance.m_parks.m_buffer[park].m_floursData = default;
                __instance.m_parks.m_buffer[park].m_paperData = default;
                __instance.m_parks.m_buffer[park].m_planedTimberData = default;
                __instance.m_parks.m_buffer[park].m_petroleumData = default;
                __instance.m_parks.m_buffer[park].m_plasticsData = default;
                __instance.m_parks.m_buffer[park].m_glassData = default;
                __instance.m_parks.m_buffer[park].m_metalsData = default;
                __instance.m_parks.m_buffer[park].m_luxuryProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_milkData = default;
                ExtendedDistrictManager.IndustryParks[park].m_fruitsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_vegetablesData = default;
                ExtendedDistrictManager.IndustryParks[park].m_cowsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_highlandCowsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_sheepData = default;
                ExtendedDistrictManager.IndustryParks[park].m_pigsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_foodProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_beverageProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_bakedGoodsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_cannedFishData = default;
                ExtendedDistrictManager.IndustryParks[park].m_furnituresData = default;
                ExtendedDistrictManager.IndustryParks[park].m_electronicProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_industrialSteelData = default;
                ExtendedDistrictManager.IndustryParks[park].m_tupperwareData = default;
                ExtendedDistrictManager.IndustryParks[park].m_toysData = default;
                ExtendedDistrictManager.IndustryParks[park].m_printedProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_tissuePaperData = default;
                ExtendedDistrictManager.IndustryParks[park].m_clothsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_petroleumProductsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_carsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_footwearData = default;
                ExtendedDistrictManager.IndustryParks[park].m_housePartsData = default;
                ExtendedDistrictManager.IndustryParks[park].m_shipData = default;
                ExtendedDistrictManager.IndustryParks[park].m_woolData = default;
                ExtendedDistrictManager.IndustryParks[park].m_cottonData = default;
                __instance.m_parks.m_buffer[park].m_tempWorkEfficiencyDelta = 0;
                __instance.m_parks.m_buffer[park].m_tempStorageDelta = 0;
                __instance.m_parks.m_buffer[park].m_finalWorkEfficiencyDelta = 0;
                __instance.m_parks.m_buffer[park].m_finalStorageDelta = 0;
                __instance.m_parks.m_buffer[park].m_studentCount = 0u;
                __instance.m_parks.m_buffer[park].m_studentCapacity = 0u;
                __instance.m_parks.m_buffer[park].m_campusBuildingAttractiveness = 0u;
                __instance.m_parks.m_buffer[park].m_academicWorksData = default;
                __instance.m_parks.m_buffer[park].m_currentYearReportData = default;
                __instance.m_parks.m_buffer[park].m_lastYearReportData = default;
                __instance.m_parks.m_buffer[park].m_secondLastYearReportData = default;
                __instance.m_parks.m_buffer[park].m_ledger = new DistrictYearReportLedger();
                __instance.m_parks.m_buffer[park].m_isMainCampus = false;
                __instance.m_parks.m_buffer[park].m_grantType = 0;
                __instance.m_parks.m_buffer[park].m_coachCount = 0;
                __instance.m_parks.m_buffer[park].m_cheerleadingBudget = 0;
                __instance.m_parks.m_buffer[park].m_coachHireTimes = new DateTime[25];
                __instance.m_parks.m_buffer[park].m_winProbability = 0;
                Randomizer randomizer = Singleton<SimulationManager>.instance.m_randomizer;
                int num = randomizer.Int32(0, 6);
                __instance.m_parks.m_buffer[park].m_varsityIdentityIndex = num;
                __instance.m_parks.m_buffer[park].m_eventPolicies |= TeamToEventPolicy((Team)num);
                __instance.m_parks.m_buffer[park].m_arenas = [];
                __instance.m_parks.m_buffer[park].m_activeArenas = [];
                __instance.m_parks.m_buffer[park].SetVarsityColor(park, new Color32(242, 199, 1, byte.MaxValue));
                __instance.m_parks.m_buffer[park].m_lifetimeTrophies = 0;
                __instance.m_parks.m_buffer[park].m_staticVarsityAttractivenessModifier = 0;
                __instance.m_parks.m_buffer[park].m_dynamicVarsityAttractivenessModifier = 0;
                __instance.m_parks.m_buffer[park].m_sports = Sports.None;
                __instance.m_parks.m_buffer[park].m_campusBuildingCount = 0;
                __instance.m_parks.m_buffer[park].m_awayMatchesDone = new bool[5];
                __instance.m_parks.m_buffer[park].m_materialRequest = null;
                __instance.m_parks.m_buffer[park].m_materialSuggestion = null;
                __instance.m_parks.m_buffer[park].m_randomServicePoints = null;
                __instance.m_parks.m_buffer[park].m_tempServicePointList = null;
                __instance.m_parks.m_buffer[park].m_finalServicePointList = null;
                __instance.m_parks.m_buffer[park].m_resourceIndexes = null;
                __instance.m_parkCount = (int)(__instance.m_parks.ItemCount() - 1);
                __instance.SetParkTypeLevel(park, type, level);
                switch (type)
                {
                    case ParkType.Generic:
                    case ParkType.AmusementPark:
                    case ParkType.Zoo:
                    case ParkType.NatureReserve:
                        __instance.m_parkAreasNotUsed.Disable();
                        break;
                    case ParkType.Industry:
                    case ParkType.Farming:
                    case ParkType.Forestry:
                    case ParkType.Ore:
                    case ParkType.Oil:
                        __instance.m_industryAreasNotUsed.Disable();
                        break;
                    case ParkType.GenericCampus:
                    case ParkType.TradeSchool:
                    case ParkType.LiberalArts:
                    case ParkType.University:
                        __instance.m_campusAreasNotUsed.Disable();
                        break;
                    case ParkType.Airport:
                        __instance.m_airportAreasNotUsed.Disable();
                        break;
                    case ParkType.PedestrianZone:
                        __instance.m_pedestrianAreasNotUsed.Disable();
                        __instance.m_parks.m_buffer[park].m_residentialData = default;
                        __instance.m_parks.m_buffer[park].m_commercialData = default;
                        __instance.m_parks.m_buffer[park].m_officeData = default;
                        __instance.m_parks.m_buffer[park].m_industrialData = default;
                        __instance.m_parks.m_buffer[park].m_materialRequest = (from i in Enumerable.Range(0, pedestrianReasonsCount + ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                               select new Queue<ushort>()).ToArray();
                        __instance.m_parks.m_buffer[park].m_materialSuggestion = (from i in Enumerable.Range(0, pedestrianReasonsCount + ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                                  select new Queue<ushort>()).ToArray();
                        __instance.m_parks.m_buffer[park].m_randomServicePoints = new ushort[kDeliveryCategories.Length];
                        __instance.m_parks.m_buffer[park].m_tempServicePointList = new ushort[255];
                        __instance.m_parks.m_buffer[park].m_finalServicePointList = new ushort[255];
                        __instance.m_parks.m_buffer[park].m_zoneFocus = ZoneFocus.Neutral;
                        __instance.m_parks.m_buffer[park].m_zonesDirty = true;
                        __instance.m_parks.m_buffer[park].m_tempIncome = new uint[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_finalIncome = new uint[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_tempOutcome = new uint[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_finalOutcome = new uint[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_tempImport = new byte[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_finalImport = new byte[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_tempExport = new byte[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_finalExport = new byte[pedestrianReasonsCount];
                        __instance.m_parks.m_buffer[park].m_tempGoodsSold = 0u;
                        __instance.m_parks.m_buffer[park].m_finalGoodsSold = 0u;
                        __instance.m_parks.m_buffer[park].m_cargoServicePointExist = false;
                        __instance.m_parks.m_buffer[park].m_garbageServicePointExist = false;
                        break;
                }
                __result = true;
                return false;
            }
            park = 0;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(DistrictPark), "SimulationStep")]
        [HarmonyPostfix]
        public static void SimulationStep(DistrictPark __instance, byte parkID)
        {
            if (__instance.m_parkType == ParkType.None)
            {
                ExtendedDistrictManager.IndustryParks[parkID].BaseSimulationStep();
            }

            switch (__instance.m_parkType)
            {
                case ParkType.None:
                    ExtendedDistrictManager.IndustryParks[parkID].BaseSimulationStep();
                    break;

                case ParkType.Industry:
                    ExtendedDistrictManager.IndustryParks[parkID].IndustrySimulationStep();
                    break;
            }
        }

        [HarmonyPatch(typeof(DistrictPark), "AddParkInOffers")]
        [HarmonyPostfix]
        public static bool AddParkInOffers(DistrictPark __instance, byte parkID)
        {
            if (!__instance.m_cargoServicePointExist && !__instance.m_garbageServicePointExist)
            {
                return true;
            }
            VehicleManager instance = Singleton<VehicleManager>.instance;
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            for (int i = 0; i < kPedestrianZoneTransferReasons.Length; i++)
            {
                PedestrianZoneTransferReason pedestrianZoneTransferReason = kPedestrianZoneTransferReasons[i];
                TransferManager.TransferReason material = pedestrianZoneTransferReason.m_material;
                if ((pedestrianZoneTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !__instance.m_cargoServicePointExist) || (pedestrianZoneTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !__instance.m_garbageServicePointExist) || !__instance.TryGetRandomServicePoint(pedestrianZoneTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = __instance.m_materialRequest[i];
                int count = queue.Count;
                if (count != 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        ushort num2 = queue.Dequeue();
                        if ((buffer[num2].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        int amount = 0;
                        int max = 0;
                        BuildingInfo info = buffer[num2].Info;
                        if (!(info == null))
                        {
                            info.m_buildingAI.GetMaterialAmount(num2, ref buffer[num2], material, out amount, out max);
                            int num3 = Math.Max(max - amount, 0);
                            num += num3;
                            if (num3 > 0)
                            {
                                queue.Enqueue(num2);
                            }
                        }
                    }
                }
                for (int k = 0; k < __instance.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = __instance.m_finalServicePointList[k];
                    if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                    {
                        continue;
                    }
                    ServicePointAI servicePointAI = buffer[num4].Info.m_buildingAI as ServicePointAI;
                    if (servicePointAI == null || (servicePointAI.m_deliveryCategories & pedestrianZoneTransferReason.m_deliveryCategory) == 0)
                    {
                        continue;
                    }
                    ushort num5 = buffer[num4].m_guestVehicles;
                    int num6 = 0;
                    while (num5 != 0 && num > 0)
                    {
                        if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[num5].m_transferType == material)
                        {
                            VehicleInfo info2 = instance.m_vehicles.m_buffer[num5].Info;
                            info2.m_vehicleAI.GetSize(num5, ref instance.m_vehicles.m_buffer[num5], out var size, out var _);
                            num -= size;
                        }
                        num5 = instance.m_vehicles.m_buffer[num5].m_nextGuestVehicle;
                        if (++num6 > 16384)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
                TransferManager.TransferOffer offer = default;
                offer.Park = parkID;
                offer.Unlimited = true;
                if (num > 0)
                {
                    offer.Position = __instance.m_nameLocation;
                    offer.Active = pedestrianZoneTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneTransferReason.m_averageTruckCapacity), __instance.m_finalGateCount * 10);
                    int num8 = Math.Min(5 + num7 / 10, 7);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Priority = Math.Max(num8 - l / 10, 3);
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<TransferManager>.instance.AddIncomingOffer(material, offer);
                    }
                }
            }
            for (int i = kPedestrianZoneTransferReasons.Length; i < ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons.Length; i++)
            {
                ExtendedDistrictPark.PedestrianZoneExtendedTransferReason pedestrianZoneExtendedTransferReason = ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[i];
                ExtendedTransferManager.TransferReason material = pedestrianZoneExtendedTransferReason.m_material;
                if ((pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !__instance.m_cargoServicePointExist) || (pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !__instance.m_garbageServicePointExist) || !ExtendedDistrictPark.TryGetRandomServicePoint(pedestrianZoneExtendedTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = __instance.m_materialRequest[i];
                int count = queue.Count;
                if (count != 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        ushort num2 = queue.Dequeue();
                        if ((buffer[num2].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        int amount = 0;
                        int max = 0;
                        BuildingInfo info = buffer[num2].Info;
                        if (!(info == null))
                        {
                            ((IExtendedBuildingAI)info.m_buildingAI).ExtendedGetMaterialAmount(num2, ref buffer[num2], material, out amount, out max);
                            int num3 = Math.Max(max - amount, 0);
                            num += num3;
                            if (num3 > 0)
                            {
                                queue.Enqueue(num2);
                            }
                        }
                    }
                }
                for (int k = 0; k < __instance.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = __instance.m_finalServicePointList[k];
                    if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                    {
                        continue;
                    }
                    ServicePointAI servicePointAI = buffer[num4].Info.m_buildingAI as ServicePointAI;
                    if (servicePointAI == null || (servicePointAI.m_deliveryCategories & pedestrianZoneExtendedTransferReason.m_deliveryCategory) == 0)
                    {
                        continue;
                    }
                    ushort num5 = buffer[num4].m_guestVehicles;
                    int num6 = 0;
                    while (num5 != 0 && num > 0)
                    {
                        if ((ExtendedTransferManager.TransferReason)instance.m_vehicles.m_buffer[num5].m_transferType == material)
                        {
                            VehicleInfo info2 = instance.m_vehicles.m_buffer[num5].Info;
                            info2.m_vehicleAI.GetSize(num5, ref instance.m_vehicles.m_buffer[num5], out var size, out var _);
                            num -= size;
                        }
                        num5 = instance.m_vehicles.m_buffer[num5].m_nextGuestVehicle;
                        if (++num6 > 16384)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
                ExtendedTransferManager.Offer offer = default;
                offer.Park = parkID;
                if (num > 0)
                {
                    offer.Position = __instance.m_nameLocation;
                    offer.Active = pedestrianZoneExtendedTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneExtendedTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneExtendedTransferReason.m_averageTruckCapacity), __instance.m_finalGateCount * 10);
                    int num8 = Math.Min(5 + num7 / 10, 7);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<ExtendedTransferManager>.instance.AddIncomingOffer(material, offer);
                    }
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(DistrictPark), "AddParkOutOffers")]
        [HarmonyPostfix]
        public static bool AddParkOutOffers(DistrictPark __instance, byte parkID)
        {
            if (!__instance.m_cargoServicePointExist && !__instance.m_garbageServicePointExist)
            {
                return true;
            }
            VehicleManager instance = Singleton<VehicleManager>.instance;
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            for (int i = 0; i < kPedestrianZoneTransferReasons.Length; i++)
            {
                PedestrianZoneTransferReason pedestrianZoneTransferReason = kPedestrianZoneTransferReasons[i];
                TransferManager.TransferReason material = pedestrianZoneTransferReason.m_material;
                if ((pedestrianZoneTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !__instance.m_cargoServicePointExist) || (pedestrianZoneTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !__instance.m_garbageServicePointExist) || !__instance.TryGetRandomServicePoint(pedestrianZoneTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = __instance.m_materialRequest[i];
                int count = queue.Count;
                if (count != 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        ushort num2 = queue.Dequeue();
                        if ((buffer[num2].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        int amount = 0;
                        int max = 0;
                        BuildingInfo info = buffer[num2].Info;
                        if (!(info == null))
                        {
                            info.m_buildingAI.GetMaterialAmount(num2, ref buffer[num2], material, out amount, out max);
                            int num3 = Math.Max(max - amount, 0);
                            num += num3;
                            if (num3 > 0)
                            {
                                queue.Enqueue(num2);
                            }
                        }
                    }
                }
                for (int k = 0; k < __instance.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = __instance.m_finalServicePointList[k];
                    if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                    {
                        continue;
                    }
                    ServicePointAI servicePointAI = buffer[num4].Info.m_buildingAI as ServicePointAI;
                    if (servicePointAI == null || (servicePointAI.m_deliveryCategories & pedestrianZoneTransferReason.m_deliveryCategory) == 0)
                    {
                        continue;
                    }
                    ushort num5 = buffer[num4].m_guestVehicles;
                    int num6 = 0;
                    while (num5 != 0 && num > 0)
                    {
                        if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[num5].m_transferType == material)
                        {
                            VehicleInfo info2 = instance.m_vehicles.m_buffer[num5].Info;
                            info2.m_vehicleAI.GetSize(num5, ref instance.m_vehicles.m_buffer[num5], out var size, out var _);
                            num -= size;
                        }
                        num5 = instance.m_vehicles.m_buffer[num5].m_nextGuestVehicle;
                        if (++num6 > 16384)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
                TransferManager.TransferOffer offer = default;
                offer.Park = parkID;
                offer.Unlimited = true;
                if (num > 0)
                {
                    offer.Position = __instance.m_nameLocation;
                    offer.Active = pedestrianZoneTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneTransferReason.m_averageTruckCapacity), __instance.m_finalGateCount * 10);
                    int num8 = Math.Min(5 + num7 / 10, 7);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Priority = Math.Max(num8 - l / 10, 3);
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<TransferManager>.instance.AddOutgoingOffer(material, offer);
                    }
                }
            }
            for (int i = kPedestrianZoneTransferReasons.Length; i < ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons.Length; i++)
            {
                ExtendedDistrictPark.PedestrianZoneExtendedTransferReason pedestrianZoneExtendedTransferReason = ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[i];
                ExtendedTransferManager.TransferReason material = pedestrianZoneExtendedTransferReason.m_material;
                if ((pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !__instance.m_cargoServicePointExist) || (pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !__instance.m_garbageServicePointExist) || !ExtendedDistrictPark.TryGetRandomServicePoint(pedestrianZoneExtendedTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = __instance.m_materialRequest[i];
                int count = queue.Count;
                if (count != 0)
                {
                    for (int j = 0; j < count; j++)
                    {
                        ushort num2 = queue.Dequeue();
                        if ((buffer[num2].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                        {
                            continue;
                        }
                        int amount = 0;
                        int max = 0;
                        BuildingInfo info = buffer[num2].Info;
                        if (!(info == null))
                        {
                            ((IExtendedBuildingAI)info.m_buildingAI).ExtendedGetMaterialAmount(num2, ref buffer[num2], material, out amount, out max);
                            int num3 = Math.Max(max - amount, 0);
                            num += num3;
                            if (num3 > 0)
                            {
                                queue.Enqueue(num2);
                            }
                        }
                    }
                }
                for (int k = 0; k < __instance.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = __instance.m_finalServicePointList[k];
                    if ((buffer[num4].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                    {
                        continue;
                    }
                    ServicePointAI servicePointAI = buffer[num4].Info.m_buildingAI as ServicePointAI;
                    if (servicePointAI == null || (servicePointAI.m_deliveryCategories & pedestrianZoneExtendedTransferReason.m_deliveryCategory) == 0)
                    {
                        continue;
                    }
                    ushort num5 = buffer[num4].m_guestVehicles;
                    int num6 = 0;
                    while (num5 != 0 && num > 0)
                    {
                        if ((ExtendedTransferManager.TransferReason)instance.m_vehicles.m_buffer[num5].m_transferType == material)
                        {
                            VehicleInfo info2 = instance.m_vehicles.m_buffer[num5].Info;
                            info2.m_vehicleAI.GetSize(num5, ref instance.m_vehicles.m_buffer[num5], out var size, out var _);
                            num -= size;
                        }
                        num5 = instance.m_vehicles.m_buffer[num5].m_nextGuestVehicle;
                        if (++num6 > 16384)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
                ExtendedTransferManager.Offer offer = default;
                offer.Park = parkID;
                if (num > 0)
                {
                    offer.Position = __instance.m_nameLocation;
                    offer.Active = pedestrianZoneExtendedTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneExtendedTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneExtendedTransferReason.m_averageTruckCapacity), __instance.m_finalGateCount * 10);
                    int num8 = Math.Min(5 + num7 / 10, 7);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<ExtendedTransferManager>.instance.AddOutgoingOffer(material, offer);
                    }
                }
            }
            return false;
        }

        private static ushort GetDefaultTicketPrice(DistrictPark.ParkType type)
        {
            switch (type)
            {
                case DistrictPark.ParkType.GenericCampus:
                case DistrictPark.ParkType.TradeSchool:
                case DistrictPark.ParkType.LiberalArts:
                case DistrictPark.ParkType.University:
                    return 3000;
                default:
                    return 1000;
            }
        }
    }
}
