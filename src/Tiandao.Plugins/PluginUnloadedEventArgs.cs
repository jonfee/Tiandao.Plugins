using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginUnloadedEventArgs : EventArgs
	{
		#region 公共属性

		public Plugin Plugin
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public PluginUnloadedEventArgs(Plugin plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			this.Plugin = plugin;
		}

		#endregion
	}
}
