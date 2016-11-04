using System;
using System.Collections.Generic;

using Tiandao.Plugins;
using Tiandao.Plugins.Parsers;

namespace Tiandao.Options.Plugins.Parsers
{
    public class OptionParser : Parser
	{
		#region 解析方法

		public override object Parse(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text))
				return null;

			PluginPathType pathType;
			string optionPath = context.Text;
			string[] memberNames;

			if(char.IsLetterOrDigit(optionPath[0]))
				optionPath = '/' + optionPath;

			if(PluginPath.TryResolvePath(optionPath, out pathType, out optionPath, out memberNames))
			{
				object target = context.PluginContext.ApplicationContext.OptionManager.GetOptionObject(optionPath);

				if(target == null)
					return null;

				return Tiandao.Common.Converter.GetValue(target, memberNames);
			}

			return null;
		}

		#endregion
	}
}
