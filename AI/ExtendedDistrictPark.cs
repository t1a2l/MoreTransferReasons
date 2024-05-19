using ColossalFramework;
using ColossalFramework.IO;
using ICities;
using MoreTransferReasons.HarmonyPatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static DistrictManager;
using static DistrictPark;

namespace MoreTransferReasons
{
    public class ExtendedDistrictPark
    {
        public class Data : IDataContainer
        {
            private Dictionary<int, string> m_TempStyleMap;

            public void Serialize(DataSerializer s)
            {
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "DistrictManager");
                DistrictManager instance = Singleton<DistrictManager>.instance;
                DistrictPark[] buffer2 = instance.m_parks.m_buffer;
                ExtendedDistrictPark[] buffer3 = DistrictPatch.IndustryParks.m_buffer;
                int num2 = buffer2.Length;

                for (int num34 = 0; num34 < num2; num34++)
                {
                    if (buffer2[num34].m_flags == DistrictPark.Flags.None)
                    {
                        continue;
                    }
                    if (buffer2[num34].IsIndustry)
                    {
                        buffer3[num34].m_milkData.Serialize(s);
                        buffer3[num34].m_fruitsData.Serialize(s);
                        buffer3[num34].m_vegetablesData.Serialize(s);
                        buffer3[num34].m_cowsData.Serialize(s);
                        buffer3[num34].m_highlandCowsData.Serialize(s);
                        buffer3[num34].m_sheepData.Serialize(s);
                        buffer3[num34].m_pigsData.Serialize(s);
                        buffer3[num34].m_foodProductsData.Serialize(s);
                        buffer3[num34].m_beverageProductsData.Serialize(s);
                        buffer3[num34].m_bakedGoodsData.Serialize(s);
                        buffer3[num34].m_cannedFishData.Serialize(s);
                        buffer3[num34].m_furnituresData.Serialize(s);
                        buffer3[num34].m_electronicProductsData.Serialize(s);
                        buffer3[num34].m_industrialSteelData.Serialize(s);
                        buffer3[num34].m_tupperwareData.Serialize(s);
                        buffer3[num34].m_toysData.Serialize(s);
                        buffer3[num34].m_printedProductsData.Serialize(s);
                        buffer3[num34].m_tissuePaperData.Serialize(s);
                        buffer3[num34].m_clothsData.Serialize(s);
                        buffer3[num34].m_petroleumProductsData.Serialize(s);
                        buffer3[num34].m_carsData.Serialize(s);
                        buffer3[num34].m_footwearData.Serialize(s);
                        buffer3[num34].m_housePartsData.Serialize(s);
                        buffer3[num34].m_shipData.Serialize(s);
                        buffer3[num34].m_woolData.Serialize(s);
                        buffer3[num34].m_cottonData.Serialize(s);
                    }
                    if (!buffer2[num34].IsPedestrianZone)
                    {
                        continue;
                    }
                    s.WriteInt32(pedestrianExtendedReasonsCount);
                    for (int num37 = 0; num37 < pedestrianExtendedReasonsCount; num37++)
                    {
                        s.WriteUInt8((byte)kPedestrianZoneExtendedTransferReasons[num37].m_material);
                        s.WriteInt32(buffer2[num34].m_materialRequest[num37].Count);
                        EncodedArray.UShort uShort = EncodedArray.UShort.BeginWrite(s);
                        foreach (ushort item in buffer2[num34].m_materialRequest[num37])
                        {
                            uShort.Write(item);
                        }
                        uShort.EndWrite();
                    }
                    for (int num38 = 0; num38 < pedestrianExtendedReasonsCount; num38++)
                    {
                        s.WriteUInt8((byte)kPedestrianZoneExtendedTransferReasons[num38].m_material);
                        s.WriteInt32(buffer2[num34].m_materialSuggestion[num38].Count);
                        EncodedArray.UShort uShort2 = EncodedArray.UShort.BeginWrite(s);
                        foreach (ushort item2 in buffer2[num34].m_materialSuggestion[num38])
                        {
                            uShort2.Write(item2);
                        }
                        uShort2.EndWrite();
                    }                  
                    for (int num42 = 0; num42 < pedestrianExtendedReasonsCount; num42++)
                    {
                        s.WriteUInt8((byte)kPedestrianZoneExtendedTransferReasons[num42].m_material);
                    }
                }
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndSerialize(s, "DistrictManager");
            }

