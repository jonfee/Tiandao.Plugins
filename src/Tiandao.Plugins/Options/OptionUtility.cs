using System;
using System.IO;

using Tiandao.Options;
using Tiandao.Options.Profiles;
using Tiandao.Options.Configuration;

namespace Tiandao.Options.Plugins
{
	internal static class OptionUtility
	{
		public static string GetAssistedFilePath(Tiandao.Plugins.Plugin plugin, string extensionName)
		{
			if(plugin == null || string.IsNullOrWhiteSpace(extensionName))
				return null;

			return Path.Combine(
				   Path.GetDirectoryName(plugin.FilePath),
				   Path.GetFileNameWithoutExtension(plugin.FilePath) + (extensionName[0] == '.' ? extensionName : "." + extensionName));
		}

		public static string GetConfigurationFilePath(Tiandao.Plugins.Plugin plugin)
		{
			return GetAssistedFilePath(plugin, ".option");
		}

		public static Profile GetProfile(Tiandao.Plugins.Plugin plugin)
		{
			if(plugin == null)
				return null;

			return Profile.Load(GetAssistedFilePath(plugin, ".ini"));
		}

		public static OptionConfiguration GetConfiguration(Tiandao.Plugins.Plugin plugin)
		{
			if(plugin == null)
				return null;

			string filePath = GetAssistedFilePath(plugin, ".option");
			return OptionConfigurationManager.Open(filePath);

			//OptionConfiguration configuration = OptionConfigurationManager.Open(filePath, true);

			//var section = configuration.GetSection("/") ?? configuration.Sections.Add("/");
			//var settings = section["settings"] as SettingElementCollection;

			//if(settings == null)
			//{
			//	settings = new SettingElementCollection();
			//	section.Children.Add("settings", settings);
			//}

			//return configuration;
		}
	}
}
