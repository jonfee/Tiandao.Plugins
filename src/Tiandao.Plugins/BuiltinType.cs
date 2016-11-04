using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class BuiltinType
    {
		#region 私有字段

		private Type _type;
		private Builtin _builtin;
		private string _typeName;
		private BuiltinTypeConstructor _constructor;

		#endregion

		#region 公共属性

		public Builtin Builtin
		{
			get
			{
				return _builtin;
			}
		}

		public Type Type
		{
			get
			{
				if(_type == null)
					_type = PluginUtility.GetType(this.TypeName);

				return _type;
			}
		}

		public string TypeName
		{
			get
			{
				return _typeName;
			}
			internal set
			{
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException();

				_typeName = value.Trim();
				_type = null;
			}
		}

		public BuiltinTypeConstructor Constructor
		{
			get
			{
				return _constructor;
			}
		}

		#endregion

		#region 构造方法

		public BuiltinType(Builtin builtin, string typeName)
		{
			if(builtin == null)
				throw new ArgumentNullException("builtin");

			if(string.IsNullOrWhiteSpace(typeName))
				throw new ArgumentNullException("typeName");

			_type = null;
			_builtin = builtin;
			_typeName = typeName.Trim();
			_constructor = new BuiltinTypeConstructor(this);
		}

		#endregion
	}
}