using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示插件树的当前状态。
	/// </summary>
	public enum PluginTreeStatus
    {
		/// <summary>未初始化。</summary>
		None = 0,

		/// <summary>加载进行中。</summary>
		Loading,

		/// <summary>已加载完成。</summary>
		Loaded,
	}
}
