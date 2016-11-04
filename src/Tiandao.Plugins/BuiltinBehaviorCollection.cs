using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class BuiltinBehaviorCollection : Collections.NamedCollectionBase<BuiltinBehavior>
	{
		#region 私有字段

		private Builtin _builtin;

		#endregion

		#region 构造方法

		public BuiltinBehaviorCollection(Builtin builtin)
		{
			if(builtin == null)
				throw new ArgumentNullException("builtin");

			_builtin = builtin;
		}

		#endregion

		#region 公共方法

		public BuiltinBehavior Add(string name, string text = null)
		{
			var result = new BuiltinBehavior(_builtin, name, text);
			this.Add(result);
			return result;
		}

		public T GetBehaviorValue<T>(string name, T defaultValue = default(T))
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			var parts = name.Split('.');

			if(parts.Length < 2)
				throw new ArgumentException("");

			var behavior = this[parts[0].Trim()];

			if(behavior == null)
				return defaultValue;

			return behavior.GetPropertyValue<T>(parts[1], defaultValue);
		}

		public object GetBehaviorValue(string name, Type valueType, object defaultValue = null)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			var parts = name.Split('.');

			if(parts.Length < 2)
				throw new ArgumentException("");

			var behavior = this[parts[0].Trim()];

			if(behavior == null)
				return defaultValue;

			return behavior.GetPropertyValue(parts[1], valueType, defaultValue);
		}

		#endregion

		#region 重写方法

		protected override void InsertItems(int index, IEnumerable<BuiltinBehavior> items)
		{
			foreach(var item in items)
			{
				if(item.Builtin != null && item.Builtin != _builtin)
					throw new InvalidOperationException();
			}

			//调用基类同名方法
			base.InsertItems(index, items);
		}

		protected override string GetKeyForItem(BuiltinBehavior item)
		{
			return item.Name;
		}

		#endregion
	}
}
