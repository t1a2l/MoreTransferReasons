using MoreTransferReasons.AI;
using UnityEngine;

namespace MoreTransferReasons
{
    public class ExtendedDistrictManagerComponent : MonoBehaviour
    {
        public void Start()
        {
            SimulationManager.RegisterManager(ColossalFramework.Singleton<ExtendedDistrictManager>.instance);
        }
    }
}
