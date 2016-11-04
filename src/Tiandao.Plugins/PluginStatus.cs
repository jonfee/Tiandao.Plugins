using System;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示插件的状态。
	/// </summary>
	public enum PluginStatus
	{
		/// <summary>尚未加载，表示插件刚创建。</summary>
		None = 0,

		/// <summary>表示插件正在加载。</summary>
		Loading,

		/// <summary>表示插件已经成功加载。</summary>
		Loaded,

		/// <summary>表示插件正在卸载。</summary>
		Unloading,

		/// <summary>表示插件已经被卸载。</summary>
		Unloaded,

		/// <summary>表示插件在解析或加过程载中出现错误，该状态的插件的构件不会被挂载到系统中。</summary>
		Failed = 0x80,
	}
}