using System;
using System.Collections.Generic;

using Tiandao.Plugins;
using Tiandao.Plugins.Builders;

namespace Tiandao.Services.Plugins.Builders
{
    public class CommandTreeNodeBuilder : BuilderBase, IAppender
	{
		public override object Build(BuilderContext context)
		{
			if(context.Builtin.BuiltinType == null || context.Builtin.BuiltinType.Type == null)
				return new CommandTreeNode(context.Builtin.Name, null);

			var command = base.Build(context) as ICommand;

			if(command != null)
				return new CommandTreeNode(command, null);

			return null;
		}

		bool IAppender.Append(AppenderContext context)
		{
			var commandNode = context.Container as CommandTreeNode;

			if(commandNode != null)
			{
				if(context.Value is ICommand)
					commandNode.Children.Add((ICommand)context.Value);
				else if(context.Value is CommandTreeNode)
					commandNode.Children.Add((CommandTreeNode)context.Value);
				else
					return false;

				return true;
			}

			return false;
		}
	}
}
