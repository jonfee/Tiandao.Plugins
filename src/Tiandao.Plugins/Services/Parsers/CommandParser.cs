using System;
using System.Collections.Generic;

using Tiandao.Services;
using Tiandao.Plugins.Parsers;

namespace Tiandao.Services.Plugins.Parsers
{
    public class CommandParser : Parser
	{
		#region 解析方法

		public override object Parse(ParserContext context)
		{
			return new DelegateCommand(context.Text);
		}

		#endregion

		#region 嵌套子类

		private class DelegateCommand : CommandBase
		{
			#region 私有变量
			private string _commandText;
			#endregion

			#region 构造函数
			public DelegateCommand(string commandText)
			{
				_commandText = commandText;
			}
			#endregion

			#region 执行方法
			protected override object OnExecute(object parameter)
			{
				var commandExecutor = CommandExecutor.Default;

				if(commandExecutor == null)
					throw new InvalidOperationException("Can not get the CommandExecutor from 'Tiandao.Services.CommandExecutor.Default' static member.");

				return commandExecutor.Execute(_commandText, parameter);
			}
			#endregion
		}

		#endregion
	}
}
