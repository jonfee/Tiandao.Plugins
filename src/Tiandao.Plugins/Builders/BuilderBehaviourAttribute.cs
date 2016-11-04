using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Builders
{
	/// <summary>
	/// 提供构建器行为约定的特性类。
	/// </summary>
	/// <remarks>
	///		<para>在特定情况建议使用该类对构建器进行定制。</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class)]
	public class BuilderBehaviourAttribute : Attribute
    {
		#region 私有字段

		private Type _valueType;

		#endregion

		#region 公共属性

		public Type ValueType
		{
			get
			{
				return _valueType;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_valueType = value;
			}
		}

		#endregion

		#region 构造方法

		public BuilderBehaviourAttribute(Type valueType)
		{
			if(valueType == null)
				throw new ArgumentNullException("valueType");

			_valueType = valueType;
		}

		#endregion
	}
}
