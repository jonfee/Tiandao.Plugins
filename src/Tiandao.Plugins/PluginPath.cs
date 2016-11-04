using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tiandao.Plugins
{
	/// <summary>
	/// 提供插件路径文本的解析功能。
	/// </summary>
	/// <remarks>
	///		<para>插件路径文本支持以下几种格式：</para>
	///		<list type="number">
	///			<item>
	///				<term>绝对路径：/root/node1/node2/node3.property1.property2</term>
	///				<term>相对路径：../siblingNode/node1/node2.property1.property2 或者 ./childNode/node1/node2.property1.property2</term>
	///				<term>属性路径：../@property1.property2 或者 ./@property1.property2（对于本节点的属性也可以简写成：@property1.property2）</term>
	///			</item>
	///		</list>
	/// </remarks>
	public static class PluginPath
    {
		#region 私有字段

		/*
^\s*
(?<prefix>\.{1,2})?
(?<path>(/[\w-]+)*)?
(?(path)|(?(prefix)/)@(?<member>[\w]+|\[[^\]]+\]))
(\.(?<member>[\w]+(\[[^\]]+\])?))*
\s*$
		 */
		private static readonly Regex _regex = new Regex(@"^\s*(?<prefix>\.{1,2})?(?<path>(/[\w-]+)*)?(?(path)|(?(prefix)/)@(?<member>[\w]+|\[[^\]]+\]))(\.(?<member>[\w]+(\[[^\]]+\])?))*\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);

		#endregion

		#region 公共方法

		public static bool IsPath(string text)
		{
			if(string.IsNullOrWhiteSpace(text))
				return false;

			return _regex.IsMatch(text);
		}

		public static bool TryResolvePath(string text, out PluginPathType type, out string path, out string[] memberNames)
		{
			type = PluginPathType.Rooted;
			path = string.Empty;
			memberNames = null;

			if(string.IsNullOrWhiteSpace(text))
				return false;

			var match = _regex.Match(text);

			if(match.Success)
			{
				path = match.Groups["path"].Value ?? string.Empty;
				memberNames = new string[match.Groups["member"].Captures.Count];

				switch(match.Groups["prefix"].Value)
				{
					case ".":
						type = PluginPathType.Current;
						path = path.Trim('/');
						break;
					case "..":
						type = PluginPathType.Parent;
						path = path.Trim('/');
						break;
					default:
						if(string.IsNullOrEmpty(path))
							type = PluginPathType.Current;
						break;
				}

				for(int i = 0; i < memberNames.Length; i++)
				{
					memberNames[i] = match.Groups["member"].Captures[i].Value;
				}
			}

			return match.Success;
		}

		public static string Combine(params string[] parts)
		{
			if(parts == null || parts.Length < 1)
				return string.Empty;

			StringBuilder text = new StringBuilder();
			string temp;

			foreach(string part in parts)
			{
				if(string.IsNullOrWhiteSpace(part))
					continue;

				temp = part.Trim('/', ' ', '\t');

				if(temp.Length > 0)
					text.Append('/' + temp);
			}

			return text.ToString();
		}
		
		#endregion
	}
}
