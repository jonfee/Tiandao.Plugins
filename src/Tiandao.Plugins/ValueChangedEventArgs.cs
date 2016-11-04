using System;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class ValueChangedEventArgs : EventArgs
	{
		#region 公共属性

		public object Value
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public ValueChangedEventArgs(object value)
		{
			this.Value = value;
		}

		#endregion
	}
}
