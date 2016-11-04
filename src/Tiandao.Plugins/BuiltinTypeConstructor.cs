using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tiandao.Plugins
{
	public class BuiltinTypeConstructor : IEnumerable<BuiltinTypeConstructor.Parameter>
	{
		#region 静态变量

		private static Parameter[] EmptyParameters = new Parameter[0];

		#endregion

		#region 私有字段

		private BuiltinType _builtinType;
		private IList<Parameter> _parameters;
		private Parameter[] _parameterArray;

		#endregion

		#region 公共属性

		public Builtin Builtin
		{
			get
			{
				return _builtinType.Builtin;
			}
		}

		public BuiltinType BuiltinType
		{
			get
			{
				return _builtinType;
			}
		}

		/// <summary>
		/// 获取构造子参数的数量。
		/// </summary>
		public int Count
		{
			get
			{
				return _parameterArray == null ? _parameters.Count : _parameterArray.Length;
			}
		}

		public Parameter[] Parameters
		{
			get
			{
				if(_parameterArray != null)
					return _parameterArray;

				if(_parameters.Count == 0)
					return EmptyParameters;
				else
					return _parameters.ToArray();
			}
		}

		#endregion

		#region 构造方法

		internal BuiltinTypeConstructor(BuiltinType builtinType)
		{
			if(builtinType == null)
				throw new ArgumentNullException("builtinType");

			_builtinType = builtinType;
			_parameters = new List<Parameter>();
		}

		#endregion

		#region 内部方法

		internal Parameter Add(string parameterType, string rawValue)
		{
			var parameter = new Parameter(this, parameterType, rawValue);
			_parameters.Add(parameter);
			_parameterArray = null;

			return parameter;
		}

		#endregion

		#region 枚举遍历

		public IEnumerator<Parameter> GetEnumerator()
		{
			Parameter[] values = _parameterArray;

			if(values == null)
				_parameterArray = values = new Parameter[_parameters.Count];

			foreach(var value in values)
			{
				yield return value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		#region 嵌套子类

#if !CORE_CLR
		[Serializable]
		public class Parameter : MarshalByRefObject
#else
		public class Parameter
#endif
		{
			#region 成员变量

			private BuiltinTypeConstructor _constructor;
			private string _rawValue;
			private string _parameterTypeName;
			private Type _parameterType;
			private object _value;

			#endregion

			#region 私有变量

			private int _evaluateValueRequired;

			#endregion

			#region 公共属性

			public Builtin Builtin
			{
				get
				{
					return _constructor._builtinType.Builtin;
				}
			}

			public BuiltinTypeConstructor Constructor
			{
				get
				{
					return _constructor;
				}
			}

			public Type ParameterType
			{
				get
				{
					if(_parameterType == null && (!string.IsNullOrWhiteSpace(_parameterTypeName)))
						_parameterType = PluginUtility.GetType(_parameterTypeName);

					return _parameterType;
				}
			}

			public string ParameterTypeName
			{
				get
				{
					return _parameterTypeName;
				}
			}

			public string RawValue
			{
				get
				{
					return _rawValue;
				}
				internal set
				{
					if(string.Equals(_rawValue, value, StringComparison.Ordinal))
						return;

					_rawValue = value;

					//启用重新计算Value属性
					System.Threading.Interlocked.Exchange(ref _evaluateValueRequired, 0);
				}
			}

			public bool HasValue
			{
				get
				{
					return _value != null;
				}
			}

			public object Value
			{
				get
				{
					return this.GetValue(null);
				}
			}

			#endregion

			#region 构造方法

			internal Parameter(BuiltinTypeConstructor constructor, string typeName, string rawValue)
			{
				if(constructor == null)
					throw new ArgumentNullException("constructor");

				_constructor = constructor;
				_parameterTypeName = typeName;
				_rawValue = rawValue;
				_evaluateValueRequired = 0;
			}

			#endregion

			#region 公共方法

			public object GetValue(Type valueType)
			{
				var original = System.Threading.Interlocked.CompareExchange(ref _evaluateValueRequired, 1, 0);

				if(original == 0)
				{
					if(valueType != null && string.IsNullOrEmpty(_parameterTypeName))
						_value = PluginUtility.ResolveValue(_constructor.Builtin, _rawValue, null, valueType, null);
					else
						_value = PluginUtility.ResolveValue(_constructor.Builtin, _rawValue, null, this.ParameterType, null);
				}

				return _value;
			}

			#endregion
		}

		#endregion
	}
}