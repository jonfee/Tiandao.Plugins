using System;
using System.Collections.Generic;

namespace Tiandao.Plugins.Parsers
{
#if !CORE_CLR
	public class ParserContext : MarshalByRefObject
#else
	public class ParserContext
#endif
	{
		#region 私有字段

		private string _text;
		private string _scheme;
		private Builtin _builtin;
		private PluginTreeNode _node;
		private Plugin _plugin;
		private string _memberName;
		private Type _memberType;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取解析文本的方案(即解析器名称)。
		/// </summary>
		public string Scheme
		{
			get
			{
				return _scheme;
			}
		}

		/// <summary>
		/// 获取待解析的不包含解析器名的文本。
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
		}

		/// <summary>
		/// 获取待解析文本所在目标对象的成员名称。
		/// </summary>
		public string MemberName
		{
			get
			{
				return _memberName;
			}
		}

		/// <summary>
		/// 获取待解析文本所在目标对象的成员类型。
		/// </summary>
		public Type MemberType
		{
			get
			{
				return _memberType;
			}
		}

		/// <summary>
		/// 获取待解析文本所在的构件(<see cref="Builtin"/>)，注意：该属性可能返回空值(null)。
		/// </summary>
		public Builtin Builtin
		{
			get
			{
				return _builtin;
			}
		}

		/// <summary>
		/// 获取待解析文本所在的插件树节点(<see cref="PluginTreeNode"/>)，注意：该属性可能返回空值(null)。
		/// </summary>
		public PluginTreeNode Node
		{
			get
			{
				return _node;
			}
		}

		/// <summary>
		/// 获取待解析文本所在构件或插件树节点所隶属的插件对象，注意：该属性可能返回空值(null)。
		/// </summary>
		public Plugin Plugin
		{
			get
			{
				return _plugin;
			}
		}

		/// <summary>
		/// 获取插件应用上下文对象，注意：该属性值可能会为空值(null)。
		/// </summary>
		/// <remarks>
		///		<para>如果当前解析器上下文关联到一个空节点或者自定义节点，则该属性返回空。</para>
		/// </remarks>
		public PluginContext PluginContext
		{
			get
			{
				return _plugin == null ? null : _plugin.Context;
			}
		}

		#endregion

		#region 构造方法

		internal ParserContext(string scheme, string text, Plugin plugin, string memberName, Type memberType)
		{
			if(plugin == null)
				throw new ArgumentNullException("plugin");

			this.Initialize(scheme, text, null, null, plugin, memberName, memberType);
		}

		internal ParserContext(string scheme, string text, PluginTreeNode node, string memberName, Type memberType)
		{
			if(node == null)
				throw new ArgumentNullException("plugin");

			this.Initialize(scheme, text, null, node, node.Plugin, memberName, memberType);
		}

		internal ParserContext(string scheme, string text, Builtin builtin, string memberName, Type memberType)
		{
			if(builtin == null)
				throw new ArgumentNullException("builtin");

			this.Initialize(scheme, text, builtin, null, builtin.Plugin, memberName, memberType);
		}

		#endregion

		#region 初始化器

		private void Initialize(string scheme, string text, Builtin builtin, PluginTreeNode node, Plugin plugin, string memberName, Type memberType)
		{
			if(string.IsNullOrWhiteSpace(scheme))
				throw new ArgumentNullException("scheme");

			if(builtin == null && node == null && plugin == null)
				throw new ArgumentException();

			_scheme = scheme;
			_text = text ?? string.Empty;
			_memberName = memberName;
			_memberType = memberType;
			_builtin = builtin;
			_node = node ?? (builtin == null ? null : builtin.Node);
			_plugin = plugin;

			if(plugin == null)
			{
				if(builtin == null)
					_plugin = node.Plugin;
				else
					_plugin = builtin.Plugin;
			}
		}

		#endregion
	}
}
