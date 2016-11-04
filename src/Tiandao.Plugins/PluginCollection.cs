using System;
using System.Collections.Generic;

namespace Tiandao.Plugins
{
    public class PluginCollection : Collections.NamedCollectionBase<Plugin>
	{
		#region 私有字段

		private Plugin _owner;

		#endregion

		#region 公共属性

		public Plugin Owner
		{
			get
			{
				return _owner;
			}
		}

		#endregion

		#region 构造方法

		internal PluginCollection(Plugin owner = null) : base(StringComparer.OrdinalIgnoreCase)
		{
			_owner = owner;
		}

		#endregion
		
		#region 重写方法

		protected override string GetKeyForItem(Plugin item)
		{
			return item.Name;
		}

		#endregion

		#region 内部方法

		/// <summary>
		/// 将指定的插件对象加入当前的集合中。
		/// </summary>
		/// <param name="item">带加入的插件对象。</param>
		/// <param name="thorwExceptionOnDuplicationName">指示当前的插件名如果在集合中已经存在是否抛出异常。</param>
		/// <returns>添加成功则返回真(True)，否则返回假(False)。</returns>
		/// <exception cref="System.ArgumentNullException">当<paramref name="item"/>参数为空(null)。</exception>
		/// <exception cref="System.InvalidOperationException">当<paramref name="item"/>参数的<see cref="Tiandao.Plugins.Plugin.Parent"/>父插件属性不为空，并且与当前集合的所有者不是同一个引用对象。</exception>
		/// <exception cref="Tiandao.Plugins.PluginException">当<paramref name="thorwExceptionOnDuplicationName" />参数为真(True)，并且待加入的插件名与当前集合中插件发生重名。</exception>
		internal bool Add(Plugin item, bool thorwExceptionOnDuplicationName)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			if(item.Parent != null && (!object.ReferenceEquals(item.Parent, _owner)))
				throw new InvalidOperationException();

			if(this.Contains(item.Name))
			{
				if(thorwExceptionOnDuplicationName)
					throw new PluginException(string.Format("The name is '{0}' of plugin was exists. it's path is: '{1}'", item.Name, item.FilePath));
				else
					return false;
			}

			base.Add(item);

			//返回添加成功
			return true;
		}

		#endregion
	}
}