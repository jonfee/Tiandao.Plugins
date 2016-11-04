using System;
using System.ComponentModel;

namespace Tiandao.Plugins
{
#if !CORE_CLR
	[Serializable]
	public abstract class PluginElement : MarshalByRefObject, INotifyPropertyChanged
#else
	public abstract class PluginElement : INotifyPropertyChanged
#endif
	{
		#region 事件定义

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region 私有字段

		private string _name;
		private Plugin _plugin;

		#endregion

		#region 公共属性

		public string Name
		{
			get
			{
				return _name;
			}
			private set
			{
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException();

				if(Tiandao.Common.StringExtension.ContainsCharacters(value, @"\/.,:;'""`@%^&*?!()[]{}|"))
					throw new ArgumentException(string.Format("The '{0}' name of plugin-element contains invalid characters in this argument.", value));

				if(string.Equals(_name, value.Trim(), StringComparison.OrdinalIgnoreCase))
					return;

				_name = value.Trim();

				//激发“PropertyChanged”事件
				this.OnPropertyChanged("Name");
			}
		}

		public Plugin Plugin
		{
			get
			{
				return _plugin;
			}
			protected set
			{
				if(object.ReferenceEquals(_plugin, value))
					return;

				_plugin = value;

				//激发“PropertyChanged”事件
				this.OnPropertyChanged("Plugin");
			}
		}

		#endregion

		#region 构造方法

		protected PluginElement(string name) : this(name, null)
		{
		}

		protected PluginElement(string name, Plugin plugin)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			this.Name = name;
			_plugin = plugin;
		}

		internal PluginElement(string name, bool ignoreNameValidation)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			if(ignoreNameValidation)
				_name = name;
			else
				this.Name = name;
		}

		#endregion

		#region 保护方法

		protected void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if(this.PropertyChanged != null)
				this.PropertyChanged(this, e);
		}

		#endregion

		#region 重写方法

		public override string ToString()
		{
			var plugin = _plugin;

			if(plugin == null)
				return string.Format("{0}[{1}]", _name, this.GetType().Name);
			else
				return string.Format("{0}[{1}]@{2}", _name, this.GetType().Name, _plugin.Name);
		}

		#endregion
	}
}
