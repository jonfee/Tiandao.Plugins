using System;

using Tiandao.Options;
using Tiandao.Options.Configuration;

namespace Tiandao.Options.Plugins
{
	internal class SettingProvider : ISettingsProvider
	{
		#region 私有字段

		private Tiandao.Plugins.Plugin _plugin;
		private OptionConfiguration _configuration;
		private ISettingsProvider _settings;

		#endregion

		#region 公共属性

		public OptionConfiguration Configuration
		{
			get
			{
				return _configuration;
			}
		}

		#endregion

		#region 构造方法

		internal SettingProvider(Tiandao.Plugins.Plugin plugin, OptionConfiguration configuration)
		{
			_plugin = plugin;
			_configuration = configuration;
			_settings = (ISettingsProvider)_configuration.GetOptionObject("/settings");
		}

		#endregion

		#region 公共方法

		public object GetValue(string name)
		{
			if(_settings == null)
				return null;

			var value = _settings.GetValue(name);

			if(value != null)
				return value;

			return this.RecursiveGetValue(name);
		}

		public void SetValue(string name, object value)
		{
			if(_settings == null)
				throw new NotSupportedException();

			_settings.SetValue(name, value);
			_configuration.Save();
		}

		#endregion

		#region 私有方法

		private object RecursiveGetValue(string name)
		{
			object value;

			foreach(var dependency in _plugin.Manifest.Dependencies)
			{
				var provider = SettingProviderFactory.GetProvider(dependency.Plugin);

				if(provider != null && provider._settings != null)
				{
					value = provider._settings.GetValue(name);

					if(value != null)
						return value;
				}
			}

			if(_plugin.Parent != null)
				return SettingProviderFactory.GetProvider(_plugin.Parent).GetValue(name);

			return null;
		}

		#endregion
	}
}
