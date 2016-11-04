using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class ValueChangingEventArgs : EventArgs
	{
		#region 公共属性

		public object OldValue
		{
			get;
			private set;
		}

		public object NewValue
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public ValueChangingEventArgs(object oldValue, object newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}

		#endregion
	}
}
