using System;

using Tiandao.Plugins;
using Tiandao.Plugins.Builders;

namespace Tiandao.Options.Plugins.Builders
{
	public class OptionBuilder : BuilderBase
	{
		#region 重写方法

		public override object Build(BuilderContext context)
		{
			Builtin builtin = context.Builtin;
			IOptionProvider provider = null;
			string providerValue = builtin.Properties.GetRawValue("provider");

			var node = new OptionNode(builtin.Name,
									  builtin.Properties.GetValue<string>("title"),
									  builtin.Properties.GetValue<string>("description"));

			if(string.IsNullOrWhiteSpace(providerValue))
				return node;

			switch(providerValue.Trim().ToLower())
			{
				case ".":
				case "plugin":
					provider = OptionUtility.GetConfiguration(builtin.Plugin);
					break;
				case "/":
				case "application":
					provider = context.PluginContext.ApplicationContext.Configuration;
					break;
				default:
					provider = builtin.Properties.GetValue<IOptionProvider>("provider");
					break;
			}

			if(provider == null)
				throw new PluginException(string.Format("Cann't obtain OptionProvider with '{0}'.", providerValue));

			node.Option = new Option(node, provider)
			{
				View = context.Builtin.Properties.GetValue<IOptionView>("view"),
				ViewBuilder = context.Builtin.Properties.GetValue<IOptionViewBuilder>("viewBuilder"),
			};

			return node;
		}

		protected override void OnBuilt(BuilderContext context)
		{
			var childNode = context.Result as OptionNode;

			if(childNode == null)
				return;

			var ownerNode = context.Owner as OptionNode;

			if(ownerNode == null)
				ownerNode = context.PluginContext.ApplicationContext.OptionManager.RootNode;

			if(ownerNode.Children.Contains(childNode.Name))
				ownerNode.Children[childNode.Name] = childNode;
			else
				ownerNode.Children.Add(childNode);
		}

		#endregion
	}
}
