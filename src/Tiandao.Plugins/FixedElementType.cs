using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示固定元件的类型。
	/// </summary>
	public enum FixedElementType
	{
		/// <summary>构建器，指实现了 <seealso cref="Tiandao.Plugins.IBuilder"/> 接口的类。</summary>
		Builder = 0,

		/// <summary>解析器，指实现了 <seealso cref="Tiandao.Plugins.IParser"/> 接口的类。</summary>
		Parser = 1,

		/// <summary>模块，指实现了 <seealso cref="Tiandao.ComponentModel.IApplicationModule"/> 接口的类。</summary>
		Module = 2,
	}
}