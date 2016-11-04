using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Builders
{
	/// <summary>
	/// 构件链接创建器。
	/// </summary>
	/// <remarks>
	///		<para>该构建器区别于<seealso cref="ObjectBuilder"/>的主要特征在于它不始终不会激发对子节点的构建。</para>
	/// </remarks>
	public class LinkBuilder : ObjectBuilder
	{
		public override Type GetValueType(Builtin builtin)
		{
			var property = builtin.Properties["ref"];

			if(property == null)
				throw new PluginException(string.Format("Missing 'ref' property in '{0}' builtin.", builtin));

			var refNode = builtin.Node.Find(property.RawValue);
			return refNode == null ? null : refNode.ValueType;
		}

		public override object Build(BuilderContext context)
		{
			var property = context.Builtin.Properties["ref"];

			if(property == null)
				throw new PluginException(string.Format("Missing 'ref' property in '{0}' builtin.", context.Builtin));

			//阻止构建下级节点
			context.Cancel = true;

			return context.PluginContext.ResolvePath(property.RawValue, context.Node, ObtainMode.Auto);
		}
	}
}
