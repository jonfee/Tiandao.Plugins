using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginLoadEventArgs : EventArgs
	{
		#region 公共属性

		public PluginLoaderSetup Settings
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public PluginLoadEventArgs(PluginLoaderSetup settings)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			this.Settings = settings;
		}

		#endregion
	}
}
