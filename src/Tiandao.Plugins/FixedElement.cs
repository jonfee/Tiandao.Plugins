using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public abstract class FixedElement : PluginElement
	{
		#region 私有字段

		private Type _type;
		private string _typeName;
		private FixedElementType _fixedElementType;
		private object _value;
		private readonly object _syncRoot;

		#endregion

		#region 公共属性

		public Type Type
		{
			get
			{
				if(_type == null)
				{
					lock (_syncRoot)
					{
						if(_type == null)
						{
							Type type = PluginUtility.GetType(_typeName);

							if(!this.ValidateType(type))
								throw new InvalidOperationException();

							_type = type;
						}
					}
				}

				return _type;
			}
		}

		public FixedElementType FixedElementType
		{
			get
			{
				return _fixedElementType;
			}
		}

		public bool HasValue
		{
			get
			{
				return _value != null;
			}
		}

		#endregion

		#region 构造方法

		protected FixedElement(Type type, string name, Plugin plugin, FixedElementType elementType) : base(name, plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");
			if(type == null)
				throw new ArgumentNullException("type");

			_syncRoot = new object();
			_type = type;
			_fixedElementType = elementType;
		}

		protected FixedElement(string typeName, string name, Plugin plugin, FixedElementType elementType) : base(name, plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");
			if(string.IsNullOrWhiteSpace(typeName))
				throw new ArgumentNullException("typeName");

			_syncRoot = new object();
			_typeName = typeName;
			_fixedElementType = elementType;
		}

		#endregion

		#region 保护方法

		internal protected object GetValue()
		{
			if(_value == null)
			{
				lock (_syncRoot)
				{
					if(_value == null)
						_value = this.CreateValue();
				}
			}

			return _value;
		}

		#endregion

		#region 虚拟方法

		protected virtual bool ValidateType(Type type)
		{
			return type != null;
		}

		protected virtual object CreateValue()
		{
			if(this.Type == null)
				return null;

			try
			{
				object result = PluginUtility.BuildType(this.Type, (Type parameterType, string parameterName, out object parameterValue) =>
				{
					return PluginUtility.ObtainParameter(this.Plugin, parameterType, parameterName, out parameterValue);
				});

				if(result == null)
					throw new PluginException(string.Format("Can not build instance of '{0}' type, Maybe that's cause type-generator not found matched constructor with parameters. in '{1}' plugin.", this.Type.FullName, this.Plugin));

				return result;
			}
			catch(Exception ex)
			{
				throw new PluginException(string.Format("Occurred an exception on create a fixed-element instance of '{0}' type, at '{1}' plugin.", this.Type.FullName, this.Plugin), ex);
			}
		}

		#endregion
	}
}
