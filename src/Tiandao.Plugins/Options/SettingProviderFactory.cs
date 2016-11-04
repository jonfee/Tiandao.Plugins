using System;
using System.Collections.Generic;

namespace Tiandao.Options.Plugins
{
	internal static class SettingProviderFactory
	{
		#region 私有字段

		private static readonly Tiandao.Collections.ObjectCache<SettingProvider> _cache;

		#endregion

		#region 静态构造

		static SettingProviderFactory()
		{
			_cache = new Collections.ObjectCache<SettingProvider>(0);
		}

		#endregion

		#region 公共方法

		public static SettingProvider GetProvider(Tiandao.Plugins.Plugin plugin)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			var configuration = OptionUtility.GetConfiguration(plugin);

			if(configuration == null)
				return null;

			return _cache.Get(plugin.FilePath, key =>
			{
				return new SettingProvider(plugin, configuration);
			});
		}

		#endregion
	}
}
