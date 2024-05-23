using ColossalFramework;
using System;
using UnityEngine;

namespace MoreTransferReasons.Utils
{
    public class Settings
    {
        public const string settingsFileName = "MoreTransferReasons_Settings";

        public static SavedBool ExtendedOutsideConnectionAI = new("ExtendedOutsideConnectionAI", settingsFileName, false, true);
        public static SavedBool ExtendedServicePointAI = new("ExtendedServicePointAI", settingsFileName, false, true);
        public static SavedBool ExtendedWarehouseAI = new("ExtendedWarehouseAI", settingsFileName, false, true);

        public static SavedBool ExtendedCargoTruckAI = new("ExtendedCargoTruckAI", settingsFileName, false, true);
        public static SavedBool ExtendedPassengerCarAI = new("ExtendedPassengerCarAI", settingsFileName, false, true);

        public static SavedBool ExtenedTouristAI = new("ExtenedTouristAI", settingsFileName, false, true);
        public static SavedBool ExtenedResidentAI = new("ExtenedResidentAI", settingsFileName, false, true);

        public static void Init()
        {
            try
            {
                // Creating setting file
                if (GameSettings.FindSettingsFileByName(settingsFileName) == null)
                {
                    GameSettings.AddSettingsFile([new SettingsFile() { fileName = settingsFileName }]);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Could not load/create the setting file.");
                Debug.LogException(e);
            }
        }
    }
}