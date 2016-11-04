using System;
using System.Collections.Generic;

using Tiandao.Plugins;
using Tiandao.Plugins.Builders;

namespace Tiandao.Collections.Plugins.Builders
{
    public class CategoryBuilder : Tiandao.Plugins.Builders.BuilderBase, IAppender
	{
		#region 重写方法

		public override object Build(BuilderContext context)
		{
			return new Tiandao.Collections.Category(context.Builtin.Name)
			{
				Title = context.Builtin.Properties.GetValue<string>("title"),
				Description = context.Builtin.Properties.GetValue<string>("description"),
				Tags = context.Builtin.Properties.GetValue<string>("tags"),
				Visible = context.Builtin.Properties.GetValue<bool>("visible"),
			};
		}

		#endregion

		#region 显式实现

		bool IAppender.Append(AppenderContext context)
		{
			if(context.Container == null || context.Value == null)
				return false;

			var category = context.Value as Category;

			if(category != null)
				((Category)context.Container).Children.Add(category);

			return true;
		}

		#endregion
	}
}
