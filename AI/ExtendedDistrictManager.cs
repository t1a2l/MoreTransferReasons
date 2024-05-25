using ColossalFramework.IO;
using ColossalFramework;
using System;
using System.Linq;
using System.Collections.Generic;
using static DistrictPark;

namespace MoreTransferReasons.AI
{
    public class ExtendedDistrictManager : SimulationManagerBase<ExtendedDistrictManager, DistrictProperties>, ISimulationManager
    {
        [NonSerialized]
        public Array8<ExtendedDistrictPark> m_industryParks;

        public class Data : IDataContainer
        {
            public void Serialize(DataSerializer s)
            {
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginSerialize(s, "ExtendedDistrictPark");
                DistrictManager instance = Singleton<DistrictManager>.instance;

                ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
                DistrictPark[] buffer2 = instance.m_parks.m_buffer;
                ExtendedDistrictPark[] buffer3 = instance2.m_industryParks.m_buffer;
                int num2 = buffer2.Length;

                for (int num34 = 0; num34 < num2; num34++)
                {
                    if (buffer2[num34].m_flags == DistrictPark.Flags.None)
                    {
                        continue;
                    }
                    if (buffer2[num34].IsIndustry)
                    {
                        buffer3[num34].m_sheepMilkData.Serialize(s);
                        buffer3[num34].m_cowMilkData.Serialize(s);
                        buffer3[num34].m_highlandCowMilkData.Serialize(s);
                        buffer3[num34].m_lambMeatData.Serialize(s);
                        buffer3[num34].m_beefMeatData.Serialize(s);
                        buffer3[num34].m_highlandBeefMeatData.Serialize(s);
                        buffer3[num34].m_porkBeefData.Serialize(s);
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
                    s.WriteInt32(ExtendedDistrictPark.pedestrianExtendedReasonsCount);
                    for (int num37 = 0; num37 < ExtendedDistrictPark.pedestrianExtendedReasonsCount; num37++)
                    {
                        s.WriteUInt8((byte)ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[num37].m_material);
                        s.WriteInt32(buffer3[num34].m_extendedMaterialRequest[num37].Count);
                        EncodedArray.UShort uShort = EncodedArray.UShort.BeginWrite(s);
                        foreach (ushort item in buffer3[num34].m_extendedMaterialRequest[num37])
                        {
                            uShort.Write(item);
                        }
                        uShort.EndWrite();
                    }
                    for (int num38 = 0; num38 < ExtendedDistrictPark.pedestrianExtendedReasonsCount; num38++)
                    {
                        s.WriteUInt8((byte)ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[num38].m_material);
                        s.WriteInt32(buffer3[num34].m_extendedMaterialSuggestion[num38].Count);
                        EncodedArray.UShort uShort2 = EncodedArray.UShort.BeginWrite(s);
                        foreach (ushort item2 in buffer3[num34].m_extendedMaterialSuggestion[num38])
                        {
                            uShort2.Write(item2);
                        }
                        uShort2.EndWrite();
                    }
                    for (int num42 = 0; num42 < ExtendedDistrictPark.pedestrianExtendedReasonsCount; num42++)
                    {
                        s.WriteUInt8((byte)ExtendedDistrictPark.kPedestrianZoneExtendedTransferReasons[num42].m_material);
                    }
                }
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndSerialize(s, "ExtendedDistrictManager");
            }

            public void Deserialize(DataSerializer s)
            {
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginDeserialize(s, "ExtendedDistrictManager");
                DistrictManager instance = Singleton<DistrictManager>.instance;
                ExtendedDistrictManager instance2 = Singleton<ExtendedDistrictManager>.instance;
                District[] buffer = instance.m_districts.m_buffer;
                DistrictPark[] buffer2 = instance.m_parks.m_buffer;
                ExtendedDistrictPark[] buffer3 = instance2.m_industryParks.m_buffer;
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
                                buffer3[num38].m_sheepMilkData.Deserialize(s);
                                buffer3[num38].m_cowMilkData.Deserialize(s);
                                buffer3[num38].m_highlandCowMilkData.Deserialize(s);
                                buffer3[num38].m_lambMeatData.Deserialize(s);
                                buffer3[num38].m_beefMeatData.Deserialize(s);
                                buffer3[num38].m_highlandBeefMeatData.Deserialize(s);
                                buffer3[num38].m_porkBeefData.Deserialize(s);
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
                                buffer3[num38].m_sheepMilkData = default;
                                buffer3[num38].m_cowMilkData = default;
                                buffer3[num38].m_highlandCowMilkData = default;
                                buffer3[num38].m_lambMeatData = default;
                                buffer3[num38].m_beefMeatData = default;
                                buffer3[num38].m_highlandBeefMeatData = default;
                                buffer3[num38].m_porkBeefData = default;
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
                            buffer3[num38].m_tempIncome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];
                            buffer3[num38].m_tempOutcome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];
                        }
                        int num42 = ExtendedDistrictPark.pedestrianExtendedReasonsCount;
                        if (s.version >= 115003 && buffer2[num38].IsPedestrianZone)
                        {
                            num42 = s.ReadInt32();
                            for (int num43 = 0; num43 < num42; num43++)
                            {
                                ExtendedTransferManager.TransferReason material = (ExtendedTransferManager.TransferReason)s.ReadUInt8();
                                int num44 = s.ReadInt32();
                                ExtendedDistrictPark.IsPedestrianReason(material, out var index);
                                EncodedArray.UShort uShort = EncodedArray.UShort.BeginRead(s);
                                for (int num45 = 0; num45 < num44; num45++)
                                {
                                    ushort item = uShort.Read();
                                    if (index >= 0)
                                    {
                                        buffer3[num38].m_extendedMaterialRequest[index].Enqueue(item);
                                    }
                                }
                                uShort.EndRead();
                            }
                            for (int num46 = 0; num46 < num42; num46++)
                            {
                                ExtendedTransferManager.TransferReason material2 = (ExtendedTransferManager.TransferReason)s.ReadUInt8();
                                int num47 = s.ReadInt32();
                                ExtendedDistrictPark.IsPedestrianReason(material2, out var index2);
                                EncodedArray.UShort uShort2 = EncodedArray.UShort.BeginRead(s);
                                for (int num48 = 0; num48 < num47; num48++)
                                {
                                    ushort item2 = uShort2.Read();
                                    if (index2 >= 0)
                                    {
                                        buffer3[num38].m_extendedMaterialSuggestion[index2].Enqueue(item2);
                                    }
                                }
                                uShort2.EndRead();
                            }
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
                                    buffer3[num38].m_tempIncome[index3] = num56;
                                    buffer3[num38].m_tempOutcome[index3] = num58;
                                }
                            }
                        }
                    }
                    else
                    {
                        buffer3[num38].m_sheepMilkData = default;
                        buffer3[num38].m_cowMilkData = default;
                        buffer3[num38].m_highlandCowMilkData = default;
                        buffer3[num38].m_lambMeatData = default;
                        buffer3[num38].m_beefMeatData = default;
                        buffer3[num38].m_highlandBeefMeatData = default;
                        buffer3[num38].m_porkBeefData = default;
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
                Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndDeserialize(s, "ExtendedDistrictManager");
            }

            public void AfterDeserialize(DataSerializer s)
            {
                throw new NotImplementedException();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_industryParks = new Array8<ExtendedDistrictPark>(128u);
            _ = CreatePark(out _, DistrictPark.ParkType.None, DistrictPark.ParkLevel.None);
        }

        public bool CreatePark(out byte park, DistrictPark.ParkType type, DistrictPark.ParkLevel level)
        {
            if (m_industryParks.CreateItem(out var item))
            {
                park = item;
                m_industryParks.m_buffer[park].m_sheepMilkData = default;
                m_industryParks.m_buffer[park].m_cowMilkData = default;
                m_industryParks.m_buffer[park].m_highlandCowMilkData = default;
                m_industryParks.m_buffer[park].m_lambMeatData = default;
                m_industryParks.m_buffer[park].m_beefMeatData = default;
                m_industryParks.m_buffer[park].m_highlandBeefMeatData = default;
                m_industryParks.m_buffer[park].m_porkBeefData = default;
                m_industryParks.m_buffer[park].m_fruitsData = default;
                m_industryParks.m_buffer[park].m_vegetablesData = default;
                m_industryParks.m_buffer[park].m_cowsData = default;
                m_industryParks.m_buffer[park].m_highlandCowsData = default;
                m_industryParks.m_buffer[park].m_sheepData = default;
                m_industryParks.m_buffer[park].m_pigsData = default;
                m_industryParks.m_buffer[park].m_foodProductsData = default;
                m_industryParks.m_buffer[park].m_beverageProductsData = default;
                m_industryParks.m_buffer[park].m_bakedGoodsData = default;
                m_industryParks.m_buffer[park].m_cannedFishData = default;
                m_industryParks.m_buffer[park].m_furnituresData = default;
                m_industryParks.m_buffer[park].m_electronicProductsData = default;
                m_industryParks.m_buffer[park].m_industrialSteelData = default;
                m_industryParks.m_buffer[park].m_tupperwareData = default;
                m_industryParks.m_buffer[park].m_toysData = default;
                m_industryParks.m_buffer[park].m_printedProductsData = default;
                m_industryParks.m_buffer[park].m_tissuePaperData = default;
                m_industryParks.m_buffer[park].m_clothsData = default;
                m_industryParks.m_buffer[park].m_petroleumProductsData = default;
                m_industryParks.m_buffer[park].m_carsData = default;
                m_industryParks.m_buffer[park].m_footwearData = default;
                m_industryParks.m_buffer[park].m_housePartsData = default;
                m_industryParks.m_buffer[park].m_shipData = default;
                m_industryParks.m_buffer[park].m_woolData = default;
                m_industryParks.m_buffer[park].m_cottonData = default;
                if (type == ParkType.PedestrianZone)
                {
                    m_industryParks.m_buffer[park].m_tempIncome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];
                    m_industryParks.m_buffer[park].m_tempOutcome = new uint[ExtendedDistrictPark.pedestrianExtendedReasonsCount];

                    m_industryParks.m_buffer[park].m_extendedMaterialRequest = (from i in Enumerable.Range(0, ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                                            select new Queue<ushort>()).ToArray();
                    m_industryParks.m_buffer[park].m_extendedMaterialSuggestion = (from i in Enumerable.Range(0, ExtendedDistrictPark.pedestrianExtendedReasonsCount)
                                                                                                select new Queue<ushort>()).ToArray();
                }
                return true;
            }
            park = 0;
            return false;
        }
        
    }
}
