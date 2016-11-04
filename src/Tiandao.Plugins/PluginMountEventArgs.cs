using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginMountEventArgs : EventArgs
	{
		#region 私有字段

		private string _path;
		private object _value;

		#endregion

		#region 公共属性

		public string Path
		{
			get
			{
				return _path;
			}
		}

		public object Value
		{
			get
			{
				return _value;
			}
		}

		#endregion

		#region 构造方法

		public PluginMountEventArgs(string path, object value)
		{
			if(string.IsNullOrEmpty(path))
				throw new ArgumentNullException("path");

			_path = path;
			_value = value;
		}

		#endregion
	}
}
