using System;
using System.Text.RegularExpressions;

using Tiandao.Plugins;
using Tiandao.Plugins.Parsers;

namespace Tiandao.Services.Plugins.Parsers
{
    public class PredicateParser : Parser
    {
		#region 私有字段

		private readonly Regex _regex = new Regex(@"[^\s]+", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

		#endregion

		#region 重写方法

		public override object Parse(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text))
				throw new PluginException("Can not parse for the predication because the parser text is empty.");

			var matches = _regex.Matches(context.Text);

			if(matches.Count < 1)
				throw new PluginException("Can not parse for the predication.");

			var parts = matches[0].Value.Split('.');

			if(parts.Length != 2)
				throw new PluginException("Can not parse for the predication because of a syntax error.");

			IPredication predication = null;

			if(parts.Length == 1)
				predication = context.PluginContext.ApplicationContext.ServiceFactory.Default.Resolve(parts[0]) as IPredication;
			else
			{
				var serviceProvider = context.PluginContext.ApplicationContext.ServiceFactory.GetProvider(parts[0]);

				if(serviceProvider == null)
					throw new PluginException(string.Format("The '{0}' ServiceProvider is not exists on the predication parsing.", parts[0]));

				predication = serviceProvider.Resolve(parts[1]) as IPredication;
			}

			if(predication != null)
			{
				string text = matches.Count <= 1 ? null : context.Text.Substring(matches[1].Index);
				object parameter = text;

				if(Tiandao.Common.TypeExtension.IsAssignableFrom(typeof(IPredication<PluginPredicationContext>), predication.GetType()))
					parameter = new PluginPredicationContext(text, context.Builtin, context.Node, context.Plugin);

				return predication.Predicate(parameter);
			}

			return false;
		}

		#endregion
	}
}
