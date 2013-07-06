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
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NCI.EasyObjects.DynamicQueryProvider;

namespace NCI.EasyObjects.Configuration.Design
{
    /// <summary>
    /// <para>
    /// Represents a container for the <see cref="DatabaseTypeNode"/>s in the data settings for an application.
    /// </para>
    /// </summary>
    [Image(typeof(DatabaseTypeCollectionNode))]
    public class DatabaseTypeCollectionNode : ConfigurationNode
    {
        private DatabaseTypeDataCollection databaseTypeDataCollection;

        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="DatabaseTypeCollectionNode"/> class with a <see cref="DatabaseTypeDataCollection"/> object.
        /// </para>
        /// </summary>
        /// <param name="databaseTypeDataCollection">
        /// <para>The <see cref="DatabaseTypeDataCollection"/> configuration.</para>
        /// </param>
        public DatabaseTypeCollectionNode(DatabaseTypeDataCollection databaseTypeDataCollection) : base()
        {
            if (databaseTypeDataCollection == null)
            {
                throw new ArgumentNullException("databaseTypes");
            }
            this.databaseTypeDataCollection = databaseTypeDataCollection;
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
        /// <para>Gets the <see cref="DatabaseTypeDataCollection"/> configuration.</para>
        /// </summary>
        /// <values>
        /// <para>A <see cref="DatabaseTypeDataCollection"/></para>
        /// </values>
        [Browsable(false)]
        public virtual DatabaseTypeDataCollection DatabaseTypeDataCollection
        {
            get
            {
                databaseTypeDataCollection.Clear();
                foreach (DatabaseTypeNode databaseTypeNode in Nodes)
                {
                    databaseTypeDataCollection[databaseTypeNode.DatabaseTypeData.Name] = databaseTypeNode.DatabaseTypeData;
                }
                return databaseTypeDataCollection;
            }
        }

        /// <summary>
        /// <para>Sets the name of node when sited to match the underlying storage name.</para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited();
            Site.Name = SR.DefaultDatabaseTypeCollectionNodeName;
            INodeCreationService service = GetService(typeof(INodeCreationService)) as INodeCreationService;
            Debug.Assert(service != null, "Could not get the INodeCreationService.");
            foreach (DatabaseTypeData databaseTypeData in databaseTypeDataCollection)
            {
                Nodes.Add(new DatabaseTypeNode(databaseTypeData));
            }
        }

        /// <summary>
        /// <para>Adds the default nodes for the connection string.</para>
        /// </summary>
        protected override void OnAddMenuItems()
        {
            AddMenuItem(ConfigurationMenuItem.CreateValidateNodeCommand(Site, this));
            ConfigurationMenuItem item = new ConfigurationMenuItem(SR.DatabaseTypeNodeMenuText, new AddChildNodeCommand(Site, typeof(DatabaseTypeNode)), this, Shortcut.None, SR.DatabaseTypeNodeStatusText, InsertionPoint.New);
            AddMenuItem(item);
        }

        /// <summary>
        /// <para>
        /// Add the default child nodes for the current node.
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>
        /// This will add the default database type nodes.
        /// </para>
        /// </remarks>
        protected override void AddDefaultChildNodes()
        {
            base.AddDefaultChildNodes();
            DatabaseTypeNode node = new DatabaseTypeNode(new DatabaseTypeData(SR.DefaultDatabaseTypeName, typeof(SqlServerDynamicQuery).AssemblyQualifiedName));
            Nodes.AddWithDefaultChildren(node);
        }
    }
}