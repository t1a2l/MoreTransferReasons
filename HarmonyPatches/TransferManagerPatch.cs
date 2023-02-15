using ColossalFramework;
using HarmonyLib;
using System.Reflection;
using static TransferManager;

namespace MoreTransferReasons.HarmonyPatches
{
    [HarmonyPatch(typeof(TransferManager))]
    public class TransferManagerPatch
    {
        private delegate void AwakeDelegate();
        private static AwakeDelegate BaseAwake = AccessTools.MethodDelegate<AwakeDelegate>(typeof(SimulationManagerBase<TransferManager, TransferProperties>).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic), null, true);

		private delegate void UpdateDataDelegate(SimulationManager.UpdateMode mode);
        private static UpdateDataDelegate BaseUpdateData = AccessTools.MethodDelegate<UpdateDataDelegate>(typeof(SimulationManagerBase<TransferManager, TransferProperties>).GetMethod("UpdateData", BindingFlags.Instance | BindingFlags.Public), null, true);

        [HarmonyPatch(typeof(TransferManager), "Awake")]
        [HarmonyPrefix]
        public static bool Awake(TransferManager __instance)
	    {
            var m_outgoingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);

            BaseAwake();
		    m_outgoingOffers = new TransferOffer[327680];
		    m_incomingOffers = new TransferOffer[327680];
		    m_outgoingCount = new ushort[1280];
		    m_incomingCount = new ushort[1280];
		    m_outgoingAmount = new int[160];
		    m_incomingAmount = new int[160];

            typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingOffers);
            typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingOffers);
            typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingCount);
            typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingCount);
            typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingAmount);
            typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingAmount);

	        return false;
	    }

		[HarmonyPatch(typeof(TransferManager), "UpdateData")]
        [HarmonyPrefix]
        public static bool UpdateData(TransferManager __instance, SimulationManager.UpdateMode mode)
		{
			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.BeginLoading("TransferManager.UpdateData");
			BaseUpdateData(mode);
			VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
			CitizenManager citizenManager = Singleton<CitizenManager>.instance;
			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			var m_outgoingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingOffers = (TransferOffer[])typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_outgoingCount = (ushort[])typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingCount = (ushort[])typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_outgoingAmount = (int[])typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            var m_incomingAmount = (int[])typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);

			for (int i = 0; i < 160; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					int num = i * 8 + j;
					int num2 = m_incomingCount[num];
					for (int num3 = num2 - 1; num3 >= 0; num3--)
					{
						int num4 = num * 256 + num3;
						bool flag = false;
						switch (m_incomingOffers[num4].m_object.Type)
						{
						case InstanceType.Vehicle:
							flag = (object)vehicleManager.m_vehicles.m_buffer[m_incomingOffers[num4].m_object.Vehicle].Info == null;
							break;
						case InstanceType.Citizen:
						{
							uint citizen = m_incomingOffers[num4].m_object.Citizen;
							flag = (object)citizenManager.m_citizens.m_buffer[citizen].GetCitizenInfo(citizen) == null;
							break;
						}
						case InstanceType.Building:
							flag = (object)buildingManager.m_buildings.m_buffer[m_incomingOffers[num4].m_object.Building].Info == null;
							break;
						}
						if (flag)
						{
							m_incomingAmount[i] -= m_incomingOffers[num4].Amount;
							ref TransferOffer reference = ref m_incomingOffers[num4];
							reference = m_incomingOffers[num * 256 + --num2];
						}
					}
					m_incomingCount[num] = (ushort)num2;
					int num5 = m_outgoingCount[num];
					for (int num6 = num5 - 1; num6 >= 0; num6--)
					{
						int num7 = num * 256 + num6;
						bool flag2 = false;
						switch (m_outgoingOffers[num7].m_object.Type)
						{
						case InstanceType.Vehicle:
							flag2 = (object)vehicleManager.m_vehicles.m_buffer[m_outgoingOffers[num7].m_object.Vehicle].Info == null;
							break;
						case InstanceType.Citizen:
						{
							uint citizen2 = m_outgoingOffers[num7].m_object.Citizen;
							flag2 = (object)citizenManager.m_citizens.m_buffer[citizen2].GetCitizenInfo(citizen2) == null;
							break;
						}
						case InstanceType.Building:
							flag2 = (object)buildingManager.m_buildings.m_buffer[m_outgoingOffers[num7].m_object.Building].Info == null;
							break;
						}
						if (flag2)
						{
							m_outgoingAmount[i] -= m_outgoingOffers[num7].Amount;
							ref TransferOffer reference2 = ref m_outgoingOffers[num7];
							reference2 = m_outgoingOffers[num * 256 + --num5];
						}
					}
					m_outgoingCount[num] = (ushort)num5;
				}
			}
			typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingOffers);
            typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingOffers);
            typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingCount);
            typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingCount);
            typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_outgoingAmount);
            typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, m_incomingAmount);

			Singleton<LoadingManager>.instance.m_loadingProfilerSimulation.EndLoading();
			return false;
		}

		[HarmonyPatch(typeof(TransferManager), "GetFrameReason")]
        [HarmonyPrefix]
		public static bool GetFrameReason(int frameIndex, ref TransferReason __result)
		{
			__result = frameIndex switch
			{
				1 => TransferReason.Garbage, // 0
				3 => TransferReason.Crime,
				5 => TransferReason.Sick,
				7 => TransferReason.Dead,
				9 => TransferReason.Worker0,
				11 => TransferReason.Worker1,
				13 => TransferReason.Worker2,
				15 => TransferReason.Worker3,
				17 => TransferReason.Student1,
				19 => TransferReason.Student2,
				21 => TransferReason.Student3, // 10
				23 => TransferReason.Fire, 
				25 => TransferReason.Bus, 
				27 => TransferReason.Oil, 
				29 => TransferReason.Ore, 
				31 => TransferReason.Logs, 
				33 => TransferReason.Grain, 
				35 => TransferReason.Goods, 
				37 => TransferReason.PassengerTrain, 
				39 => TransferReason.Coal, 
				41 => TransferReason.Family0, // 20
				43 => TransferReason.Family1, 
				45 => TransferReason.Family2, 
				47 => TransferReason.Family3, 
				49 => TransferReason.Single0, 
				51 => TransferReason.Single1, 
				53 => TransferReason.Single2, 
				55 => TransferReason.Single3, 
				57 => TransferReason.PartnerYoung, 
				59 => TransferReason.PartnerAdult, 
				61 => TransferReason.Shopping, // 30
				63 => TransferReason.Petrol, 
				65 => TransferReason.Food, 
				67 => TransferReason.LeaveCity0, 
				69 => TransferReason.LeaveCity1, 
				71 => TransferReason.LeaveCity2, 
				73 => TransferReason.Entertainment, 
				75 => TransferReason.Lumber, 
				77 => TransferReason.GarbageMove, 
				79 => TransferReason.MetroTrain, 
				81 => TransferReason.PassengerPlane, // 40
				83 => TransferReason.PassengerShip, 
				85 => TransferReason.DeadMove, 
				87 => TransferReason.DummyCar, 
				89 => TransferReason.DummyTrain, 
				91 => TransferReason.DummyShip, 
				93 => TransferReason.DummyPlane, 
				95 => TransferReason.Single0B, 
				97 => TransferReason.Single1B, 
				99 => TransferReason.Single2B, 
				101 => TransferReason.Single3B, // 50
				103 => TransferReason.ShoppingB, 
				105 => TransferReason.ShoppingC, 
				107 => TransferReason.ShoppingD, 
				109 => TransferReason.ShoppingE, 
				111 => TransferReason.ShoppingF, 
				113 => TransferReason.ShoppingG, 
				115 => TransferReason.ShoppingH, 
				117 => TransferReason.EntertainmentB, 
				119 => TransferReason.EntertainmentC, 
				121 => TransferReason.EntertainmentD, // 60
				123 => TransferReason.Taxi, 
				125 => TransferReason.CriminalMove, 
				127 => TransferReason.Tram, 
				129 => TransferReason.Snow, 
				131 => TransferReason.SnowMove, 
				133 => TransferReason.RoadMaintenance, 
				135 => TransferReason.SickMove, 
				137 => TransferReason.ForestFire, 
				139 => TransferReason.Collapsed, 
				141 => TransferReason.Collapsed2, // 70
				143 => TransferReason.Fire2, 
				145 => TransferReason.Sick2, 
				147 => TransferReason.FloodWater, 
				149 => TransferReason.EvacuateA, 
				151 => TransferReason.EvacuateB, 
				153 => TransferReason.EvacuateC, 
				155 => TransferReason.EvacuateD, 
				157 => TransferReason.EvacuateVipA, 
				159 => TransferReason.EvacuateVipB, 
				161 => TransferReason.EvacuateVipC, // 80
				163 => TransferReason.EvacuateVipD, 
				165 => TransferReason.Ferry, 
				167 => TransferReason.CableCar, 
				169 => TransferReason.Blimp, 
				171 => TransferReason.Monorail, 
				173 => TransferReason.TouristBus, 
				175 => TransferReason.ParkMaintenance, 
				177 => TransferReason.TouristA, 
				179 => TransferReason.TouristB, 
				181 => TransferReason.TouristC, // 90
				183 => TransferReason.TouristD, 
				185 => TransferReason.Mail, 
				187 => TransferReason.UnsortedMail, 
				189 => TransferReason.SortedMail, 
				191 => TransferReason.OutgoingMail, 
				193 => TransferReason.IncomingMail, 
				195 => TransferReason.AnimalProducts, 
				197 => TransferReason.Flours, 
				199 => TransferReason.Paper, 
				201 => TransferReason.PlanedTimber,  // 100
				203 => TransferReason.Petroleum,
				205 => TransferReason.Plastics, 
				207 => TransferReason.Glass, 
				209 => TransferReason.Metals, 
				211 => TransferReason.LuxuryProducts, 
				213 => TransferReason.GarbageTransfer, 
				215 => TransferReason.PassengerHelicopter, 
				217 => TransferReason.Fish, 
				219 => TransferReason.Trolleybus, 
				221 => TransferReason.ElderCare, // 110 
				223 => TransferReason.ChildCare,
				225 => TransferReason.IntercityBus, 
				227 => TransferReason.BiofuelBus, 
				229 => TransferReason.Cash, 
				231 => (TransferReason)115, 
				233 => (TransferReason)116, 
				235 => (TransferReason)117, 
				237 => (TransferReason)118, 
				239 => (TransferReason)119, 
				241 => (TransferReason)120, 
				243 => (TransferReason)121, 
				245 => (TransferReason)122, 
				247 => (TransferReason)123, 
				249 => (TransferReason)124, 
				251 => (TransferReason)125, 
				253 => (TransferReason)126, 
				255 => (TransferReason)127, 
				257 => (TransferReason)128, 
				259 => (TransferReason)129, 
				261 => (TransferReason)130, 
				263 => (TransferReason)131, 
				265 => (TransferReason)132, 
				267 => (TransferReason)133, 
				269 => (TransferReason)134, 
				271 => (TransferReason)135, 
				273 => (TransferReason)136, 
				275 => (TransferReason)137, 
				279 => (TransferReason)138, 
				281 => (TransferReason)139, 
				283 => (TransferReason)140, 
				285 => (TransferReason)141, 
				287 => (TransferReason)142, 
				289 => (TransferReason)143, 
				291 => (TransferReason)144, 
				293 => (TransferReason)145, 
				295 => (TransferReason)146, 
				297 => (TransferReason)147, 
				299 => (TransferReason)148, 
				301 => (TransferReason)149, 
				303 => (TransferReason)150, 
				305 => (TransferReason)151, 
				307 => (TransferReason)152, 
				309 => (TransferReason)153, 
				311 => (TransferReason)154, 
				313 => (TransferReason)155, 
				315 => (TransferReason)156, 
				317 => (TransferReason)157, 
				319 => (TransferReason)158, 
				321 => (TransferReason)159, 
				_ => TransferReason.None
			};
			return false;
		}
        
    }
}
