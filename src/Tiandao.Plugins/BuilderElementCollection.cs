using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class BuilderElementCollection : FixedElementCollection
	{
		#region 公共属性

		public BuilderElement this[int index]
		{
			get
			{
				return (BuilderElement)this.Get(index);
			}
		}

		public BuilderElement this[string name]
		{
			get
			{
				return (BuilderElement)this.Get(name);
			}
		}

		#endregion

		#region 构造方法

		internal BuilderElementCollection()
		{
		}

		#endregion

		#region 公共方法

		public BuilderElement Add(string typeName, string name, Plugin plugin)
		{
			BuilderElement item = item = new BuilderElement(typeName, name, plugin);

			base.Insert(item, -1);

			return item;
		}

		#endregion
	}
}
