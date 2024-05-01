using ICities;
using CitiesHarmony.API;
using MoreTransferReasons.Utils;
using UnityEngine;

namespace MoreTransferReasons
{
    public class MoreTransferReasonsMod : IUserMod
    {
        string IUserMod.Name => "More Transfer Reasons Mod";

        string IUserMod.Description => "Add more transfer reasons to the game for all kinds of mods";

        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }

        public class CustomLoader : LoadingExtensionBase
        {
            /// <summary>
            /// This event is triggerred when a level is loaded
            /// </summary>
            public override void OnLevelLoaded(LoadMode mode)
            {
                // Instantiate a custom object
                GameObject go = new GameObject("ExtendedTransferManagerMod");
                go.AddComponent<ExtendedTransferManagerComponent>();

                base.OnLevelLoaded(mode);
            }
        }

        public class ExtendedTransferManagerComponent : MonoBehaviour
		{
            void Start()
            {
                SimulationManager.RegisterManager(ColossalFramework.Singleton<ExtendedTransferManager>.instance);
            }
		}


    }
}
