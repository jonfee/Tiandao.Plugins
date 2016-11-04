using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Parsers
{
    public class ObjectParser : Parser
	{
		public override object Parse(ParserContext context)
		{
			return PluginUtility.BuildType(context.Text, context.Builtin);
		}
	}
}
