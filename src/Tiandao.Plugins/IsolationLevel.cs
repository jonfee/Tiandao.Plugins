using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示插件的隔离级别。
	/// </summary>
	public enum IsolationLevel
    {
		/// <summary>无隔离，所有插件与宿主在同一个应用域。此模式不支持动态卸载。</summary>
		None = 0,

		/// <summary>按插件隔离，表示每个插件均处于单独的应用域。此模式支持动态卸载。</summary>
		PerPlugin,
	}
}
