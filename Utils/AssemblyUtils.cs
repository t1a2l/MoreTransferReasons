using System.Reflection;
using ColossalFramework.Plugins;
using static ColossalFramework.Plugins.PluginManager;

namespace MoreTransferReasons.Utils
{
    public static class AssemblyUtils
    {
        public static bool IsAssemblyEnabled(string assemblyName)
        {
            // Iterate through the full list of plugins.
            foreach (PluginInfo plugin in PluginManager.instance.GetPluginsInfo())
            {
                // Only looking at enabled plugins.
                if (plugin.isEnabled)
                {
                    foreach (Assembly assembly in plugin.GetAssemblies())
                    {
                        if (assembly.GetName().Name.Equals(assemblyName))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
