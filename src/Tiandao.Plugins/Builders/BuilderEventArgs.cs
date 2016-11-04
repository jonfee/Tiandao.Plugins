using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Builders
{
    public class BuilderEventArgs : EventArgs
	{
		#region 私有字段

		private IBuilder _builder;
		private BuilderContext _context;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取当前的构建器对象。
		/// </summary>
		public IBuilder Builder
		{
			get
			{
				return _builder;
			}
		}

		/// <summary>
		/// 获取当前的构建上下文对象。
		/// </summary>
		public BuilderContext Context
		{
			get
			{
				return _context;
			}
		}

		#endregion

		#region 构造方法

		public BuilderEventArgs(IBuilder builder, BuilderContext context)
		{
			if(builder == null)
				throw new ArgumentNullException("builder");

			if(context == null)
				throw new ArgumentNullException("context");

			_builder = builder;
			_context = context;
		}

		#endregion
	}
}
