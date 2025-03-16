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
            AtlasUtils.CreateAtlas();
            Utils.Settings.Init();
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }

        /// <summary>
        /// mod's settings
        /// </summary>
        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelper ExtendedBuildingsAISettings = helper.AddGroup("Extended Buildings AI Settings") as UIHelper;

            ExtendedBuildingsAISettings.AddCheckbox("Convert commercial buildings to accept new product types", Utils.Settings.ExtendedCommercialBuildingAI.value, (b) =>
            {
                Utils.Settings.ExtendedCommercialBuildingAI.value = b;
            });

            ExtendedBuildingsAISettings.AddCheckbox("Convert outside connections to support more transfer reasons", Utils.Settings.ExtendedOutsideConnectionAI.value, (b) =>
            {
                Utils.Settings.ExtendedOutsideConnectionAI.value = b;
            });

            ExtendedBuildingsAISettings.AddCheckbox("Convert service points to support more transfer reasons", Utils.Settings.ExtendedServicePointAI.value, (b) =>
            {
                Utils.Settings.ExtendedServicePointAI.value = b;
            });

            ExtendedBuildingsAISettings.AddCheckbox("Convert warehouses to support more transfer reasons", Utils.Settings.ExtendedWarehouseAI.value, (b) =>
            {
                Utils.Settings.ExtendedWarehouseAI.value = b;
            });

            UIHelper ExtendedVehiclesAISettings = helper.AddGroup("Extended Vehicles AI Settings") as UIHelper;

            ExtendedVehiclesAISettings.AddCheckbox("Convert cargo trucks to support more transfer reasons", Utils.Settings.ExtendedCargoTruckAI.value, (b) =>
            {
                Utils.Settings.ExtendedCargoTruckAI.value = b;
            });

            UIHelper ExtendedCitizensAISettings = helper.AddGroup("Extended Citizens AI Settings") as UIHelper;

            ExtendedCitizensAISettings.AddCheckbox("Convert tourist type citizens to support more transfer reasons", Utils.Settings.ExtenedTouristAI.value, (b) =>
            {
                Utils.Settings.ExtenedTouristAI.value = b;
            });

            ExtendedCitizensAISettings.AddCheckbox("Convert resident type citizens to support more transfer reasons", Utils.Settings.ExtenedResidentAI.value, (b) =>
            {
                Utils.Settings.ExtenedResidentAI.value = b;
            });

            UIHelper ClearTransferManager = helper.AddGroup("Clear Transfer Manager") as UIHelper;

            ClearTransferManager.AddButton("Clear all transfers", () =>
            {
                ExtendedTransferManager.instance.ClearTransferManager();
            });
        }
    }
}
