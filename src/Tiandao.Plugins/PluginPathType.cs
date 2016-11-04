using System;
using System.Collections.Generic;
namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示插件路径的类型。
	/// </summary>
	public enum PluginPathType
	{
		/// <summary>基于根节点的绝对路径。</summary>
		Rooted,

		/// <summary>相对于父节点的路径。</summary>
		Parent,

		/// <summary>相对于当前节点的路径。</summary>
		Current,
	}
}