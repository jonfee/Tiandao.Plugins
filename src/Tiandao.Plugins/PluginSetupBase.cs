using System;
using System.IO;
using System.ComponentModel;

#if CORE_CLR
using Microsoft.Extensions.PlatformAbstractions;
#endif

namespace Tiandao.Plugins
{
#if !CORE_CLR
	public class PluginSetupBase : MarshalByRefObject, INotifyPropertyChanged
#else
	public class PluginSetupBase : INotifyPropertyChanged
#endif
	{
		#region 事件定义

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region 私有字段

		private string _applicationDirectory;
		private string _pluginsDirectoryName;
		private string _pluginsDirectory;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取应用程序的目录路径，该属性值由构造函数注入。
		/// </summary>
		/// <remarks>
		/// 应用目录是基于完全限定的路径格式，在Windows操作系统中大致如：C:\Program Files\Tiandao.EAS 或 D:\Tiandao\Tiandao.EAS 等等。
		/// </remarks>
		public string ApplicationDirectory
		{
			get
			{
				return _applicationDirectory;
			}
		}

		/// <summary>
		/// 获取插件文件夹名称，该属性值由构造函数注入。
		/// </summary>
		/// <remarks>
		/// 注意：该属性值仅为目录名本身，不含其它路径部分。它不是一个完全限定的路径格式。如果要获取插件文件夹的完全限定路径，请使用<seealso cref="Tiandao.Plugins.PluginSetupBase.PluginsDirectory"/>属性。
		/// </remarks>
		public string PluginsDirectoryName
		{
			get
			{
				return _pluginsDirectoryName;
			}
		}

		/// <summary>
		/// 获取插件文件夹的完全限定路径。
		/// </summary>
		/// <remarks>
		/// 该属性值根据<seealso cref="Tiandao.Plugins.PluginSetupBase.ApplicationDirectory"/>和<seealso cref="Tiandao.Plugins.PluginSetupBase.PluginsDirectoryName"/>属性值合并计算而来。
		/// </remarks>
		public string PluginsDirectory
		{
			get
			{
				if(string.IsNullOrEmpty(_pluginsDirectory))
					_pluginsDirectory = Path.Combine(_applicationDirectory, _pluginsDirectoryName);

				return _pluginsDirectory;
			}
		}

		#endregion

		#region 构造方法

		/// <summary>
		/// 构造插件设置对象。
		/// </summary>
		protected PluginSetupBase() : this(null, null)
		{
		}

		/// <summary>
		/// 构造插件设置对象。
		/// </summary>
		/// <param name="applicationDirectory">应用程序目录完整限定路径。</param>
		protected PluginSetupBase(string applicationDirectory) : this(applicationDirectory, null)
		{
		}

		/// <summary>
		/// 构造插件设置对象。
		/// </summary>
		/// <param name="applicationDirectory">应用程序目录完整限定路径。</param>
		/// <param name="pluginsDirectoryName">插件目录名，非完整路径。默认为 plugins</param>
		/// <remarks>
		///		如果<paramref name="applicationDirectory"/>参数为空或空字符串("")，则试图获取默认应用域(AppDomain)中的入口程序集路径。但在类似ASP.NET这样的服务器程序中无法获取入口程序集，因此本方法会抛出<seealso cref="System.ArgumentNullException"/>异常，在此场景下请指定<paramref name="applicationDirectory"/>参数值。
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">当<paramref name="applicationDirectory"/>参数为空并且无法获取默认应用域(AppDomain)中入口程序集。</exception>
		/// <exception cref="System.ArgumentException">当<paramref name="applicationDirectory"/>参数值不为路径完全限定格式。</exception>
		/// <exception cref="System.ArgumentException">当<paramref name="pluginsDirectoryName"/>参数值为路径完全限定格式，但其并不是基于<paramref name="applicationDirectory"/>参数指定的应用程序路径下的子目录。</exception>
		protected PluginSetupBase(string applicationDirectory, string pluginsDirectoryName)
		{
			_pluginsDirectory = null;

			if(string.IsNullOrEmpty(applicationDirectory))
			{
#if !CORE_CLR
				_applicationDirectory = AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY").ToString();
#else
				_applicationDirectory = PlatformServices.Default.Application.ApplicationBasePath;
#endif

			}
			else
			{
				_applicationDirectory = applicationDirectory.Trim();

				if(!Path.IsPathRooted(_applicationDirectory))
					throw new ArgumentException("This value of 'applicationDirectory' parameter is invalid.");
			}

			if(string.IsNullOrEmpty(pluginsDirectoryName))
				_pluginsDirectoryName = "plugins";
			else
			{
				_pluginsDirectoryName = pluginsDirectoryName.Trim();

				if(Path.IsPathRooted(_pluginsDirectoryName))
				{
					if(_applicationDirectory.StartsWith(_pluginsDirectoryName))
						_pluginsDirectoryName = _pluginsDirectoryName.Substring(_applicationDirectory.Length);
					else
						throw new ArgumentException("This value of 'pluginsDirectoryName' parameter is invalid.");
				}
			}
		}

		#endregion

		#region 保护方法

		protected void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName ?? string.Empty));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if(this.PropertyChanged != null)
				this.PropertyChanged(this, args);
		}

		#endregion
	}
}
