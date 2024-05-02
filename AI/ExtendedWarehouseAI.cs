using ColossalFramework.DataBinding;
using ColossalFramework.Math;
using ColossalFramework;
using MoreTransferReasons.Utils;
using System;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace MoreTransferReasons.AI
{
    public class ExtendedWarehouseAI : WarehouseAI, IExtendedBuildingAI
    {
        private delegate void CreateBuildingPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building data);
        private static readonly CreateBuildingPlayerBuildingAIDelegate CreateBuildingPlayerBuildingAI = AccessTools.MethodDelegate<CreateBuildingPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("CreateBuilding", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate string GetDebugStringPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building data);
        private static readonly GetDebugStringPlayerBuildingAIDelegate GetDebugStringPlayerBuildingAI = AccessTools.MethodDelegate<GetDebugStringPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("GetDebugString", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate string GetLocalizedTooltipPlayerBuildingAIDelegate(PlayerBuildingAI instance);
        private static readonly GetLocalizedTooltipPlayerBuildingAIDelegate GetLocalizedTooltipPlayerBuildingAI = AccessTools.MethodDelegate<GetLocalizedTooltipPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("GetLocalizedTooltip", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate void ProduceGoodsPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building buildingData, ref Building.Frame frameData, int productionRate, int finalProductionRate, ref Citizen.BehaviourData behaviour, int aliveWorkerCount, int totalWorkerCount, int workPlaceCount, int aliveVisitorCount, int totalVisitorCount, int visitPlaceCount);
        private static readonly ProduceGoodsPlayerBuildingAIDelegate ProduceGoodsPlayerBuildingAI = AccessTools.MethodDelegate<ProduceGoodsPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("ProduceGoods", BindingFlags.Instance | BindingFlags.NonPublic), null, true);

        private delegate Color GetColorPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort vehicleID, ref Building data, InfoManager.InfoMode infoMode, InfoManager.SubInfoMode subInfoMode);
        private static readonly GetColorPlayerBuildingAIDelegate GetColorPlayerBuildingAI = AccessTools.MethodDelegate<GetColorPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("GetColor", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate void BuildingDeactivatedPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building data);
        private static readonly BuildingDeactivatedPlayerBuildingAIDelegate BuildingDeactivatedPlayerBuildingAI = AccessTools.MethodDelegate<BuildingDeactivatedPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate void CheckRoadAccessPlayerBuildingAIDelegate(PlayerBuildingAI instance, ushort buildingID, ref Building data);
        private static readonly CheckRoadAccessPlayerBuildingAIDelegate CheckRoadAccessPlayerBuildingAI = AccessTools.MethodDelegate<CheckRoadAccessPlayerBuildingAIDelegate>(typeof(PlayerBuildingAI).GetMethod("CheckRoadAccess", BindingFlags.Instance | BindingFlags.Public), null, false);

        private delegate void CalculateGuestVehiclesCommonBuildingAIDelegate(CommonBuildingAI instance, ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside);
        private static readonly CalculateGuestVehiclesCommonBuildingAIDelegate CalculateGuestVehiclesCommonBuildingAI = AccessTools.MethodDelegate<CalculateGuestVehiclesCommonBuildingAIDelegate>(typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.Instance | BindingFlags.Public), null, true);

        [CustomizableProperty("Storage Type")]
        public ExtendedTransferManager.TransferReason m_extendedStorageType = ExtendedTransferManager.TransferReason.None;

        [NonSerialized]
        private int m_subStations = -1;

        public override Color GetColor(ushort buildingID, ref Building data, InfoManager.InfoMode infoMode, InfoManager.SubInfoMode subInfoMode)
        {
            switch (infoMode)
            {
                case InfoManager.InfoMode.Connections:
                    switch (subInfoMode)
                    {
                        case InfoManager.SubInfoMode.Default:
                            {
                                byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref data);
                                if (actualTransferReason != 255 && (data.m_tempImport != 0 || data.m_finalImport != 0))
                                {
                                    if (actualTransferReason >= 200)
                                    {
                                        actualTransferReason = (byte)(actualTransferReason - 200);
                                    }
                                    return Singleton<ExtendedTransferManager>.instance.m_properties.m_resourceColors[(int)actualTransferReason];
                                }
                                return Singleton<InfoManager>.instance.m_properties.m_neutralColor;
                            }
                        case InfoManager.SubInfoMode.WaterPower:
                            {
                                byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref data);
                                if (actualTransferReason != 255 && (data.m_tempExport != 0 || data.m_finalExport != 0))
                                {
                                    if (actualTransferReason >= 200)
                                    {
                                        actualTransferReason = (byte)(actualTransferReason - 200);
                                    }
                                    return Singleton<ExtendedTransferManager>.instance.m_properties.m_resourceColors[(int)actualTransferReason];
                                }
                                return Singleton<InfoManager>.instance.m_properties.m_neutralColor;
                            }
                        default:
                            return Singleton<InfoManager>.instance.m_properties.m_neutralColor;
                    }
                case InfoManager.InfoMode.Pollution:
                    {
                        int pollutionAccumulation = m_pollutionAccumulation;
                        return ColorUtils.LinearLerp(Singleton<InfoManager>.instance.m_properties.m_neutralColor, Singleton<InfoManager>.instance.m_properties.m_modeProperties[(int)infoMode].m_activeColor, Mathf.Clamp01((float)pollutionAccumulation * 0.03f));
                    }
                case InfoManager.InfoMode.NoisePollution:
                    {
                        int noiseAccumulation = m_noiseAccumulation;
                        return GetNoisePollutionColor(noiseAccumulation);
                    }
                case InfoManager.InfoMode.Industry:
                    if (subInfoMode == IndustryBuildingAI.ServiceToInfoMode(m_info.m_class.m_subService))
                    {
                        if ((data.m_flags & Building.Flags.Active) != 0)
                        {
                            return Singleton<InfoManager>.instance.m_properties.m_modeProperties[(int)infoMode].m_activeColor;
                        }
                        return Singleton<InfoManager>.instance.m_properties.m_modeProperties[(int)infoMode].m_inactiveColor;
                    }
                    return Singleton<InfoManager>.instance.m_properties.m_neutralColor;
                case InfoManager.InfoMode.Transport:
                    if (m_subStations > 0)
                    {
                        for (ushort subBuilding = data.m_subBuilding; subBuilding != 0; subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding)
                        {
                            BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].Info;
                            if (info != null)
                            {
                                ExtendedWarehouseStationAI extendedWarehouseStationAI = info.m_buildingAI as ExtendedWarehouseStationAI;
                                if (extendedWarehouseStationAI != null)
                                {
                                    return extendedWarehouseStationAI.GetColor(subBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding], infoMode, subInfoMode);
                                }
                            }
                        }
                    }
                    return GetColorPlayerBuildingAI(this, buildingID, ref data, infoMode, subInfoMode);
                default:
                    return GetColorPlayerBuildingAI(this, buildingID, ref data, infoMode, subInfoMode);
            }
        }

        public override string GetDebugString(ushort buildingID, ref Building data)
        {
            byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref data);
            if (actualTransferReason != 255 && actualTransferReason < 200)
            {
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                CalculateGuestVehicles(buildingID, ref data, (TransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                int num = data.m_customBuffer1 * 100;
                return StringUtils.SafeFormat("{0}\n{1}: {2} (+{3})", GetDebugStringPlayerBuildingAI(this, buildingID, ref data), (TransferManager.TransferReason)actualTransferReason, num, cargo);
            }
            else if (actualTransferReason != 255 && actualTransferReason >= 200)
            {
                byte material_byte = (byte)(actualTransferReason - 200);
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                CalculateGuestVehiclesExtended(buildingID, ref data, (ExtendedTransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                int num = data.m_customBuffer1 * 100;
                return StringUtils.SafeFormat("{0}\n{1}: {2} (+{3})", GetDebugStringPlayerBuildingAI(this, buildingID, ref data), (ExtendedTransferManager.TransferReason)material_byte, num, cargo);
            }
            else
            {
                return GetDebugStringPlayerBuildingAI(this, buildingID, ref data);
            }

        }

        public override void CreateBuilding(ushort buildingID, ref Building data)
        {
            CreateBuildingPlayerBuildingAI(this, buildingID, ref data);
            int workCount = m_workPlaceCount0 + m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
            Singleton<CitizenManager>.instance.CreateUnits(out data.m_citizenUnits, ref Singleton<SimulationManager>.instance.m_randomizer, buildingID, 0, 0, workCount);
            data.m_seniors = byte.MaxValue;
            data.m_adults = byte.MaxValue;
            if (GetExtendedTransferReason(buildingID, ref data) == (byte)TransferManager.TransferReason.None)
            {
                data.m_problems = Notification.AddProblems(data.m_problems, Notification.Problem1.ResourceNotSelected);
            }
            CountStations();
        }

        void IExtendedBuildingAI.ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
            var transferType = GetExtendedTransferReason(buildingID, ref data);
            var actual_reason_byte = (byte)(GetExtendedActualTransferReason(buildingID, ref data) - 200);
            ExtendedTransferManager.TransferReason actualTransferReason = (ExtendedTransferManager.TransferReason)actual_reason_byte;
            if (material != actualTransferReason)
            {
                return;
            }
            VehicleInfo transferVehicleService = GetExtendedTransferVehicleService(material, ItemClass.Level.Level1, ref Singleton<SimulationManager>.instance.m_randomizer);
            if (transferVehicleService == null)
            {
                return;
            }
            Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
            if (ExtedndedVehicleManager.CreateVehicle(out var vehicle, ref Singleton<SimulationManager>.instance.m_randomizer, transferVehicleService, data.m_position, transferType, transferToSource: false, transferToTarget: true) && transferVehicleService.m_vehicleAI is ExtendedCargoTruckAI cargoTruckAI)
            {
                transferVehicleService.m_vehicleAI.SetSource(vehicle, ref vehicles.m_buffer[vehicle], buildingID);
                ((IExtendedVehicleAI)cargoTruckAI).ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[(int)vehicle], material, offer);
            }
        }

        public static VehicleInfo GetExtendedTransferVehicleService(ExtendedTransferManager.TransferReason material, ItemClass.Level level, ref Randomizer randomizer)
        {
            ItemClass.SubService subService = ItemClass.SubService.None;
            ItemClass.Service service;
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.FoodProducts:
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                case ExtendedTransferManager.TransferReason.BakedGoods:
                case ExtendedTransferManager.TransferReason.CannedFish:
                case ExtendedTransferManager.TransferReason.Furnitures:
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                case ExtendedTransferManager.TransferReason.Tupperware:
                case ExtendedTransferManager.TransferReason.Toys:
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                case ExtendedTransferManager.TransferReason.TissuePaper:
                case ExtendedTransferManager.TransferReason.Cloths:
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                case ExtendedTransferManager.TransferReason.Cars:
                case ExtendedTransferManager.TransferReason.Footwear:
                case ExtendedTransferManager.TransferReason.Houses:
                    service = ItemClass.Service.PlayerIndustry;
                    break;
                default:
                    return null;
            }
            return Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref randomizer, service, subService, level);
        }

        void IExtendedBuildingAI.ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max)
        {
            amount = 0;
            max = 0;
        }

        void IExtendedBuildingAI.ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {
            var actual_reason_byte = (byte)(GetExtendedActualTransferReason(buildingID, ref data) - 200);
            ExtendedTransferManager.TransferReason actualTransferReason = (ExtendedTransferManager.TransferReason)actual_reason_byte;
            if (material == actualTransferReason)
            {
                int num = data.m_customBuffer1 * 100;
                amountDelta = Mathf.Clamp(amountDelta, -num, m_storageCapacity - num);
                data.m_customBuffer1 = (ushort)((num + amountDelta) / 100);
            }
        }

        public override void BuildingDeactivated(ushort buildingID, ref Building data)
        {
            byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref data);
            if (actualTransferReason != 255 && actualTransferReason < 200)
            {
                TransferManager.TransferOffer offer = default;
                offer.Building = buildingID;
                Singleton<TransferManager>.instance.RemoveIncomingOffer((TransferManager.TransferReason)actualTransferReason, offer);
                Singleton<TransferManager>.instance.RemoveOutgoingOffer((TransferManager.TransferReason)actualTransferReason, offer);
                RemoveGuestVehicles(buildingID, ref data, (TransferManager.TransferReason)actualTransferReason);
            }
            else if (actualTransferReason != 255 && actualTransferReason >= 200)
            {
                byte material_byte = (byte)(actualTransferReason - 200);
                ExtendedTransferManager.Offer offer = default;
                offer.Building = buildingID;
                Singleton<ExtendedTransferManager>.instance.RemoveIncomingOffer((ExtendedTransferManager.TransferReason)material_byte, offer);
                Singleton<ExtendedTransferManager>.instance.RemoveOutgoingOffer((ExtendedTransferManager.TransferReason)material_byte, offer);
                RemoveExtendedGuestVehicles(buildingID, ref data, (ExtendedTransferManager.TransferReason)material_byte);
            }
            ReleaseAnimals(buildingID, ref data);
            if (data.m_subBuilding != 0 && data.m_parentBuilding == 0)
            {
                int num = 0;
                ushort subBuilding = data.m_subBuilding;
                while (subBuilding != 0)
                {
                    ReleaseAnimals(subBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding]);
                    subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding;
                    if (++num > 49152)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
            BuildingDeactivatedPlayerBuildingAI(this, buildingID, ref data);
        }

        private void ReleaseAnimals(ushort buildingID, ref Building data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort num = data.m_targetCitizens;
            int num2 = 0;
            while (num != 0)
            {
                ushort nextTargetInstance = instance.m_instances.m_buffer[num].m_nextTargetInstance;
                if (instance.m_instances.m_buffer[num].Info.m_citizenAI.IsAnimal())
                {
                    instance.ReleaseCitizenInstance(num);
                }
                num = nextTargetInstance;
                if (++num2 > 65536)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        private void RemoveGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material)
        {
            Vehicle[] buffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                ushort nextGuestVehicle = buffer[num].m_nextGuestVehicle;
                if (buffer[num].m_targetBuilding == buildingID && ((uint)buffer[num].m_transferType & (uint)material) != 0)
                {
                    VehicleInfo info = buffer[num].Info;
                    if (info != null)
                    {
                        info.m_vehicleAI.SetTarget(num, ref buffer[num], 0);
                    }
                }
                num = nextGuestVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        private void RemoveExtendedGuestVehicles(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material)
        {
            Vehicle[] buffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                ushort nextGuestVehicle = buffer[num].m_nextGuestVehicle;
                if (buffer[num].m_targetBuilding == buildingID && ((uint)buffer[num].m_transferType & (uint)material) != 0)
                {
                    VehicleInfo info = buffer[num].Info;
                    if (info != null)
                    {
                        info.m_vehicleAI.SetTarget(num, ref buffer[num], 0);
                    }
                }
                num = nextGuestVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        protected override void ProduceGoods(ushort buildingID, ref Building buildingData, ref Building.Frame frameData, int productionRate, int finalProductionRate, ref Citizen.BehaviourData behaviour, int aliveWorkerCount, int totalWorkerCount, int workPlaceCount, int aliveVisitorCount, int totalVisitorCount, int visitPlaceCount)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte b = instance.GetPark(buildingData.m_position);
            if (b != 0 && !instance.m_parks.m_buffer[b].IsIndustry)
            {
                b = 0;
            }
            if (finalProductionRate != 0)
            {
                HandleDead(buildingID, ref buildingData, ref behaviour, totalWorkerCount);
                byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref buildingData);
                byte transferReason = GetExtendedTransferReason(buildingID, ref buildingData);
                if (actualTransferReason != 255 && actualTransferReason < 200)
                {
                    int maxLoadSize = GetMaxLoadSize();
                    bool flag = IsFull(buildingID, ref buildingData);
                    int num = buildingData.m_customBuffer1 * 100;
                    int num2 = (finalProductionRate * m_truckCount + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    CalculateOwnVehicles(buildingID, ref buildingData, (TransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                    buildingData.m_tempExport = (byte)Mathf.Clamp(outside, buildingData.m_tempExport, 255);
                    int count2 = 0;
                    int cargo2 = 0;
                    int capacity2 = 0;
                    int outside2 = 0;
                    CalculateGuestVehicles(buildingID, ref buildingData, (TransferManager.TransferReason)actualTransferReason, ref count2, ref cargo2, ref capacity2, ref outside2);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(outside2, buildingData.m_tempImport, 255);
                    if (b != 0)
                    {
                        instance.m_parks.m_buffer[b].AddBufferStatus((TransferManager.TransferReason)actualTransferReason, num, cargo2, m_storageCapacity);
                    }
                    ushort num3 = buildingID;
                    if (m_subStations > 0)
                    {
                        uint num4 = Singleton<SimulationManager>.instance.m_randomizer.UInt32((uint)(m_subStations * 5 + 1));
                        if (num4 != 0)
                        {
                            for (ushort subBuilding = buildingData.m_subBuilding; subBuilding != 0; subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding)
                            {
                                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].Info;
                                if (info != null && info.m_buildingAI is ExtendedWarehouseStationAI)
                                {
                                    if (num4 <= 5)
                                    {
                                        num3 = subBuilding;
                                        break;
                                    }
                                    num4 -= 5;
                                }
                            }
                        }
                    }
                    bool flag2 = num3 == buildingID;
                    if (transferReason == actualTransferReason)
                    {
                        if (num >= maxLoadSize && (count < num2 || !flag2))
                        {
                            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                            if ((buildingData.m_flags & Building.Flags.Filling) != Building.Flags.None)
                            {
                                offer.Priority = 0;
                            }
                            else if ((buildingData.m_flags & Building.Flags.Downgrading) != 0)
                            {
                                offer.Priority = Mathf.Clamp(num / Mathf.Max(1, m_storageCapacity >> 2) + 2, 0, 2);
                                if (!flag2)
                                {
                                    offer.Priority += 2;
                                }
                            }
                            else
                            {
                                offer.Priority = Mathf.Clamp(num / Mathf.Max(1, m_storageCapacity >> 2) - 1, 0, 2);
                            }
                            offer.Building = num3;
                            offer.Position = buildingData.m_position;
                            offer.Amount = ((!flag2) ? Mathf.Clamp(num / maxLoadSize, 1, 15) : Mathf.Min(Mathf.Max(1, num / maxLoadSize), num2 - count));
                            offer.Active = true;
                            offer.Exclude = flag2;
                            offer.Unlimited = !flag2;
                            Singleton<TransferManager>.instance.AddOutgoingOffer((TransferManager.TransferReason)actualTransferReason, offer);
                        }
                        if ((buildingData.m_flags & Building.Flags.Downgrading) != Building.Flags.None)
                        {
                            Vehicle[] buffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
                            ushort num14 = buildingData.m_guestVehicles;
                            int num15 = 0;
                            while (num14 != 0 && cargo2 > 0 && (float)(num + cargo2) > (float)m_storageCapacity * 0.2f + (float)maxLoadSize)
                            {
                                ushort nextGuestVehicle = buffer[(int)num14].m_nextGuestVehicle;
                                if (buffer[(int)num14].m_targetBuilding == buildingID && (byte)buffer[(int)num14].m_transferType == actualTransferReason)
                                {
                                    VehicleInfo info2 = buffer[(int)num14].Info;
                                    if (info2 != null)
                                    {
                                        cargo2 = Mathf.Max(0, cargo2 - (int)buffer[(int)num14].m_transferSize);
                                        info2.m_vehicleAI.SetTarget(num14, ref buffer[(int)num14], 0);
                                    }
                                }
                                num14 = nextGuestVehicle;
                                if (++num15 > 16384)
                                {
                                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                    break;
                                }
                            }
                        }
                        num += cargo2;
                        if (num < m_storageCapacity)
                        {
                            TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                            bool flag3 = true;
                            if ((buildingData.m_flags & Building.Flags.Downgrading) != Building.Flags.None)
                            {
                                if ((float)num < (float)m_storageCapacity * 0.2f)
                                {
                                    offer2.Priority = 0;
                                }
                                else
                                {
                                    flag3 = false;
                                }
                            }
                            else if ((buildingData.m_flags & Building.Flags.Filling) != Building.Flags.None)
                            {
                                offer2.Priority = Mathf.Clamp((m_storageCapacity - num) / Mathf.Max(1, m_storageCapacity >> 2) + 1, 0, 2);
                                if (!flag2)
                                {
                                    offer2.Priority += 2;
                                }
                            }
                            else
                            {
                                offer2.Priority = Mathf.Clamp((m_storageCapacity - num) / Mathf.Max(1, m_storageCapacity >> 2) - 1, 0, 2);
                            }
                            if (flag3)
                            {
                                offer2.Building = num3;
                                offer2.Position = buildingData.m_position;
                                offer2.Amount = Mathf.Max(1, (m_storageCapacity - num) / maxLoadSize);
                                offer2.Active = false;
                                offer2.Exclude = flag2;
                                offer2.Unlimited = !flag2;
                                Singleton<TransferManager>.instance.AddIncomingOffer((TransferManager.TransferReason)actualTransferReason, offer2);
                            }
                        }
                    }
                    else if (num > 0 && (count < num2 || !flag2))
                    {
                        TransferManager.TransferOffer offer3 = default;
                        offer3.Priority = 8;
                        offer3.Building = num3;
                        offer3.Position = buildingData.m_position;
                        offer3.Amount = ((!flag2) ? Mathf.Clamp(num / maxLoadSize, 1, 15) : Mathf.Min(Mathf.Max(1, num / maxLoadSize), num2 - count));
                        offer3.Active = true;
                        offer3.Exclude = flag2;
                        offer3.Unlimited = !flag2;
                        Singleton<TransferManager>.instance.AddOutgoingOffer((TransferManager.TransferReason)actualTransferReason, offer3);
                    }
                    bool flag4 = IsFull(buildingID, ref buildingData);
                    if (flag != flag4)
                    {
                        if (flag4)
                        {
                            if ((object)m_fullPassMilestone != null)
                            {
                                m_fullPassMilestone.Unlock();
                            }
                        }
                        else if ((object)m_fullPassMilestone != null)
                        {
                            m_fullPassMilestone.Relock();
                        }
                    }
                    if (actualTransferReason != transferReason && buildingData.m_customBuffer1 == 0)
                    {
                        buildingData.m_adults = buildingData.m_seniors;
                        SetContentFlags(buildingID, ref buildingData, (TransferManager.TransferReason)transferReason);
                    }
                }
                else if (actualTransferReason != 255 && actualTransferReason >= 200)
                {
                    int maxLoadSize = GetMaxLoadSize();
                    bool flag = IsFull(buildingID, ref buildingData);
                    int num = buildingData.m_customBuffer1 * 100;
                    int num2 = (finalProductionRate * m_truckCount + 99) / 100;
                    int count = 0;
                    int cargo = 0;
                    int capacity = 0;
                    int outside = 0;
                    ExtedndedVehicleManager.CalculateOwnVehicles(buildingID, ref buildingData, (ExtendedTransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                    buildingData.m_tempExport = (byte)Mathf.Clamp(outside, buildingData.m_tempExport, 255);
                    int count2 = 0;
                    int cargo2 = 0;
                    int capacity2 = 0;
                    int outside2 = 0;
                    CalculateGuestVehiclesExtended(buildingID, ref buildingData, (ExtendedTransferManager.TransferReason)actualTransferReason, ref count2, ref cargo2, ref capacity2, ref outside2);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(outside2, buildingData.m_tempImport, 255);
                    if (b != 0)
                    {
                        instance.m_parks.m_buffer[b].AddBufferStatus((TransferManager.TransferReason)actualTransferReason, num, cargo2, m_storageCapacity);
                    }
                    ushort num3 = buildingID;
                    if (m_subStations > 0)
                    {
                        uint num4 = Singleton<SimulationManager>.instance.m_randomizer.UInt32((uint)(m_subStations * 5 + 1));
                        if (num4 != 0)
                        {
                            for (ushort subBuilding = buildingData.m_subBuilding; subBuilding != 0; subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding)
                            {
                                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].Info;
                                if (info != null && info.m_buildingAI is ExtendedWarehouseStationAI)
                                {
                                    if (num4 <= 5)
                                    {
                                        num3 = subBuilding;
                                        break;
                                    }
                                    num4 -= 5;
                                }
                            }
                        }
                    }
                    bool flag2 = num3 == buildingID;
                    if (actualTransferReason == transferReason)
                    {
                        if (num >= maxLoadSize && (count < num2 || !flag2))
                        {
                            var material_byte = (byte)(actualTransferReason - 200);
                            ExtendedTransferManager.Offer offer = default;
                            if ((buildingData.m_flags & Building.Flags.Filling) != Building.Flags.None)
                            {
                                offer.IsWarehouse = false;
                            }
                            else if ((buildingData.m_flags & Building.Flags.Downgrading) != 0)
                            {
                                offer.IsWarehouse = false;
                            }
                            else
                            {
                                offer.IsWarehouse = true;
                            }
                            offer.Building = num3;
                            offer.Position = buildingData.m_position;
                            offer.Amount = ((!flag2) ? Mathf.Clamp(num / maxLoadSize, 1, 15) : Mathf.Min(Mathf.Max(1, num / maxLoadSize), num2 - count));
                            offer.Active = true;
                            Singleton<ExtendedTransferManager>.instance.AddOutgoingOffer((ExtendedTransferManager.TransferReason)material_byte, offer);
                        }
                        if ((buildingData.m_flags & Building.Flags.Downgrading) != Building.Flags.None)
                        {
                            Vehicle[] buffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;
                            ushort num14 = buildingData.m_guestVehicles;
                            int num15 = 0;
                            while (num14 != 0 && cargo2 > 0 && (float)(num + cargo2) > (float)m_storageCapacity * 0.2f + (float)maxLoadSize)
                            {
                                ushort nextGuestVehicle = buffer[(int)num14].m_nextGuestVehicle;
                                if (buffer[(int)num14].m_targetBuilding == buildingID && (byte)buffer[(int)num14].m_transferType == actualTransferReason)
                                {
                                    VehicleInfo info2 = buffer[(int)num14].Info;
                                    if (info2 != null)
                                    {
                                        cargo2 = Mathf.Max(0, cargo2 - (int)buffer[(int)num14].m_transferSize);
                                        info2.m_vehicleAI.SetTarget(num14, ref buffer[(int)num14], 0);
                                    }
                                }
                                num14 = nextGuestVehicle;
                                if (++num15 > 16384)
                                {
                                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                    break;
                                }
                            }
                        }
                        num += cargo2;
                        if (num < m_storageCapacity)
                        {
                            ExtendedTransferManager.Offer offer2 = default;
                            bool flag3 = true;
                            if ((buildingData.m_flags & Building.Flags.Downgrading) != Building.Flags.None)
                            {
                                if ((float)num < (float)m_storageCapacity * 0.2f)
                                {
                                    offer2.IsWarehouse = false;
                                }
                                else
                                {
                                    flag3 = false;
                                }
                            }
                            else if ((buildingData.m_flags & Building.Flags.Filling) != 0)
                            {
                                offer2.IsWarehouse = false;
                            }
                            else
                            {
                                offer2.IsWarehouse = true;
                            }
                            if (flag3)
                            {
                                var material_byte = (byte)(actualTransferReason - 200);
                                offer2.Building = num3;
                                offer2.Position = buildingData.m_position;
                                offer2.Amount = Mathf.Max(1, (m_storageCapacity - num) / maxLoadSize);
                                offer2.Active = false;
                                Singleton<ExtendedTransferManager>.instance.AddIncomingOffer((ExtendedTransferManager.TransferReason)material_byte, offer2);
                            }
                        }
                    }
                    else if (num > 0 && (count < num2 || !flag2))
                    {
                        var material_byte = (byte)(actualTransferReason - 200);
                        ExtendedTransferManager.Offer offer3 = default;
                        offer3.Building = num3;
                        offer3.Position = buildingData.m_position;
                        offer3.Amount = ((!flag2) ? Mathf.Clamp(num / maxLoadSize, 1, 15) : Mathf.Min(Mathf.Max(1, num / maxLoadSize), num2 - count));
                        offer3.Active = true;
                        Singleton<ExtendedTransferManager>.instance.AddOutgoingOffer((ExtendedTransferManager.TransferReason)material_byte, offer3);
                    }
                    bool flag4 = IsFull(buildingID, ref buildingData);
                    if (flag != flag4)
                    {
                        if (flag4)
                        {
                            if ((object)m_fullPassMilestone != null)
                            {
                                m_fullPassMilestone.Unlock();
                            }
                        }
                        else if ((object)m_fullPassMilestone != null)
                        {
                            m_fullPassMilestone.Relock();
                        }
                    }
                    if (actualTransferReason != transferReason && buildingData.m_customBuffer1 == 0)
                    {
                        buildingData.m_adults = buildingData.m_seniors;
                        SetExtendedContentFlags(buildingID, ref buildingData, (ExtendedTransferManager.TransferReason)transferReason);
                    }
                }
                int num5 = finalProductionRate * m_noiseAccumulation / 100;
                if (num5 != 0)
                {
                    Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.NoisePollution, num5, buildingData.m_position, m_noiseRadius);
                }
            }
            ProduceGoodsPlayerBuildingAI(this, buildingID, ref buildingData, ref frameData, productionRate, finalProductionRate, ref behaviour, aliveWorkerCount, totalWorkerCount, workPlaceCount, aliveVisitorCount, totalVisitorCount, visitPlaceCount);
        }

        public override void CalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            CalculateGuestVehiclesCommonBuildingAI(this, buildingID, ref data, material, ref count, ref cargo, ref capacity, ref outside);
            if (m_subStations <= 0)
            {
                return;
            }
            for (ushort subBuilding = data.m_subBuilding; subBuilding != 0; subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding)
            {
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].Info;
                if (info != null)
                {
                    ExtendedWarehouseStationAI extendedWarehouseStationAI = info.m_buildingAI as ExtendedWarehouseStationAI;
                    if (extendedWarehouseStationAI != null)
                    {
                        extendedWarehouseStationAI.CalculateGuestVehicles(subBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding], material, ref count, ref cargo, ref capacity, ref outside);
                    }
                }
            }
        }

        public void CalculateGuestVehiclesExtended(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            ExtedndedVehicleManager.CalculateGuestVehicles(buildingID, ref data, material, ref count, ref cargo, ref capacity, ref outside);
            if (m_subStations <= 0)
            {
                return;
            }
            for (ushort subBuilding = data.m_subBuilding; subBuilding != 0; subBuilding = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].m_subBuilding)
            {
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding].Info;
                if (info != null)
                {
                    ExtendedWarehouseStationAI extendedWarehouseStationAI = info.m_buildingAI as ExtendedWarehouseStationAI;
                    if (extendedWarehouseStationAI != null)
                    {
                        ExtedndedVehicleManager.CalculateGuestVehicles(subBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[subBuilding], material, ref count, ref cargo, ref capacity, ref outside);
                    }
                }
            }
        }

        public void SetExtendedTransferReason(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material)
        {
            if (m_extendedStorageType != ExtendedTransferManager.TransferReason.None)
            {
                return;
            }
            ExtendedTransferManager.TransferReason seniors = (ExtendedTransferManager.TransferReason)data.m_seniors;
            if (seniors != ExtendedTransferManager.TransferReason.None)
            {
                ExtendedTransferManager.Offer offer = default;
                offer.Building = buildingID;
                Singleton<ExtendedTransferManager>.instance.RemoveIncomingOffer(seniors, offer);
                CancelExtendedIncomingTransfer(buildingID, ref data, seniors);
            }
            // set new transfer reason
            data.m_seniors = (byte)((byte)material + 200);
            if (data.m_customBuffer1 == 0)
            {
                data.m_adults = (byte)((byte)material + 200);
                SetExtendedContentFlags(buildingID, ref data, material);
            }
            Notification.ProblemStruct problems = data.m_problems;
            if (material == ExtendedTransferManager.TransferReason.None)
            {
                data.m_problems = Notification.AddProblems(data.m_problems, Notification.Problem1.ResourceNotSelected);
            }
            else
            {
                data.m_problems = Notification.RemoveProblems(data.m_problems, Notification.Problem1.ResourceNotSelected);
            }
            if (data.m_problems != problems)
            {
                Singleton<BuildingManager>.instance.UpdateNotifications(buildingID, problems, data.m_problems);
            }
        }

        private void SetContentFlags(ushort buildingID, ref Building data, TransferManager.TransferReason material)
        {
            switch (material)
            {
                case TransferManager.TransferReason.AnimalProducts:
                case TransferManager.TransferReason.Fish:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content01_Forbid) | Building.Flags.LevelUpEducation;
                    break;
                case TransferManager.TransferReason.Coal:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content02_Forbid) | Building.Flags.LevelUpLandValue;
                    break;
                case TransferManager.TransferReason.Flours:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content03_Forbid) | Building.Flags.Content03;
                    break;
                case TransferManager.TransferReason.Food:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content04_Forbid) | Building.Flags.Loading1;
                    break;
                case TransferManager.TransferReason.Petroleum:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content05_Forbid) | Building.Flags.Content05;
                    break;
                case TransferManager.TransferReason.Glass:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content06_Forbid) | Building.Flags.Content06;
                    break;
                case TransferManager.TransferReason.Goods:
                    data.m_flags = (data.m_flags & ~Building.Flags.Loading2) | Building.Flags.Content07;
                    break;
                case TransferManager.TransferReason.Lumber:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content07) | Building.Flags.Loading2;
                    break;
                case TransferManager.TransferReason.LuxuryProducts:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content06) | Building.Flags.Content06_Forbid;
                    break;
                case TransferManager.TransferReason.Metals:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content05) | Building.Flags.Content05_Forbid;
                    break;
                case TransferManager.TransferReason.Paper:
                    data.m_flags = (data.m_flags & ~Building.Flags.Loading1) | Building.Flags.Content04_Forbid;
                    break;
                case TransferManager.TransferReason.Petrol:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content03) | Building.Flags.Content03_Forbid;
                    break;
                case TransferManager.TransferReason.Plastics:
                    data.m_flags = (data.m_flags & ~Building.Flags.LevelUpLandValue) | Building.Flags.Content02_Forbid;
                    break;
                case TransferManager.TransferReason.PlanedTimber:
                    data.m_flags = (data.m_flags & ~Building.Flags.LevelUpEducation) | Building.Flags.Content01_Forbid;
                    break;
                default:
                    data.m_flags &= ~Building.Flags.ContentMask;
                    break;
            }
        }

        private void SetExtendedContentFlags(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material)
        {
            switch (material)
            {
                case ExtendedTransferManager.TransferReason.FoodProducts:
                case ExtendedTransferManager.TransferReason.BeverageProducts:
                case ExtendedTransferManager.TransferReason.BakedGoods:
                case ExtendedTransferManager.TransferReason.CannedFish:
                case ExtendedTransferManager.TransferReason.Furnitures:
                case ExtendedTransferManager.TransferReason.ElectronicProducts:
                case ExtendedTransferManager.TransferReason.IndustrialSteel:
                case ExtendedTransferManager.TransferReason.Tupperware:
                case ExtendedTransferManager.TransferReason.Toys:
                case ExtendedTransferManager.TransferReason.PrintedProducts:
                case ExtendedTransferManager.TransferReason.TissuePaper:
                case ExtendedTransferManager.TransferReason.Cloths:
                case ExtendedTransferManager.TransferReason.PetroleumProducts:
                case ExtendedTransferManager.TransferReason.Cars:
                case ExtendedTransferManager.TransferReason.Footwear:
                case ExtendedTransferManager.TransferReason.Houses:
                    data.m_flags = (data.m_flags & ~Building.Flags.Content06) | Building.Flags.Content06_Forbid;
                    break;
                default:
                    data.m_flags &= ~Building.Flags.ContentMask;
                    break;
            }
        }

        private void CancelExtendedIncomingTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                ushort nextGuestVehicle = instance.m_vehicles.m_buffer[num].m_nextGuestVehicle;
                if ((ExtendedTransferManager.TransferReason)instance.m_vehicles.m_buffer[num].m_transferType == material && (instance.m_vehicles.m_buffer[num].m_flags & (Vehicle.Flags.TransferToTarget | Vehicle.Flags.GoingBack)) == Vehicle.Flags.TransferToTarget && instance.m_vehicles.m_buffer[num].m_targetBuilding == buildingID)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[num].Info;
                    info.m_vehicleAI.SetTarget(num, ref instance.m_vehicles.m_buffer[num], 0);
                }
                num = nextGuestVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        public override string GetLocalizedTooltip()
        {
            string text = LocaleFormatter.FormatGeneric("AIINFO_WATER_CONSUMPTION", GetWaterConsumption() * 16) + Environment.NewLine + LocaleFormatter.FormatGeneric("AIINFO_ELECTRICITY_CONSUMPTION", GetElectricityConsumption() * 16);
            string text2 = LocaleFormatter.FormatGeneric("AIINFO_CAPACITY", m_storageCapacity);
            text2 = text2 + Environment.NewLine + LocaleFormatter.FormatGeneric("AIINFO_INDUSTRY_VEHICLE_COUNT", m_truckCount);
            string baseTooltip = TooltipHelper.Append(GetLocalizedTooltipPlayerBuildingAI(this), TooltipHelper.Format(LocaleFormatter.Info1, text, LocaleFormatter.Info2, text2));
            string addTooltip = TooltipHelper.Format("arrowVisible", "false", "input1Visible", "true", "input2Visible", "false", "input3Visible", "false", "input4Visible", "false", "outputVisible", "false");
            string addTooltip2;
            if (m_extendedStorageType != ExtendedTransferManager.TransferReason.None)
            {
                addTooltip2 = TooltipHelper.Format("input1", m_extendedStorageType.ToString(), "input2", string.Empty, "input3", string.Empty, "input4", string.Empty, "output", string.Empty);
            }
            else
            {
                addTooltip2 = TooltipHelper.Format("input1", m_storageType.ToString(), "input2", string.Empty, "input3", string.Empty, "input4", string.Empty, "output", string.Empty);
            }
            baseTooltip = TooltipHelper.Append(baseTooltip, addTooltip);
            return TooltipHelper.Append(baseTooltip, addTooltip2);
        }

        public override string GetLocalizedStats(ushort buildingID, ref Building data)
        {
            string text = string.Empty;
            byte actualTransferReason = GetExtendedActualTransferReason(buildingID, ref data);
            if (actualTransferReason != 255 && actualTransferReason < 200)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(m_info.m_class);
                int productionRate = GetProductionRate(100, budget);
                int num = (productionRate * m_truckCount + 99) / 100;
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                CalculateOwnVehicles(buildingID, ref data, (TransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                int num2 = data.m_customBuffer1 * 100;
                int num3 = 0;
                if (num2 != 0)
                {
                    num3 = Mathf.Max(1, num2 * 100 / m_storageCapacity);
                }
                text = text + LocaleFormatter.FormatGeneric("AIINFO_FULL", num3) + Environment.NewLine;
                text += LocaleFormatter.FormatGeneric("AIINFO_INDUSTRY_VEHICLES", count, num);
            }
            else if (actualTransferReason != 255 && actualTransferReason >= 200)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(m_info.m_class);
                int productionRate = GetProductionRate(100, budget);
                int num = (productionRate * m_truckCount + 99) / 100;
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                ExtedndedVehicleManager.CalculateOwnVehicles(buildingID, ref data, (ExtendedTransferManager.TransferReason)actualTransferReason, ref count, ref cargo, ref capacity, ref outside);
                int num2 = data.m_customBuffer1 * 100;
                int num3 = 0;
                if (num2 != 0)
                {
                    num3 = Mathf.Max(1, num2 * 100 / m_storageCapacity);
                }
                text = text + LocaleFormatter.FormatGeneric("AIINFO_FULL", num3) + Environment.NewLine;
                text += LocaleFormatter.FormatGeneric("AIINFO_INDUSTRY_VEHICLES", count, num);
            }
            return text;
        }

        public override void CheckRoadAccess(ushort buildingID, ref Building data)
        {
            CheckRoadAccessPlayerBuildingAI(this, buildingID, ref data);
            Notification.ProblemStruct problems = data.m_problems;
            if (GetExtendedTransferReason(buildingID, ref data) == (byte)TransferManager.TransferReason.None)
            {
                data.m_problems = Notification.AddProblems(data.m_problems, Notification.Problem1.ResourceNotSelected);
            }
            if (data.m_problems != problems)
            {
                Singleton<BuildingManager>.instance.UpdateNotifications(buildingID, problems, data.m_problems);
            }
        }

        public byte GetExtendedTransferReason(ushort buildingID, ref Building data)
        {
            if (m_storageType != TransferManager.TransferReason.None)
            {
                return (byte)m_storageType;
            }
            if (m_extendedStorageType != ExtendedTransferManager.TransferReason.None)
            {
                return (byte)m_extendedStorageType;
            }
            return data.m_seniors;
        }

        public byte GetExtendedActualTransferReason(ushort buildingID, ref Building data)
        {
            if (m_storageType != TransferManager.TransferReason.None)
            {
                return (byte)m_storageType;
            }
            if (m_extendedStorageType != ExtendedTransferManager.TransferReason.None)
            {
                return (byte)m_extendedStorageType;
            }
            return data.m_adults;
        }

        private void CountStations()
        {
            if (m_subStations != -1)
            {
                return;
            }
            m_subStations = 0;
            if (m_info.m_subBuildings == null)
            {
                return;
            }
            for (int i = 0; i < m_info.m_subBuildings.Length; i++)
            {
                BuildingInfo buildingInfo = m_info.m_subBuildings[i].m_buildingInfo;
                if (buildingInfo != null && buildingInfo.m_buildingAI is ExtendedWarehouseStationAI)
                {
                    m_subStations++;
                }
            }
        }

        private int GetMaxLoadSize()
        {
            return 8000;
        }

    }
}
