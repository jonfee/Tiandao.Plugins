using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 提供追加器的功能接口。
	/// </summary>
	/// <remarks>
	///		<para>由构建器可选性的实现该接口，实现该接口的构建器支持将子级构建器创建的目标对象追加到当前目标对象的特定集合中。</para>
	/// </remarks>
	public interface IAppender
    {
		/// <summary>
		/// 将指定的子级目标对象追加到当前目标对象的特定集合中。
		/// </summary>
		/// <param name="context">追加器的上下文对象。</param>
		/// <returns>如果追加成功则返回真(true)，否则返回假(false)。</returns>
		bool Append(AppenderContext context);
	}
}
