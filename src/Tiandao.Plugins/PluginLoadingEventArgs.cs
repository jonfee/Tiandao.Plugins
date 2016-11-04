using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginLoadingEventArgs : PluginLoadEventArgs
	{
		public string PluginFile
		{
			get;
			private set;
		}

		public PluginLoadingEventArgs(PluginLoaderSetup settings, string pluginFile) : base(settings)
		{
			if(string.IsNullOrEmpty(pluginFile))
				throw new ArgumentNullException("pluginFile");

			this.PluginFile = pluginFile;
		}
	}
}
