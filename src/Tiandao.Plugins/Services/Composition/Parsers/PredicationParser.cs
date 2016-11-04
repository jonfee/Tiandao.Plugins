using System;
using System.Collections.Generic;

using Tiandao.Plugins;
using Tiandao.Plugins.Parsers;

namespace Tiandao.Services.Composition.Plugins.Parsers
{
	[Obsolete]
	public class PredicationParser : Parser
	{
		#region 私有字段

		private string _source;

		#endregion

		#region 构造方法

		public PredicationParser()
		{
			_source = "/Workspace/Predication";
		}

		#endregion

		#region 公共属性

		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException();

				if(!PluginPath.IsPath(value))
					throw new ArgumentException();

				_source = value.Trim();
			}
		}

		#endregion

		#region 解析方法

		public override object Parse(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text))
				return null;

			var node = context.PluginContext.PluginTree.Find(_source, context.Text);

			if(node != null)
			{
				var predication = node.UnwrapValue<IPredication>();

				if(predication != null)
				{
					var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

					//设置内置的参数到字典中
					parameters["$builtin"] = context.Builtin;
					parameters["$node"] = context.Node;

					return predication.Predicate(parameters);
				}
			}

			return null;
		}

		#endregion
	}
}
