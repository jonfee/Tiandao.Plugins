using System;
using System.Runtime.Serialization;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
#endif
	public class PluginFileException : PluginException
	{
		#region 公共属性

		public string FileName
		{
			get;
			private set;
		}

		#endregion

		#region 构造方法

		public PluginFileException(string fileName) : base(string.Empty, null)
		{
			this.FileName = fileName;
		}

		public PluginFileException(string fileName, string message) : base(message, null)
		{
			this.FileName = fileName;
		}

		public PluginFileException(string fileName, string message, Exception innerException) : base(message, innerException)
		{
			this.FileName = fileName;
		}

#if !CORE_CLR
		protected PluginFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
#endif

		#endregion

		#region 重写方法

#if !CORE_CLR
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("FileName", this.FileName);
		}
#endif

		#endregion
	}
}
