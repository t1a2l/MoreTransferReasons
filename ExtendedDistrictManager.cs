using System.Collections.Generic;

namespace MoreTransferReasons
{
    public static class ExtendedDistrictManager
    {

        public static Dictionary<byte, ExtendedDistrictPark> IndustryParks { get; private set; }


        public static void Init()
        {
            IndustryParks ??= [];
        }

        public static void Deinit()
        {
            IndustryParks = [];
        }

    }
}
