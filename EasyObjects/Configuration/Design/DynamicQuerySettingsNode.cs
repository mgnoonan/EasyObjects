using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;
using NCI.EasyObjects.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;

namespace NCI.EasyObjects.Configuration.Design
{
	/// <summary>
	/// <para>
	/// Represents the dynamic query settings for an application.
	/// </para>
	/// </summary>
	[Image(typeof(DynamicQuerySettingsNode))]
	[ServiceDependency(typeof(ILinkNodeService))]
	public class DynamicQuerySettingsNode : ConfigurationNode
	{
		private DynamicQuerySettings dynamicQuerySettings;
		private InstanceNode instanceNode;
		private ConfigurationNodeChangedEventHandler instanceRemovedHandler;
		private ConfigurationNodeChangedEventHandler instanceRenameHandler;

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="DynamicQuerySettingsNode"/> class.</para>
		/// </summary>
		public DynamicQuerySettingsNode() : this(new DynamicQuerySettings())
		{
		}

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="DynamicQuerySettingsNode"/> class with a <see cref="DynamicQuerySettings"/> object.</para>
		/// </summary>
		/// <param name="databaseSettings">
		/// <para>The <see cref="DatabaseSettings"/> runtime configuration.</para>
		/// </param>
		public DynamicQuerySettingsNode(DynamicQuerySettings dynamicQuerySettings) : base()
		{
			if (dynamicQuerySettings == null)
			{
				throw new ArgumentNullException("DynamicQuerySettings");
			}
			this.dynamicQuerySettings = dynamicQuerySettings;
			this.instanceRemovedHandler = new ConfigurationNodeChangedEventHandler(OnInstanceNodeRemoved);
			this.instanceRenameHandler = new ConfigurationNodeChangedEventHandler(OnInstanceNodeRenamed);
		}

		/// <summary>
		/// <para>
		/// Gets or sets the <see cref="DynamicQueryTypeNode"/> for the instance.
		/// </para>
		/// </summary>
		/// <value>
		/// <para>
		/// The <see cref="DynamicQueryTypeNode"/> for the instance.
		/// </para>
		/// </value>
		[Editor(typeof(ReferenceEditor), typeof(UITypeEditor))]
		[ReferenceType(typeof(InstanceNode))]
		[Required]
		[SRCategory(SR.Keys.CategoryGeneral)]
		[SRDescription(SR.Keys.DefaultInstanceTypeDescription)]
		public InstanceNode DefaultInstanceNode
		{
			get { return this.instanceNode; }
			set
			{
				ILinkNodeService service = GetService(typeof(ILinkNodeService)) as ILinkNodeService;
				Debug.Assert(service != null, "Could not get the ILinkNodeService");
				this.instanceNode = (InstanceNode)service.CreateReference(instanceNode, value, instanceRemovedHandler, instanceRenameHandler);
				dynamicQuerySettings.DefaultInstance = string.Empty;
				if (this.instanceNode != null)
				{
					dynamicQuerySettings.DefaultInstance = this.instanceNode.Name;
				}
			}
		}

		/// <summary>
		/// <para>Gets the name for the node.</para>
		/// </summary>
		/// <value>
		/// <para>The display name for the node.</para>
		/// </value>
		[ReadOnly(true)]
		public override string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		/// <summary>
		/// <para>Gets the runtime configuration data for the database configuration.</para>
		/// </summary>
		/// <value>
		/// <para>A <see cref="DynamicQuerySettings"/> reference.</para>
		/// </value>
		[Browsable(false)]
		public virtual DynamicQuerySettings DynamicQuerySettings
		{
			get
			{
				GetInstanceCollectionData();
				GetDatabaseTypeDataCollection();
				return this.dynamicQuerySettings;
			}
		}

		private void GetDatabaseTypeDataCollection()
		{
			DatabaseTypeCollectionNode node = Hierarchy.FindNodeByType(typeof(DatabaseTypeCollectionNode)) as DatabaseTypeCollectionNode;
			if (node == null) return;

			DatabaseTypeDataCollection data = node.DatabaseTypeDataCollection;
			if (Object.ReferenceEquals(dynamicQuerySettings.DatabaseTypes, data)) return;

			dynamicQuerySettings.DatabaseTypes.Clear();
			foreach (DatabaseTypeData databseTypeData in data)
			{
				dynamicQuerySettings.DatabaseTypes[databseTypeData.Name] = databseTypeData;
			}
		}

		private void GetDynamicQueryTypeDataCollection()
		{
			DatabaseTypeCollectionNode node = Hierarchy.FindNodeByType(typeof(DatabaseTypeCollectionNode)) as DatabaseTypeCollectionNode;
			if (node == null) return;

			DatabaseTypeDataCollection data = node.DatabaseTypeDataCollection;
			if (Object.ReferenceEquals(dynamicQuerySettings.DatabaseTypes, data)) return;

			dynamicQuerySettings.DatabaseTypes.Clear();
			foreach (DatabaseTypeData databaseTypeData in data)
			{
				dynamicQuerySettings.DatabaseTypes[databaseTypeData.Name] = databaseTypeData;
			}
		}

