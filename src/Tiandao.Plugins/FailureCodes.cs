using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 表示插件环境中的各种失败信息的编码。
	/// </summary>
	public static class FailureCodes
	{
		/// <summary>未确定的失败。</summary>
		public static readonly int Undefined = -999;

		/// <summary>插件目录不存在。</summary>
		public static readonly int PluginsDirectoryNotExists = 101;

		/// <summary>插件文件不存在。</summary>
		public static readonly int PluginFileNotExists = 102;

		/// <summary>插件命名被重复。</summary>
		public static readonly int PluginNameDuplication = 103;

		/// <summary>插件文件读取失败。</summary>
		public static readonly int PluginFileReadFailed = 104;

		/// <summary>插件文件解析失败。</summary>
		public static readonly int PluginResolveFailed = 105;

		/// <summary>无效的插件文件格式。</summary>
		public static readonly int InvalidPluginFileFormat = 106;

		/// <summary>插件文件中出现未定义的元素。</summary>
		public static readonly int UndefinedElementInPluginFile = 107;

		/// <summary>从属插件加载失败。</summary>
		public static readonly int SlavePluginsLoadFailed = 108;

		/// <summary>创建固定元素失败。</summary>
		public static readonly int FixedElementBuildFailed = 109;

		/// <summary>创建构件失败。</summary>
		public static readonly int BuiltinBuildFailed = 110;

		/// <summary>无效的构件属性。</summary>
		public static readonly int InvalidBuiltinProperty = 111;

		/// <summary>名称中含有非法字符。</summary>
		public static readonly int IllegalCharactersInName = 112;

		/// <summary>无效的路径。</summary>
		public static readonly int InvalidPath = 113;

		/// <summary>类型解析失败。</summary>
		public static readonly int TypeResolveFailed = 114;

		/// <summary>无效的解析器格式。</summary>
		public static readonly int ParserInvalidFormat = 115;

		/// <summary>解析器匹配失败。</summary>
		public static readonly int ParserMatchFailed = 116;

		/// <summary>解析器获取失败。</summary>
		public static readonly int ParserObtainFailed = 117;

		/// <summary>无效的扩展元素。</summary>
		public static readonly int InvalidExtendElement = 118;

		/// <summary>扩展元素不存在。</summary>
		public static readonly int ExtendElementNotExists = 119;
	}
}