using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Tiandao.Plugins.Builders
{
    public class ObjectBuilder : BuilderBase, IAppender
	{
		#region 构造方法

		public ObjectBuilder() : base(new string[] { "value" })
		{
		}

		public ObjectBuilder(IEnumerable<string> ignoredProperties) : base(ignoredProperties)
		{
		}

		#endregion

		#region 重写方法

		public override Type GetValueType(Builtin builtin)
		{
			if(builtin == null)
				return base.GetValueType(builtin);

			var property = builtin.Properties["value"];

			if(property != null && Parsers.Parser.CanParse(property.RawValue))
				return Parsers.Parser.GetValueType(property.RawValue, builtin);

			return base.GetValueType(builtin);
		}

		public override object Build(BuilderContext context)
		{
			object result = null;

			if(context.Builtin.Properties.TryGetValue("value", out result))
				PluginUtility.UpdateProperties(result, context.Builtin, this.IgnoredProperties);
			else
				result = base.Build(context);

			return result;
		}

		#endregion

		#region 显式实现

		bool IAppender.Append(AppenderContext context)
		{
			if(context.Container == null || context.Value == null)
				return false;

			if(this.Append(context.Container, context.Value, context.Node.Name))
				return true;

			var names = new string[] { "Children", "Items", "Nodes", "Controls" };

			foreach(string name in names)
			{
				//获取插入属性的反射对象
				var property = context.Container.GetType().GetProperty(name, (BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty));

				if(property != null)
				{
					object children = property.GetValue(context.Container, null);

					if(this.Append(children, context.Value, context.Node.Name))
						return true;
				}
			}

			//最后返回失败
			return false;
		}

		#endregion

		#region 私有方法

		private bool Append(object container, object item, string key)
		{
			if(container == null || item == null)
				return false;

			Type containerType = container.GetType();

			if(typeof(IDictionary).IsAssignableFrom(containerType))
			{
				((IDictionary)container).Add(key, item);
				return true;
			}
			else if(typeof(IList).IsAssignableFrom(containerType))
			{
				((IList)container).Add(item);
				return true;
			}

			var methods = containerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
						  .Where(method => method.Name == "Add" || method.Name == "Register")
						  .OrderByDescending(method => method.GetParameters().Length);

			foreach(var method in methods)
			{
				var parameters = method.GetParameters();

				if(parameters.Length == 2)
				{
					if(parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType.IsAssignableFrom(item.GetType()))
					{
						method.Invoke(container, new object[] { key, item });
						return true;
					}
				}
				else if(parameters.Length == 1)
				{
					if(parameters[0].ParameterType.IsAssignableFrom(item.GetType()))
					{
						method.Invoke(container, new object[] { item });
						return true;
					}
				}
			}

			return false;
		}

		#endregion
	}
}