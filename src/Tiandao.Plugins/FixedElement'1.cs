using System;
using Tiandao.Common;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class FixedElement<T> : FixedElement
	{
		#region 公共属性

		public T Value
		{
			get
			{
				return (T)base.GetValue();
			}
		}

		#endregion

		#region 构造方法

		public FixedElement(Type type, string name, Plugin plugin, FixedElementType elementType) : base(type, name, plugin, elementType)
		{
		}

		public FixedElement(string typeName, string name, Plugin plugin, FixedElementType elementType) : base(typeName, name, plugin, elementType)
		{
		}

		#endregion

		#region 重写方法

		protected override bool ValidateType(Type type)
		{
			if(type == null)
				return false;

			return TypeExtension.IsAssignableFrom(typeof(T), type);
		}

		#endregion
	}
}
