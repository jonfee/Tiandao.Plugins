using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginLoadedEventArgs : PluginLoadEventArgs
	{
		public Plugin Plugin
		{
			get;
			private set;
		}

		public PluginLoadedEventArgs(PluginLoaderSetup settings, Plugin plugin) : base(settings)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			this.Plugin = plugin;
		}
	}
}
