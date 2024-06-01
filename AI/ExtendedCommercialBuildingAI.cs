using ColossalFramework;
using ColossalFramework.Math;
using System.Collections.Generic;
using UnityEngine;

namespace MoreTransferReasons.AI
{
    public class ExtendedCommercialBuildingAI : CommercialBuildingAI, IExtendedBuildingAI
    {
         
        public List<ExtendedTransferManager.TransferReason> extendedUniqueProductslist =
        [
            ExtendedTransferManager.TransferReason.Anchovy,
            ExtendedTransferManager.TransferReason.Salmon,
            ExtendedTransferManager.TransferReason.Shellfish,
            ExtendedTransferManager.TransferReason.Tuna,
            ExtendedTransferManager.TransferReason.Trout,
            ExtendedTransferManager.TransferReason.Algae,
            ExtendedTransferManager.TransferReason.Seaweed,
            ExtendedTransferManager.TransferReason.SheepMilk,
            ExtendedTransferManager.TransferReason.CowMilk,
            ExtendedTransferManager.TransferReason.HighlandCowMilk,
            ExtendedTransferManager.TransferReason.LambMeat,
            ExtendedTransferManager.TransferReason.BeefMeat,
            ExtendedTransferManager.TransferReason.HighlandBeefMeat,
            ExtendedTransferManager.TransferReason.PorkMeat,
            ExtendedTransferManager.TransferReason.Fruits,
            ExtendedTransferManager.TransferReason.Vegetables,
            ExtendedTransferManager.TransferReason.FoodProducts,
            ExtendedTransferManager.TransferReason.BeverageProducts,
            ExtendedTransferManager.TransferReason.BakedGoods,
            ExtendedTransferManager.TransferReason.CannedFish,
            ExtendedTransferManager.TransferReason.Furnitures,
            ExtendedTransferManager.TransferReason.ElectronicProducts,
            ExtendedTransferManager.TransferReason.Tupperware,
            ExtendedTransferManager.TransferReason.Toys,
            ExtendedTransferManager.TransferReason.PrintedProducts,
            ExtendedTransferManager.TransferReason.TissuePaper,
            ExtendedTransferManager.TransferReason.Cloths,
            ExtendedTransferManager.TransferReason.Footwear,
        ];

