using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示当获取构件或者插件树节点中内部属性时的方式。
	/// </summary>
	public enum ObtainMode
	{
		/// <summary>只有当Value属性为空(null)时，才调用Build方法。</summary>
		Auto = 0,

		/// <summary>无论Value属性是否可用，始终调用Build方法。</summary>
		Alway,

		/// <summary>无论Value属性是否为空，始终返回节点的Value。</summary>
		Never,
	}
}