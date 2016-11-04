using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
	public class PluginDependency : MarshalByRefObject
#else
	public class PluginDependency
#endif
	{
		#region 公共属性

		/// <summary>
		/// 获取依赖的插件名。注：此名称不是插件的文件名。
		/// </summary>
		public string Name
		{
			get;
			internal set;
		}

		/// <summary>
		/// 获取依赖的插件对象。
		/// </summary>
		/// <remarks>如果插件未加载完成，该属性返回空(null)。</remarks>
		public Plugin Plugin
		{
			get;
			internal set;
		}

		#endregion

		#region 构造方法

		public PluginDependency(string name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			this.Name = name.Trim();
			this.Plugin = null;
		}

		#endregion
	}
}
