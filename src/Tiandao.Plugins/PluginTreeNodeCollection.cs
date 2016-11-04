using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class PluginTreeNodeCollection : PluginElementCollection<PluginTreeNode>
	{
		#region 私有字段

		private PluginTreeNode _owner;

		#endregion

		#region 公共属性

		public PluginTreeNode this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		public PluginTreeNode this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		#endregion

		#region 构造方法

		public PluginTreeNodeCollection(PluginTreeNode owner)
		{
			_owner = owner;
		}

		#endregion

		#region 重写方法

		protected override void OnSetComplete(PluginTreeNode oldValue, PluginTreeNode newValue)
		{
			oldValue.Parent = null;
			newValue.Parent = null;

			base.OnSetComplete(oldValue, newValue);
		}

		protected override void OnInsertComplete(PluginTreeNode value, int index)
		{
			value.Parent = _owner;
			base.OnInsertComplete(value, index);
		}

		protected override void OnRemoveComplete(PluginTreeNode value)
		{
			value.Parent = null;
			base.OnRemoveComplete(value);
		}

		protected override bool ValidateElement(PluginTreeNode element)
		{
			if(element == null)
				throw new ArgumentNullException("element");

			if(element.Name.Contains("/"))
				return false;

			if(element.Parent == null)
				return true;

			return element.Parent == _owner;
		}

		#endregion

		#region 内部方法

		internal void Add(PluginTreeNode node)
		{
			if(node == null)
				throw new ArgumentNullException("node");

			if(node.NodeType == PluginTreeNodeType.Builtin)
				this.Insert(node, ((Builtin)node.Value).Position);
			else
				this.Insert(node, -1);
		}

		internal void Clear()
		{
			this.BaseClear();
		}

		internal void Remove(PluginTreeNode node)
		{
			this.BaseRemove(node);
		}

		internal void Remove(string name)
		{
			this.BaseRemoveKey(name);
		}

		#endregion
	}
}