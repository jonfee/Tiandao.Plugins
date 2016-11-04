using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 封装了有关插件特定的信息。
	/// </summary>
#if !CORE_CLR
	public class PluginContext : MarshalByRefObject
#else
	public class PluginContext
#endif
	{
		#region 私有字段

		private PluginTree _pluginTree;
		private PluginSetup _settings;
		private PluginApplicationContext _applicationContext;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取当前插件运行时的唯一插件树对象。
		/// </summary>
		public PluginTree PluginTree
		{
			get
			{
				return _pluginTree;
			}
		}

		/// <summary>
		/// 获取加载的根插件集。
		/// </summary>
		public IEnumerable<Plugin> Plugins
		{
			get
			{
				return _pluginTree.Plugins;
			}
		}

		/// <summary>
		/// 获取当前插件运行时所属的应用程序上下文对象。
		/// </summary>
		public PluginApplicationContext ApplicationContext
		{
			get
			{
				return _applicationContext;
			}
		}

		/// <summary>
		/// 获取当前插件运行时的服务供应程序工厂。
		/// </summary>
		public Tiandao.Services.IServiceProviderFactory ServiceFactory
		{
			get
			{
				return _applicationContext.ServiceFactory;
			}
		}

		/// <summary>
		/// 获取当前插件上下文对应的设置。
		/// </summary>
		public PluginSetup Settings
		{
			get
			{
				return _settings;
			}
		}

		/// <summary>
		/// 获取插件的隔离级别。
		/// </summary>
		public IsolationLevel IsolationLevel
		{
			get
			{
				return _settings.IsolationLevel;
			}
		}

		/// <summary>
		/// 获取当前工作台(主界面)对象。
		/// </summary>
		public IWorkbenchBase Workbench
		{
			get
			{
				return this.ResolvePath(this.Settings.WorkbenchPath, ObtainMode.Auto) as IWorkbenchBase;
			}
		}

		#endregion

		#region 构造方法

		internal PluginContext(PluginSetup settings, PluginApplicationContext applicationContext)
		{
			if(settings == null)
				throw new ArgumentNullException("settings");

			if(applicationContext == null)
				throw new ArgumentNullException("applicationContext");

			_settings = (PluginSetup)settings.Clone();
			_pluginTree = new PluginTree(this);
			_applicationContext = applicationContext;

			_settings.PropertyChanged += delegate
			{
				if(_pluginTree != null && _pluginTree.Status != PluginTreeStatus.None)
					throw new InvalidOperationException();
			};
		}

		#endregion

		#region 解析路径

		/// <summary>
		/// 根据指定的路径文本获取其对应的缓存对象或该对象的成员值。
		/// </summary>
		/// <param name="pathText">要获取的路径文本，该文本可以用过句点符号(.)表示缓存对象的成员名。</param>
		/// <returns>返回获取的缓存对象或其成员值。</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="pathText"/>参数为空或全空字符串。</exception>
		/// <exception cref="System.ArgumentException">参数中包含成员名，但是在该缓存对象中并没找到其成员。</exception>
		/// <remarks>
		/// 注意：成员名只能是公共的实例属性或字段。
		/// <example>/Workspace/Environment/ApplicationContext.ApplicationId</example>
		/// </remarks>
		public object ResolvePath(string pathText)
		{
			return this.ResolvePath(pathText, ObtainMode.Auto);
		}

		public object ResolvePath(string pathText, ObtainMode obtainMode)
		{
			return this.ResolvePath(pathText, null, obtainMode);
		}
		
		internal object ResolvePath(string text, PluginTreeNode current, ObtainMode obtainMode)
		{
			PluginPathType pathType;
			string path;
			string[] memberNames;

			if(!PluginPath.TryResolvePath(text, out pathType, out path, out memberNames))
				throw new PluginException(string.Format("Resolve ‘{0}’ plugin-path was failed.", text));

			PluginTreeNode node = null;

			switch(pathType)
			{
				case PluginPathType.Rooted:
					node = _pluginTree.RootNode;
					break;
				case PluginPathType.Parent:
					node = current.Parent;
					break;
				case PluginPathType.Current:
					node = current;
					break;
			}

			if(node != null && (!string.IsNullOrWhiteSpace(path)))
				node = node.Find(path);

			//注意：如果没有找到指定路径的对象不需要写日志，在ServicesParser解析中需要先在默认工厂查询指定路径的服务如果查找失败则查找服务工厂集
			if(node == null)
				return null;

			try
			{
				//获取指定路径的目标对象
				object target = node.UnwrapValue(obtainMode, this, null);

				if(target != null && memberNames.Length > 0)
					return Tiandao.Common.Converter.GetValue(target, memberNames);

				return target;
			}
			catch(Exception ex)
			{
				var fileName = string.Empty;

				if(current != null && current.Plugin != null)
					fileName = System.IO.Path.GetFileName(current.Plugin.FilePath);

				throw new PluginException(FailureCodes.InvalidPath, string.Format("Resolve target error from '{0}' path in '{1}' plugin file.", text, fileName), ex);
			}
		}

		#endregion
	}
}
