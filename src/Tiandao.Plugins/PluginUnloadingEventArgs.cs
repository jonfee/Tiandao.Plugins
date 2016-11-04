using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginUnloadingEventArgs : CancelEventArgs
	{
		#region 公共属性

		public Plugin Plugin
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public PluginUnloadingEventArgs(Plugin plugin) : this(plugin, false)
		{
		}

		public PluginUnloadingEventArgs(Plugin plugin, bool cancel) : base(cancel)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			this.Plugin = plugin;
		}

		#endregion
	}
}
