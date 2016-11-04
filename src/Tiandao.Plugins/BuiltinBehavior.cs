using System;
using System.Collections.Generic;

using Tiandao.Common;
using Tiandao.Collections;
using Tiandao.Serialization;

namespace Tiandao.Plugins
{
    public class BuiltinBehavior
    {
		#region 私有字段

		private Builtin _builtin;
		private string _name;
		private string _text;
		private IDictionary<string, string> _properties;

		#endregion

		#region 公共属性

		public Builtin Builtin
		{
			get
			{
				return _builtin;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		public IDictionary<string, string> Properties
		{
			get
			{
				return _properties;
			}
		}

		#endregion

		#region 构造方法

		public BuiltinBehavior(Builtin builtin, string name, string text = null)
		{
			if(builtin == null)
				throw new ArgumentNullException("builtin");

			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			_builtin = builtin;
			_name = name.Trim();
			_text = text;
			_properties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		#endregion

		#region 公共方法

		public T Populate<T>(Func<T> creator = null)
		{
			var dictionary = DictionaryExtension.ToDictionary<string, object>((System.Collections.IDictionary)_properties);

			return DictionarySerializer.Default.Deserialize<T>((System.Collections.IDictionary)dictionary, creator, ctx =>
			{
				if(ctx.Direction == Converter.ObjectResolvingDirection.Get)
				{
					ctx.Handled = false;
					return;
				}

				var text = ctx.Value as string;

				if(text != null)
					ctx.Value = PluginUtility.ResolveValue(_builtin, text, ctx.MemberName, ctx.MemberType, Converter.GetDefaultValue(ctx.MemberType));
			});
		}

		public T GetPropertyValue<T>(string propertyName, T defaultValue = default(T))
		{
			if(string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException("propertyName");

			string rawValue;

			if(_properties.TryGetValue(propertyName, out rawValue))
				return Converter.ConvertValue<T>(PluginUtility.ResolveValue(_builtin, rawValue, propertyName, typeof(T), defaultValue));

			return defaultValue;
		}

		public object GetPropertyValue(string propertyName, Type valueType, object defaultValue = null)
		{
			if(string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException("propertyName");

			string rawValue;

			if(_properties.TryGetValue(propertyName, out rawValue))
				return PluginUtility.ResolveValue(_builtin, rawValue, propertyName, valueType, defaultValue);

			return defaultValue;
		}

		#endregion
	}
}