            public void Deserialize(DataSerializer s)
            {
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginDeserialize(s, "DistrictManager");
                DistrictManager instance = Singleton<DistrictManager>.instance;
                District[] buffer = instance.m_districts.m_buffer;
                DistrictPark[] buffer2 = instance.m_parks.m_buffer;
                ExtendedDistrictPark[] buffer3 = DistrictPatch.IndustryParks.m_buffer;
                int num = buffer.Length;
                int num2 = buffer2.Length;

                for (int num38 = 0; num38 < num2; num38++)
                {
                    if (buffer2[num38].m_flags != 0)
                    {
                        if (s.version >= 111009)
                        {
                            if (buffer2[num38].IsIndustry)
                            {
                                buffer3[num38].m_milkData.Deserialize(s);
                                buffer3[num38].m_fruitsData.Deserialize(s);
                                buffer3[num38].m_vegetablesData.Deserialize(s);
                                buffer3[num38].m_cowsData.Deserialize(s);
                                buffer3[num38].m_highlandCowsData.Deserialize(s);
                                buffer3[num38].m_sheepData.Deserialize(s);
                                buffer3[num38].m_pigsData.Deserialize(s);
                                buffer3[num38].m_foodProductsData.Deserialize(s);
                                buffer3[num38].m_beverageProductsData.Deserialize(s);
                                buffer3[num38].m_bakedGoodsData.Deserialize(s);
                                buffer3[num38].m_cannedFishData.Deserialize(s);
                                buffer3[num38].m_furnituresData.Deserialize(s);
                                buffer3[num38].m_electronicProductsData.Deserialize(s);
                                buffer3[num38].m_industrialSteelData.Deserialize(s);
                                buffer3[num38].m_tupperwareData.Deserialize(s);
                                buffer3[num38].m_toysData.Deserialize(s);
                                buffer3[num38].m_printedProductsData.Deserialize(s);
                                buffer3[num38].m_tissuePaperData.Deserialize(s);
                                buffer3[num38].m_clothsData.Deserialize(s);
                                buffer3[num38].m_petroleumProductsData.Deserialize(s);
                                buffer3[num38].m_carsData.Deserialize(s);
                                buffer3[num38].m_footwearData.Deserialize(s);
                                buffer3[num38].m_housePartsData.Deserialize(s);
                                buffer3[num38].m_shipData.Deserialize(s);
                                buffer3[num38].m_woolData.Deserialize(s);
                                buffer3[num38].m_cottonData.Deserialize(s);
                            }
                            else
                            {
                                buffer3[num38].m_milkData = default;
                                buffer3[num38].m_fruitsData = default;
                                buffer3[num38].m_vegetablesData = default;
                                buffer3[num38].m_cowsData = default;
                                buffer3[num38].m_highlandCowsData = default;
                                buffer3[num38].m_sheepData = default;
                                buffer3[num38].m_pigsData = default;
                                buffer3[num38].m_foodProductsData = default;
                                buffer3[num38].m_beverageProductsData = default;
                                buffer3[num38].m_bakedGoodsData = default;
                                buffer3[num38].m_cannedFishData = default;
                                buffer3[num38].m_furnituresData = default;
                                buffer3[num38].m_electronicProductsData = default;
                                buffer3[num38].m_industrialSteelData = default;
                                buffer3[num38].m_tupperwareData = default;
                                buffer3[num38].m_toysData = default;
                                buffer3[num38].m_printedProductsData = default;
                                buffer3[num38].m_tissuePaperData = default;
                                buffer3[num38].m_clothsData = default;
                                buffer3[num38].m_petroleumProductsData = default;
                                buffer3[num38].m_carsData = default;
                                buffer3[num38].m_footwearData = default;
                                buffer3[num38].m_housePartsData = default;
                                buffer3[num38].m_shipData = default;
                                buffer3[num38].m_woolData = default;
                                buffer3[num38].m_cottonData = default;
                            }
                        }
                        if (s.version >= 115000 && buffer2[num38].IsPedestrianZone)
                        {

                            buffer2[num38].m_tempIncome = new uint[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_finalIncome = new uint[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_tempOutcome = new uint[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_finalOutcome = new uint[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_tempImport = new byte[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_finalImport = new byte[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_tempExport = new byte[DistrictPark.pedestrianReasonsCount];
                            buffer2[num38].m_finalExport = new byte[DistrictPark.pedestrianReasonsCount];
                        }
                        int num42 = DistrictPark.pedestrianReasonsCount;
                        if (s.version >= 115003 && buffer2[num38].IsPedestrianZone)
                        {
                            num42 = s.ReadInt32();
                            for (int num43 = 0; num43 < num42; num43++)
                            {
                                TransferManager.TransferReason material = (TransferManager.TransferReason)s.ReadUInt8();
                                int num44 = s.ReadInt32();
                                DistrictPark.IsPedestrianReason(material, out var index);
                                EncodedArray.UShort uShort = EncodedArray.UShort.BeginRead(s);
                                for (int num45 = 0; num45 < num44; num45++)
                                {
                                    ushort item = uShort.Read();
                                    if (index >= 0)
                                    {
                                        buffer2[num38].m_materialRequest[index].Enqueue(item);
                                    }
                                }
                                uShort.EndRead();
                            }
                            for (int num46 = 0; num46 < num42; num46++)
                            {
                                TransferManager.TransferReason material2 = (TransferManager.TransferReason)s.ReadUInt8();
                                int num47 = s.ReadInt32();
                                DistrictPark.IsPedestrianReason(material2, out var index2);
                                EncodedArray.UShort uShort2 = EncodedArray.UShort.BeginRead(s);
                                for (int num48 = 0; num48 < num47; num48++)
                                {
                                    ushort item2 = uShort2.Read();
                                    if (index2 >= 0)
                                    {
                                        buffer2[num38].m_materialSuggestion[index2].Enqueue(item2);
                                    }
                                }
                                uShort2.EndRead();
                            }
                        }
                        if (s.version >= 115013 && buffer2[num38].IsPedestrianZone)
                        {
                            EncodedArray.UShort uShort3 = EncodedArray.UShort.BeginRead(s);
                            for (int num49 = 0; num49 < buffer2[num38].m_finalGateCount; num49++)
                            {
                                buffer2[num38].m_finalServicePointList[num49] = uShort3.Read();
                            }
                            uShort3.EndRead();
                            EncodedArray.UShort uShort4 = EncodedArray.UShort.BeginRead(s);
                            for (int num50 = 0; num50 < buffer2[num38].m_tempGateCount; num50++)
                            {
                                buffer2[num38].m_tempServicePointList[num50] = uShort4.Read();
                            }
                            uShort4.EndRead();
                            int num51 = s.ReadInt8();
                            for (int num52 = 0; num52 < num51; num52++)
                            {
                                VehicleInfo.VehicleCategory vehicleCategory = (VehicleInfo.VehicleCategory)s.ReadLong64();
                                ushort num53 = (ushort)s.ReadUInt16();
                                int num54 = Array.IndexOf(DistrictPark.kDeliveryCategories, vehicleCategory);
                                if (num54 >= 0)
                                {
                                    buffer2[num38].m_randomServicePoints[num52] = num53;
                                }
                            }
                        }
                        if (s.version >= 115007 && buffer2[num38].IsPedestrianZone)
                        {
                            buffer2[num38].m_residentialData.Deserialize(s);
                            buffer2[num38].m_commercialData.Deserialize(s);
                            buffer2[num38].m_industrialData.Deserialize(s);
                            buffer2[num38].m_officeData.Deserialize(s);
                        }
                        if (s.version >= 115015 && buffer2[num38].IsPedestrianZone)
                        {
                            buffer2[num38].m_zoneFocus = (DistrictPark.ZoneFocus)s.ReadInt8();
                            buffer2[num38].m_zonesDirty = s.ReadBool();
                        }
                        if (s.version >= 115016 && buffer2[num38].IsPedestrianZone)
                        {
                            if (s.version < 115021)
                            {
                                s.ReadUInt32();
                                s.ReadUInt32();
                                s.ReadUInt32();
                                s.ReadUInt32();
                            }
                            buffer2[num38].m_tempGoodsSold = s.ReadUInt32();
                            buffer2[num38].m_finalGoodsSold = s.ReadUInt32();
                        }
                        if (s.version >= 115021 && buffer2[num38].IsPedestrianZone)
                        {
                            for (int num55 = 0; num55 < num42; num55++)
                            {
                                TransferManager.TransferReason material3 = (TransferManager.TransferReason)s.ReadUInt8();
                                uint num56 = s.ReadUInt32();
                                uint num57 = s.ReadUInt32();
                                uint num58 = s.ReadUInt32();
                                uint num59 = s.ReadUInt32();
                                DistrictPark.IsPedestrianReason(material3, out var index3);
                                if (index3 >= 0)
                                {
                                    buffer2[num38].m_tempIncome[index3] = num56;
                                    buffer2[num38].m_finalIncome[index3] = num57;
                                    buffer2[num38].m_tempOutcome[index3] = num58;
                                    buffer2[num38].m_finalOutcome[index3] = num59;
                                }
                            }
                        }
                        if (s.version >= 115023 && buffer2[num38].IsPedestrianZone)
                        {
                            buffer2[num38].m_cargoServicePointExist = s.ReadBool();
                            buffer2[num38].m_garbageServicePointExist = s.ReadBool();
                        }
                    }
                    else
                    {
                        buffer2[num38].m_randomSeed = 0uL;
                        buffer2[num38].m_mainGate = 0;
                        buffer2[num38].m_condition = 0;
                        buffer2[num38].m_tempEntertainmentAccumulation = 0;
                        buffer2[num38].m_finalEntertainmentAccumulation = 0;
                        buffer2[num38].m_totalVisitorCount = 0u;
                        buffer2[num38].m_lastVisitorCount = 0u;
                        buffer2[num38].m_tempTicketIncome = 0u;
                        buffer2[num38].m_finalTicketIncome = 0u;
                        buffer2[num38].m_ticketPrice = 0;
                        buffer2[num38].m_tempResidentCount = 0;
                        buffer2[num38].m_finalResidentCount = 0;
                        buffer2[num38].m_tempTouristCount = 0;
                        buffer2[num38].m_finalTouristCount = 0;
                        buffer2[num38].m_parkType = DistrictPark.ParkType.None;
                        buffer2[num38].m_parkLevel = DistrictPark.ParkLevel.None;
                        buffer2[num38].m_randomGate = 0;
                        buffer2[num38].m_tempGateCount = 0;
                        buffer2[num38].m_finalGateCount = 0;
                        buffer2[num38].m_tempVisitorCapacity = 0;
                        buffer2[num38].m_finalVisitorCapacity = 0;
                        buffer2[num38].m_tempMainCapacity = 0;
                        buffer2[num38].m_finalMainCapacity = 0;
                        buffer2[num38].m_dayNightCount = 0;
                        buffer2[num38].m_tempAttractivenessAccumulation = 0;
                        buffer2[num38].m_finalAttractivenessAccumulation = 0;
                        buffer2[num38].m_areaFactor = 0;
                        buffer2[num38].m_tempWorkEfficiencyDelta = 0;
                        buffer2[num38].m_tempStorageDelta = 0;
                        buffer2[num38].m_finalWorkEfficiencyDelta = 0;
                        buffer2[num38].m_finalStorageDelta = 0;
                        buffer2[num38].m_totalProductionAmount = 0uL;
                        buffer2[num38].m_tempWorkerCount = 0;
                        buffer2[num38].m_finalWorkerCount = 0;
                        buffer2[num38].m_grainData = default(DistrictAreaResourceData);
                        buffer2[num38].m_logsData = default(DistrictAreaResourceData);
                        buffer2[num38].m_oreData = default(DistrictAreaResourceData);
                        buffer2[num38].m_oilData = default(DistrictAreaResourceData);
                        buffer2[num38].m_animalProductsData = default(DistrictAreaResourceData);
                        buffer2[num38].m_floursData = default(DistrictAreaResourceData);
                        buffer2[num38].m_paperData = default(DistrictAreaResourceData);
                        buffer2[num38].m_planedTimberData = default(DistrictAreaResourceData);
                        buffer2[num38].m_petroleumData = default(DistrictAreaResourceData);
                        buffer2[num38].m_plasticsData = default(DistrictAreaResourceData);
                        buffer2[num38].m_glassData = default(DistrictAreaResourceData);
                        buffer2[num38].m_metalsData = default(DistrictAreaResourceData);
                        buffer2[num38].m_luxuryProductsData = default(DistrictAreaResourceData);
                        buffer2[num38].m_studentCount = 0u;
                        buffer2[num38].m_studentCapacity = 0u;
                        buffer2[num38].m_campusBuildingAttractiveness = 0u;
                        buffer2[num38].m_academicWorksData = default(AcademicWorksData);
                        buffer2[num38].m_currentYearReportData = default(DistrictYearReportData);
                        buffer2[num38].m_lastYearReportData = default(DistrictYearReportData);
                        buffer2[num38].m_secondLastYearReportData = default(DistrictYearReportData);
                        buffer2[num38].m_ledger = new DistrictYearReportLedger();
                        buffer2[num38].m_isMainCampus = false;
                        buffer2[num38].m_grantType = 0;
                        buffer2[num38].m_coachCount = 0;
                        buffer2[num38].m_cheerleadingBudget = 0;
                        buffer2[num38].m_coachHireTimes = new DateTime[25];
                        buffer2[num38].m_winProbability = 0;
                        buffer2[num38].m_varsityIdentityIndex = 0;
                        buffer2[num38].m_varsityColor = Color.white;
                        buffer2[num38].m_lifetimeTrophies = 0;
                        buffer2[num38].m_staticVarsityAttractivenessModifier = 0;
                        buffer2[num38].m_dynamicVarsityAttractivenessModifier = 0;
                        buffer2[num38].m_sports = DistrictPark.Sports.None;
                        buffer2[num38].m_campusBuildingCount = 0;
                        buffer2[num38].m_awayMatchesDone = new bool[5];
                        buffer2[num38].m_arenas = new Dictionary<DistrictPark.SportIndex, FastList<ushort>>();
                        buffer2[num38].m_activeArenas = new Dictionary<DistrictPark.SportIndex, FastList<ushort>>();
                        buffer2[num38].m_academicStaffCount = 0;
                        buffer2[num38].m_academicStaffAccumulation = 0f;
                        buffer2[num38].m_terrainHeight = 0f;
                        buffer2[num38].m_totalIncomingPassengerCount = 0u;
                        buffer2[num38].m_totalOutgoingPassengerCount = 0u;
                        buffer2[num38].m_tempWeeklyPassengerCapacity = 0u;
                        buffer2[num38].m_finalWeeklyPassengerCapacity = 0u;
                        buffer2[num38].m_tempIncomingPassengers = 0u;
                        buffer2[num38].m_finalIncomingPassengers = 0u;
                        buffer2[num38].m_hasTerminal = false;
                        buffer2[num38].m_hasAircraftStand = false;
                        buffer2[num38].m_hasRunway = false;
                        if (num38 == 0)
                        {
                            buffer2[num38].m_flags |= DistrictPark.Flags.Created;
                        }
                        else
                        {
                            instance.m_parks.ReleaseItem((byte)num38);
                        }
                    }
                }
                EncodedArray.Byte byte2 = EncodedArray.Byte.BeginRead(s);
                for (int num60 = 0; num60 < num3; num60++)
                {
                    districtGrid[num60].m_district1 = byte2.Read();
                }
                for (int num61 = 0; num61 < num3; num61++)
                {
                    districtGrid[num61].m_district2 = byte2.Read();
                }
                for (int num62 = 0; num62 < num3; num62++)
                {
                    districtGrid[num62].m_district3 = byte2.Read();
                }
                for (int num63 = 0; num63 < num3; num63++)
                {
                    districtGrid[num63].m_district4 = byte2.Read();
                }
                for (int num64 = 0; num64 < num3; num64++)
                {
                    districtGrid[num64].m_alpha1 = byte2.Read();
                }
                for (int num65 = 0; num65 < num3; num65++)
                {
                    districtGrid[num65].m_alpha2 = byte2.Read();
                }
                for (int num66 = 0; num66 < num3; num66++)
                {
                    districtGrid[num66].m_alpha3 = byte2.Read();
                }
                for (int num67 = 0; num67 < num3; num67++)
                {
                    districtGrid[num67].m_alpha4 = byte2.Read();
                }
                byte2.EndRead();
                if (s.version >= 110001)
                {
                    EncodedArray.Byte byte3 = EncodedArray.Byte.BeginRead(s);
                    for (int num68 = 0; num68 < num4; num68++)
                    {
                        parkGrid[num68].m_district1 = byte3.Read();
                    }
                    for (int num69 = 0; num69 < num4; num69++)
                    {
                        parkGrid[num69].m_district2 = byte3.Read();
                    }
                    for (int num70 = 0; num70 < num4; num70++)
                    {
                        parkGrid[num70].m_district3 = byte3.Read();
                    }
                    for (int num71 = 0; num71 < num4; num71++)
                    {
                        parkGrid[num71].m_district4 = byte3.Read();
                    }
                    for (int num72 = 0; num72 < num4; num72++)
                    {
                        parkGrid[num72].m_alpha1 = byte3.Read();
                    }
                    for (int num73 = 0; num73 < num4; num73++)
                    {
                        parkGrid[num73].m_alpha2 = byte3.Read();
                    }
                    for (int num74 = 0; num74 < num4; num74++)
                    {
                        parkGrid[num74].m_alpha3 = byte3.Read();
                    }
                    for (int num75 = 0; num75 < num4; num75++)
                    {
                        parkGrid[num75].m_alpha4 = byte3.Read();
                    }
                    byte3.EndRead();
                }
                else
                {
                    for (int num76 = 0; num76 < num4; num76++)
                    {
                        parkGrid[num76].m_district1 = 0;
                        parkGrid[num76].m_district2 = 1;
                        parkGrid[num76].m_district3 = 2;
                        parkGrid[num76].m_district4 = 3;
                        parkGrid[num76].m_alpha1 = byte.MaxValue;
                        parkGrid[num76].m_alpha2 = 0;
                        parkGrid[num76].m_alpha3 = 0;
                        parkGrid[num76].m_alpha4 = 0;
                    }
                }
                if (s.version >= 156)
                {
                    instance.m_districtsNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_policiesNotUsed = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_districtsNotUsed = null;
                    instance.m_policiesNotUsed = null;
                }
                if (s.version >= 110022)
                {
                    instance.m_parkAreasNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_parkAreaCreated = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_parkAreasNotUsed = null;
                    instance.m_parkAreaCreated = null;
                }
                if (s.version >= 111008)
                {
                    instance.m_industryAreasNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_industryAreaCreated = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_industryAreasNotUsed = null;
                    instance.m_industryAreaCreated = null;
                }
                if (s.version >= 112016)
                {
                    instance.m_campusAreasNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_campusAreaCreated = s.ReadObject<GenericGuide>();
                    instance.m_academicWorksTabOpened = s.ReadObject<GenericGuide>();
                    if (s.version >= 112027)
                    {
                        instance.m_academicYearMidterm = s.ReadObject<BuildingInstanceGuide>();
                    }
                    else
                    {
                        instance.m_academicYearMidterm = new BuildingInstanceGuide();
                        s.ReadObject<GenericGuide>();
                    }
                    if (s.version >= 112028)
                    {
                        instance.m_museumUnlocked = s.ReadObject<BuildingTypeGuide>();
                    }
                    else
                    {
                        instance.m_museumUnlocked = new BuildingTypeGuide();
                        s.ReadObject<GenericGuide>();
                    }
                    if (s.version >= 112027)
                    {
                        instance.m_academicYearReportClosed = s.ReadObject<BuildingInstanceGuide>();
                    }
                    else
                    {
                        instance.m_academicYearReportClosed = new BuildingInstanceGuide();
                        s.ReadObject<GenericGuide>();
                    }
                    instance.m_varsityTabSelected = s.ReadObject<GenericGuide>();
                    instance.m_vsTrophyEarned = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_campusAreasNotUsed = null;
                    instance.m_campusAreaCreated = null;
                    instance.m_academicWorksTabOpened = null;
                    instance.m_academicYearMidterm = null;
                    instance.m_museumUnlocked = null;
                    instance.m_academicYearReportClosed = null;
                    instance.m_varsityTabSelected = null;
                    instance.m_vsTrophyEarned = null;
                }
                if (s.version >= 114009)
                {
                    instance.m_airportAreasNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_airportAreaCreated = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_airportAreasNotUsed = null;
                    instance.m_airportAreaCreated = null;
                }
                if (s.version >= 115024)
                {
                    instance.m_pedestrianAreasNotUsed = s.ReadObject<GenericGuide>();
                    instance.m_pedestrianAreaCreated = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_pedestrianAreasNotUsed = null;
                    instance.m_pedestrianAreaCreated = null;
                }
                if (s.version >= 116016)
                {
                    instance.m_stockExchangeNotUsed = s.ReadObject<GenericGuide>();
                }
                else
                {
                    instance.m_stockExchangeNotUsed = null;
                }
                if (s.version >= 148)
                {
                    instance.m_nextPolicyMessageFrame1 = s.ReadUInt32();
                    instance.m_nextPolicyMessageFrame2 = s.ReadUInt32();
                }
                else
                {
                    instance.m_nextPolicyMessageFrame1 = 0u;
                    instance.m_nextPolicyMessageFrame2 = 0u;
                }
                if (s.version >= 178)
                {
                    instance.m_districtPoliciesSet = s.ReadUInt32();
                }
                else
                {
                    instance.m_districtPoliciesSet = 0u;
                }
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndDeserialize(s, "DistrictManager");
            }

            private ushort FindStyleByName(string name)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                if (instance.m_Styles != null)
                {
                    for (ushort num = 0; num < instance.m_Styles.Length; num++)
                    {
                        if (instance.m_Styles[num].FullName.Equals(name))
                        {
                            return (ushort)(num + 1);
                        }
                    }
                }
                CODebugBase<LogChannel>.Warn(LogChannel.Modding, "Warning: Unknown district style: " + name);
                return 0;
            }

            public void AfterDeserialize(DataSerializer s)
            {
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginAfterDeserialize(s, "DistrictManager");
                Singleton<LoadingManager>.instance.WaitUntilEssentialScenesLoaded();
                DistrictManager instance = Singleton<DistrictManager>.instance;
                District[] buffer = instance.m_districts.m_buffer;
                DistrictPark[] buffer2 = instance.m_parks.m_buffer;
                Cell[] districtGrid = instance.m_districtGrid;
                Cell[] parkGrid = instance.m_parkGrid;
                int num = districtGrid.Length;
                int num2 = parkGrid.Length;
                for (int i = 0; i < num; i++)
                {
                    Cell cell = districtGrid[i];
                    buffer[cell.m_district1].m_totalAlpha += cell.m_alpha1;
                    buffer[cell.m_district2].m_totalAlpha += cell.m_alpha2;
                    buffer[cell.m_district3].m_totalAlpha += cell.m_alpha3;
                    buffer[cell.m_district4].m_totalAlpha += cell.m_alpha4;
                }
                for (int j = 1; j < 128; j++)
                {
                    if ((buffer2[j].m_flags & DistrictPark.Flags.Created) == 0)
                    {
                        continue;
                    }
                    switch (buffer2[j].m_parkType)
                    {
                        case DistrictPark.ParkType.Generic:
                        case DistrictPark.ParkType.AmusementPark:
                        case DistrictPark.ParkType.Zoo:
                        case DistrictPark.ParkType.NatureReserve:
                            if (!Singleton<LoadingManager>.instance.SupportsExpansion(Expansion.Parks))
                            {
                                buffer2[j].m_flags |= DistrictPark.Flags.Invalid;
                            }
                            break;
                        case DistrictPark.ParkType.Industry:
                        case DistrictPark.ParkType.Farming:
                        case DistrictPark.ParkType.Forestry:
                        case DistrictPark.ParkType.Ore:
                        case DistrictPark.ParkType.Oil:
                            if (!Singleton<LoadingManager>.instance.SupportsExpansion(Expansion.Industry))
                            {
                                buffer2[j].m_flags |= DistrictPark.Flags.Invalid;
                            }
                            break;
                        case DistrictPark.ParkType.GenericCampus:
                        case DistrictPark.ParkType.TradeSchool:
                        case DistrictPark.ParkType.LiberalArts:
                        case DistrictPark.ParkType.University:
                            if (!Singleton<LoadingManager>.instance.SupportsExpansion(Expansion.Campuses))
                            {
                                buffer2[j].m_flags |= DistrictPark.Flags.Invalid;
                            }
                            break;
                    }
                }
                for (int k = 0; k < num2; k++)
                {
                    bool flag = false;
                    Cell cell2 = parkGrid[k];
                    if ((buffer2[cell2.m_district1].m_flags & DistrictPark.Flags.Invalid) != 0)
                    {
                        cell2.m_district1 = 0;
                        flag = true;
                    }
                    if ((buffer2[cell2.m_district2].m_flags & DistrictPark.Flags.Invalid) != 0)
                    {
                        cell2.m_district2 = 0;
                        flag = true;
                    }
                    if ((buffer2[cell2.m_district3].m_flags & DistrictPark.Flags.Invalid) != 0)
                    {
                        cell2.m_district3 = 0;
                        flag = true;
                    }
                    if ((buffer2[cell2.m_district4].m_flags & DistrictPark.Flags.Invalid) != 0)
                    {
                        cell2.m_district4 = 0;
                        flag = true;
                    }
                    if (flag)
                    {
                        int num3 = 0;
                        if (cell2.m_district1 == 0)
                        {
                            num3 = 1;
                        }
                        if (cell2.m_district2 == 0)
                        {
                            if (num3 == 1)
                            {
                                cell2.m_alpha1 = (byte)Mathf.Min(255, cell2.m_alpha1 + cell2.m_alpha2);
                                cell2.m_alpha2 = 0;
                            }
                            else
                            {
                                num3 = 2;
                            }
                        }
                        if (cell2.m_district3 == 0)
                        {
                            switch (num3)
                            {
                                case 1:
                                    cell2.m_alpha1 = (byte)Mathf.Min(255, cell2.m_alpha1 + cell2.m_alpha3);
                                    cell2.m_alpha3 = 0;
                                    break;
                                case 2:
                                    cell2.m_alpha2 = (byte)Mathf.Min(255, cell2.m_alpha2 + cell2.m_alpha3);
                                    cell2.m_alpha3 = 0;
                                    break;
                                default:
                                    num3 = 3;
                                    break;
                            }
                        }
                        if (cell2.m_district4 == 0)
                        {
                            switch (num3)
                            {
                                case 1:
                                    cell2.m_alpha1 = (byte)Mathf.Min(255, cell2.m_alpha1 + cell2.m_alpha4);
                                    cell2.m_alpha4 = 0;
                                    break;
                                case 2:
                                    cell2.m_alpha2 = (byte)Mathf.Min(255, cell2.m_alpha2 + cell2.m_alpha4);
                                    cell2.m_alpha4 = 0;
                                    break;
                                case 3:
                                    cell2.m_alpha3 = (byte)Mathf.Min(255, cell2.m_alpha3 + cell2.m_alpha4);
                                    cell2.m_alpha4 = 0;
                                    break;
                            }
                        }
                        if (cell2.m_alpha2 > cell2.m_alpha1)
                        {
                            Exchange(ref cell2.m_alpha1, ref cell2.m_alpha2, ref cell2.m_district1, ref cell2.m_district2);
                        }
                        if (cell2.m_alpha3 > cell2.m_alpha1)
                        {
                            Exchange(ref cell2.m_alpha1, ref cell2.m_alpha3, ref cell2.m_district1, ref cell2.m_district3);
                        }
                        if (cell2.m_alpha4 > cell2.m_alpha1)
                        {
                            Exchange(ref cell2.m_alpha1, ref cell2.m_alpha4, ref cell2.m_district1, ref cell2.m_district4);
                        }
                        parkGrid[k] = cell2;
                    }
                    buffer2[cell2.m_district1].m_totalAlpha += cell2.m_alpha1;
                    buffer2[cell2.m_district2].m_totalAlpha += cell2.m_alpha2;
                    buffer2[cell2.m_district3].m_totalAlpha += cell2.m_alpha3;
                    buffer2[cell2.m_district4].m_totalAlpha += cell2.m_alpha4;
                }
                if (s.version < 110011)
                {
                    instance.m_parkGateCheckNeeded = true;
                }
                foreach (int key in m_TempStyleMap.Keys)
                {
                    instance.m_districts.m_buffer[key].m_Style = FindStyleByName(m_TempStyleMap[key]);
                }
                instance.m_districtCount = (int)(instance.m_districts.ItemCount() - 1);
                instance.m_parkCount = (int)(instance.m_parks.ItemCount() - 1);
                instance.AreaModified(0, 0, 511, 511, fullUpdate: true);
                instance.ParksAreaModified(0, 0, 511, 511, fullUpdate: true);
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndAfterDeserialize(s, "DistrictManager");
            }
        }

        public struct PedestrianZoneExtendedTransferReason
        {
            public ExtendedTransferManager.TransferReason m_material;

            public bool m_activeIn;

            public bool m_activeOut;

            public DeliveryCategories m_deliveryCategory;

            public VehicleInfo.VehicleCategory m_vehicleCategory;

            public int m_averageTruckCapacity;
        }

        public static PedestrianZoneExtendedTransferReason[] kPedestrianZoneExtendedTransferReasons;

        public static int pedestrianExtendedReasonsCount => kPedestrianZoneExtendedTransferReasons.Length;

        public DistrictAreaResourceData m_milkData;

        public DistrictAreaResourceData m_fruitsData;

        public DistrictAreaResourceData m_vegetablesData;

        public DistrictAreaResourceData m_cowsData;

        public DistrictAreaResourceData m_highlandCowsData;

        public DistrictAreaResourceData m_sheepData;

        public DistrictAreaResourceData m_pigsData;

        public DistrictAreaResourceData m_foodProductsData;

        public DistrictAreaResourceData m_beverageProductsData;

        public DistrictAreaResourceData m_bakedGoodsData;

        public DistrictAreaResourceData m_cannedFishData;

        public DistrictAreaResourceData m_furnituresData;

        public DistrictAreaResourceData m_electronicProductsData;

        public DistrictAreaResourceData m_industrialSteelData;

        public DistrictAreaResourceData m_tupperwareData;

        public DistrictAreaResourceData m_toysData;

        public DistrictAreaResourceData m_printedProductsData;

        public DistrictAreaResourceData m_tissuePaperData;

        public DistrictAreaResourceData m_clothsData;

        public DistrictAreaResourceData m_petroleumProductsData;

        public DistrictAreaResourceData m_carsData;

        public DistrictAreaResourceData m_footwearData;

        public DistrictAreaResourceData m_housePartsData;

        public DistrictAreaResourceData m_shipData;

        public DistrictAreaResourceData m_woolData;

        public DistrictAreaResourceData m_cottonData;

        public uint[] m_tempIncome;

        public uint[] m_tempOutcome;

        public Queue<ushort>[] m_extendedMaterialRequest;

        public Queue<ushort>[] m_extendedMaterialSuggestion;

        static ExtendedDistrictPark()
        {
            kPedestrianZoneExtendedTransferReasons =
            [
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Milk,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Fruits,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Vegetables,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Cows,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.HighlandCows,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Sheep,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Pigs,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.FoodProducts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.BeverageProducts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.BakedGoods,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.CannedFish,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Furnitures,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.ElectronicProducts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.IndustrialSteel,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Tupperware,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Toys,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.PrintedProducts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.TissuePaper,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Cloths,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.PetroleumProducts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Cars,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 5
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Footwear,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.HouseParts,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 1
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Wool,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Cotton,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Anchovy,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Salmon,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Shellfish,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Tuna,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Algae,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                },
                new PedestrianZoneExtendedTransferReason
                {
                    m_material = ExtendedTransferManager.TransferReason.Seaweed,
                    m_deliveryCategory = DeliveryCategories.Cargo,
                    m_vehicleCategory = VehicleInfo.VehicleCategory.CargoTruck,
                    m_activeIn = false,
                    m_activeOut = true,
                    m_averageTruckCapacity = 8000
                }
            ];

            var hashSet = new HashSet<DeliveryCategories>(kDeliveryCategories);

            for (int i = 0; i < kPedestrianZoneExtendedTransferReasons.Length; i++)
            {
                hashSet.Add(kPedestrianZoneExtendedTransferReasons[i].m_deliveryCategory);
            }

            kDeliveryCategories = hashSet.ToArray();

            var pedestrianReasonsCount = (int)typeof(DistrictPark).GetField("pedestrianReasonsCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).GetValue(null);

            pedestrianReasonsCount += pedestrianExtendedReasonsCount;

            typeof(DistrictPark).GetField("pedestrianReasonsCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).SetValue(null, pedestrianReasonsCount);

        }

        public void AddExtendedExportAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempExport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempExport += (uint)amount;
                    break;
            }
        }

        public void AddExtendedImportAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempImport += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempImport += (uint)amount;
                    break;
            }
        }

        public void AddBufferStatus(ExtendedTransferManager.TransferReason material, int amount, int incoming, int capacity)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.Add(amount, incoming, capacity);
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.Add(amount, incoming, capacity);
                    break;
            }
        }

