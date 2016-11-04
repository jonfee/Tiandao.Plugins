using System;
using System.Collections.Generic;
using Tiandao.ComponentModel;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示工作台的接口，包含对工作台的基本行为特性的定义。
	/// </summary>
	public interface IWorkbenchBase
	{
		/// <summary>当工作台被打开后。</summary>
		event EventHandler Opened;

		/// <summary>当工作台被打开前。</summary>
		event EventHandler Opening;

		/// <summary>当工作台被关闭后。</summary>
		event EventHandler Closed;

		/// <summary>当工作台被关闭前。</summary>
		event CancelEventHandler Closing;

		/// <summary>
		/// 获取工作台的当前状态。
		/// </summary>
		WorkbenchStatus Status
		{
			get;
		}

		/// <summary>
		/// 获取或设置工作台标题。
		/// </summary>
		string Title
		{
			get;
			set;
		}

		/// <summary>
		/// 关闭工作台。
		/// </summary>
		/// <returns>如果关闭成功返回真(true)，否则返回假(false)。如果取消关闭操作，亦返回假(false)。</returns>
		bool Close();

		/// <summary>
		/// 启动工作台。
		/// </summary>
		/// <param name="args">传入的启动参数。</param>
		void Open(string[] args);
	}
}