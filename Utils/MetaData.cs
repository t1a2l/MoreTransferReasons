using System.Collections.Generic;
using ColossalFramework;

namespace MoreTransferReasons.Utils
{
    public static class MetaData
    {
        // Metadata key for storing mod override flag in simulation maetadata.
        private static readonly string MetadataKey = "User/More Transfer Reasons";

        /// <summary>
        /// Returns whether (true) or not (false) this save was made using using an expanded CitizenUnit array.
        /// </summary>
        internal static bool LoadingExtended
        {
            get
            {
                // Try to get simulation manager metadata dictionary of mod override flags.
                Dictionary<string, bool> metaDataDict = Singleton<SimulationManager>.instance?.m_metaData?.m_modOverride;
                if (metaDataDict != null)
                {
                    // Got it - see if it contains our key.
                    if (metaDataDict.TryGetValue(MetadataKey, out bool isOverridden))
                    {
                        // Key found; if it's set to true, then this save has been serialized with More Transfer Reasons.
                        if (isOverridden)
                        {
                            LogHelper.Information("deserializing using 160 reasons count");
                            return true;
                        }
                    }
                }
                else
                {
                    LogHelper.Information("no SimulationManager modOverride dictionary present");
                }

                // Default is original CitizenUnit count.
                LogHelper.Information("deserializing using vanilla unit count");
                return false;
            }
        }

        /// <summary>
        /// Sets the simulation metadata to indicate that this save was made using an expanded Transfers arrays.
        /// </summary>
        internal static void SetMetaData()
        {
            LogHelper.Information("setting simulation metadata");
            // Try to get simulation manager metadata dictionary of mod override flags.
            SimulationMetaData metaData = Singleton<SimulationManager>.instance.m_metaData;
            lock (metaData)
            {
                if (metaData.m_modOverride == null)
                {
                    metaData.m_modOverride = [];
                }

                // Got it - see if it contains our key.
                if (metaData.m_modOverride.ContainsKey(MetadataKey))
                {
                    // Yes - set this key to true, just to be sure.
                    metaData.m_modOverride[MetadataKey] = true;
                }
                else
                {
                    // No key found - add our key for future reference when the game saves.
                    LogHelper.Information("adding new metadata dictionary entry");
                    metaData.m_modOverride.Add(MetadataKey, true);
                }
            }
        }
    }
}