        public void AddProductionAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            var m_mainGate = (ushort)typeof(DistrictPark).GetField("m_mainGate", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
            if (m_mainGate != 0 && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[m_mainGate].m_flags & Building.Flags.Active) != 0)
            {
                var m_totalProductionAmount = (ulong)typeof(DistrictPark).GetField("m_totalProductionAmount", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
                m_totalProductionAmount += (uint)amount;
                typeof(DistrictPark).GetField("m_totalProductionAmount", BindingFlags.Instance | BindingFlags.Public).SetValue(null, m_totalProductionAmount);
            }
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempProduction += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempProduction += (uint)amount;
                    break;
            }
        }

        public void AddConsumptionAmount(ExtendedTransferManager.TransferReason material, int amount)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.Milk:
                    m_milkData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Fruits:
                    m_fruitsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Vegetables:
                    m_vegetablesData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cows:
                    m_cowsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HighlandCows:
                    m_highlandCowsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Sheep:
                    m_sheepData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Pigs:
                    m_pigsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.FoodProducts:
                    m_foodProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                    m_beverageProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.BakedGoods:
                    m_bakedGoodsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.CannedFish:
                    m_cannedFishData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Furnitures:
                    m_furnituresData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                    m_electronicProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                    m_industrialSteelData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Tupperware:
                    m_tupperwareData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Toys:
                    m_toysData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                    m_printedProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.TissuePaper:
                    m_tissuePaperData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cloths:
                    m_clothsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                    m_petroleumProductsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cars:
                    m_carsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Footwear:
                    m_footwearData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.HouseParts:
                    m_housePartsData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Ship:
                    m_shipData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Wool:
                    m_woolData.m_tempConsumption += (uint)amount;
                    break;
                case ExtendedTransferManager.TransferReason.Cotton:
                    m_cottonData.m_tempConsumption += (uint)amount;
                    break;
            }
        }

        public void IndustrySimulationStep()
        {
            if ((Singleton<SimulationManager>.instance.m_currentFrameIndex & 0xFFF) >= 3840)
            {
                m_milkData.Add(ref m_milkData);
                m_fruitsData.Add(ref m_fruitsData);
                m_vegetablesData.Add(ref m_vegetablesData);
                m_cowsData.Add(ref m_cowsData);
                m_highlandCowsData.Add(ref m_highlandCowsData);
                m_sheepData.Add(ref m_sheepData);
                m_pigsData.Add(ref m_pigsData);
                m_foodProductsData.Add(ref m_foodProductsData);
                m_beverageProductsData.Add(ref m_beverageProductsData);
                m_bakedGoodsData.Add(ref m_bakedGoodsData);
                m_cannedFishData.Add(ref m_cannedFishData);
                m_furnituresData.Add(ref m_furnituresData);
                m_electronicProductsData.Add(ref m_electronicProductsData);
                m_industrialSteelData.Add(ref m_milkData);
                m_tupperwareData.Add(ref m_tupperwareData);
                m_toysData.Add(ref m_toysData);
                m_printedProductsData.Add(ref m_printedProductsData);
                m_tissuePaperData.Add(ref m_tissuePaperData);
                m_clothsData.Add(ref m_clothsData);
                m_petroleumProductsData.Add(ref m_petroleumProductsData);
                m_carsData.Add(ref m_carsData);
                m_footwearData.Add(ref m_footwearData);
                m_housePartsData.Add(ref m_housePartsData);
                m_shipData.Add(ref m_shipData);
                m_woolData.Add(ref m_woolData);
                m_cottonData.Add(ref m_cottonData);
                m_milkData.Update();
                m_fruitsData.Update();
                m_vegetablesData.Update();
                m_cowsData.Update();
                m_highlandCowsData.Update();
                m_sheepData.Update();
                m_pigsData.Update();
                m_foodProductsData.Update();
                m_beverageProductsData.Update();
                m_bakedGoodsData.Update();
                m_cannedFishData.Update();
                m_furnituresData.Update();
                m_electronicProductsData.Update();
                m_industrialSteelData.Update();
                m_tupperwareData.Update();
                m_toysData.Update();
                m_printedProductsData.Update();
                m_tissuePaperData.Update();
                m_clothsData.Update();
                m_petroleumProductsData.Update();
                m_carsData.Update();
                m_footwearData.Update();
                m_housePartsData.Update();
                m_shipData.Update();
                m_woolData.Update();
                m_cottonData.Update();
                m_milkData.Reset();
                m_fruitsData.Reset();
                m_vegetablesData.Reset();
                m_cowsData.Reset();
                m_highlandCowsData.Reset();
                m_sheepData.Reset();
                m_pigsData.Reset();
                m_foodProductsData.Reset();
                m_beverageProductsData.Reset();
                m_bakedGoodsData.Reset();
                m_cannedFishData.Reset();
                m_furnituresData.Reset();
                m_electronicProductsData.Reset();
                m_industrialSteelData.Reset();
                m_tupperwareData.Reset();
                m_toysData.Reset();
                m_printedProductsData.Reset();
                m_tissuePaperData.Reset();
                m_clothsData.Reset();
                m_petroleumProductsData.Reset();
                m_carsData.Reset();
                m_footwearData.Reset();
                m_housePartsData.Reset();
                m_shipData.Reset();
                m_woolData.Reset();
                m_cottonData.Reset();
            }
            else
            {
                m_milkData.ResetBuffers();
                m_fruitsData.ResetBuffers();
                m_vegetablesData.ResetBuffers();
                m_cowsData.ResetBuffers();
                m_highlandCowsData.ResetBuffers();
                m_sheepData.ResetBuffers();
                m_pigsData.ResetBuffers();
                m_foodProductsData.ResetBuffers();
                m_beverageProductsData.ResetBuffers();
                m_bakedGoodsData.ResetBuffers();
                m_cannedFishData.ResetBuffers();
                m_furnituresData.ResetBuffers();
                m_electronicProductsData.ResetBuffers();
                m_industrialSteelData.ResetBuffers();
                m_tupperwareData.ResetBuffers();
                m_toysData.ResetBuffers();
                m_printedProductsData.ResetBuffers();
                m_tissuePaperData.ResetBuffers();
                m_clothsData.ResetBuffers();
                m_petroleumProductsData.ResetBuffers();
                m_carsData.ResetBuffers();
                m_footwearData.ResetBuffers();
                m_housePartsData.ResetBuffers();
                m_shipData.ResetBuffers();
                m_woolData.ResetBuffers();
                m_cottonData.ResetBuffers();
            }
        }

        public void BaseSimulationStep()
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            if ((instance.m_currentFrameIndex & 0xFFF) >= 3840)
            {
                m_milkData.Update();
                m_fruitsData.Update();
                m_vegetablesData.Update();
                m_cowsData.Update();
                m_highlandCowsData.Update();
                m_sheepData.Update();
                m_pigsData.Update();
                m_foodProductsData.Update();
                m_beverageProductsData.Update();
                m_bakedGoodsData.Update();
                m_cannedFishData.Update();
                m_furnituresData.Update();
                m_electronicProductsData.Update();
                m_industrialSteelData.Update();
                m_tupperwareData.Update();
                m_toysData.Update();
                m_printedProductsData.Update();
                m_tissuePaperData.Update();
                m_clothsData.Update();
                m_petroleumProductsData.Update();
                m_carsData.Update();
                m_footwearData.Update();
                m_housePartsData.Update();
                m_shipData.Update();
                m_woolData.Update();
                m_cottonData.Update();
                m_milkData.Reset();
                m_fruitsData.Reset();
                m_vegetablesData.Reset();
                m_cowsData.Reset();
                m_highlandCowsData.Reset();
                m_sheepData.Reset();
                m_pigsData.Reset();
                m_foodProductsData.Reset();
                m_beverageProductsData.Reset();
                m_bakedGoodsData.Reset();
                m_cannedFishData.Reset();
                m_furnituresData.Reset();
                m_electronicProductsData.Reset();
                m_industrialSteelData.Reset();
                m_tupperwareData.Reset();
                m_toysData.Reset();
                m_printedProductsData.Reset();
                m_tissuePaperData.Reset();
                m_clothsData.Reset();
                m_petroleumProductsData.Reset();
                m_carsData.Reset();
                m_footwearData.Reset();
                m_housePartsData.Reset();
                m_shipData.Reset();
                m_woolData.Reset();
                m_cottonData.Reset();
            }
        }

        public void CreatePark()
        {
            m_milkData = default;
            m_fruitsData = default;
            m_vegetablesData = default;
            m_cowsData = default;
            m_highlandCowsData = default;
            m_sheepData = default;
            m_pigsData = default;
            m_foodProductsData = default;
            m_beverageProductsData = default;
            m_bakedGoodsData = default;
            m_cannedFishData = default;
            m_furnituresData = default;
            m_electronicProductsData = default;
            m_industrialSteelData = default;
            m_tupperwareData = default;
            m_toysData = default;
            m_printedProductsData = default;
            m_tissuePaperData = default;
            m_clothsData = default;
            m_petroleumProductsData = default;
            m_carsData = default;
            m_footwearData = default;
            m_housePartsData = default;
            m_shipData = default;
            m_woolData = default;
            m_cottonData = default;
        }

        public bool TryGetRandomServicePoint(ExtendedTransferManager.TransferReason material, out ushort buildingID)
        {
            if (TryGetPedestrianReason(material, out var reason) && TryGetRandomServicePoint(reason.m_deliveryCategory, out buildingID))
            {
                ServicePointAI servicePointAI = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI as ServicePointAI;
                if (servicePointAI != null && !servicePointAI.IsReachedCriticalTrafficLimit(buildingID, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID], reason.m_deliveryCategory))
                {
                    return true;
                }
            }
            buildingID = 0;
            return false;
        }

        public static bool TryGetRandomServicePoint(DeliveryCategories deliveryCategory, out ushort buildingID)
        {
            if (deliveryCategory != 0 && IsServicePointDelivery(deliveryCategory, out var index))
            {
                var m_randomServicePoints = (ushort[])typeof(DistrictPark).GetField("m_randomServicePoints", BindingFlags.Instance | BindingFlags.Public).GetValue(null);
                buildingID = m_randomServicePoints[index];
                return buildingID != 0;
            }
            buildingID = 0;
            return false;
        }

        public static bool TryGetPedestrianReason(ExtendedTransferManager.TransferReason material, out PedestrianZoneExtendedTransferReason reason)
        {
            if (IsPedestrianReason(material, out var index))
            {
                reason = kPedestrianZoneExtendedTransferReasons[index];
                return true;
            }
            reason = default;
            return false;
        }

        public static bool IsPedestrianReason(ExtendedTransferManager.TransferReason material, out int index)
        {
            index = Array.FindIndex(kPedestrianZoneExtendedTransferReasons, (PedestrianZoneExtendedTransferReason i) => i.m_material == material);
            return index >= 0;
        }

        public void AddMaterialRequest(ushort buildingID, ExtendedTransferManager.TransferReason material)
        {
            if (IsPedestrianReason(material, out var index) && m_extendedMaterialRequest[index].All((ushort id) => id != buildingID))
            {
                m_extendedMaterialRequest[index].Enqueue(buildingID);
            }
        }

        public void AddMaterialSuggestion(ushort buildingID, ExtendedTransferManager.TransferReason material)
        {
            if (IsPedestrianReason(material, out var index) && m_extendedMaterialSuggestion[index].All((ushort id) => id != buildingID))
            {
                m_extendedMaterialSuggestion[index].Enqueue(buildingID);
            }
        }

        public void ModifyMaterialBuffer(ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {
            if (IsPedestrianReason(material, out var index))
            {
                int num = 0;
                if (amountDelta > 0)
                {
                    Queue<ushort> queue = m_extendedMaterialRequest[index];
                    int count = queue.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (queue.Count <= 0)
                        {
                            break;
                        }
                        if (num >= amountDelta)
                        {
                            break;
                        }
                        int num2 = amountDelta - num;
                        int amountDelta2 = num2;
                        ushort num3 = queue.Dequeue();
                        BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[num3].Info;
                        if (!(info == null))
                        {
                            ((IExtendedBuildingAI)info.m_buildingAI).ExtendedModifyMaterialBuffer(num3, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[num3], material, ref amountDelta2);
                            num += amountDelta2;
                            if (amountDelta2 <= num2)
                            {
                                Singleton<ExtendedTransferManager>.instance.RemoveIncomingOffer(material, new ExtendedTransferManager.Offer
                                {
                                    Building = num3
                                });
                            }
                            else
                            {
                                queue.Enqueue(num3);
                            }
                        }
                    }
                }
                else
                {
                    Queue<ushort> queue2 = m_extendedMaterialSuggestion[index];
                    int count2 = queue2.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        if (queue2.Count <= 0)
                        {
                            break;
                        }
                        if (num <= amountDelta)
                        {
                            break;
                        }
                        int num4 = amountDelta - num;
                        int amountDelta3 = num4;
                        ushort num5 = queue2.Dequeue();
                        BuildingInfo info2 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[num5].Info;
                        if (!(info2 == null))
                        {
                            ((IExtendedBuildingAI)info2.m_buildingAI).ExtendedModifyMaterialBuffer(num5, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[num5], material, ref amountDelta3);
                            num += amountDelta3;
                            if (amountDelta3 >= num4)
                            {
                                Singleton<ExtendedTransferManager>.instance.RemoveOutgoingOffer(material, new ExtendedTransferManager.Offer
                                {
                                    Building = num5
                                });
                            }
                            else
                            {
                                queue2.Enqueue(num5);
                            }
                        }
                    }
                }
                amountDelta = num;
                if (amountDelta > 0)
                {
                    m_tempIncome[index] += (uint)amountDelta;
                }
                else
                {
                    m_tempOutcome[index] -= (uint)amountDelta;
                }
            }
            else
            {
                amountDelta = 0;
            }
        }

        public void StartLocalTransfer(ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            BuildingAI buildingAI = Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_buildingAI;
            ((IExtendedBuildingAI)buildingAI).ExtendedGetMaterialAmount(offer.Building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building], material, out var amount, out var max);
            int amountDelta = -(max - amount);
            ModifyMaterialBuffer(material, ref amountDelta);
            amountDelta = -amountDelta;
            ((IExtendedBuildingAI)buildingAI).ExtendedModifyMaterialBuffer(offer.Building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building], material, ref amountDelta);
        }

        public void PedestrianZoneSimulationStep(DistrictPark instance3, byte parkID)
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            if (((instance.m_currentFrameIndex >> 8) & 0xF) == (parkID & 0xF))
            {
                for (int i = 0; i < pedestrianExtendedReasonsCount; i++)
                {
                    instance3.m_finalIncome[i] = m_tempIncome[i];
                    instance3.m_finalOutcome[i] = m_tempOutcome[i];
                    instance3.m_finalImport[i] = instance3.m_tempImport[i];
                    instance3.m_finalExport[i] = instance3.m_tempExport[i];
                    m_tempIncome[i] = 0u;
                    m_tempOutcome[i] = 0u;
                    instance3.m_tempImport[i] = 0;
                    instance3.m_tempExport[i] = 0;
                }
            }
            instance3.m_cargoServicePointExist = false;
            instance3.m_garbageServicePointExist = false;
            AddParkInOffers(instance3, parkID);
            AddParkOutOffers(instance3, parkID);
            CalculateImport(instance3, parkID);
            CalculateExport(instance3, parkID);
        }

        private void AddParkInOffers(DistrictPark instance3, byte parkID)
        {
            if (!instance3.m_cargoServicePointExist && !instance3.m_garbageServicePointExist)
            {
                return;
            }
            VehicleManager instance = Singleton<VehicleManager>.instance;
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            for (int i = kPedestrianZoneTransferReasons.Length; i < kPedestrianZoneExtendedTransferReasons.Length; i++)
            {
                PedestrianZoneExtendedTransferReason pedestrianZoneExtendedTransferReason = kPedestrianZoneExtendedTransferReasons[i];
                ExtendedTransferManager.TransferReason material = pedestrianZoneExtendedTransferReason.m_material;
                if ((pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !instance3.m_cargoServicePointExist) || (pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !instance3.m_garbageServicePointExist) || !TryGetRandomServicePoint(pedestrianZoneExtendedTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = m_extendedMaterialRequest[i];
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
                for (int k = 0; k < instance3.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = instance3.m_finalServicePointList[k];
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
                    offer.Position = instance3.m_nameLocation;
                    offer.Active = pedestrianZoneExtendedTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneExtendedTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneExtendedTransferReason.m_averageTruckCapacity), instance3.m_finalGateCount * 10);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<ExtendedTransferManager>.instance.AddIncomingOffer(material, offer);
                    }
                }
            }
            return;
        }

        private void AddParkOutOffers(DistrictPark instance3, byte parkID)
        {
            if (!instance3.m_cargoServicePointExist && !instance3.m_garbageServicePointExist)
            {
                return;
            }
            VehicleManager instance = Singleton<VehicleManager>.instance;
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            for (int i = kPedestrianZoneTransferReasons.Length; i < kPedestrianZoneExtendedTransferReasons.Length; i++)
            {
                PedestrianZoneExtendedTransferReason pedestrianZoneExtendedTransferReason = kPedestrianZoneExtendedTransferReasons[i];
                ExtendedTransferManager.TransferReason material = pedestrianZoneExtendedTransferReason.m_material;
                if ((pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Cargo && !instance3.m_cargoServicePointExist) || (pedestrianZoneExtendedTransferReason.m_deliveryCategory == DeliveryCategories.Garbage && !instance3.m_garbageServicePointExist) || !TryGetRandomServicePoint(pedestrianZoneExtendedTransferReason.m_deliveryCategory, out var _))
                {
                    continue;
                }
                int num = 0;
                Queue<ushort> queue = m_extendedMaterialRequest[i];
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
                for (int k = 0; k < instance3.m_finalGateCount; k++)
                {
                    if (num <= 0)
                    {
                        break;
                    }
                    ushort num4 = instance3.m_finalServicePointList[k];
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
                    offer.Position = instance3.m_nameLocation;
                    offer.Active = pedestrianZoneExtendedTransferReason.m_activeIn;
                    offer.Amount = 1;
                    int num7 = Math.Min((pedestrianZoneExtendedTransferReason.m_averageTruckCapacity <= 0) ? 1 : Mathf.CeilToInt((float)num / (float)pedestrianZoneExtendedTransferReason.m_averageTruckCapacity), instance3.m_finalGateCount * 10);
                    for (int l = 0; l < num7; l += 10)
                    {
                        offer.Amount = Math.Min(num7 - l, 10);
                        Singleton<ExtendedTransferManager>.instance.AddOutgoingOffer(material, offer);
                    }
                }
            }
            return;
        }

        private void CalculateImport(DistrictPark instance3, byte parkID)
        {
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            Vehicle[] buffer2 = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
            for (int i = 0; i < instance3.m_finalGateCount; i++)
            {
                ushort num = instance3.m_finalServicePointList[i];
                if ((buffer[num].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                {
                    continue;
                }
                ServicePointAI servicePointAI = buffer[num].Info.m_buildingAI as ServicePointAI;
                if (servicePointAI == null)
                {
                    continue;
                }
                ushort num2 = buffer[num].m_guestVehicles;
                int num3 = 0;
                var material_byte = buffer2[num2].m_transferType - 200;
                while (num2 != 0 && (buffer2[num2].m_flags & Vehicle.Flags.Importing) != 0)
                {
                    ExtendedTransferManager.TransferReason transferType = (ExtendedTransferManager.TransferReason)material_byte;
                    if (IsPedestrianReason(transferType, out var index))
                    {
                        instance3.m_tempImport[index] = (byte)Math.Min(instance3.m_tempImport[index] + 1, 255);
                    }
                    num2 = buffer2[num2].m_nextGuestVehicle;
                    if (++num3 > 16384)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
        }

        private void CalculateExport(DistrictPark instance3, byte parkID)
        {
            Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
            Vehicle[] buffer2 = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
            for (int i = 0; i < instance3.m_finalGateCount; i++)
            {
                ushort num = instance3.m_finalServicePointList[i];
                if ((buffer[num].m_problems & Notification.Problem1.TurnedOff).IsNotNone)
                {
                    continue;
                }
                ServicePointAI servicePointAI = buffer[num].Info.m_buildingAI as ServicePointAI;
                if (servicePointAI == null)
                {
                    continue;
                }
                ushort num2 = buffer[num].m_ownVehicles;
                int num3 = 0;
                var material_byte = buffer2[num2].m_transferType - 200;
                while (num2 != 0 && (buffer2[num2].m_flags & Vehicle.Flags.Exporting) != 0)
                {
                    ExtendedTransferManager.TransferReason transferType = (ExtendedTransferManager.TransferReason)material_byte;
                    if (IsPedestrianReason(transferType, out var index))
                    {
                        instance3.m_tempExport[index] = (byte)Math.Min(instance3.m_tempExport[index] + 1, 255);
                    }
                    num2 = buffer2[num2].m_nextOwnVehicle;
                    if (++num3 > 16384)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
        }
    }
}
