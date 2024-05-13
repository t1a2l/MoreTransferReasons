using ICities;
using CitiesHarmony.API;
using MoreTransferReasons.Utils;

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
    }
}
