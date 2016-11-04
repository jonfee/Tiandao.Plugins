using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class BuilderElement : FixedElement<IBuilder>
	{
		#region 构造方法

		public BuilderElement(string typeName, string name, Plugin plugin) : base(typeName, name, plugin, FixedElementType.Builder)
		{

		}

		public BuilderElement(Type type, string name, Plugin plugin) : base(type, name, plugin, FixedElementType.Builder)
		{

		}

		#endregion
	}
}
