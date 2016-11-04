using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginLoaderSetup : PluginSetupBase
	{
		#region 构造方法

		public PluginLoaderSetup() : this(null, null)
		{
		}

		public PluginLoaderSetup(string applicationDirectory) : this(applicationDirectory, null)
		{
		}

		public PluginLoaderSetup(string applicationDirectory, string pluginsDirectoryName) : base(applicationDirectory, pluginsDirectoryName)
		{
		}

		#endregion
	}
}
