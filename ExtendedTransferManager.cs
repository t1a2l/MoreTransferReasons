using ColossalFramework;
using ColossalFramework.IO;
using MoreTransferReasons.AI;
using MoreTransferReasons.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreTransferReasons
{
    public class ExtendedTransferManager : SimulationManagerBase<ExtendedTransferManager, TransferProperties>, ISimulationManager
    {
        public const ushort DataVersion = 1;

        public struct Offer
        {
            public byte m_isLocalPark;
            public Vector3 Position;
            public Vector3 FixedPosition;
            public int Amount;
            public bool Active;
            public ushort Building;
            public ushort Vehicle;
            public uint Citizen;
            public InstanceID m_object;
            public bool IsWarehouse;
            public byte Park;
        }

        public enum TransferReason
        {
            MealsDeliveryLow = 0, // deliver low end food - vehicle
            MealsDeliveryMedium = 1, // deliver regular food - vehicle
            MealsDeliveryHigh = 2, // deliver high end food - vehicle
            Anchovy = 3,
            Salmon = 4,
            Shellfish = 5,
            Tuna = 6,
            Algae = 7,
            Seaweed = 8,
            Trout = 9,
            SheepMilk = 10,
            CowMilk = 11,
            HighlandCowMilk = 12,
            LambMeat = 13,
            BeefMeat = 14,
            HighlandBeefMeat = 15,
            PorkMeat = 16,
            Fruits = 17,
            Vegetables = 18,
            Wool = 19,
            Cotton = 20,
            Cows = 21,
            HighlandCows = 22,
            Sheep = 23,
            Pigs = 24,
            FoodProducts = 25,
            BeverageProducts = 26,
            BakedGoods = 27,
            CannedFish = 28,
            Furnitures = 29,
            ElectronicProducts = 30,
            IndustrialSteel = 31,
            Tupperware = 32,
            Toys = 33,
            PrintedProducts = 34,
            TissuePaper = 35,
            Cloths = 36,
            PetroleumProducts = 37,
            Cars = 38,
            Footwear = 39,
            HouseParts = 40,
            Ship = 41,
            ConstructionResources = 42,
            OperationResources = 43,
            ProcessedVegetableOil = 44,
            Leather = 45,
            // until 54 only transfer types that needs a vehicle to transport them
            MealsLow = 55, // serve low end food
            MealsMedium = 56, // serve normal food
            MealsHigh = 57,  // serve high end food
            PoliceVanCriminalMove = 58, // carry prisoners from small police stations to big ones
            PrisonHelicopterCriminalPickup = 59, // pick up from big police stations
            PrisonHelicopterCriminalMove = 60, // transfer to prison
            CarRent = 61,
            CarBuy = 62,
            CarSell = 63,
            VehicleFuel = 64,
            VehicleFuelElectric = 65,
            VehicleWash = 66,
            VehicleMinorRepair = 68,
            VehicleMajorRepair = 69,
            VehicleOutOfFuel = 70,
            VehicleBrokenDown = 71,
            None = 255
        }

        public class Data : IDataContainer
        {
            public void Serialize(DataSerializer s)
            {
                LogHelper.Information("Begin Serializing ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "ExtendedTransferManager");
                s.version = DataVersion;
                ExtendedTransferManager instance = Singleton<ExtendedTransferManager>.instance;
                int num = 128;
                EncodedArray.Int integer = EncodedArray.Int.BeginWrite(s);
                for (int j = 0; j < num; j++)
                {
                    integer.Write(instance.OutgoingIndexes[j]);
                    integer.Write(instance.IncomingIndexes[j]);
                }
                integer.EndWrite();
                EncodedArray.Bool @bool = EncodedArray.Bool.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        @bool.Write(instance.OutgoingOffers[offer_material + i].Active);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        @bool.Write(instance.IncomingOffers[offer_material2 + j].Active);
                    }
                }
                @bool.EndWrite();
                EncodedArray.Byte byte2 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte2.Write((byte)instance.OutgoingOffers[offer_material + i].Amount);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte2.Write((byte)instance.IncomingOffers[offer_material2 + j].Amount);
                    }
                }
                byte2.EndWrite();
                EncodedArray.Byte byte3 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.x);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.x);
                    }
                }
                byte3.EndWrite();
                EncodedArray.Byte byte4 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.y);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.y);
                    }
                }
                byte4.EndWrite();
                EncodedArray.Byte byte5 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte3.Write((byte)instance.OutgoingOffers[offer_material + i].Position.z);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte3.Write((byte)instance.IncomingOffers[offer_material2 + j].Position.z);
                    }
                }
                byte5.EndWrite();
                EncodedArray.Byte byte6 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte6.Write(instance.OutgoingOffers[offer_material + i].m_isLocalPark);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte6.Write(instance.IncomingOffers[offer_material2 + j].m_isLocalPark);
                    }
                }
                byte6.EndWrite();
                EncodedArray.Byte byte7 = EncodedArray.Byte.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        byte7.Write((byte)instance.OutgoingOffers[offer_material + i].m_object.Type);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        byte7.Write((byte)instance.IncomingOffers[offer_material2 + j].m_object.Type);
                    }
                }
                byte7.EndWrite();
                EncodedArray.UInt uInt = EncodedArray.UInt.BeginWrite(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        uInt.Write(instance.OutgoingOffers[offer_material + i].m_object.Index);
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        uInt.Write(instance.IncomingOffers[offer_material2 + j].m_object.Index);
                    }
                }
                uInt.EndWrite();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndSerialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish Serializing ExtendedTransferManager");
            }

            public void Deserialize(DataSerializer s)
            {
                LogHelper.Information("Begin Deserializing ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginDeserialize(s, "ExtendedTransferManager");
                ExtendedTransferManager instance = Singleton<ExtendedTransferManager>.instance;
                if(s.version != DataVersion)
                {
                    LogHelper.Error("ExtendedTransferManager data version mismatch. Expected: " + DataVersion + ", Actual: " + s.version);
                    return;
                }

                int num = 128;
                EncodedArray.Bool @bool = EncodedArray.Bool.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Active = @bool.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Active = @bool.Read();
                    }
                }
                @bool.EndRead();
                EncodedArray.Byte byte2 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Amount = byte2.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Amount = byte2.Read();
                    }
                }
                byte2.EndRead();
                EncodedArray.Byte byte3 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.x = byte3.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.x = byte3.Read();
                    }
                }
                byte3.EndRead();
                EncodedArray.Byte byte4 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.y = byte4.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.y = byte4.Read();
                    }
                }
                byte4.EndRead();
                EncodedArray.Byte byte5 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].Position.z = byte5.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].Position.z = byte5.Read();
                    }
                }
                byte5.EndRead();
                EncodedArray.Byte byte6 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_isLocalPark = byte6.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_isLocalPark = byte6.Read();
                    }
                }
                byte6.EndRead();
                EncodedArray.Byte byte7 = EncodedArray.Byte.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_object.Type = (InstanceType)byte7.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_object.Type = (InstanceType)byte7.Read();
                    }
                }
                byte7.EndRead();
                EncodedArray.UInt uInt = EncodedArray.UInt.BeginRead(s);
                for (int material = 0; material < num; material++)
                {
                    int material_index = instance.OutgoingIndexes[material];
                    int offer_material = material;
                    offer_material *= 256;
                    for (int i = 0; i < material_index; i++)
                    {
                        instance.OutgoingOffers[offer_material + i].m_object.Index = uInt.Read();
                    }
                    int material_index2 = instance.IncomingIndexes[material];
                    int offer_material2 = material;
                    offer_material2 *= 256;
                    for (uint j = 0u; j < material_index2; j++)
                    {
                        instance.IncomingOffers[offer_material2 + j].m_object.Index = uInt.Read();
                    }
                }
                uInt.EndRead();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndDeserialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish Deserializing ExtendedTransferManager");
            }

            public void AfterDeserialize(DataSerializer s)
            {
                LogHelper.Information("Begin AfterDeserialize ExtendedTransferManager");
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginAfterDeserialize(s, "ExtendedTransferManager");
                Singleton<LoadingManager>.instance.WaitUntilEssentialScenesLoaded();
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndAfterDeserialize(s, "ExtendedTransferManager");
                LogHelper.Information("Finish AfterDeserialize ExtendedTransferManager");
            }
        }

        int[] IncomingIndexes;

        int[] OutgoingIndexes;

        Offer[] OutgoingOffers;

        Offer[] IncomingOffers;

        int Transfers_Length;

        public void ClearTransferManager()
        {
            Transfers_Length = 72;

            Array.Clear(OutgoingIndexes, 0, OutgoingIndexes.Length);
            Array.Clear(IncomingIndexes, 0, IncomingIndexes.Length);
            Array.Clear(OutgoingOffers, 0, OutgoingOffers.Length);
            Array.Clear(IncomingOffers, 0, IncomingOffers.Length);

            Array.Resize(ref OutgoingIndexes, Transfers_Length);
            Array.Resize(ref IncomingIndexes, Transfers_Length);
            Array.Resize(ref OutgoingOffers, Transfers_Length * 256);
            Array.Resize(ref IncomingOffers, Transfers_Length * 256);
        }

        protected override void Awake()
        {
            base.Awake();
            Transfers_Length = 72;
            OutgoingIndexes = new int[Transfers_Length];
            IncomingIndexes = new int[Transfers_Length];
            OutgoingOffers = new Offer[Transfers_Length * 256];
            IncomingOffers = new Offer[Transfers_Length * 256];
        }

        public static TransferReason GetFrameReason(int frameIndex)
        {
            return frameIndex switch
            {
                1 => TransferReason.MealsDeliveryLow,
                3 => TransferReason.MealsDeliveryMedium,
                5 => TransferReason.MealsDeliveryHigh, // deliver high end food - vehicle
                7 => TransferReason.Anchovy,
                9 => TransferReason.Salmon,
                11 => TransferReason.Shellfish,
                13 => TransferReason.Tuna,
                15 => TransferReason.Algae,
                17 => TransferReason.Seaweed,
                19 => TransferReason.Trout,
                21 => TransferReason.SheepMilk,
                23 => TransferReason.CowMilk,
                25 => TransferReason.HighlandCowMilk,
                27 => TransferReason.LambMeat,
                29 => TransferReason.BeefMeat,
                31 => TransferReason.HighlandBeefMeat,
                33 => TransferReason.PorkMeat,
                35 => TransferReason.Fruits,
                37 => TransferReason.Vegetables,
                39 => TransferReason.Wool,
                41 => TransferReason.Cotton,
                43 => TransferReason.Cows,
                45 => TransferReason.HighlandCows,
                47 => TransferReason.Sheep,
                49 => TransferReason.Pigs,
                51 => TransferReason.FoodProducts,
                53 => TransferReason.BeverageProducts,
                55 => TransferReason.BakedGoods,
                57 => TransferReason.CannedFish,
                59 => TransferReason.Furnitures,
                61 => TransferReason.ElectronicProducts,
                63 => TransferReason.IndustrialSteel,
                65 => TransferReason.Tupperware,
                67 => TransferReason.Toys,
                69 => TransferReason.PrintedProducts,
                71 => TransferReason.TissuePaper,
                73 => TransferReason.Cloths,
                75 => TransferReason.PetroleumProducts,
                77 => TransferReason.Cars,
                79 => TransferReason.Footwear,
                81 => TransferReason.HouseParts,
                83 => TransferReason.Ship,
                85 => TransferReason.ConstructionResources,
                87 => TransferReason.OperationResources,
                89 => TransferReason.ProcessedVegetableOil,
                91 => TransferReason.Leather,
                // 46 47 48 49 50  51  52  53  54
                // 93 95 97 99 101 103 105 107 109
                // until 54 only transfer types that needs vehicle to transport them
                111 => TransferReason.MealsLow,
                113 => TransferReason.MealsMedium,
                115 => TransferReason.MealsHigh,
                117 => TransferReason.PoliceVanCriminalMove,
                119 => TransferReason.PrisonHelicopterCriminalPickup,
                121 => TransferReason.PrisonHelicopterCriminalMove,
                123 => TransferReason.CarRent,
                125 => TransferReason.CarBuy,
                127 => TransferReason.CarSell,
                129 => TransferReason.VehicleFuel,
                131 => TransferReason.VehicleFuelElectric,
                133 => TransferReason.VehicleWash,
                135 => TransferReason.VehicleMinorRepair,
                137 => TransferReason.VehicleMajorRepair,
                139 => TransferReason.VehicleOutOfFuel,
                141 => TransferReason.VehicleBrokenDown,
                _ => TransferReason.None,
            };
        }

        public void AddOutgoingOffer(TransferReason material, Offer offer)
        {
            if(offer.FixedPosition == Vector3.zero)
            {
                offer.FixedPosition = offer.Position;
            }

            if (offer.Building != 0)
            {
                Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
                ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
                byte park = Singleton<DistrictManager>.instance.GetPark(buffer[offer.Building].m_position);
                if (park != 0 && Singleton<DistrictManager>.instance.m_parks.m_buffer[park].IsPedestrianZone 
                    && buffer[offer.Building].Info.m_buildingAI.GetUseServicePoint(offer.Building, ref buffer[offer.Building]) 
                    && ExtendedDistrictPark.TryGetPedestrianReason(material, out var reason))
                {
                    bool flag = false;
                    if ((Singleton<DistrictManager>.instance.m_parks.m_buffer[park].m_parkPolicies & DistrictPolicies.Park.ForceServicePoint) != 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        ushort accessSegment = buffer[offer.Building].m_accessSegment;
                        if (accessSegment == 0 && (buffer[offer.Building].m_problems & new Notification.ProblemStruct(Notification.Problem1.RoadNotConnected, Notification.Problem2.NotInPedestrianZone)).IsNone)
                        {
                            buffer[offer.Building].Info.m_buildingAI.CheckRoadAccess(offer.Building, ref buffer[offer.Building]);
                            accessSegment = buffer[offer.Building].m_accessSegment;
                        }
                        if (accessSegment != 0 && (Singleton<NetManager>.instance.m_segments.m_buffer[accessSegment].Info.m_vehicleCategories & reason.m_vehicleCategory) == 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        offer.m_isLocalPark = park;
                        instance2.m_industryParks.m_buffer[park].AddMaterialSuggestion(offer.Building, material);
                    }
                }
            }
            int index = OutgoingIndexes[(int)material];
            if (index < 256)
            {
                OutgoingIndexes[(int)material] = index + 1;
                int FreeIndex = (int)material * 256 + index;
                OutgoingOffers[FreeIndex] = offer;
            }
        }

        public void AddIncomingOffer(TransferReason material, Offer offer)
        {
            if (offer.FixedPosition == Vector3.zero)
            {
                offer.FixedPosition = offer.Position;
            }

            if (offer.Building != 0)
            {
                ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
                Building[] buffer = Singleton<BuildingManager>.instance.m_buildings.m_buffer;
                byte park = Singleton<DistrictManager>.instance.GetPark(buffer[offer.Building].m_position);
                if (park != 0 && Singleton<DistrictManager>.instance.m_parks.m_buffer[park].IsPedestrianZone 
                    && buffer[offer.Building].Info.m_buildingAI.GetUseServicePoint(offer.Building, ref buffer[offer.Building]) 
                    && ExtendedDistrictPark.TryGetPedestrianReason(material, out var reason))
                {
                    bool flag = false;
                    if ((Singleton<DistrictManager>.instance.m_parks.m_buffer[park].m_parkPolicies & DistrictPolicies.Park.ForceServicePoint) != 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        ushort accessSegment = buffer[offer.Building].m_accessSegment;
                        if (accessSegment == 0 && (buffer[offer.Building].m_problems & new Notification.ProblemStruct(Notification.Problem1.RoadNotConnected, Notification.Problem2.NotInPedestrianZone)).IsNone)
                        {
                            buffer[offer.Building].Info.m_buildingAI.CheckRoadAccess(offer.Building, ref buffer[offer.Building]);
                            accessSegment = buffer[offer.Building].m_accessSegment;
                        }
                        if (accessSegment != 0 && (Singleton<NetManager>.instance.m_segments.m_buffer[accessSegment].Info.m_vehicleCategories & reason.m_vehicleCategory) == 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        offer.m_isLocalPark = park;
                        instance2.m_industryParks.m_buffer[park].AddMaterialRequest(offer.Building, material);
                    }
                }
            }
            int index = IncomingIndexes[(int)material];
            if (index < 256)
            {
                IncomingIndexes[(int)material] = index + 1;
                int FreeIndex = (int)material * 256 + index;
                IncomingOffers[FreeIndex] = offer;
            }
        }

        public void RemoveOutgoingOffer(TransferReason material, Offer offer)
        {
            int index = OutgoingIndexes[(int)material];
            for (int num3 = index - 1; num3 >= 0; num3--)
            {
                int materail_index = (int)material * 256 + index;
                if (OutgoingOffers[materail_index].m_object == offer.m_object && OutgoingOffers[materail_index].m_isLocalPark == offer.m_isLocalPark)
                {
                    OutgoingOffers[(int)material].Amount -= OutgoingOffers[materail_index].Amount;
                    int num5 = (int)material * 256 + --index;
                    ref Offer reference = ref OutgoingOffers[materail_index];
                    reference = OutgoingOffers[num5];
                }
            }
            OutgoingIndexes[(int)material] = (ushort)index;
        }

        public void RemoveIncomingOffer(TransferReason material, Offer offer)
        {
            int index = IncomingIndexes[(int)material];
            for (int num3 = index - 1; num3 >= 0; num3--)
            {
                int materail_index = (int)material * 256 + index;
                if (IncomingOffers[materail_index].m_object == offer.m_object && IncomingOffers[materail_index].m_isLocalPark == offer.m_isLocalPark)
                {
                    IncomingOffers[(int)material].Amount -= IncomingOffers[materail_index].Amount;
                    int num5 = (int)material * 256 + --index;
                    ref Offer reference = ref IncomingOffers[materail_index];
                    reference = IncomingOffers[num5];
                }
            }
            IncomingIndexes[(int)material] = (ushort)index;
        }

        protected override void SimulationStepImpl(int subStep)
        {
            if (subStep != 0)
            {
                int frameIndex = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 0xFF);
                MatchOffers(GetFrameReason(frameIndex));
            }
        }

        private void MatchOffers(TransferReason material)
        {
            if (material == TransferReason.None)
            {
                return;
            }
            int outgoing_matched_count = 0;
            int incoming_matched_count = 0;
            int outgoing_ocuppied_count = OutgoingIndexes[(int)material];
            int incoming_ocuppied_count = IncomingIndexes[(int)material];
            while (outgoing_matched_count < outgoing_ocuppied_count || incoming_matched_count < incoming_ocuppied_count)
            {
                if (outgoing_matched_count < outgoing_ocuppied_count)
                {
                    Offer outgoing_offer = OutgoingOffers[(int)material * 256 + outgoing_matched_count];
                    Vector3 outgoing_position = outgoing_offer.Position;
                    Vector3 outgoing_position_fixed = outgoing_offer.FixedPosition;
                    int outgoing_amount = outgoing_offer.Amount;
                    while (outgoing_amount != 0)
                    {
                        int chosen_index = -1;
                        double min_distance = Math.Pow(65000, 2);
                        for (int i = 0; i < incoming_ocuppied_count; i++)
                        {
                            Offer incoming_offer = IncomingOffers[(int)material * 256 + i];
                            if (outgoing_offer.Building == incoming_offer.Building)
                            {
                                continue;
                            }
                            if (outgoing_offer.IsWarehouse && incoming_offer.IsWarehouse)
                            {
                                continue;
                            }
                            if (incoming_offer.Position != incoming_offer.FixedPosition && outgoing_position != outgoing_position_fixed)
                            {
                                var actual_distance = Vector3.SqrMagnitude(incoming_offer.FixedPosition - outgoing_position_fixed);
                                if (actual_distance < 10000)
                                {
                                    continue;
                                }
                            }
                            double distance = Vector3.SqrMagnitude(incoming_offer.Position - outgoing_position);
                            if (distance < min_distance)
                            {
                                chosen_index = i;
                                min_distance = distance;
                                if (distance < 0f)
                                {
                                    break;
                                }
                            }
                        }
                        if (chosen_index == -1)
                        {
                            break;
                        }
                        Offer chosen_incoming_offer = IncomingOffers[(int)material * 256 + chosen_index];
                        int incoming_amount = chosen_incoming_offer.Amount;
                        int min_amount = Mathf.Min(outgoing_amount, incoming_amount);
                        if (min_amount != 0)
                        {
                            StartTransfer(material, chosen_incoming_offer, outgoing_offer, min_amount);
                        }
                        outgoing_amount -= min_amount;
                        incoming_amount -= min_amount;
                        if (incoming_amount == 0)
                        {
                            int new_index = IncomingIndexes[(int)material] - 1;
                            IncomingIndexes[(int)material] = new_index;
                            ref Offer reference = ref IncomingOffers[(int)material * 256 + chosen_index];
                            reference = IncomingOffers[(int)material * 256 + new_index];
                            incoming_ocuppied_count = new_index;
                        }
                        else
                        {
                            chosen_incoming_offer.Amount = incoming_amount;
                            IncomingOffers[(int)material * 256 + chosen_index] = chosen_incoming_offer;
                        }
                        outgoing_offer.Amount = outgoing_amount;
                    }
                    if (outgoing_amount == 0)
                    {
                        outgoing_ocuppied_count--;
                        OutgoingIndexes[(int)material] = outgoing_ocuppied_count;
                        ref Offer reference2 = ref OutgoingOffers[(int)material * 256 + outgoing_matched_count];
                        reference2 = OutgoingOffers[(int)material * 256 + outgoing_ocuppied_count];
                    }
                    else
                    {
                        outgoing_offer.Amount = outgoing_amount;
                        OutgoingOffers[(int)material * 256 + outgoing_matched_count] = outgoing_offer;
                        outgoing_matched_count++;
                    }
                }
                if (incoming_matched_count < incoming_ocuppied_count)
                {
                    Offer incoming_offer = IncomingOffers[(int)material * 256 + incoming_matched_count];
                    Vector3 incoming_position = incoming_offer.Position;
                    Vector3 incoming_position_fixed = incoming_offer.FixedPosition;
                    int incoming_amount = incoming_offer.Amount;
                    while (incoming_amount != 0)
                    {
                        int chosen_index = -1;
                        float min_distance = -1f;
                        for (int i = 0; i < outgoing_ocuppied_count; i++)
                        {
                            Offer outgoing_offer = OutgoingOffers[(int)material * 256 + i];
                            if (incoming_offer.Building == outgoing_offer.Building)
                            {
                                continue;
                            }
                            if (incoming_offer.IsWarehouse && outgoing_offer.IsWarehouse)
                            {
                                continue;
                            }
                            if (outgoing_offer.Position != outgoing_offer.FixedPosition && incoming_position != incoming_position_fixed)
                            {
                                var actual_distance = Vector3.SqrMagnitude(outgoing_offer.FixedPosition - incoming_position_fixed);
                                if (actual_distance < 10000)
                                {
                                    continue;
                                }
                            }
                            float distance = Vector3.SqrMagnitude(outgoing_offer.Position - incoming_position);
                            if (distance < min_distance)
                            {
                                chosen_index = i;
                                min_distance = distance;
                                if (distance < 0f)
                                {
                                    break;
                                }
                            }
                        }
                        if (chosen_index == -1)
                        {
                            break;
                        }
                        Offer chosen_outgoing_offer = OutgoingOffers[(int)material * 256 + chosen_index];
                        int outgoing_amount = chosen_outgoing_offer.Amount;
                        int min_amount = Mathf.Min(outgoing_amount, incoming_amount);
                        if (min_amount != 0)
                        {
                            StartTransfer(material, chosen_outgoing_offer, incoming_offer, min_amount);
                        }
                        incoming_amount -= min_amount;
                        outgoing_amount -= min_amount;
                        if (outgoing_amount == 0)
                        {
                            int new_index = OutgoingIndexes[(int)material] - 1;
                            OutgoingIndexes[(int)material] = new_index;
                            ref Offer reference = ref OutgoingOffers[(int)material * 256 + chosen_index];
                            reference = OutgoingOffers[(int)material * 256 + new_index];
                            outgoing_ocuppied_count = new_index;
                        }
                        else
                        {
                            chosen_outgoing_offer.Amount = outgoing_amount;
                            OutgoingOffers[(int)material * 256 + chosen_index] = chosen_outgoing_offer;
                        }
                        incoming_offer.Amount = incoming_amount;
                    }
                    if (incoming_amount == 0)
                    {
                        incoming_ocuppied_count--;
                        IncomingIndexes[(int)material] = incoming_ocuppied_count;
                        ref Offer reference2 = ref IncomingOffers[(int)material * 256 + incoming_matched_count];
                        reference2 = IncomingOffers[(int)material * 256 + incoming_ocuppied_count];
                    }
                    else
                    {
                        incoming_offer.Amount = incoming_amount;
                        IncomingOffers[(int)material * 256 + incoming_matched_count] = incoming_offer;
                        incoming_matched_count++;
                    }
                }
            }
            OutgoingIndexes[(int)material] = 0;
            IncomingIndexes[(int)material] = 0;
        }

        private void StartTransfer(TransferReason material, Offer offerOut, Offer offerIn, int delta)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
            bool active = offerIn.Active;
            bool active2 = offerOut.Active;
            if (offerOut.Park != 0 && instance2.m_industryParks.m_buffer[offerOut.Park].TryGetRandomServicePoint(instance.m_parks.m_buffer[offerOut.Park], material, out var buildingID))
            {
                offerOut.Building = buildingID;
            }
            if (offerIn.Park != 0 && instance2.m_industryParks.m_buffer[offerIn.Park].TryGetRandomServicePoint(instance.m_parks.m_buffer[offerIn.Park], material, out var buildingID2))
            {
                offerIn.Building = buildingID2;
            }
            if (active && offerIn.Vehicle != 0)
            {
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle = offerIn.Vehicle;
                offerOut.Amount = delta;
                VehicleInfo info = vehicles.m_buffer[vehicle].Info;
                if (info.m_vehicleAI is IExtendedVehicleAI extendedVehicleAI)
                {
                    extendedVehicleAI.ExtendedStartTransfer(vehicle, ref vehicles.m_buffer[vehicle], material, offerOut);
                }
            }
            else if (active2 && offerOut.Vehicle != 0)
            {
                Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle2 = offerOut.Vehicle;
                offerIn.Amount = delta;
                VehicleInfo info2 = vehicles2.m_buffer[vehicle2].Info;
                if (info2.m_vehicleAI is IExtendedVehicleAI extendedVehicleAI)
                {
                    extendedVehicleAI.ExtendedStartTransfer(vehicle2, ref vehicles2.m_buffer[vehicle2], material, offerIn);
                }
            }
            else if (active && offerIn.Citizen != 0U)
            {
                Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen = offerIn.Citizen;
                offerOut.Amount = delta;
                CitizenInfo citizenInfo = citizens.m_buffer[(int)(UIntPtr)citizen].GetCitizenInfo(citizen);
                if (citizenInfo != null)
                {
                    if (citizenInfo.m_citizenAI is IExtendedCitizenAI extendedCitizenAI)
                    {
                        extendedCitizenAI.ExtendedStartTransfer(citizen, ref citizens.m_buffer[(int)(UIntPtr)citizen], material, offerOut);
                    }
                }
            }
            else if (active2 && offerOut.Citizen != 0U)
            {
                Array32<Citizen> citizens2 = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen2 = offerOut.Citizen;
                offerIn.Amount = delta;
                CitizenInfo citizenInfo2 = citizens2.m_buffer[(int)(UIntPtr)citizen2].GetCitizenInfo(citizen2);
                if (citizenInfo2 != null)
                {
                    if (citizenInfo2.m_citizenAI is IExtendedCitizenAI extendedCitizenAI)
                    {
                        extendedCitizenAI.ExtendedStartTransfer(citizen2, ref citizens2.m_buffer[(int)(UIntPtr)citizen2], material, offerIn);
                    }
                }
            }
            else if (active2 && offerOut.Building != 0)
            {
                Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
                offerIn.Amount = delta;
                if (offerOut.m_isLocalPark != 0 && offerOut.m_isLocalPark == offerIn.m_isLocalPark)
                {
                    StartDistrictTransfer(material, offerOut, offerIn);
                }
                else
                {
                    ushort building = offerOut.Building;
                    BuildingInfo info3 = buildings.m_buffer[building].Info;
                    if (info3.m_buildingAI is IExtendedBuildingAI extendedBuildingAI)
                    {
                        extendedBuildingAI.ExtendedStartTransfer(building, ref buildings.m_buffer[building], material, offerIn);
                    }
                }
            }
            else if (active && offerIn.Building != 0)
            {
                Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
                offerOut.Amount = delta;
                if (offerIn.m_isLocalPark != 0 && offerIn.m_isLocalPark == offerOut.m_isLocalPark)
                {
                    StartDistrictTransfer(material, offerOut, offerIn);
                }
                else
                {
                    ushort building2 = offerIn.Building;
                    BuildingInfo info4 = buildings2.m_buffer[building2].Info;
                    if (info4.m_buildingAI is IExtendedBuildingAI extendedBuildingAI)
                    {
                        extendedBuildingAI.ExtendedStartTransfer(building2, ref buildings2.m_buffer[building2], material, offerOut);
                    }
                }
            }
        }

        private void StartDistrictTransfer(TransferReason material, Offer offerOut, Offer offerIn)
        {
            Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
            ushort building = offerOut.Building;
            ushort building2 = offerIn.Building;
            BuildingInfo info = buildings.m_buffer[building].Info;
            BuildingInfo info2 = buildings.m_buffer[building2].Info;
            if (info.m_buildingAI is IExtendedBuildingAI extendedBuildingAI && info2.m_buildingAI is IExtendedBuildingAI extendedBuildingAI2)
            {
                extendedBuildingAI.ExtendedGetMaterialAmount(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, out int num, out int num2);
                extendedBuildingAI2.ExtendedGetMaterialAmount(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, out int num3, out int num4);
                int num5 = Math.Min(num, num4 - num3);
                if (num5 > 0)
                {
                    num = -num5;
                    num3 = num5;
                    extendedBuildingAI.ExtendedModifyMaterialBuffer(building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building], material, ref num);
                    extendedBuildingAI2.ExtendedModifyMaterialBuffer(building2, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[building2], material, ref num3);
                }
            }
        }

        private List<TransferReason> GetExtendedTransferReason(ushort buildingId)
        {
            List<TransferReason> transferReasons = [];
            var building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId];
            var buildingAI = building.Info.GetAI();

            if(buildingAI.GetType().Name.Equals("PrisonCopterPoliceStationAI") && building.Info.m_class.m_level < ItemClass.Level.Level4 && (building.m_flags & Building.Flags.Downgrading) != 0)
            {
                transferReasons.Add(TransferReason.PoliceVanCriminalMove);
            }
            if (buildingAI.GetType().Name.Equals("PoliceHelicopterDepotAI") && (building.m_flags & Building.Flags.Downgrading) != 0)
            {
                transferReasons.Add(TransferReason.PrisonHelicopterCriminalPickup);
            }
            return transferReasons;
        }

    }
}
