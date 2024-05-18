using static DistrictPark;

namespace MoreTransferReasons
{
    public static class PedestrianZoneExtendedTransferReasonManager
    {
        public struct PedestrianZoneExtendedTransferReason
        {
            public ExtendedTransferManager.TransferReason m_material;

            public bool m_activeIn;

            public bool m_activeOut;

            public DeliveryCategories m_deliveryCategory;

            public VehicleInfo.VehicleCategory m_vehicleCategory;

            public int m_averageTruckCapacity;
        }

        public static PedestrianZoneExtendedTransferReason[] kPedestrianZoneExtendedTransferReasons { get; set; }


        public static void Init()
        {
            kPedestrianZoneExtendedTransferReasons ??= [];
        }

        public static void Deinit()
        {
            kPedestrianZoneExtendedTransferReasons = [];
        }

    }
}
