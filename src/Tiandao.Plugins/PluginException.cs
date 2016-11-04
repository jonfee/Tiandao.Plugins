using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginException : Exception
	{
		#region 私有字段

		private int _failureCode;

		#endregion

		#region 公共属性

		public int FailureCode
		{
			get
			{
				return _failureCode;
			}
		}

		#endregion

		#region 构造方法

		public PluginException() : this(string.Empty, null)
		{
		}

		public PluginException(string message) : this(message, null)
		{
		}

		public PluginException(string message, Exception innerException) : base(message, innerException)
		{
			_failureCode = 0;
		}

		public PluginException(int failureCode, string message) : this(failureCode, message, null)
		{
		}

		public PluginException(int failureCode, string message, Exception innerException) : base(message, innerException)
		{
			_failureCode = failureCode;
		}

#if !CORE_CLR
		protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			_failureCode = info.GetInt32("FailureCode");
		}
#endif

		#endregion

		#region 重写方法

#if !CORE_CLR
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FailureCode", _failureCode);
		}
#endif

		#endregion
	}
}