using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	public class FixedElementCollection : PluginElementCollection<FixedElement>
	{
		#region 公共属性

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region 构造方法

		internal protected FixedElementCollection()
		{
		}

		#endregion

		#region 公共方法

		public void Clear()
		{
			this.BaseClear();
		}

		public void Remove(string name)
		{
			this.BaseRemoveKey(name);
		}

		#endregion
	}
}
