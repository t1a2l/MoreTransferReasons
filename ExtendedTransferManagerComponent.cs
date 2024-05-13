using UnityEngine;

namespace MoreTransferReasons
{
    public class ExtendedTransferManagerComponent : MonoBehaviour
    {
        public void Start()
        {
            SimulationManager.RegisterManager(ColossalFramework.Singleton<ExtendedTransferManager>.instance);
        }
    }
}
