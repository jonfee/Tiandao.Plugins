using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 退出命令。
	/// </summary>
    public class ExitCommand : Services.CommandBase<Services.CommandContextBase>
	{
		#region 构造方法

		public ExitCommand() : base("Exit")
		{
		}

		#endregion

		#region 运行方法

		/// <summary>
		/// 执行退出命令。
		/// </summary>
		/// <param name="context">当前命令的执上下文对象。</param>
		protected override void OnExecute(Services.CommandContextBase context)
		{
			Application.Exit();
		}

		#endregion
	}
}
