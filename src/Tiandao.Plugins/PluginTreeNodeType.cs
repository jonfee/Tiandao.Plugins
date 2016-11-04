using System;
using Tiandao.ComponentModel;

namespace Tiandao.Plugins
{
	public enum PluginTreeNodeType
	{
		/// <summary>空节点(路径节点)，即该节点的<see cref="Tiandao.Plugins.PluginTreeNode.Value"/>属性为空。</summary>
		[Description("空节点")]
		Empty,

		/// <summary>构件节点，即该节点的<see cref="Tiandao.Plugins.PluginTreeNode.Value"/>属性值的类型为<seealso cref="Tiandao.Plugins.Builtin"/>。</summary>
		[Description("构件节点")]
		Builtin,

		/// <summary>自定义节点，即该节点对应的值为内部挂载的自定义对象。</summary>
		[Description("对象节点")]
		Custom,
	}
}