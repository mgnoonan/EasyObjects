//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;

namespace NCI.EasyObjects.Configuration.Design
{
    /// <summary>
    /// <para>
    /// Represents a container for the <see cref="InstanceNode"/>s in the data settings for an application.
    /// </para>
    /// </summary>    
    [Image(typeof(InstanceCollectionNode))]
    public class InstanceCollectionNode : ConfigurationNode
    {
        private InstanceDataCollection instanceDataCollection;
        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="ConnectionStringCollectionNode"/> class with a <see cref="InstanceDataCollection"/> object.
        /// </para>
        /// </summary>   
        /// <param name="instanceDataCollection">
        /// <para>A reference to the <see cref="InstanceDataCollection"/> runtime configuration.</para>
        /// </param>        
        public InstanceCollectionNode(InstanceDataCollection instanceDataCollection) : base()
        {
            if (instanceDataCollection == null)
            {
                throw new ArgumentNullException("instanceDataCollection");
            }
            this.instanceDataCollection = instanceDataCollection;
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
        /// <para>Get a reference to the runtime configuration <see cref="InstanceDataCollection"/>.</para>
        /// </summary>
        /// <value>
        /// <para>A reference to the runtime configuration <see cref="InstanceDataCollection"/>.</para>
        /// </value>
        [Browsable(false)]
        public virtual InstanceDataCollection InstanceDataCollection
        {
            get
            {
                instanceDataCollection.Clear();
                foreach (InstanceNode instanceNode in Nodes)
                {
                    instanceDataCollection[instanceNode.InstanceData.Name] = instanceNode.InstanceData;
                }
                return instanceDataCollection;
            }
        }

        /// <summary>
        /// <para>
        /// Add the default child nodes for the current node.
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>
        /// This will add the default instance nodes for an application.
        /// </para>
        /// </remarks>
        protected override void AddDefaultChildNodes()
        {
            base.AddDefaultChildNodes();
            InstanceNode sqlServer = new InstanceNode();
            Nodes.Add(sqlServer);
//            ConnectionStringCollectionNode connectionStringCollectionNode = Hierarchy.FindNodeByType(Parent, typeof(ConnectionStringCollectionNode)) as ConnectionStringCollectionNode;
//            sqlServer.ConnectionStringNode = Hierarchy.FindNodeByName(connectionStringCollectionNode, SR.DefaultNewConnectionStringName) as ConnectionStringNode;
            DatabaseTypeCollectionNode databaseTypeCollectionNode = Hierarchy.FindNodeByType(Parent, typeof(DatabaseTypeCollectionNode)) as DatabaseTypeCollectionNode;
            sqlServer.DatabaseTypeNode = Hierarchy.FindNodeByName(databaseTypeCollectionNode, SR.DefaultDatabaseTypeName) as DatabaseTypeNode;
        }

        /// <summary>
        /// <para>Sets the name of node when sited to match the underlying storage name and adds each <see cref="InstanceNode"/> based on any active configuration data.</para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited();
            Site.Name = SR.DefaultInstanceCollectionNodeName;
            Debug.Assert(instanceDataCollection != null, "There is no way to construct this without the data.. how did this happen");
            foreach (InstanceData instance in instanceDataCollection)
            {
                Nodes.Add(new InstanceNode(instance));
            }
        }

        /// <summary>
        /// <para>Adds the default nodes for the connection string.</para>
        /// </summary>
        protected override void OnAddMenuItems()
        {
            AddMenuItem(ConfigurationMenuItem.CreateValidateNodeCommand(Site, this));
            ConfigurationMenuItem item = new ConfigurationMenuItem(SR.InstanceNodeMenuText, new AddChildNodeCommand(Site, typeof(InstanceNode)), this, Shortcut.None, SR.InstanceNodeStatusText, InsertionPoint.New);
            AddMenuItem(item);
        }
    }
}