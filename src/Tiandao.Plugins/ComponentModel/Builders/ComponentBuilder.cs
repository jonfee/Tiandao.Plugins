using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

using Tiandao.Common;
using Tiandao.Plugins;
using Tiandao.Plugins.Builders;

namespace Tiandao.ComponentModel.Plugins.Builders
{
    public class ComponentBuilder : BuilderBase
	{
		#region 重写方法

		protected override void OnBuilt(BuilderContext context)
		{
			IContainer container = null;
			IComponent component = context.Result as IComponent;

			if(component == null)
				return;

			container = context.Owner as IContainer;

			if(container == null)
			{
				var workbench = context.Owner as IWorkbench;

				if(workbench == null)
				{
					container = this.GetContainer(context.Owner);
				}
				else
				{
					container = workbench.Window as IContainer;

					if(container == null)
						container = this.GetContainer(workbench.Window);
				}
			}

			if(container != null)
				container.Add(component, context.Builtin.Name);
		}

		#endregion

		#region 私有方法

		private IContainer GetContainer(object target)
		{
			if(target == null)
				return null;

#if !CORE_CLR
			var memberInfo = target.GetType().FindMembers((MemberTypes.Field | MemberTypes.Property),
									(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.GetProperty),
									(member, criteria) =>
									{
										if(member.MemberType == MemberTypes.Field)
											return typeof(IContainer).IsAssignableFrom(((FieldInfo)member).FieldType);

										if(member.MemberType == MemberTypes.Property)
											return typeof(IContainer).IsAssignableFrom(((PropertyInfo)member).PropertyType);

										return false;
									}, null).FirstOrDefault();

			if(memberInfo.MemberType == MemberTypes.Field)
				return ((FieldInfo)memberInfo).GetValue(target) as IContainer;

			if(memberInfo.MemberType == MemberTypes.Property)
				return ((PropertyInfo)memberInfo).GetValue(target, null) as IContainer;
#else
			var memberInfo = target.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(member =>
			{
				if(member.IsField())
					return TypeExtension.IsAssignableFrom(typeof(IContainer), ((FieldInfo)member).FieldType);

				if(member.IsProperty())
					return TypeExtension.IsAssignableFrom(typeof(IContainer), ((PropertyInfo)member).PropertyType);

				return false;
			}).FirstOrDefault();

			if(memberInfo.IsField())
				return ((FieldInfo)memberInfo).GetValue(target) as IContainer;

			if(memberInfo.IsProperty())
				return ((PropertyInfo)memberInfo).GetValue(target, null) as IContainer;
#endif

			return null;
		}

		#endregion
	}
}
