using System;
using System.Linq;

using Tiandao.Plugins;

namespace Tiandao.Options.Plugins
{
    public class OptionModule : ComponentModel.IApplicationModule
	{
		#region 公共属性

		public string Name
		{
			get
			{
				return "OptionConfigurationModule";
			}
		}

		#endregion

		#region 初始化器

		public void Initialize(PluginApplicationContext context)
		{
			if(context == null)
				return;

			//将当前应用的主配置文件加入到选项管理器中
			if(context.Configuration != null)
				context.OptionManager.Providers.Add(context.Configuration);

			context.PluginContext.PluginTree.Loader.PluginLoaded += Loader_PluginLoaded;
			context.PluginContext.PluginTree.Loader.PluginUnloaded += Loader_PluginUnloaded;
		}

		void Tiandao.ComponentModel.IApplicationModule.Initialize(Tiandao.ComponentModel.ApplicationContextBase context)
		{
			this.Initialize(context as PluginApplicationContext);
		}
		
		#endregion

		#region 事件处理

		private void Loader_PluginLoaded(object sender, PluginLoadedEventArgs e)
		{
			var proxy = new ConfigurationProxy(() => OptionUtility.GetConfiguration(e.Plugin));
			e.Plugin.Context.ApplicationContext.OptionManager.Providers.Add(proxy);
		}

		private void Loader_PluginUnloaded(object sender, PluginUnloadedEventArgs e)
		{
			var providers = e.Plugin.Context.ApplicationContext.OptionManager.Providers;

			var found = providers.FirstOrDefault(provider =>
			{
				var proxy = provider as ConfigurationProxy;

				return (proxy != null && proxy.IsValueCreated &&
						string.Equals(proxy.Value.FilePath, OptionUtility.GetConfigurationFilePath(e.Plugin)));
			});

			if(found != null)
				providers.Remove(found);
		}
		
		#endregion

		#region 释放资源

		void IDisposable.Dispose()
		{
		}
		
		#endregion

		#region 嵌套子类

		private class ConfigurationProxyLoader : Configuration.OptionConfigurationLoader
		{
			#region 构造方法

			public ConfigurationProxyLoader(OptionNode root) : base(root)
			{
			}

			#endregion

			public override void Load(IOptionProvider provider)
			{
				var proxy = provider as ConfigurationProxy;

				if(proxy != null)
					base.LoadConfiguration(proxy.Value);
				else
					base.Load(provider);
			}

			public override void Unload(IOptionProvider provider)
			{
				var proxy = provider as ConfigurationProxy;

				if(proxy != null)
					base.UnloadConfiguration(proxy.Value);
				else
					base.Unload(provider);
			}
		}

		[OptionLoader(LoaderType = typeof(ConfigurationProxyLoader))]
		private class ConfigurationProxy : IOptionProvider
		{
			#region 私有字段

			private readonly Lazy<Configuration.OptionConfiguration> _proxy;

			#endregion

			#region 公共属性

			public Configuration.OptionConfiguration Value
			{
				get
				{
					return _proxy.Value;
				}
			}

			public bool IsValueCreated
			{
				get
				{
					return _proxy.IsValueCreated;
				}
			}

			#endregion

			#region 构造方法

			public ConfigurationProxy(Func<Configuration.OptionConfiguration> valueFactory)
			{
				if(valueFactory == null)
					throw new ArgumentNullException("valueFactory");

				_proxy = new Lazy<Configuration.OptionConfiguration>(valueFactory, true);
			}

			#endregion

			#region 公共方法

			public object GetOptionObject(string path)
			{
				var configuration = _proxy.Value;

				if(configuration != null)
					return configuration.GetOptionObject(path);

				return null;
			}

			public void SetOptionObject(string path, object optionObject)
			{
				var configuration = _proxy.Value;

				if(configuration != null)
					configuration.SetOptionObject(path, optionObject);
			}

			#endregion
		}

		#endregion
	}
}
