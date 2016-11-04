using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class ApplicationEventArgs : EventArgs
	{
		#region 公共属性

		public PluginApplicationContext Context
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public ApplicationEventArgs(PluginApplicationContext context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			this.Context = context;
		}

		#endregion
	}
}
