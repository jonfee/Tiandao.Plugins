using System;
using System.Collections.Generic;

using Tiandao.Plugins;
using Tiandao.Plugins.Builders;

namespace Tiandao.Services.Plugins.Builders
{
	[BuilderBehaviour(typeof(PluginServiceProvider))]
	public class ServiceProviderBuilder : BuilderBase
	{
		#region 重写方法

		public override object Build(BuilderContext context)
		{
			//阻止对子节点的构建
			context.Cancel = true;

			string providerPath = context.Builtin.Properties.GetRawValue("path");

			if(string.IsNullOrWhiteSpace(providerPath))
				providerPath = "/Workspace/Services/" + context.Builtin.Name;
			else if(providerPath == ".")
				providerPath = context.Builtin.FullPath;

			return new PluginServiceProvider(context.PluginContext, providerPath);
		}

		protected override void OnBuilt(BuilderContext context)
		{
			var serviceFactory = context.Owner as ServiceProviderFactory;

			if(serviceFactory != null)
				serviceFactory.Register(context.Builtin.Name, (IServiceProvider)context.Result);
			else
				base.OnBuilt(context);
		}

		#endregion
	}
}
