using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示工作台的运行状态。
	/// </summary>
	public enum WorkbenchStatus
	{
		/// <summary>未启动或已关闭。</summary>
		None = 0,

		/// <summary>正在打开中，表示正在启动工作台。</summary>
		Opening = 1,

		/// <summary>正常运行。</summary>
		Running = 2,

		/// <summary>正在关闭中，表示正在执行关闭操作。</summary>
		Closing = 3,
	}
}