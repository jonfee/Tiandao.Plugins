using System;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 有关插件运行环境的设置信息。
	/// </summary>
#if !CORE_CLR
	[Serializable]
	public class PluginSetup : PluginSetupBase, ICloneable
#else
	public class PluginSetup : PluginSetupBase
#endif
	{
		#region 私有字段

		private IsolationLevel _isolationLevel;
		private string _workbenchPath;
		private string _applicationContextPath;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取或设置插件的隔离级别。
		/// </summary>
		public IsolationLevel IsolationLevel
		{
			get
			{
				return _isolationLevel;
			}
			set
			{
				if(_isolationLevel == value)
					return;

				_isolationLevel = value;
				this.OnPropertyChanged("IsolationLevel");
			}
		}

		/// <summary>
		/// 获取或设置应用程序上下文位于插件树的路径。默认值为：/Workspace/Environment/ApplicationContext
		/// </summary>
		public string ApplicationContextPath
		{
			get
			{
				return _applicationContextPath;
			}
			set
			{
				if(string.IsNullOrEmpty(value))
					throw new ArgumentNullException();

				if(!string.Equals(_applicationContextPath, value, StringComparison.OrdinalIgnoreCase))
				{
					_applicationContextPath = value.Trim().TrimEnd('/');
					this.OnPropertyChanged("ApplicationContextPath");
				}
			}
		}

		/// <summary>
		/// 获取或设置<seealso cref="Tiandao.Plugins.IWorkbenchBase"/>工作台位于插件树的路径。默认值为：/Workbench
		/// </summary>
		public string WorkbenchPath
		{
			get
			{
				return _workbenchPath;
			}
			set
			{
				if(string.IsNullOrEmpty(value))
					throw new ArgumentNullException();

				if(!string.Equals(_workbenchPath, value, StringComparison.OrdinalIgnoreCase))
				{
					_workbenchPath = value.Trim().TrimEnd('/');
					this.OnPropertyChanged("WorkbenchPath");
				}
			}
		}

		#endregion

		#region 构造方法

		public PluginSetup() : this(null, null)
		{
		}

		public PluginSetup(string applicationDirectory) : this(applicationDirectory, null)
		{
		}

		public PluginSetup(string applicationDirectory, string pluginsDirectoryName) : this(applicationDirectory, pluginsDirectoryName, IsolationLevel.None)
		{
		}

		public PluginSetup(string applicationDirectory, string pluginsDirectoryName, IsolationLevel isolationLevel) : base(applicationDirectory, pluginsDirectoryName)
		{
			_isolationLevel = isolationLevel;

			_workbenchPath = "/Workbench";
			_applicationContextPath = "/Workspace/Environment/ApplicationContext";
		}

		#endregion

		#region 虚拟方法

		public virtual object Clone()
		{
			return new PluginSetup(this.ApplicationDirectory, this.PluginsDirectoryName)
			{
				IsolationLevel = _isolationLevel,
				ApplicationContextPath = _applicationContextPath,
				WorkbenchPath = _workbenchPath,
			};
		}

		#endregion
	}
}
