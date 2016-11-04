using System;
using System.Collections.Generic;

using Tiandao.Plugins;

namespace Tiandao.Services.Plugins
{
    public class PluginPredicationContext
    {
		#region 私有字段

		private string _parameter;
		private Builtin _builtin;
		private PluginTreeNode _node;
		private Plugin _plugin;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获传入的参数文本。
		/// </summary>
		public string Parameter
		{
			get
			{
				return _parameter;
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

		public PluginPredicationContext(string parameter, Builtin builtin)
		{
			_parameter = parameter;
			_builtin = builtin;
			_node = builtin.Node;
			_plugin = builtin.Plugin;
		}

		public PluginPredicationContext(string parameter, PluginTreeNode node, Plugin plugin)
		{
			_parameter = parameter;
			_node = node;
			_plugin = plugin ?? node.Plugin;
		}

		public PluginPredicationContext(string parameter, Builtin builtin, PluginTreeNode node, Plugin plugin)
		{
			_parameter = parameter;

			_builtin = builtin;

			if(builtin != null)
			{
				_node = builtin.Node;
				_plugin = builtin.Plugin;
			}

			if(node != null)
			{
				_node = node;
				_plugin = plugin ?? node.Plugin;
			}

			if(plugin != null)
				_plugin = plugin;
		}

		#endregion
	}
}