		private void GetInstanceCollectionData()
		{
			InstanceCollectionNode node = Hierarchy.FindNodeByType(typeof(InstanceCollectionNode)) as InstanceCollectionNode;
			if (node == null) return;

			InstanceDataCollection data = node.InstanceDataCollection;
			if (Object.ReferenceEquals(dynamicQuerySettings.Instances, data)) return;

			dynamicQuerySettings.Instances.Clear();
			foreach (InstanceData instanceData in data)
			{
				dynamicQuerySettings.Instances[instanceData.Name] = instanceData;
			}
		}

		/// <summary>
		/// <para>
		/// Add the default child nodes for the current node.
		/// </para>
		/// </summary>
		/// <remarks>
		/// <para>
		/// This will add the default <see cref="DatabaseTypeCollectionNode"/>, <see cref="ConnectionStringCollectionNode"/>, and <see cref="InstanceCollectionNode"/>.
		/// </para>
		/// </remarks>
		protected override void AddDefaultChildNodes()
		{
			base.AddDefaultChildNodes();
			AddDefaultDynamicQueryTypeCollectionNode();
			AddDefaultInstanceNode();
			InstanceCollectionNode nodes = (InstanceCollectionNode)Hierarchy.FindNodeByType(this, typeof(InstanceCollectionNode));
			if (nodes.Nodes.Count > 0)
			{
				this.DefaultInstanceNode = nodes.Nodes[0] as InstanceNode;
			}
		}

		/// <summary>
		/// <para>After the node is loaded, allows child nodes to resolve references to sibling nodes in configuration.</para>
		/// </summary>
		public override void ResolveNodeReferences()
		{
			base.ResolveNodeReferences();
			InstanceCollectionNode node = Hierarchy.FindNodeByType(this, typeof(InstanceCollectionNode)) as InstanceCollectionNode;
			if (node == null) return;
			DefaultInstanceNode = Hierarchy.FindNodeByName(node, dynamicQuerySettings.DefaultInstance) as InstanceNode;
			if (instanceNode == null)
			{
				throw new InvalidOperationException(SR.ExceptionInstanceNodeNotFound(dynamicQuerySettings.DefaultInstance));
			}
		}


		/// <summary>
		/// <para>Sets the default name of the node.</para>
		/// </summary>
		protected override void OnSited()
		{
			base.OnSited();
			Site.Name = SR.DefaultDynamicQuerySettingsName;
			if (dynamicQuerySettings.DatabaseTypes.Count > 0)
			{
				Nodes.Add(new DatabaseTypeCollectionNode(this.dynamicQuerySettings.DatabaseTypes));
			}
			if (dynamicQuerySettings.Instances.Count > 0)
			{
				Nodes.Add(new InstanceCollectionNode(this.dynamicQuerySettings.Instances));
			}
		}

		/// <summary>
		/// <para>Adds the base menu items and menu items for creating <see cref="ConnectionStringCollectionNode"/>, <see cref="DatabaseTypeCollectionNode"/>, <see cref="InstanceCollectionNode"/> objects.</para>
		/// </summary>
		protected override void OnAddMenuItems()
		{
			base.OnAddMenuItems();            
			AddNodeMenu(typeof(DatabaseTypeCollectionNode), SR.DatabaseTypeNodeMenuText, SR.DatabaseTypeNodeStatusText);
			AddNodeMenu(typeof(InstanceCollectionNode), SR.InstanceCollectionNodeMenuText, SR.InstanceCollectionNodeStatusText);
		}

		private void AddNodeMenu(Type type, string menuText, string statusText)
		{
			ConfigurationMenuItem item = new ConfigurationMenuItem(menuText, 
				new AddChildNodeCommand(Site, type), 
				this, 
				Shortcut.None, 
				statusText, InsertionPoint.New);
			//item.Enabled = !DoesChildNodeExist(typeof(ConnectionStringCollectionNode));
			AddMenuItem(item);
		}

		private bool DoesChildNodeExist(Type type)
		{
			if (Hierarchy.FindNodeByType(this, type) != null)
			{
				return true;
			}
			return false;
		}


		private void AddDefaultInstanceNode()
		{
			InstanceCollectionNode node = new InstanceCollectionNode(dynamicQuerySettings.Instances);
			Nodes.AddWithDefaultChildren(node);            
		}

//		private void AddDefaultConnectionStringCollectionNode()
//		{
//			ConnectionStringCollectionNode node = new ConnectionStringCollectionNode(databaseSettings.ConnectionStrings);
//			Nodes.AddWithDefaultChildren(node);            
//		}

		private void AddDefaultDynamicQueryTypeCollectionNode()
		{
			DatabaseTypeCollectionNode node = new DatabaseTypeCollectionNode(dynamicQuerySettings.DatabaseTypes);
			Nodes.AddWithDefaultChildren(node);            
		}

		/// <devdoc>
		/// Handles the remove of a instnce node.
		/// </devdoc>                
		private void OnInstanceNodeRemoved(object sender, ConfigurationNodeChangedEventArgs e)
		{
			this.instanceNode = null;
			dynamicQuerySettings.DefaultInstance = string.Empty;
		}

		/// <devdoc>
		/// Handles the rename of a database node.
		/// </devdoc>                
		private void OnInstanceNodeRenamed(object sender, ConfigurationNodeChangedEventArgs e)
		{
			dynamicQuerySettings.DefaultInstance = e.Node.Name;
		}
	}
}