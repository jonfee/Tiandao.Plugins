using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Parsers
{
    public class PluginPathParser : Parser
	{
		public override Type GetValueType(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text))
				return null;

			//处理特殊路径表达式，即获取插件文件路径或目录
			if(context.Text.StartsWith("~"))
				return typeof(string);

			var path = this.ResolveText(context.Text);
			var node = context.PluginContext.PluginTree.Find(path);

			return node?.ValueType;
		}

		public override object Parse(ParserContext context)
		{
			if(string.IsNullOrWhiteSpace(context.Text))
				return null;

			//处理特殊路径表达式，即获取插件文件路径或目录
			if(context.Text == "~")
				return context.Plugin.FilePath;
			else if(context.Text == "~/")
				return System.IO.Path.GetDirectoryName(context.Plugin.FilePath);

			var mode = ObtainMode.Auto;
			var path = this.ResolveText(context.Text, out mode);

			return context.PluginContext.ResolvePath(path, context.Node, mode);
		}

		internal string ResolveText(string text)
		{
			ObtainMode mode;
			return this.ResolveText(text, out mode);
		}

		internal string ResolveText(string text, out ObtainMode mode)
		{
			mode = ObtainMode.Auto;

			if(string.IsNullOrWhiteSpace(text))
				return text;

			var parts = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			if(parts.Length == 2)
				Enum.TryParse<ObtainMode>(parts[1], true, out mode);

			return parts[0];
		}
	}
}
