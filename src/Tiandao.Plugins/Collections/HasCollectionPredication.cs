using System;
using System.Collections;
using System.Collections.Generic;

namespace Tiandao.Collections.Plugins
{
    public class HasCollectionPredication : Tiandao.Services.PredicationBase<Tiandao.Services.Plugins.PluginPredicationContext>
	{
		public HasCollectionPredication(string name) : base(name)
		{

		}

		public override bool Predicate(Services.Plugins.PluginPredicationContext context)
		{
			if(context == null || context.Node == null)
				return false;

			var target = context.Node.UnwrapValue(Tiandao.Plugins.ObtainMode.Never, null);
			ICollection collection;

			if(string.IsNullOrWhiteSpace(context.Parameter))
				collection = target as ICollection;
			else
				collection = Tiandao.Common.Converter.GetValue(target, context.Parameter) as ICollection;

			return collection != null && collection.Count > 0;
		}
	}
}
