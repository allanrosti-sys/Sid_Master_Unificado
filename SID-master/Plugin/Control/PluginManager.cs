using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SID.Plugin.Control
{
    public class PluginManager
    {
        #region Arquitetura Singleton

        private static PluginManager _Instance;
        public static PluginManager Current
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PluginManager();
                }
                return _Instance;
            }
        }
        #endregion
        private PluginManager()
        {
            _Plugins = new List<Plugin_Model>();
            Load();
        }
        private readonly string Folder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Plugin";

        private List<Plugin_Model> _Plugins;
        public ICollection<Plugin_Model> Plugins
        {
            get
            {
                return _Plugins;
            }
        }

        private void Load()
        {
            if (!System.IO.Directory.Exists(Folder)) return;
            string[] pluginsPaths = System.IO.Directory.GetFiles(Folder, "*.dll");
            foreach (string path in pluginsPaths)
            {
                Assembly pluginAssembly = Assembly.LoadFile(path);
                LoadPlugin(pluginAssembly);
            }
        }

        private void LoadPlugin(Assembly Plugin)
        {
            Type[] typesInAssembly = Plugin.GetTypes();
            Type IPlugin = typeof(Plugin_Model);
            foreach (Type type in typesInAssembly)
            {
                if (!IPlugin.IsAssignableFrom(type)) continue;
                Plugin_Model Extension = (Plugin_Model)Plugin.CreateInstance(type.FullName);
                if (Extension != null) _Plugins.Add(Extension);
            }
            typesInAssembly = null;

        }

    }
}
