using System;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class ViewEventArgs : EventArgs
	{
		#region 公共属性

		public object View
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public ViewEventArgs(object view)
		{
			if(view == null)
				throw new ArgumentNullException("view");

			this.View = view;
		}

		#endregion
	}
}
