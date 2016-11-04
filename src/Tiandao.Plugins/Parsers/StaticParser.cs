using System;
using System.Reflection;
using Tiandao.Common;

namespace Tiandao.Plugins.Parsers
{
    public class StaticParser : Parser
	{
		public override object Parse(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text) || string.Equals(context.Text, "null", StringComparison.OrdinalIgnoreCase))
				return null;

			var member = PluginUtility.GetStaticMember(context.Text);

			if(member != null)
			{
				if(member.IsField())
				{
					return ((FieldInfo)member).GetValue(null);
				}
				else if(member.IsProperty())
				{
					return ((PropertyInfo)member).GetValue(null, null);
				}
			}

			return null;
		}
	}
}
