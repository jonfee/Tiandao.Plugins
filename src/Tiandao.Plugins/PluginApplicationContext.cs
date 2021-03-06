﻿using System;
using System.ComponentModel;

namespace Tiandao.Plugins
{
	public class PluginApplicationContext : ComponentModel.ApplicationContextBase
	{
		#region 事件声明

		public event EventHandler WorkbenchCreated;

		#endregion

		#region 私有字段

		private IWorkbenchBase _workbench;
		private PluginContext _pluginContext;

		#endregion

		#region 公共属性

		/// <summary>
		/// 获取当前应用程序的工作台(主界面)。
		/// </summary>
		/// <remarks>
		///		<para>必须使用<seealso cref="Tiandao.Plugins.Application"/>类的Start方法，启动应用程序后才能使用该属性获取到创建成功的工作台对象。</para>
		/// </remarks>
		public IWorkbenchBase Workbench
		{
			get
			{
				return _workbench;
			}
		}

		/// <summary>
		/// 获取当前应用程序的插件上下文对象。
		/// </summary>
		/// <remarks>
		/// 本属性在首次创建<seealso cref="Tiandao.Plugins.PluginContext"/>时，会调用<see cref="Tiandao.Plugins.PluginApplicationContext.CreatePluginSetup"/>方法以获得插件启动配置参数，如果要提供不同的启动信息，必须重写该虚拟方法。
		/// </remarks>
		public PluginContext PluginContext
		{
			get
			{
				if(_pluginContext == null)
				{
					lock (SyncRoot)
					{
						if(_pluginContext == null)
							_pluginContext = new PluginContext(this.CreatePluginSetup(), this);
					}
				}

				return _pluginContext;
			}
		}

		#endregion

		#region 构造方法

		protected PluginApplicationContext(string applicationId) : base(applicationId)
		{
			this.Modules.Add(new Tiandao.Options.Plugins.OptionModule());
		}

		#endregion

		#region 虚拟方法

		/// <summary>
		/// 创建一个主窗体对象。
		/// </summary>
		/// <returns>返回的主窗体对象。</returns>
		/// <remarks>
		/// 通常子类中实现的该方法只是创建空的工作台对象，并没有构建出该工作台下面的子构件。
		/// 具体构建工作台子构件的最佳时机通常在 Workbench 类的 Open 方法内进行。
		/// </remarks>
		protected virtual IWorkbenchBase CreateWorkbench(string[] args)
		{
			return this.PluginContext.Workbench;
		}

		/// <summary>
		/// 创建插件启动配置对象。
		/// </summary>
		/// <returns>返回创建成功的插件启动配置对象。</returns>
		/// <remarks></remarks>
		protected virtual PluginSetup CreatePluginSetup()
		{
			return new PluginSetup(this.ApplicationDirectory);
		}

		#endregion

		#region 内部方法

		/// <summary>
		/// 获取当前应用程序的工作台(主界面)。
		/// </summary>
		/// <param name="args">初始化的参数。</param>
		/// <returns>返回新建或者已创建的工作台对象。</returns>
		/// <remarks>
		/// <para>如果当前工作台为空(null)则调用 <seealso cref="CreateWorkbench"/> 虚拟方法，以创建工作台对象，并将创建后的对象挂入到由 <see cref="PluginSetup.WorkbenchPath"/> 指定的插件树节点中。</para>
		/// <para>如果当前插件树还没加载，则将在插件树加载完成事件中将该工作台对象再挂入到由 <see cref="PluginSetup.WorkbenchPath"/> 指定的插件树节点中。</para>
		/// <para>注意：该属性是线程安全的，在多线程中对该属性的多次调用不会导致重复生成工作台对象。</para>
		/// <para>有关子类实现 <seealso cref="CreateWorkbench"/> 虚拟方法的一般性机制请参考该方法的帮助。</para>
		/// </remarks>
		internal IWorkbenchBase GetWorkbench(string[] args)
		{
			if(_workbench == null)
			{
				lock (SyncRoot)
				{
					if(_workbench == null)
					{
						//创建工作台对象
						_workbench = this.CreateWorkbench(args);

						//将当前工作台对象挂载到插件结构中
						if(_workbench != null)
							this.PluginContext.PluginTree.Mount(this.PluginContext.Settings.WorkbenchPath, _workbench);

						//确认工作台路径及其下属所有节点均已构建完成
						this.EnsureNodes(this.PluginContext.PluginTree.Find(this.PluginContext.Settings.WorkbenchPath));

						//激发“WorkbenchCreated”事件
						if(_workbench != null)
							this.OnWorkbenchCreated(EventArgs.Empty);

						return _workbench;
					}
				}
			}

			return _workbench;
		}

		private void EnsureNodes(PluginTreeNode node)
		{
			if(node == null)
				return;

			if(node.NodeType == PluginTreeNodeType.Builtin)
			{
				if(!((Builtin)node.Value).IsBuilded)
					node.Build();

				return;
			}

			foreach(var child in node.Children)
				this.EnsureNodes(child);
		}

		#endregion

		#region 激发事件

		internal void RaiseStarting(string[] args)
		{
			this.OnStarting(new ComponentModel.ApplicationEventArgs(this, args));
		}

		internal void RaiseStarted(string[] args)
		{
			this.OnStarted(new ComponentModel.ApplicationEventArgs(this, args));
		}

		internal void RaiseInitializing(string[] args)
		{
			this.OnInitializing(new ComponentModel.ApplicationEventArgs(this, args));
		}

		internal void RaiseInitialized(string[] args)
		{
			this.OnInitialized(new ComponentModel.ApplicationEventArgs(this, args));
		}

		internal void RaiseExiting(CancelEventArgs args)
		{
			this.OnExiting(args);
		}

		#endregion

		#region 保护方法

		protected virtual void OnWorkbenchCreated(EventArgs e)
		{
			if(this.WorkbenchCreated != null)
				this.WorkbenchCreated(this, e);
		}

		#endregion
	}
}