        public override string GetDebugString(ushort buildingID, ref Building data)
        {
            string text = base.GetDebugString(buildingID, ref data);
            TransferManager.TransferReason incomingTransferReason = GetIncomingTransferReason();
            TransferManager.TransferReason outgoingTransferReason = GetOutgoingTransferReason(buildingID);
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                int count = 0;
                int cargo = 0;
                int capacity = 0;
                int outside = 0;
                if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
                {
                    CalculateGuestVehicles(buildingID, ref data, incomingTransferReason, TransferManager.TransferReason.LuxuryProducts, ref count, ref cargo, ref capacity, ref outside);
                    for(int i = 0; i < extendedUniqueProductslist.Count; i++)
                    {
                        ExtendedVehicleManager.CalculateGuestVehicles(buildingID, ref data, extendedUniqueProductslist[i], ref count, ref cargo, ref capacity, ref outside);
                    }
                }
                else
                {
                    CalculateGuestVehicles(buildingID, ref data, incomingTransferReason, ref count, ref cargo, ref capacity, ref outside);
                }
                Citizen.BehaviourData behaviour = default;
                int aliveCount = 0;
                int totalCount = 0;
                GetVisitBehaviour(buildingID, ref data, ref behaviour, ref aliveCount, ref totalCount);
                int num = CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), data.Width, data.Length);
                int a = Mathf.Min(num * 500, 65535);
                int num2 = Mathf.Max(a, MaxIncomingLoadSize() * 4);
                text = StringUtils.SafeFormat("{0}\n{1}: {2} (+{3}) of {4}", text, incomingTransferReason.ToString(), GetGoodsAmount(ref data), cargo, num2);
            }
            if (outgoingTransferReason != TransferManager.TransferReason.None)
            {
                text = StringUtils.SafeFormat("{0}\n{1}: {2}", text, outgoingTransferReason.ToString(), data.m_customBuffer2);
            }
            return StringUtils.SafeFormat("{0}\nMoney: {1}/{2}", text, data.m_cashBuffer / 10, GetCashCapacity(buildingID, ref data) / 10);
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            Singleton<SimulationManager>.instance.m_randomizer.Int32(3, 18);

            Singleton<SimulationManager>.instance.m_randomizer.Int32(25, 30);

            Singleton<SimulationManager>.instance.m_randomizer.Int32(25, 30);

            TransferManager.TransferReason incomingTransferReason = GetIncomingTransferReason();
            int width = buildingData.Width;
            int length = buildingData.Length;
            int num2 = MaxIncomingLoadSize();
            int num3 = CalculateVisitplaceCount((ItemClass.Level)buildingData.m_level, new Randomizer(buildingID), width, length);
            int num5 = Mathf.Min(num3 * 500, 65535);
            int num6 = Mathf.Max(num5, num2 * 4);
            int count = 0;
            int cargo = 0;
            int capacity = 0;
            int outside = 0;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
                {
                    for (int i = 0; i < extendedUniqueProductslist.Count; i++)
                    {
                        ExtendedVehicleManager.CalculateGuestVehicles(buildingID, ref buildingData, extendedUniqueProductslist[i], ref count, ref cargo, ref capacity, ref outside);
                    }
                    buildingData.m_tempImport = (byte)Mathf.Clamp(outside, buildingData.m_tempImport, 255);
                }                
            }
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            if (buildingData.m_fireIntensity == 0 && incomingTransferReason != TransferManager.TransferReason.None)
            {
                int num27 = num6 - GetGoodsAmount(ref buildingData) - capacity;
                num27 -= num2 >> 1;
                if (num27 >= 0)
                {
                    ExtendedTransferManager.Offer offer = default;
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    if ((incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food) && (instance2.m_currentFrameIndex & 0x300) >> 8 == (buildingID & 3))
                    {
                        for (int i = 0; i < extendedUniqueProductslist.Count; i++)
                        {
                            Singleton<ExtendedTransferManager>.instance.AddIncomingOffer(extendedUniqueProductslist[i], offer);
                        }    
                    }
                }
            }
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }

        public override void BuildingDeactivated(ushort buildingID, ref Building data)
        {
            TransferManager.TransferReason incomingTransferReason = GetIncomingTransferReason();
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                ExtendedTransferManager.Offer offer = default;
                offer.Building = buildingID;
                if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
                {
                    for (int i = 0; i < extendedUniqueProductslist.Count; i++)
                    {
                        Singleton<ExtendedTransferManager>.instance.RemoveIncomingOffer(extendedUniqueProductslist[i], offer);
                    }
                }
            }
            base.BuildingDeactivated(buildingID, ref data);
        }

        void IExtendedBuildingAI.ExtendedStartTransfer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ExtendedTransferManager.Offer offer)
        {
        }

        void IExtendedBuildingAI.ExtendedGetMaterialAmount(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, out int amount, out int max)
        {
            amount = 0;
            max = 0;
            TransferManager.TransferReason incomingTransferReason = GetIncomingTransferReason();
            if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
            {
                for (int i = 0; i < extendedUniqueProductslist.Count; i++)
                {
                    if (material == extendedUniqueProductslist[i])
                    {
                        int width = data.Width;
                        int length = data.Length;
                        int num = MaxIncomingLoadSize();
                        int num2 = CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), width, length);
                        amount = GetGoodsAmount(ref data);
                        max = Mathf.Min(Mathf.Max(num2 * 500, num * 4), 65535);
                    }
                }
            }
        }

        void IExtendedBuildingAI.ExtendedModifyMaterialBuffer(ushort buildingID, ref Building data, ExtendedTransferManager.TransferReason material, ref int amountDelta)
        {
            TransferManager.TransferReason incomingTransferReason = GetIncomingTransferReason();
            if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
            {
                for (int i = 0; i < extendedUniqueProductslist.Count; i++)
                {
                    if (material == extendedUniqueProductslist[i])
                    {
                        int width = data.Width;
                        int length = data.Length;
                        int num = MaxIncomingLoadSize();
                        int num2 = CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), width, length);
                        int num3 = Mathf.Min(Mathf.Max(num2 * 500, num * 4), 65535);
                        int goodsAmount = GetGoodsAmount(ref data);
                        amountDelta = Mathf.Clamp(amountDelta, 0, num3 - goodsAmount);
                        SetGoodsAmount(ref data, (ushort)(goodsAmount + amountDelta));
                    }
                }
            }
        }

        private TransferManager.TransferReason GetIncomingTransferReason()
        {
            return m_incomingResource;
        }

        private int MaxIncomingLoadSize()
        {
            return 4000;
        }

        private int GetCashCapacity(ushort buildingID, ref Building data)
        {
            return GetGoodsCapacity(buildingID, ref data) * 4;
        }

        private int GetGoodsCapacity(ushort buildingID, ref Building data)
        {
            int width = data.Width;
            int length = data.Length;
            int num = MaxIncomingLoadSize();
            int num2 = CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), width, length);
            return Mathf.Min(Mathf.Max(num2 * 500, num * 4), 65535);
        }

    }
}
