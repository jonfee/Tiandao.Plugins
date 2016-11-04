using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Parsers
{
    public class TypeParser : Parser
	{
		public override object Parse(ParserContext context)
		{
			return PluginUtility.GetType(context.Text);
		}
	}
}
